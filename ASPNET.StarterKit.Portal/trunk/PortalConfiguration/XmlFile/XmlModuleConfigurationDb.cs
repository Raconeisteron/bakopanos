using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace ASPNET.StarterKit.Portal.XmlFile
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// tab configuration settings, module configuration settings and module 
    /// definition configuration settings from the PortalCfg.xml file.
    /// </summary>
    internal class XmlModuleConfigurationDb : IModuleConfigurationDb
    {

        public ModuleAuthorization FindModuleRolesByModuleId(int moduleId)
        {
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration)HttpContext.Current.Items["SiteSettings"];

            SiteConfiguration.ModuleRow moduleRow = siteSettings.Module.FindByModuleId(moduleId);

            return new ModuleAuthorization
                       {
                           AccessRoles = moduleRow.TabRow.AccessRoles,
                           EditRoles = moduleRow.EditRoles
                       };
        }


        //*********************************************************************
        //
        // UpdateModuleOrder Method  <a name="UpdateModuleOrder"></a>
        //
        // The UpdateModuleOrder method updates the order in which the modules
        // in a tab are displayed.  These settings are stored in the Xml file 
        // PortalCfg.xml.
        //
        // Other relevant sources:
        //    + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
        //	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
        //
        //*********************************************************************
        public void UpdateModuleOrder(int moduleId, int moduleOrder, String pane)
        {
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration)HttpContext.Current.Items["SiteSettings"];

            // Find the appropriate Module in the Module table and update the properties
            SiteConfiguration.ModuleRow moduleRow = siteSettings.Module.FindByModuleId(moduleId);

            moduleRow.ModuleOrder = moduleOrder;
            moduleRow.PaneName = pane;

            // Save the changes 
            IConfigurationDb config = new XmlConfigurationDb();
            config.SaveSiteSettings();
        }


        //*********************************************************************
        //
        // AddModule Method  <a name="AddModule"></a>
        //
        // The AddModule method adds Portal Settings for a new Module within
        // a Tab.  These settings are stored in the Xml file PortalCfg.xml.
        //
        // Other relevant sources:
        //    + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
        //	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
        //
        //*********************************************************************
        public int AddModule(int tabId, int moduleOrder, String paneName, String title, int moduleDefId, int cacheTime,
                             String editRoles, bool showMobile)
        {
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration)HttpContext.Current.Items["SiteSettings"];

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
            IConfigurationDb config = new XmlConfigurationDb();
            config.SaveSiteSettings();

            // Return the new Module ID
            return newModule.ModuleId;
        }


        //*********************************************************************
        //
        // UpdateModule Method  <a name="UpdateModule"></a>
        //
        // The UpdateModule method updates the Portal Settings for an existing 
        // Module within a Tab.  These settings are stored in the Xml file
        // PortalCfg.xml.
        //
        // Other relevant sources:
        //    + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
        //	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
        //
        //*********************************************************************
        public int UpdateModule(int moduleId, int moduleOrder, String paneName, String title, int cacheTime,
                                String editRoles, bool showMobile)
        {
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration)HttpContext.Current.Items["SiteSettings"];

            // Find the appropriate Module in the Module table and update the properties
            SiteConfiguration.ModuleRow moduleRow = siteSettings.Module.FindByModuleId(moduleId);

            moduleRow.ModuleOrder = moduleOrder;
            moduleRow.ModuleTitle = title;
            moduleRow.PaneName = paneName;
            moduleRow.CacheTimeout = cacheTime;
            moduleRow.EditRoles = editRoles;
            moduleRow.ShowMobile = showMobile;

            // Save the changes 
            IConfigurationDb config = new XmlConfigurationDb();
            config.SaveSiteSettings();

            // Return the existing Module ID
            return moduleId;
        }

        //*********************************************************************
        //
        // DeleteModule Method  <a name="DeleteModule"></a>
        //
        // The DeleteModule method deletes a specified Module from the settings
        // stored in the Xml file PortalCfg.xml.  This method also deletes any 
        // data from the database associated with this module.
        //
        // Other relevant sources:
        //    + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
        //	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
        //	  + <a href="DeleteModule.htm" style="color:green">DeleteModule stored procedure</a>
        //
        //*********************************************************************
        public void DeleteModule(int moduleId)
        {
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration)HttpContext.Current.Items["SiteSettings"];

            //
            // Delete information in the Database relating to Module being deleted
            //

            // Create Instance of Connection and Command Object
            var myConnection =
                new SqlConnection(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            var myCommand = new SqlCommand("Portal_DeleteModule", myConnection);

            // Mark the Command as a SPROC
            myCommand.CommandType = CommandType.StoredProcedure;

            // Add Parameters to SPROC
            var parameterModuleID = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            myConnection.Open();

            parameterModuleID.Value = moduleId;
            myCommand.Parameters.Add(parameterModuleID);

            // Open the database connection and execute the command
            myCommand.ExecuteNonQuery();
            myConnection.Close();

            // Finish removing Module
            siteSettings.Module.RemoveModuleRow(siteSettings.Module.FindByModuleId(moduleId));

            // Save the changes 
            IConfigurationDb config = new XmlConfigurationDb();
            config.SaveSiteSettings();
        }


        //*********************************************************************
        //
        // UpdateModuleSetting Method  <a name="UpdateModuleSetting"></a>
        //
        // The UpdateModuleSetting Method updates a single module setting 
        // in the configuration file.  If the value passed in is String.Empty,
        // the Setting element is deleted if it exists.  If not, either a 
        // matching Setting element is updated, or a new Setting element is 
        // created.
        //
        // Other relevant sources:
        //    + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
        //	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
        //
        //*********************************************************************
        public void UpdateModuleSetting(int moduleId, String key, String val)
        {
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration)HttpContext.Current.Items["SiteSettings"];

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
            var config = new XmlConfigurationDb();
            config.SaveSiteSettings();
        }

        //*********************************************************************
        //
        // GetModuleSettings Method  <a name="GetModuleSettings"></a>
        //
        // The GetModuleSettings Method returns a hashtable of custom,
        // module-specific settings from the configuration file.  This method is
        // used by some user control modules (Xml, Image, etc) to access misc
        // settings.
        //
        // Other relevant sources:
        //    + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
        //	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
        //
        //*********************************************************************
        public Hashtable GetModuleSettings(int moduleId)
        {
            // Create a new Hashtable
            var _settingsHT = new Hashtable();

            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration)HttpContext.Current.Items["SiteSettings"];

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
                        _settingsHT[sRow.Name] = sRow.Setting_Text;
                    }
                }
            }

            return _settingsHT;
        }
    }
}