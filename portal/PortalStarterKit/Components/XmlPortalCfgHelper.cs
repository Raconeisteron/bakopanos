using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using PortalStarterKit.Model;

namespace PortalStarterKit.Components
{
    public static class XmlPortalCfgHelper 
    {
        public static List<ModuleDefSettings> GetModuleDefSettings(this XElement element)
        {
            var moduleDefSettings = new List<ModuleDefSettings>();
            foreach (XElement moduleDef in element.Descendants("ModuleDefinition"))
            {
                moduleDefSettings.Add(
                    new ModuleDefSettings
                        {
                            ModuleDefId = moduleDef.Attribute("ModuleDefId").Value,
                            DesktopSrc = moduleDef.Attribute("DesktopSourceFile").Value,
                            FriendlyName = moduleDef.Attribute("FriendlyName").Value
                        });
            }

            return moduleDefSettings;
        }

        public static PortalSettings GetPortalSetting(this XElement element)
        {
            var portalItem = new PortalSettings();
            portalItem.AlwaysShowEditButton = Convert.ToBoolean(element.Attribute("AlwaysShowEditButton").Value);
            portalItem.PortalName = element.Attribute("PortalName").Value;
            portalItem.PortalId = element.Attribute("PortalId").Value;
            return portalItem;
        }

        public static TabSettings GetTabSetting(this XElement element)
        {
            var tabItem = new TabSettings();

            tabItem.TabName = element.Attribute("TabName").Value;
            tabItem.TabId = element.Attribute("TabId").Value;
            tabItem.AccessRoles =
                    (from item in element.Attribute("AccessRoles").Value.Split(';')
                     where item.Length > 0
                     select item).ToList();

            return tabItem;
        }

        public static ModuleSettings GetModuleSetting(this XElement element)
        {
            var moduleItem = new ModuleSettings();

            moduleItem.ModuleTitle = element.Attribute("ModuleTitle").Value;

            moduleItem.EditRoles =
                (from item in element.Attribute("EditRoles").Value.Split(';')
                 where item.Length > 0
                 select item).ToList();

            moduleItem.PaneName =
                (PortalPane)Enum.Parse(typeof(PortalPane), element.Attribute("PaneName").Value);
            moduleItem.ModuleId = element.Attribute("ModuleId").Value;

            string moduleDefId = element.Attribute("ModuleDefId").Value;
            moduleItem.ModuleDef = new ModuleDefSettings {ModuleDefId = moduleDefId};

            return moduleItem;
        }
    }
}