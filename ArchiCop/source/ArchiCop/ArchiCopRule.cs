using Microsoft.Build.Utilities;

namespace ArchiCop
{
    public abstract class ArchiCopRule : IArchiCopRule
    {
        public TaskLoggingHelper Log { get; set; }        

        public abstract void Init(string table, string providerName, string connectionString);

        public abstract void CheckProject(VsProject project);
    }
}