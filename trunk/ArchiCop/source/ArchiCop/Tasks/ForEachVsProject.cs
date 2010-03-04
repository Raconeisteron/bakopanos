using System;
using System.Diagnostics;
using ArchiCop.Library;
using Microsoft.Build.Framework;

namespace ArchiCop.Tasks
{
    public class ForEachVsProject : ArchiCopTask
    {
        [Required]
        public string DumpFile { get; set; }

        [Required]
        public string ConnectionString { get; set; }

        [Required]
        public string ProviderName { get; set; }

        [Required]
        public string Table { get; set; }

        [Required]
        public string RuleCategory { get; set; }

        public override bool Execute()
        {
#if DEBUG
            //do break
            Debugger.Launch();
#endif
            string cmdText = "Select RuleCategory, RuleType, ConnectionString, ProviderName, Table From " + Table + " Where RuleCategory='" + RuleCategory+"'";
            var parameters = DataHelper.GetData<ArchiCopRuleParams>(ProviderName, ConnectionString, cmdText);

            foreach (ArchiCopRuleParams parameter in parameters)
            {
                Type type = Type.GetType(parameter.RuleType);
                var instance = (IArchiCopRule)Activator.CreateInstance(type, new object[] { });
                instance.Log = Log;
                instance.Init(new ArchiCopRuleParams
                                  {
                                      Table = parameter.Table,
                                      ProviderName = parameter.ProviderName,
                                      ConnectionString = parameter.ConnectionString
                                  });
                Log.LogMessage(ToString());

                try
                {
                    VsProjectList projects = VsProjectHandler.LoadVisualStudioProjectList(DumpFile);
                    projects.ForEach(instance.CheckProject);
                }
                catch (Exception error)
                {
                    Log.LogErrorFromException(error, true);
                }
            }

            return true;
        }
    }
}
