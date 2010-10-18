using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using PortalStarterKit.Components;

namespace PortalStarterKit.Components
{
    public class XmlSiteConfigurationRepository : ISiteConfigurationRepository
    {
        #region ISiteConfigurationRepository Members

        public List<PortalSettings> Read()
        {
            var deskotPortals = new List<PortalSettings>();
            string path = HttpContext.Current.Server.MapPath("portalcfg.xml");
            XDocument document = XDocument.Load(path);

            foreach (XElement portal in document.Descendants("Portal"))
            {
                var portalItem = new PortalSettings();

                portalItem.AlwaysShowEditButton = Convert.ToBoolean(portal.Attribute("AlwaysShowEditButton").Value);
                portalItem.PortalName = portal.Attribute("PortalName").Value;
                portalItem.PortalId = portal.Attribute("PortalId").Value;

                deskotPortals.Add(portalItem);
                int tabOrder = 0;
                foreach (XElement tab in portal.Descendants("Tab"))
                {
                    var tabItem = new TabSettings();

                    tabItem.TabName = tab.Attribute("TabName").Value;
                    tabItem.TabId = tab.Attribute("TabId").Value;
                    tabOrder++;
                    tabItem.TabOrder = tabOrder;

                    tabItem.AccessRoles =
                            (from item in tab.Attribute("AccessRoles").Value.Split(';')
                             where item.Length > 0
                             select item).ToList();  

                    int moduleOrder = 0;
                    foreach (XElement module in tab.Descendants("Module"))
                    {
                        var moduleItem = new ModuleSettings();

                        moduleItem.TabId = tabItem.TabId;
                        moduleOrder++;
                        moduleItem.ModuleOrder = moduleOrder;

                        moduleItem.ModuleTitle =  module.Attribute("ModuleTitle").Value;

                        moduleItem.EditRoles =
                            (from item in module.Attribute("EditRoles").Value.Split(';')
                             where item.Length > 0
                             select item).ToList();                       
                        
                        moduleItem.PaneName =
                            (PortalPane) Enum.Parse(typeof (PortalPane), module.Attribute("PaneName").Value);
                        moduleItem.ModuleId = module.Attribute("ModuleId").Value;

                        int moduleDefId = Convert.ToInt32(module.Attribute("ModuleDefId").Value);

                        foreach (XElement moduleDef in document.Descendants("ModuleDefinition"))
                        {
                            if (moduleDefId == Convert.ToInt32(moduleDef.Attribute("ModuleDefId").Value))
                            {
                                moduleItem.DesktopSrc = moduleDef.Attribute("DesktopSourceFile").Value;
                                moduleItem.FriendlyName = moduleDef.Attribute("FriendlyName").Value;
                            }
                        }

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