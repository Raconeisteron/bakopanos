using System.Collections.Generic;
using System.Xml.Serialization;

namespace PortalStarterKit.Data.Xml
{
    [XmlRoot("SiteConfiguration")]
    public class SiteConfigurationEntity
    {
        [XmlElement("Portal")]
        public List<PortalEntity> Portals { get; set; }

        /*[XmlElement("TabDefinition")]
        public List<TabDefinitionEntity> TabDefinitions { get; set; }*/

        [XmlElement("ModuleDefinition")]
        public List<ModuleDefinitionEntity> ModuleDefinitions { get; set; }
    }
}