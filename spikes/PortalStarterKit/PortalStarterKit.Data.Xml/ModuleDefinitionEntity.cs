using System.Xml.Serialization;

namespace PortalStarterKit.Data.Xml
{
    public class ModuleDefinitionEntity
    {
        [XmlAttribute]
        public int ModuleDefId { get; set; }

        [XmlAttribute]
        public string FriendlyName { get; set; }

        [XmlAttribute("DesktopSourceFile")]
        public string SourceFile { get; set; }
    }
}