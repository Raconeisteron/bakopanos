using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public abstract void CheckProject(VisualStudioProject project);

    }
}
