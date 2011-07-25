using System.Collections.Generic;
using System.Xml.Serialization;

namespace PortalStarterKit.Data.Xml
{
    public class PortalEntity
    {
        [XmlAttribute]
        public int PortalId { get; set; }

        [XmlAttribute]
        public string PortalName { get; set; }

        [XmlAttribute]
        public bool AlwaysShowEditButton { get; set; }

        [XmlElement("Tab")]
        public List<TabEntity> Tabs { get; set; }
    }
}