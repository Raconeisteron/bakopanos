using Microsoft.Build.Utilities;

namespace ArchiCop.Rules
{   
    public interface IArchiCopRule
    {
        TaskLoggingHelper Log { get; set; }        
        void Init(string table,string providerName, string connectionString);
        void CheckProject(VisualStudioProject project);
    }
}
