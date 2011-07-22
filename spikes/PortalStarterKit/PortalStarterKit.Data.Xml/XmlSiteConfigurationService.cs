using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Xml.Serialization;
using PortalStarterKit.Model;

namespace PortalStarterKit.Data.Xml
{
    public class XmlSiteConfigurationService : SiteConfigurationService
    {        
        public XmlSiteConfigurationService()
        {
            string xmlFile = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["XmlSiteConfigurationFile"]);

            // Code that runs on application startup            
            var serializer = new XmlSerializer(typeof (SiteConfigurationEntity));
            var fs = new FileStream(xmlFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            
            var siteConfigurationEntity = (SiteConfigurationEntity) serializer.Deserialize(fs);

            SiteConfiguration = new SiteConfiguration();
            
            foreach (TabDefinitionEntity entity in siteConfigurationEntity.TabDefinitions)
            {
                var item = new TabDefinition
                {
                    TabDefId = entity.TabDefId,
                    FriendlyName = entity.FriendlyName,
                    SourceFile = entity.SourceFile
                };
                SiteConfiguration.TabDefinitions.Add(item);
            }
            
            foreach (ModuleDefinitionEntity entity in siteConfigurationEntity.ModuleDefinitions)
            {
                var item = new ModuleDefinition
                {
                    ModuleDefId = entity.ModuleDefId,
                    FriendlyName = entity.FriendlyName,
                    SourceFile = entity.SourceFile
                };
                SiteConfiguration.ModuleDefinitions.Add(item);
            }

            foreach (PortalEntity portalEntity in siteConfigurationEntity.Portals)
            {
                var portal = new Portal
                {
                    PortalId = portalEntity.PortalId,
                    PortalName = portalEntity.PortalName,
                    AlwaysShowEditButton = portalEntity.AlwaysShowEditButton                    
                };

                SiteConfiguration.Portals.Add(portal);
                MakeTabs(portal.Tabs, portalEntity.Tabs);
            }

            //
            InitializeSiteConfiguration(SiteConfiguration.Portals[0].Tabs);
        }

        private static void MakeTabs(List<Tab> tabs, IEnumerable<TabEntity> tabEntities)
        {
            foreach (TabEntity tabEntity in tabEntities)
            {
                var tab = new Tab
                              {
                                  TabId = tabEntity.TabId,
                                  TabName = tabEntity.TabName,                                  
                                  TabDefId = tabEntity.TabDefId,
                                  TabOrder = tabEntity.TabOrder                                  
                              };
                tabs.Add(tab);
                foreach (ModuleEntity moduleEntity in tabEntity.Modules)
                {
                    var module = new Module
                                     {
                                         ModuleId = moduleEntity.ModuleId,
                                         ModuleDefId = moduleEntity.ModuleDefId,
                                         ModuleTitle = moduleEntity.ModuleTitle,
                                         ModuleOrder = moduleEntity.ModuleOrder,
                                         PaneName = moduleEntity.PaneName
                                     };
                    tab.Modules.Add(module);
                }
                MakeTabs(tab.Tabs, tabEntity.Tabs);
            }
        }

        [XmlRoot("SiteConfiguration")]
        public class SiteConfigurationEntity
        {
            [XmlElement("Portal")]
            public List<PortalEntity> Portals { get; set; }

            [XmlElement("TabDefinition")]
            public List<TabDefinitionEntity> TabDefinitions { get; set; }

            [XmlElement("ModuleDefinition")]
            public List<ModuleDefinitionEntity> ModuleDefinitions { get; set; }
        }

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

        public class ModuleDefinitionEntity
        {
            [XmlAttribute]
            public int ModuleDefId { get; set; }

            [XmlAttribute]
            public string FriendlyName { get; set; }

            [XmlAttribute("DesktopSourceFile")]
            public string SourceFile { get; set; }
        }

        public class TabDefinitionEntity
        {
            [XmlAttribute]
            public int TabDefId { get; set; }

            [XmlAttribute]
            public string FriendlyName { get; set; }

            [XmlAttribute("DesktopSourceFile")]
            public string SourceFile { get; set; }
        }

        public class TabEntity
        {
            [XmlAttribute]
            public int TabId { get; set; }

            [XmlAttribute]
            public int TabDefId { get; set; }

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

        public class SettingEntity
        {
            [XmlAttribute]
            public string Key { get; set; }

            [XmlAttribute]
            public string Value { get; set; }
        }

    }
}