using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using Microsoft.Practices.Unity;

namespace ASPNETPortal
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// tab configuration settings, module configuration settings and module 
    /// definition configuration settings from the PortalCfg.xml file.
    /// </summary>
    internal class ConfigurationDb : IGlobalDb, ITabDb, IModuleDb, IModuleDefinitionDb, IAuthorizationDb
    {
        private readonly string _configFile;
        private readonly IPortalModulesDb _portalModulesDb;
        private readonly HttpContextBase _context;

        public ConfigurationDb(HttpContextBase context, IPortalModulesDb portalModulesDb, [Dependency("ConfigFile")] string configFile)
        {
            _context = context;
            _portalModulesDb = portalModulesDb;
            _configFile = configFile;
        }

        #region PORTAL

        public GlobalItem GetGlobalByPortalId(int portalId)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Find the appropriate Module in the Module table
            return siteSettings.Global.FindByPortalId(portalId).ToGlobalItem();
        }

        /// <summary>
        /// The UpdatePortalInfo method updates the name and access settings for the portal.
        /// These settings are stored in the Xml file PortalCfg.xml.
        /// </summary>        
        public void UpdatePortalInfo(int portalId, String portalName, bool alwaysShow)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Get first record of the "Global" element 
            SiteConfiguration.GlobalRow globalRow = siteSettings.Global.FindByPortalId(portalId);

            // Update the values
            globalRow.PortalId = portalId;
            globalRow.PortalName = portalName;
            globalRow.AlwaysShowEditButton = alwaysShow;

            // Save the changes 
            SaveSiteSettings(siteSettings);
        }

        #endregion

        #region TABS

        /// <summary>
        /// Returns a list of all tabs for the portal.
        /// </summary>        
        public IEnumerable<TabItem> GetTabs()
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Find the appropriate Tab in the Tab table
            return
                siteSettings.Tab.Select("", "TabOrder").Cast<SiteConfiguration.TabRow>().Select(item => item.ToTabItem());
        }

        /// <summary>
        /// Returns a list of all mobile tabs for the portal.
        /// </summary>        
        public IEnumerable<TabItem> GetMobileTabs()
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Find the appropriate Tab in the Tab table
            return
                siteSettings.Tab.Select("ShowMobile='true'", "TabOrder").Cast<SiteConfiguration.TabRow>().Select(
                    item => item.ToTabItem());
        }

        /// <summary>
        /// Returns a tab by tabid
        /// </summary>        
        public TabItem GetSingleTabByTabId(int tabId)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Find the appropriate Tab in the Tab table
            return siteSettings.Tab.FindByTabId(tabId).ToTabItem();
        }


        /// <summary>
        /// The AddTab method adds a new tab to the portal.  These settings are 
        /// stored in the Xml file PortalCfg.xml.
        /// </summary>        
        public int AddTab(int portalId, String tabName, int tabOrder)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Create a new TabRow from the Tab table
            SiteConfiguration.TabRow newRow = siteSettings.Tab.NewTabRow();

            // Set the properties on the new row
            newRow.TabName = tabName;
            newRow.TabOrder = tabOrder;
            newRow.MobileTabName = String.Empty;
            newRow.ShowMobile = true;
            newRow.AccessRoles = "All Users;";

            // Add the new TabRow to the Tab table
            siteSettings.Tab.AddTabRow(newRow);

            // Save the changes 
            SaveSiteSettings(siteSettings);

            // Return the new TabID
            return newRow.TabId;
        }

        /// <summary>
        /// The UpdateTab method updates the settings for the specified tab. 
        /// These settings are stored in the Xml file PortalCfg.xml.
        /// </summary>        
        public void UpdateTab(int portalId, int tabId, String tabName, int tabOrder, String authorizedRoles,
                              String mobileTabName, bool showMobile)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Find the appropriate tab in the Tab table and set the properties
            SiteConfiguration.TabRow tabRow = siteSettings.Tab.FindByTabId(tabId);

            tabRow.TabName = tabName;
            tabRow.TabOrder = tabOrder;
            tabRow.AccessRoles = authorizedRoles;
            tabRow.MobileTabName = mobileTabName;
            tabRow.ShowMobile = showMobile;

            // Save the changes 
            SaveSiteSettings(siteSettings);
        }

        /// <summary>
        /// The UpdateTabOrder method changes the position of the tab with respect
        /// to other tabs in the portal.  These settings are stored in the Xml 
        /// file PortalCfg.xml.
        /// </summary>        
        public void UpdateTabOrder(int tabId, int tabOrder)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Find the appropriate tab in the Tab table and set the property
            SiteConfiguration.TabRow tabRow = siteSettings.Tab.FindByTabId(tabId);

            tabRow.TabOrder = tabOrder;

            // Save the changes 
            SaveSiteSettings(siteSettings);
        }

        /// <summary>
        /// The DeleteTab method deletes the selected tab and its modules from 
        /// the settings which are stored in the Xml file PortalCfg.xml.  This 
        /// method also deletes any data from the database associated with all 
        /// modules within this tab.
        /// </summary>
        public void DeleteTab(int tabId)
        {
            //
            // Delete the Tab in the XML file
            //

            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Find the appropriate tab in the Tab table
            SiteConfiguration.TabDataTable tabTable = siteSettings.Tab;
            SiteConfiguration.TabRow tabRow = siteSettings.Tab.FindByTabId(tabId);

            //
            // Delete information in the Database relating to each Module being deleted
            //
            foreach (SiteConfiguration.ModuleRow moduleRow in tabRow.GetModuleRows())
            {
                _portalModulesDb.DeletePortalModule(moduleRow.ModuleId);
            }

            // Finish removing the Tab row from the Xml file
            tabTable.RemoveTabRow(tabRow);

            // Save the changes 
            SaveSiteSettings(siteSettings);
        }

        #endregion

        #region MODULES

        public IEnumerable<ModuleItem> GetModulesByTabId(int tabId)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            return
                siteSettings.Tab.FindByTabId(tabId).GetModuleRows().Select(
                    item => item.ToModuleItem());
        }

        /// <summary>
        /// The UpdateModuleOrder method updates the order in which the modules
        /// in a tab are displayed.  These settings are stored in the Xml file 
        /// PortalCfg.xml.
        /// </summary>        
        public void UpdateModuleOrder(int moduleId, int moduleOrder, String pane)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Find the appropriate Module in the Module table and update the properties
            SiteConfiguration.ModuleRow moduleRow = siteSettings.Module.FindByModuleId(moduleId);

            moduleRow.ModuleOrder = moduleOrder;
            moduleRow.PaneName = pane;

            // Save the changes 
            SaveSiteSettings(siteSettings);
        }

        /// <summary>
        /// The AddModule method adds Portal Settings for a new Module within
        /// a Tab.  These settings are stored in the Xml file PortalCfg.xml.
        /// </summary>        
        public int AddModule(int tabId, int moduleOrder, String paneName, String title, int moduleDefId,
                             int cacheTime,
                             String editRoles, bool showMobile)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Create a new ModuleRow from the Module table
            SiteConfiguration.ModuleRow newModule = siteSettings.Module.NewModuleRow();

            // Set the properties on the new Module
            newModule.ModuleDefId = moduleDefId;
            newModule.ModuleOrder = moduleOrder;
            newModule.ModuleTitle = title;
            newModule.PaneName = paneName;
            newModule.EditRoles = editRoles;
            newModule.CacheTimeout = cacheTime;
            newModule.ShowMobile = showMobile;
            newModule.TabRow = siteSettings.Tab.FindByTabId(tabId);

            // Add the new ModuleRow to the Module table
            siteSettings.Module.AddModuleRow(newModule);

            // Save the changes
            SaveSiteSettings(siteSettings);

            // Return the new Module ID
            return newModule.ModuleId;
        }

        /// <summary>
        /// The UpdateModule method updates the Portal Settings for an existing 
        /// Module within a Tab.  These settings are stored in the Xml file
        /// PortalCfg.xml.
        /// </summary>
        public int UpdateModule(int moduleId, int moduleOrder, String paneName, String title, int cacheTime,
                                String editRoles, bool showMobile)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Find the appropriate Module in the Module table and update the properties
            SiteConfiguration.ModuleRow moduleRow = siteSettings.Module.FindByModuleId(moduleId);

            moduleRow.ModuleOrder = moduleOrder;
            moduleRow.ModuleTitle = title;
            moduleRow.PaneName = paneName;
            moduleRow.CacheTimeout = cacheTime;
            moduleRow.EditRoles = editRoles;
            moduleRow.ShowMobile = showMobile;

            // Save the changes 
            SaveSiteSettings(siteSettings);

            // Return the existing Module ID
            return moduleId;
        }

        /// <summary>
        /// The DeleteModule method deletes a specified Module from the settings
        /// stored in the Xml file PortalCfg.xml.  This method also deletes any 
        /// data from the database associated with this module.
        /// </summary>        
        public void DeleteModule(int moduleId)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            //
            // Delete information in the Database relating to Module being deleted
            //            
            _portalModulesDb.DeletePortalModule(moduleId);

            // Finish removing Module
            siteSettings.Module.RemoveModuleRow(siteSettings.Module.FindByModuleId(moduleId));

            // Save the changes 
            SaveSiteSettings(siteSettings);
        }

        /// <summary>
        /// The UpdateModuleSetting Method updates a single module setting 
        /// in the configuration file.  If the value passed in is String.Empty,
        /// the Setting element is deleted if it exists.  If not, either a 
        /// matching Setting element is updated, or a new Setting element is 
        /// created.
        /// </summary>
        public void UpdateModuleSetting(int moduleId, String key, String val)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Find the appropriate Module in the Module table
            SiteConfiguration.ModuleRow moduleRow = siteSettings.Module.FindByModuleId(moduleId);

            // Find the first (only) settings element
            SiteConfiguration.SettingsRow settingsRow;

            if (moduleRow.GetSettingsRows().Length > 0)
            {
                settingsRow = moduleRow.GetSettingsRows()[0];
            }
            else
            {
                // Add new settings element
                settingsRow = siteSettings.Settings.NewSettingsRow();

                // Set the parent relationship
                settingsRow.ModuleRow = moduleRow;

                siteSettings.Settings.AddSettingsRow(settingsRow);
            }

            // Find the child setting elements
            SiteConfiguration.SettingRow settingRow;

            SiteConfiguration.SettingRow[] settingRows = settingsRow.GetSettingRows();

            if (settingRows.Length == 0)
            {
                // If there are no Setting elements at all, add one with the new name and value,
                // but only if the value is not empty
                if (val != String.Empty)
                {
                    settingRow = siteSettings.Setting.NewSettingRow();

                    // Set the parent relationship and data
                    settingRow.SettingsRow = settingsRow;
                    settingRow.Name = key;
                    settingRow.Setting_Text = val;

                    siteSettings.Setting.AddSettingRow(settingRow);
                }
            }
            else
            {
                // Update existing setting element if it matches
                bool found = false;
                Int32 i;

                // Find which row matches the input parameter "key" and update the
                // value.  If the value is String.Empty, however, delete the row.
                for (i = 0; i < settingRows.Length; i++)
                {
                    if (settingRows[i].Name == key)
                    {
                        if (val == String.Empty)
                        {
                            // Delete the row
                            siteSettings.Setting.RemoveSettingRow(settingRows[i]);
                        }
                        else
                        {
                            // Update the value
                            settingRows[i].Setting_Text = val;
                        }

                        found = true;
                    }
                }

                if (found == false)
                {
                    // Setting elements exist, however, there is no matching Setting element.
                    // Add one with new name and value, but only if the value is not empty
                    if (val != String.Empty)
                    {
                        settingRow = siteSettings.Setting.NewSettingRow();

                        // Set the parent relationship and data
                        settingRow.SettingsRow = settingsRow;
                        settingRow.Name = key;
                        settingRow.Setting_Text = val;

                        siteSettings.Setting.AddSettingRow(settingRow);
                    }
                }
            }

            // Save the changes 
            SaveSiteSettings(siteSettings);
        }

        /// <summary>
        /// The GetModuleSettings Method returns a hashtable of custom,
        /// module-specific settings from the configuration file.  This method is
        /// used by some user control modules (Xml, Image, etc) to access misc
        /// settings.
        /// </summary>        
        public Hashtable GetModuleSettings(int moduleId)
        {
            // Create a new Hashtable
            var settingsHt = new Hashtable();

            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Find the appropriate Module in the Module table
            SiteConfiguration.ModuleRow moduleRow = siteSettings.Module.FindByModuleId(moduleId);

            // Find the first (only) settings element
            if (moduleRow.GetSettingsRows().Length > 0)
            {
                SiteConfiguration.SettingsRow settingsRow = moduleRow.GetSettingsRows()[0];

                if (settingsRow != null)
                {
                    // Find the child setting elements and add to the hashtable
                    foreach (SiteConfiguration.SettingRow sRow in settingsRow.GetSettingRows())
                    {
                        settingsHt[sRow.Name] = sRow.Setting_Text;
                    }
                }
            }

            return settingsHt;
        }

        public ModuleItem GetModuleByModuleId(int moduleId)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Find the appropriate Module in the Module table
            return siteSettings.Module.FindByModuleId(moduleId).ToModuleItem();
        }

        #endregion

        #region MODULE DEFINITIONS

        public ModuleDefinitionItem GetModuleDefinitionByModuleDefId(int moduleDefId)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Find the appropriate Module in the Module table
            return siteSettings.ModuleDefinition.FindByModuleDefId(moduleDefId).ToModuleDefinitionItem();
        }

        /// <summary>
        /// The GetModuleDefinitions method returns a list of all module type 
        /// definitions for the portal.
        /// </summary>        
        public IEnumerable<ModuleDefinitionItem> GetModuleDefinitions(int portalId)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Find the appropriate Module in the Module table
            return
                siteSettings.ModuleDefinition.Select().Cast<SiteConfiguration.ModuleDefinitionRow>().Select(
                    item => item.ToModuleDefinitionItem());
        }

        /// <summary>
        /// The AddModuleDefinition add the definition for a new module type
        /// to the portal.
        /// </summary>        
        public int AddModuleDefinition(int portalId, String name, String desktopSrc, String mobileSrc)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Create new ModuleDefinitionRow
            SiteConfiguration.ModuleDefinitionRow newModuleDef = siteSettings.ModuleDefinition.NewModuleDefinitionRow();

            // Set the parameter values
            newModuleDef.FriendlyName = name;
            newModuleDef.DesktopSourceFile = desktopSrc;
            newModuleDef.MobileSourceFile = mobileSrc;

            // Add the new ModuleDefinitionRow to the ModuleDefinition table
            siteSettings.ModuleDefinition.AddModuleDefinitionRow(newModuleDef);

            // Save the changes
            SaveSiteSettings(siteSettings);

            // Return the new ModuleDefID
            return newModuleDef.ModuleDefId;
        }

        /// <summary>
        /// The DeleteModuleDefinition method deletes the specified module type 
        /// definition from the portal.  Each module which is related to the
        /// ModuleDefinition is deleted from each tab in the configuration
        /// file, and all data relating to each module is deleted from the
        /// database.
        /// </summary>        
        public void DeleteModuleDefinition(int defId)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            //
            // Delete information in the Database relating to each Module being deleted
            //
            foreach (SiteConfiguration.ModuleRow moduleRow in siteSettings.Module.Select())
            {
                if (moduleRow.ModuleDefId == defId)
                {
                    _portalModulesDb.DeletePortalModule(moduleRow.ModuleId);

                    // Delete the xml module associated with the ModuleDef
                    // in the configuration file
                    siteSettings.Module.RemoveModuleRow(moduleRow);
                }
            }

            // Finish removing Module Definition
            siteSettings.ModuleDefinition.RemoveModuleDefinitionRow(
                siteSettings.ModuleDefinition.FindByModuleDefId(defId));

            // Save the changes 
            SaveSiteSettings(siteSettings);
        }

        /// <summary>
        /// The UpdateModuleDefinition method updates the settings for the 
        /// specified module type definition.
        /// </summary>
        public void UpdateModuleDefinition(int defId, String name, String desktopSrc, String mobileSrc)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Find the appropriate Module in the Module table and update the properties
            SiteConfiguration.ModuleDefinitionRow modDefRow = siteSettings.ModuleDefinition.FindByModuleDefId(defId);

            modDefRow.FriendlyName = name;
            modDefRow.DesktopSourceFile = desktopSrc;
            modDefRow.MobileSourceFile = mobileSrc;

            // Save the changes 
            SaveSiteSettings(siteSettings);
        }

        /// <summary>
        /// The GetSingleModuleDefinition method returns a ModuleDefinitionRow
        /// object containing details about a specific module definition in the
        /// configuration file.
        /// </summary>        
        public ModuleDefinitionItem GetSingleModuleDefinition(int defId)
        {
            // Obtain SiteSettings from Current Context
            SiteConfiguration siteSettings = GetSiteSettings();

            // Find the appropriate Module in the Module table
            return siteSettings.ModuleDefinition.FindByModuleDefId(defId).ToModuleDefinitionItem();
        }

        #endregion

        #region IAuthorizationDb Members

        public string GetEditRolesByModuleId(int moduleId)
        {
            return GetModuleByModuleId(moduleId).EditRoles;
        }

        public string GetAccessRolesByModuleId(int moduleId)
        {
            int tabId = GetModuleByModuleId(moduleId).TabId;
            return GetSingleTabByTabId(tabId).AccessRoles;
        }

        #endregion

        /// <summary>
        /// The Configuration.GetSiteSettings Method returns a typed
        /// dataset of the all of the site configuration settings from the
        /// XML configuration file.  This method is used in Global.asax to
        /// push the settings into the current HttpContext, so that all of the 
        /// pages, content modules and classes throughout the rest of the request
        /// may access them.
        ///
        /// The SiteConfiguration object is cached using the ASP.NET Cache API,
        /// with a file-change dependency on the XML configuration file.  Normallly,
        /// this method just returns a copy of the object in the cache.  When the
        /// configuration is updated and changes are saved to the the XML file,
        /// the SiteConfiguration object is evicted from the cache.  The next time 
        /// this method runs, it will read from the XML file again and insert a
        /// fresh copy of the SiteConfiguration into the cache.
        /// </summary>
        private SiteConfiguration GetSiteSettings()
        {
            //only a hack for unit tests...
            if (_context.Cache == null)
            {
                return CreateSiteSettings();
            }

            var siteSettings = (SiteConfiguration)_context.Cache["SiteSettings"];

            // If the SiteConfiguration isn't cached, load it from the XML file and add it into the cache.
            if (siteSettings == null)
            {
                siteSettings = CreateSiteSettings();

                // Store the dataset in the cache
                _context.Cache.Insert("SiteSettings", siteSettings, new CacheDependency(_configFile));
            }

            return siteSettings;
        }

        private SiteConfiguration CreateSiteSettings()
        {
            var siteSettings = new SiteConfiguration();

            // Set the AutoIncrement property to true for easier adding of rows
            siteSettings.Tab.TabIdColumn.AutoIncrement = true;
            siteSettings.Module.ModuleIdColumn.AutoIncrement = true;
            siteSettings.ModuleDefinition.ModuleDefIdColumn.AutoIncrement = true;

            // Load the XML data into the DataSet
            siteSettings.ReadXml(_configFile);
            return siteSettings;
        }

        /// <summary>
        /// The Configuration.SaveSiteSettings overwrites the the XML file with the
        /// settings in the SiteConfiguration object in context.  The object will in 
        /// turn be evicted from the cache and be reloaded from the XML file the next
        /// time GetSiteSettings() is called.
        /// </summary>
        private void SaveSiteSettings(SiteConfiguration siteSettings)
        {
            //// Obtain SiteSettings from the Cache
            //SiteConfiguration siteSettings = GetSiteSettings();

            //// Check the object
            //if (siteSettings == null)
            //{
            //    // If SaveSiteSettings() is called once, the cache is cleared.  If it is
            //    // then called again before Global.Application_BeginRequest is called, 
            //    // which reloads the cache, the siteSettings object will be Null 
            //    siteSettings = GetSiteSettings();
            //}

            // Object is evicted from the Cache here.  
            siteSettings.WriteXml(_configFile);

            //only a hack for unit tests...
            if (_context.Cache != null)
            {
                // Store the dataset in the cache
                _context.Cache.Insert("SiteSettings", siteSettings, new CacheDependency(_configFile));
            }
            
        }
    }
}