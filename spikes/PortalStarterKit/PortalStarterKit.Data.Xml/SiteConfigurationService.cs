using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;
using PortalStarterKit.Model;

namespace PortalStarterKit.Data.Xml
{
    public class SiteConfigurationService : ConfigurationSection, ISiteConfigurationService
    {
        [ConfigurationProperty("XmlFile")]
        public string XmlFile
        {
            get
            {
                return this["XmlFile"] as string;
            }
        }

        public SiteConfiguration ReadSiteConfiguration(Func<string, string> serverMapPath)
        {             
            // Code that runs on application startup            
            var serializer = new XmlSerializer(typeof (SiteConfigurationEntity));
            var fs = new FileStream(serverMapPath(XmlFile), FileMode.Open, FileAccess.Read, FileShare.Read);
            
            var siteConfigurationEntity = (SiteConfigurationEntity) serializer.Deserialize(fs);

            var configuration = new SiteConfiguration();
            
            foreach (TabDefinitionEntity entity in siteConfigurationEntity.TabDefinitions)
            {
                var item = new TabDefinition
                {
                    TabDefId = entity.TabDefId,
                    FriendlyName = entity.FriendlyName,
                    SourceFile = entity.SourceFile
                };
                configuration.TabDefinitions.Add(item);
            }
            
            foreach (ModuleDefinitionEntity entity in siteConfigurationEntity.ModuleDefinitions)
            {
                var item = new ModuleDefinition
                {
                    ModuleDefId = entity.ModuleDefId,
                    FriendlyName = entity.FriendlyName,
                    SourceFile = entity.SourceFile
                };
                configuration.ModuleDefinitions.Add(item);
            }

            foreach (PortalEntity portalEntity in siteConfigurationEntity.Portals)
            {
                var portal = configuration.NewPortal();
                portal.PortalId = portalEntity.PortalId;
                portal.PortalName = portalEntity.PortalName;
                portal.AlwaysShowEditButton = portalEntity.AlwaysShowEditButton;                

                configuration.Portals.Add(portal);
                MakeTabs(portal, portalEntity.Tabs);
            }

            return configuration;
        }

        private static void MakeTabs(ITabContainer tabContainer, IEnumerable<TabEntity> tabEntities)
        {
            foreach (TabEntity tabEntity in tabEntities)
            {
                var tab = tabContainer.NewTab();
                tab.TabId = tabEntity.TabId;
                tab.TabName = tabEntity.TabName;
                tab.TabDefId = tabEntity.TabDefId;
                tab.TabOrder = tabEntity.TabOrder;
                tabContainer.Tabs.Add(tab);
                foreach (ModuleEntity moduleEntity in tabEntity.Modules)
                {
                    var module = tab.NewModule();
                    module.ModuleId = moduleEntity.ModuleId;
                    module.ModuleDefId = moduleEntity.ModuleDefId;
                    module.ModuleTitle = moduleEntity.ModuleTitle;
                    module.ModuleOrder = moduleEntity.ModuleOrder;
                    module.PaneName = moduleEntity.PaneName;
                    
                    tab.Modules.Add(module);
                }
                MakeTabs(tab, tabEntity.Tabs);
            }
        }
    }
}