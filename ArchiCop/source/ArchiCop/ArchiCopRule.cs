using Microsoft.Build.Utilities;

namespace ArchiCop
{
    public class ArchiCopRuleParams
    {
        public string RuleCategory { get; set; }
        public string RuleType { get; set; }
        public string Table { get; set; }
        public string ProviderName { get; set; }
        public string ConnectionString { get; set; }
    }

    public abstract class ArchiCopRule : IArchiCopRule
    {
        public TaskLoggingHelper Log { get; set; }        

        public abstract void Init(ArchiCopRuleParams parameters);

        public abstract void CheckProject(VsProject project);
    }
}