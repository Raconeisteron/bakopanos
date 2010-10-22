using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using PortalStarterKit.Core;

namespace PortalStarterKit.DesktopModules
{
    public class FakeHtmlModuleDb : IHtmlModuleDb
    {
        private ISiteEnvironment _environment;
        public FakeHtmlModuleDb(ISiteEnvironment environment)
        {
            _environment = environment;
        }

        public string GetHtmlText(string portalId, string tabId, string moduleId)
        {
            string path = Path.Combine(_environment.DataPhysicalPath, portalId+@"\"+tabId+@"\"+moduleId);
            path = Path.Combine(path, "test.htm");
            string htmlText;
            // create reader & open file
            using (TextReader tr = new StreamReader(path))
            {
                htmlText = tr.ReadToEnd();
                tr.Close();
            }
            return htmlText;
        }
    }
}