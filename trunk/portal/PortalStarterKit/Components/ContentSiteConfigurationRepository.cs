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

            var moduleDefSettings = new List<ModuleDefSettings>();
            foreach (XElement moduleDef in document.Descendants("ModuleDefinition"))
            {
                moduleDefSettings.Add(
                    new ModuleDefSettings
                        {
                            ModuleDefId = moduleDef.Attribute("ModuleDefId").Value,
                            DesktopSrc = moduleDef.Attribute("DesktopSourceFile").Value,
                            FriendlyName = moduleDef.Attribute("FriendlyName").Value
                        });
            }
            
            var deskotPortals = new List<PortalSettings>();
            string[] portals = Directory.GetDirectories(path);

            foreach (string portal in portals)
            {
                string pname = new DirectoryInfo(portal).Name;
                if (pname==".svn")
                {
                    continue;
                }
                var portalItem = new PortalSettings();

                //portalItem.AlwaysShowEditButton = Convert.ToBoolean(portal.Attribute("AlwaysShowEditButton").Value);
                portalItem.PortalName = pname;
                portalItem.PortalId = pname;

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
                    var tabItem = new TabSettings();

                    tabItem.TabName = tname;
                    tabItem.TabId = tname;
                    tabOrder++;
                    tabItem.TabOrder = tabOrder;

                    //tabItem.AccessRoles =
                    //        (from item in tab.Attribute("AccessRoles").Value.Split(';')
                    //         where item.Length > 0
                    //         select item).ToList();  
                    
                    int moduleOrder = 0;
                    string[] modules = Directory.GetDirectories(tab);
                    foreach (string module in modules)
                    {
                        string mname = new DirectoryInfo(module).Name;
                        if (mname == ".svn")
                        {
                            continue;
                        }
                        var moduleItem = new ModuleSettings();

                        moduleItem.TabId = tabItem.TabId;
                        moduleOrder++;
                        moduleItem.ModuleOrder = moduleOrder;

                        moduleItem.ModuleTitle =  mname;

                        /*moduleItem.EditRoles =
                            (from item in module.Attribute("EditRoles").Value.Split(';')
                             where item.Length > 0
                             select item).ToList();                       
                        */
                        moduleItem.PaneName =
                            (PortalPane) Enum.Parse(typeof (PortalPane), "Content");
                        moduleItem.ModuleId = mname;

                        moduleItem.ModuleDef = moduleDefSettings.Single(item => item.ModuleDefId == "1");
                        
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