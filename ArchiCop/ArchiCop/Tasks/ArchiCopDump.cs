using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Utilities;
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
