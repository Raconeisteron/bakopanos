using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using PortalStarterKit.Model;

namespace PortalStarterKit.Components
{
    public class XmlSiteConfigurationRepository : ISiteConfigurationRepository
    {
        #region ISiteConfigurationRepository Members

        public List<PortalSettings> Read()
        {
            string path = HttpContext.Current.Server.MapPath(@"App_Data\portalcfg.xml");
            XDocument document = XDocument.Load(path);

            var moduleDefSettings = document.Element("SiteConfiguration").GetModuleDefSettings();

            var deskotPortals = new List<PortalSettings>();
            foreach (XElement portal in document.Descendants("Portal"))
            {
                var portalItem = portal.GetPortalSetting();
                deskotPortals.Add(portalItem);
                int tabOrder = 0;
                foreach (XElement tab in portal.Descendants("Tab"))
                {
                    var tabItem = tab.GetTabSetting();
                    tabOrder++;
                    tabItem.TabOrder = tabOrder;

                    int moduleOrder = 0;
                    foreach (XElement module in tab.Descendants("Module"))
                    {
                        var moduleItem = module.GetModuleSetting();

                        moduleItem.TabId = tabItem.TabId;
                        moduleOrder++;
                        moduleItem.ModuleOrder = moduleOrder;

                        moduleItem.ModuleDef = moduleDefSettings.Single(item => item.ModuleDefId == moduleItem.ModuleDef.ModuleDefId);

                        tabItem.Modules.Add(moduleItem);
                    }

                    portalItem.DesktopTabs.Add(tabItem);
                }
            }
            return deskotPortals;
        }

        #endregion
    }
}