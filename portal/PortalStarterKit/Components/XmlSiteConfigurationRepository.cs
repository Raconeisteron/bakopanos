using System;
using System.Collections.Generic;
using System.Web;
using System.Xml.Linq;

namespace ASPNET.StarterKit.Portal
{
    public class XmlSiteConfigurationRepository : ISiteConfigurationRepository
    {
        public List<PortalSettings> Read()
        {
            var _deskotPortals = new List<PortalSettings>();
            string path = HttpContext.Current.Server.MapPath("portalcfg.xml");
            XDocument document = XDocument.Load(path);

            foreach (XElement portal in document.Descendants("Portal"))
            {
                var portalItem = new PortalSettings();

                portalItem.AlwaysShowEditButton = Convert.ToBoolean(portal.Attribute("AlwaysShowEditButton").Value);
                portalItem.PortalName = portal.Attribute("PortalName").Value;
                portalItem.PortalId = portal.Attribute("PortalId").Value;

                _deskotPortals.Add(portalItem);

                foreach (XElement tab in portal.Descendants("Tab"))
                {
                    var tabItem = new TabSettings();

                    tabItem.TabName = tab.Attribute("TabName").Value;
                    tabItem.TabId = tab.Attribute("TabId").Value;

                    foreach (XElement module in tab.Descendants("Module"))
                    {
                        var moduleItem = new ModuleSettings();

                        moduleItem.TabId = tabItem.TabId;
                        moduleItem.ModuleTitle = module.Attribute("ModuleTitle").Value;
                        moduleItem.PaneName = (PortalPane)Enum.Parse(typeof(PortalPane), module.Attribute("PaneName").Value);
                        moduleItem.ModuleId = module.Attribute("ModuleId").Value;

                        int moduleDefId = Convert.ToInt32(module.Attribute("ModuleDefId").Value);

                        foreach (XElement moduleDef in document.Descendants("ModuleDefinition"))
                        {
                            if (moduleDefId == Convert.ToInt32(moduleDef.Attribute("ModuleDefId").Value))
                            {
                                moduleItem.DesktopSrc = moduleDef.Attribute("DesktopSourceFile").Value;
                            }
                        }

                        tabItem.Modules.Add(moduleItem);
                    }

                    portalItem.DesktopTabs.Add(tabItem);
                }
            }
            return _deskotPortals;
        }
    }
}