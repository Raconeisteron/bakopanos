using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;

namespace ArchiCop
{   
    public class VisualStudioProject 
    {
        private static readonly Func<XDocument, string, string> _getElementOrEmpty =
            delegate(XDocument xDocument, string elementName)
            {
                XElement element = _getElements(xDocument, elementName).FirstOrDefault();
                return element == null ? string.Empty : element.Value;
            };

      
        private static readonly Func<XDocument, string, IEnumerable<XElement>> _getElements =
            (xDocument, elementName) => (from e in xDocument.Descendants(XNameSpace + elementName)
                                         select e);

        private static readonly XNamespace XNameSpace = "http://schemas.microsoft.com/developer/msbuild/2003";

        public VisualStudioProject() { }
        public VisualStudioProject(string projectFile) 
        {
            if (!File.Exists(projectFile))
            {
                throw new FileNotFoundException("File doesn't exist", projectFile);
            }            
            XDocument xdoc = XDocument.Load(projectFile);

            FileInfo file = new FileInfo(projectFile);
            ProjectFileName = file.Name;
            ProjectType = (VisualStudioProjectType)Enum.Parse(typeof(VisualStudioProjectType), 
                file.Extension.Substring(1));
            ProjectFileFullName = file.FullName;
            ProjectDirectory = file.Directory.Name;

            ToolsVersion = xdoc.Root.Attribute("ToolsVersion").Value;
            
            ProjectGuid = new Guid(_getElementOrEmpty(xdoc, "ProjectGuid"));
            ProductVersion = _getElementOrEmpty(xdoc, "ProductVersion");
            SchemaVersion = _getElementOrEmpty(xdoc, "SchemaVersion");
            OutputType = (OutputType)Enum.Parse(typeof(OutputType), _getElementOrEmpty(xdoc, "OutputType"));
            RootNamespace = _getElementOrEmpty(xdoc, "RootNamespace");
            AssemblyName = _getElementOrEmpty(xdoc, "AssemblyName");
            TargetFrameworkVersion = _getElementOrEmpty(xdoc, "TargetFrameworkVersion");
        }

        public VisualStudioProjectType ProjectType { get; set; }
        public string ProjectFileName { get; set; }
        public string ProjectFileFullName { get; set; }
        public string ProjectDirectory { get; set; }
        public string ToolsVersion { get; set; }
        public string ProductVersion { get; set; }
        public string SchemaVersion { get; set; }
        public Guid ProjectGuid { get; set; }
        public OutputType OutputType { get; set; }
        public string RootNamespace { get; set; }
        public string AssemblyName { get; set; }
        public string TargetFrameworkVersion { get; set; }
    }
}
