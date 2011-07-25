using System.Xml.Serialization;

namespace PortalStarterKit.Data.Xml
{
    public class SettingEntity
    {
        [XmlAttribute]
        public string Key { get; set; }

        [XmlAttribute]
        public string Value { get; set; }
    }
}