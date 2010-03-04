using Microsoft.Build.Utilities;

namespace ArchiCop
{
    public enum ArchiCopRuleType { Invalid, Valid, Warning, Exclude }

    public interface IArchiCopRule
    {
        TaskLoggingHelper Log { get; set; }        
        void Init(ArchiCopRuleParams parameters);
        void CheckProject(VsProject project);
    }
}