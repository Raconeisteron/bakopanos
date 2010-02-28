using System;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;

namespace ArchiCop.Build
{
    public abstract class CheckVisualStudioProjectList : Task
    {        
        [Required]
        public string DumpFile { get; set; }

        public override bool Execute()
        {
            Log.LogMessage(ToString());
            Init();
            try
            {
                VisualStudioProjects projects = ProjectHandler.LoadVisualStudioProjectList(DumpFile);

                projects.ForEach(CheckProject);

                return true;
            }
            catch (Exception error)
            {
                Log.LogErrorFromException(error, true);
                return false;
            }
        }

        public abstract void Init();
        public abstract void CheckProject(VisualStudioProject project);

    }
}
