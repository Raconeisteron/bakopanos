using System;
using System.Diagnostics;
using ArchiCop.Library;
using Microsoft.Build.Framework;

namespace ArchiCop.Tasks
{
    public class ArchiCopDump : ArchiCopTask
    {
        [Required]
        public string RootPath { get; set; }

        [Required]
        public string DumpFile { get; set; }

        public override bool Execute()
        {

#if DEBUG            
            //do break
            Debugger.Launch();
#endif        
            
            Log.LogMessage(ToString());
            try
            {
                ProjectHandler.DumpVisualStudioProjectList(RootPath, DumpFile);
                return true;
            }
            catch (Exception error)
            {
                Log.LogErrorFromException(error, true);
                return false;
            }
        }
    }
}
