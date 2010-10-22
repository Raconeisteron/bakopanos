using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using PortalStarterKit.Core;
using PortalStarterKit.Model;

namespace PortalStarterKit.Components
{
    public class ContentSiteConfigurationRepository : ISiteConfigurationRepository
    {
        private ISiteEnvironment _environment;
        public ContentSiteConfigurationRepository(ISiteEnvironment environment)
        {
            _environment = environment;
        }

        #region ISiteConfigurationRepository Members

        public List<PortalSettings> Read()
        {
            XDocument document = XDocument.Load(Path.Combine( _environment.DataPhysicalPath , "ModuleDefinition.xml"));

            var moduleDefSettings = document.Element("SiteConfiguration").
                Descendants("ModuleDefinition").GetModuleDefSettings();
            
            var deskotPortals = new List<PortalSettings>();
            string[] portals = Directory.GetDirectories(_environment.DataPhysicalPath);

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
                    var tabdoc = XDocument.Load(Path.Combine(tab, "Tab.xml"));
                    var tabItem = tabdoc.
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

                        var moduledoc = XDocument.Load(Path.Combine(module, "Module.xml"));
                        var moduleItem = moduledoc.
                            Element("SiteConfiguration").
                            Element("Module").GetModuleSetting(mname);

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