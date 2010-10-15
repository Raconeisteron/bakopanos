using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ASPNET.StarterKit.Portal
{
    public class XmlSiteConfigurationService : ISiteConfigurationService
    {
        readonly PortalSettings _settings = new PortalSettings();

        public XmlSiteConfigurationService()
        {
            
            string path = HttpContext.Current.Server.MapPath("portalcfg.xml");
            XDocument document = XDocument.Load(path);

            foreach (XElement global in document.Descendants("Global"))
            {
                _settings.AlwaysShowEditButton = Convert.ToBoolean(global.Attribute("AlwaysShowEditButton").Value);
                _settings.PortalName = global.Attribute("PortalName").Value as string;
                _settings.PortalId = global.Attribute("PortalId").Value;
            }

            foreach (XElement tab in document.Descendants("Tab"))
            {
                var tabItem = new TabSettings();

                tabItem.TabName = tab.Attribute("TabName").Value as string;
                tabItem.TabId = tab.Attribute("TabId").Value;

                foreach (XElement module in tab.Descendants("Module"))
                {
                    var moduleItem = new ModuleSettings();

                    moduleItem.TabId = tabItem.TabId;
                    moduleItem.ModuleTitle = module.Attribute("ModuleTitle").Value as string;
                    moduleItem.PaneName = module.Attribute("PaneName").Value as string;
                    moduleItem.ModuleId = module.Attribute("ModuleId").Value;

                    int moduleDefId = Convert.ToInt32(module.Attribute("ModuleDefId").Value);

                    foreach (XElement moduleDef in document.Descendants("ModuleDefinition"))
                    {
                        if (moduleDefId == Convert.ToInt32(moduleDef.Attribute("ModuleDefId").Value))
                        {
                            moduleItem.DesktopSrc = moduleDef.Attribute("DesktopSourceFile").Value as string;
                        }
                    }

                    tabItem.Modules.Add(moduleItem);
                }

                _settings.DesktopTabs.Add(tabItem);
            }
        }

        public PortalSettings ActivePortal (string portalId)
        {
            return _settings;
        }

        public TabSettings ActiveTab(string tabId)
        {
            return _settings.DesktopTabs.Single<TabSettings>(item=>item.TabId==tabId);
        }
    }
}