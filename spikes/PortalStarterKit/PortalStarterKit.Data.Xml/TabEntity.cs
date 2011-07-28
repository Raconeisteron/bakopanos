using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PortalStarterKit.Data.Xml
{
    public class TabEntity
    {
        [XmlAttribute]
        public int TabId { get; set; }

        [XmlAttribute]
        public Guid TabDefId { get; set; }

        [XmlAttribute]
        public string TabName { get; set; }

        [XmlAttribute]
        public int TabOrder { get; set; }

        [XmlElement("Tab")]
        public List<TabEntity> Tabs { get; set; }

        [XmlElement("Module")]
        public List<ModuleEntity> Modules { get; set; }

        [XmlElement("Setting")]
        public List<SettingEntity> Settings { get; set; }
    }
}