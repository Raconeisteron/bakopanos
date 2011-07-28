using System.Xml.Serialization;

namespace PortalStarterKit.Data.Xml
{
    public class TabDefinitionEntity
    {
        [XmlAttribute]
        public int TabDefId { get; set; }

        [XmlAttribute]
        public string FriendlyName { get; set; }

        [XmlAttribute("DesktopSourceFile")]
        public string SourceFile { get; set; }
    }
}