using System;
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
    public class XmlTabConfigurationDb : ITabConfigurationDb
    {
        private readonly IConfigurationDb _configurationDb;

        public XmlTabConfigurationDb(IConfigurationDb configurationDb)
        {
            _configurationDb = configurationDb;
        }

        #region ITabConfigurationDb Members

        public int AddTab(int portalId, String tabName, int tabOrder)
        {
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration) HttpContext.Current.Items["SiteSettings"];

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
            _configurationDb.SaveSiteSettings();

            // Return the new TabID
            return newRow.TabId;
        }

        public void UpdateTab(int portalId, int tabId, String tabName, int tabOrder, String authorizedRoles,
                              String mobileTabName, bool showMobile)
        {
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration) HttpContext.Current.Items["SiteSettings"];

            // Find the appropriate tab in the Tab table and set the properties
            SiteConfiguration.TabRow tabRow = siteSettings.Tab.FindByTabId(tabId);

            tabRow.TabName = tabName;
            tabRow.TabOrder = tabOrder;
            tabRow.AccessRoles = authorizedRoles;
            tabRow.MobileTabName = mobileTabName;
            tabRow.ShowMobile = showMobile;

            // Save the changes 
            _configurationDb.SaveSiteSettings();
        }

        public void UpdateTabOrder(int tabId, int tabOrder)
        {
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration) HttpContext.Current.Items["SiteSettings"];

            // Find the appropriate tab in the Tab table and set the property
            SiteConfiguration.TabRow tabRow = siteSettings.Tab.FindByTabId(tabId);

            tabRow.TabOrder = tabOrder;

            // Save the changes 
            _configurationDb.SaveSiteSettings();
        }

        /// <summary>
        /// deletes the selected tab and its modules from 
        /// the settings which are stored in the Xml file PortalCfg.xml.  This 
        /// method also deletes any data from the database associated with all 
        /// modules within this tab.
        /// </summary>
        /// <param name="tabId"></param>
        public void DeleteTab(int tabId)
        {
            //
            // Delete the Tab in the XML file
            //

            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration) HttpContext.Current.Items["SiteSettings"];

            // Find the appropriate tab in the Tab table
            SiteConfiguration.TabDataTable tabTable = siteSettings.Tab;
            SiteConfiguration.TabRow tabRow = siteSettings.Tab.FindByTabId(tabId);

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

            foreach (SiteConfiguration.ModuleRow moduleRow in tabRow.GetModuleRows())
            {
                myCommand.Parameters.Clear();
                parameterModuleID.Value = moduleRow.ModuleId;
                myCommand.Parameters.Add(parameterModuleID);

                // Open the database connection and execute the command
                myCommand.ExecuteNonQuery();
            }

            // Close the connection
            myConnection.Close();

            // Finish removing the Tab row from the Xml file
            tabTable.RemoveTabRow(tabRow);

            // Save the changes 
            _configurationDb.SaveSiteSettings();
        }

        #endregion
    }
}