using System;
using System.Diagnostics;
using ArchiCop.Library;
using Microsoft.Build.Framework;

namespace ArchiCop.Tasks
{
    public class ArchiCopCheck : ArchiCopTask
    {        
        [Required]
        public string DumpFile { get; set; }

        [Required]
        public string[] Rules { get; set; }

        [Required]
        public string ConnectionString { get; set; }

        [Required]
        public string ProviderName { get; set; }

        [Required]
        public string Table { get; set; }

        public override bool Execute()
        {
#if DEBUG
            //do break
            Debugger.Launch();
#endif        

            foreach (string rule in Rules)
            {                
                Type type = Type.GetType(rule);
                var instance = (IArchiCopRule)Activator.CreateInstance(type, new object[] {  });
                instance.Log = Log;
                instance.Init(Table, ProviderName, ConnectionString);

                Log.LogMessage(ToString());

                try
                {
                    VisualStudioProjects projects = ProjectHandler.LoadVisualStudioProjectList(DumpFile);
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
