using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PortalStarterKit.DesktopModules
{
    public class FakeHtmlModuleDb : IHtmlModuleDb
    {
        public string GetHtmlText(string portalId, string tabId, string moduleId)
        {
            string path = Path.Combine(@"C:\google\google.portal\PortalStarterKit", "test.htm");
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