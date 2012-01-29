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
    public class XmlModuleDefConfigurationDb : IModuleDefConfigurationDb
    {
        private readonly IConfigurationDb _configurationDb;
        private readonly IPortalCacheUtility _cacheUtility;

        public XmlModuleDefConfigurationDb(IConfigurationDb configurationDb,IPortalCacheUtility cacheUtility)
        {
            _configurationDb = configurationDb;
            _cacheUtility = cacheUtility;
        }

        //*********************************************************************
        //
        // GetModuleDefinitions() Method <a name="GetModuleDefinitions"></a>
        //
        // The GetModuleDefinitions method returns a list of all module type 
        // definitions for the portal.
        //
        // Other relevant sources:
        //    + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
        //	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
        //
        //*********************************************************************

        #region IModuleDefConfigurationDb Members

        public DataRow[] GetModuleDefinitions(int portalId)
        {
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration) _cacheUtility.SiteSettings;

            // Find the appropriate Module in the Module table
            return siteSettings.ModuleDefinition.Select();
        }

        //*********************************************************************
        //
        // AddModuleDefinition() Method <a name="AddModuleDefinition"></a>
        //
        // The AddModuleDefinition add the definition for a new module type
        // to the portal.
        //
        // Other relevant sources:
        //    + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
        //	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
        //
        //*********************************************************************
        public int AddModuleDefinition(int portalId, string name, string desktopSrc, string mobileSrc)
        {
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration)_cacheUtility.SiteSettings;

            // Create new ModuleDefinitionRow
            SiteConfiguration.ModuleDefinitionRow newModuleDef = siteSettings.ModuleDefinition.NewModuleDefinitionRow();

            // Set the parameter values
            newModuleDef.FriendlyName = name;
            newModuleDef.DesktopSourceFile = desktopSrc;
            newModuleDef.MobileSourceFile = mobileSrc;

            // Add the new ModuleDefinitionRow to the ModuleDefinition table
            siteSettings.ModuleDefinition.AddModuleDefinitionRow(newModuleDef);

            // Save the changes
            _configurationDb.SaveSiteSettings();

            // Return the new ModuleDefID
            return newModuleDef.ModuleDefId;
        }

        //*********************************************************************
        //
        // DeleteModuleDefinition() Method <a name="DeleteModuleDefinition"></a>
        //
        // The DeleteModuleDefinition method deletes the specified module type 
        // definition from the portal.  Each module which is related to the
        // ModuleDefinition is deleted from each tab in the configuration
        // file, and all data relating to each module is deleted from the
        // database.
        //
        // Other relevant sources:
        //    + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
        //	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
        //    + <a href="DeleteModule.htm" style="color:green">DeleteModule Stored Procedure</a>
        //
        //*********************************************************************
        public void DeleteModuleDefinition(int defId)
        {
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration) _cacheUtility.SiteSettings;

            //
            // Delete information in the Database relating to each Module being deleted
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

            foreach (SiteConfiguration.ModuleRow moduleRow in siteSettings.Module.Select())
            {
                if (moduleRow.ModuleDefId == defId)
                {
                    myCommand.Parameters.Clear();
                    parameterModuleID.Value = moduleRow.ModuleId;
                    myCommand.Parameters.Add(parameterModuleID);

                    // Delete the xml module associated with the ModuleDef
                    // in the configuration file
                    siteSettings.Module.RemoveModuleRow(moduleRow);

                    // Open the database connection and execute the command
                    myCommand.ExecuteNonQuery();
                }
            }

            myConnection.Close();

            // Finish removing Module Definition
            siteSettings.ModuleDefinition.RemoveModuleDefinitionRow(
                siteSettings.ModuleDefinition.FindByModuleDefId(defId));

            // Save the changes 
            _configurationDb.SaveSiteSettings();
        }

        //*********************************************************************
        //
        // UpdateModuleDefinition() Method <a name="UpdateModuleDefinition"></a>
        //
        // The UpdateModuleDefinition method updates the settings for the 
        // specified module type definition.
        //
        // Other relevant sources:
        //    + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
        //	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
        //
        //*********************************************************************
        public void UpdateModuleDefinition(int defId, string name, string desktopSrc, string mobileSrc)
        {
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration)_cacheUtility.SiteSettings;

            // Find the appropriate Module in the Module table and update the properties
            SiteConfiguration.ModuleDefinitionRow modDefRow = siteSettings.ModuleDefinition.FindByModuleDefId(defId);

            modDefRow.FriendlyName = name;
            modDefRow.DesktopSourceFile = desktopSrc;
            modDefRow.MobileSourceFile = mobileSrc;

            // Save the changes 
            _configurationDb.SaveSiteSettings();
        }

        //*********************************************************************
        //
        // GetSingleModuleDefinition Method
        //
        // The GetSingleModuleDefinition method returns a ModuleDefinitionRow
        // object containing details about a specific module definition in the
        // configuration file.
        //
        // Other relevant sources:
        //    + <a href="#SaveSiteSettings" style="color:green">SaveSiteSettings() method</a>
        //	  + <a href="PortalCfg.xml" style="color:green">PortalCfg.xml</a>
        //
        //*********************************************************************
        public ModuleDefinitionItem GetSingleModuleDefinition(int defId)
        {
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration)_cacheUtility.SiteSettings;

            // Find the appropriate Module in the Module table
            return ToModuleDefinitionItem(siteSettings.ModuleDefinition.FindByModuleDefId(defId));
        }

        #endregion

        private static ModuleDefinitionItem ToModuleDefinitionItem(SiteConfiguration.ModuleDefinitionRow row)
        {
            var item = new ModuleDefinitionItem();
            item.DesktopSourceFile = row.DesktopSourceFile;
            item.FriendlyName = row.FriendlyName;
            item.MobileSourceFile = row.MobileSourceFile;
            return item;
        }
    }
}