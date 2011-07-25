using System.Collections.Generic;
using System.Xml.Serialization;
using PortalStarterKit.Model;

namespace PortalStarterKit.Data.Xml
{
    public class ModuleEntity
    {
        [XmlAttribute]
        public int ModuleId { get; set; }

        [XmlAttribute]
        public int ModuleDefId { get; set; }

        [XmlAttribute]
        public int ModuleOrder { get; set; }

        [XmlAttribute]
        public string ModuleTitle { get; set; }

        [XmlAttribute]
        public PaneType PaneName { get; set; }

        [XmlElement("Setting")]
        public List<SettingEntity> Settings { get; set; }
    }
}