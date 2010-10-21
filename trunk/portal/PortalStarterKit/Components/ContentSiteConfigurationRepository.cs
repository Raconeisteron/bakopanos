using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using PortalStarterKit.Model;

namespace PortalStarterKit.Components
{
    public class ContentSiteConfigurationRepository : ISiteConfigurationRepository
    {
        #region ISiteConfigurationRepository Members

        public List<PortalSettings> Read()
        {
            string path = HttpContext.Current.Server.MapPath(@"App_Data");
            XDocument document = XDocument.Load(Path.Combine( path , "ModuleDefinition.xml"));

            var moduleDefSettings = document.Element("SiteConfiguration").
                Descendants("ModuleDefinition").GetModuleDefSettings();
            
            var deskotPortals = new List<PortalSettings>();
            string[] portals = Directory.GetDirectories(path);

            foreach (string portal in portals)
            {
                string pname = new DirectoryInfo(portal).Name;
                if (pname==".svn")
                {
                    continue;
                }
                PortalSettings portalItem = XDocument.Load(Path.Combine(portal, "Portal.xml")).
                    Element("SiteConfiguration").Element("Portal").GetPortalSetting(pname);

                deskotPortals.Add(portalItem);
                int tabOrder = 0;
                string[] tabs = Directory.GetDirectories(portal);
                foreach (string tab in tabs)
                {
                    string tname = new DirectoryInfo(tab).Name;
                    if (tname == ".svn")
                    {
                        continue;
                    }
                    var tabItem = XDocument.Load(Path.Combine(tab, "Tab.xml")).
                    Element("SiteConfiguration").Element("Tab").GetTabSetting(tname);
                    tabOrder++;
                    tabItem.TabOrder = tabOrder;
                    
                    int moduleOrder = 0;
                    string[] modules = Directory.GetDirectories(tab);
                    foreach (string module in modules)
                    {
                        string mname = new DirectoryInfo(module).Name;
                        if (mname == ".svn")
                        {
                            continue;
                        }

                        var moduleItem = XDocument.Load(Path.Combine(module, "Module.xml")).
                    Element("SiteConfiguration").Element("Module").GetModuleSetting(mname);

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