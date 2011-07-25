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
        public override SiteConfiguration ReadSiteConfiguration()
        {
            string xmlFile = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["XmlSiteConfigurationFile"]);

            // Code that runs on application startup            
            var serializer = new XmlSerializer(typeof (SiteConfigurationEntity));
            var fs = new FileStream(xmlFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            
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
                MakeTabs(tab, tabEntity.Tabs);
            }
        }
    }
}