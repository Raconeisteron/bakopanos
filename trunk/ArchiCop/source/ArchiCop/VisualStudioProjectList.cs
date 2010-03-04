using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Xml.Serialization;

namespace ArchiCop
{   
    [XmlRoot(ElementName = "VisualStudioProjects")]
    public class VisualStudioProjectList : List<VisualStudioProject>
    {
    }

    public enum VisualStudioProjectType { csproj }

    public enum OutputType { Library, Exe, WinExe }
       
}
