using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace ASPNET.StarterKit.Portal
{
    public class XmlSiteConfigurationService : ISiteConfigurationService
    {
        readonly List<PortalSettings> _deskotPortals = new List<PortalSettings>();

        public XmlSiteConfigurationService()
        {
            
            string path = HttpContext.Current.Server.MapPath("portalcfg.xml");
            XDocument document = XDocument.Load(path);

            foreach (XElement portal in document.Descendants("Portal"))
            {
                var portalItem = new PortalSettings();

                portalItem.AlwaysShowEditButton = Convert.ToBoolean(portal.Attribute("AlwaysShowEditButton").Value);
                portalItem.PortalName = portal.Attribute("PortalName").Value;
                portalItem.PortalId = portal.Attribute("PortalId").Value;

                DesktopPortals.Add(portalItem);

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
                        moduleItem.PaneName = module.Attribute("PaneName").Value;
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
        }

        public List<PortalSettings> DesktopPortals
        {
            get { return _deskotPortals;}
        }
        public PortalSettings DefaultPortal
        {
            get
            {
                return DesktopPortals[0];
            }
        }

        public TabSettings DefaultTab
        {
            get
            {
                return DefaultPortal.DesktopTabs[0];
            }
        }

        public PortalSettings GetPortal (string portalId)
        {
            return DesktopPortals.Single<PortalSettings>(item => item.PortalId == portalId);
        }

        public TabSettings ActiveTab(string portalId, string tabId)
        {
            return GetPortal(portalId).DesktopTabs.Single<TabSettings>(item=>item.TabId==tabId);
        }
    }
}