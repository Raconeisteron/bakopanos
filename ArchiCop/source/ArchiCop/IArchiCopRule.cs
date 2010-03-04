using Microsoft.Build.Utilities;

namespace ArchiCop
{
    public enum ArchiCopRuleType { Invalid, Valid, Warning, Exclude }

    public interface IArchiCopRule
    {
        TaskLoggingHelper Log { get; set; }        
        void Init(string table,string providerName, string connectionString);
        void CheckProject(VsProject project);
    }
}