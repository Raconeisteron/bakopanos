using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ArchiCop
{
    public class MsBuildTargetList : List<MsBuildTarget>
    {
        private static readonly Func<XDocument, string, IEnumerable<XElement>> _getElements =
            (xDocument, elementName) => (from e in xDocument.Descendants(XNameSpace + elementName)
                                         select e);

        private static readonly XNamespace XNameSpace = "http://schemas.microsoft.com/developer/msbuild/2003";

        public MsBuildTargetList(string location)
        {
            string[] projectFiles = Directory.GetFiles(location, "*.proj");

            foreach (string projectFile in projectFiles)
            {
                XDocument xdoc = XDocument.Load(projectFile);

                IEnumerable<XElement> targets = _getElements(xdoc, "Target");

                foreach (XElement target in targets)
                {
                    var item =
                        new MsBuildTarget
                            {
                                ProjectName = projectFile,
                                TargetName = target.Attribute("Name").Value
                            };
                    Add(item);
                }
            }
        }

    }
}
