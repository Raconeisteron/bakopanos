using System;
using System.Collections.Generic;

namespace ASPNETPortal
{
    /// <summary>
    /// This class encapsulates all of the settings for the Portal, as well
    /// as the configuration settings required to execute the current tab
    /// view within the portal.
    /// </summary>
    public class PortalSettings
    {
        public TabSettings ActiveTab { get; set; }
        public bool AlwaysShowEditButton { get; set; }
        public List<TabStripDetails> DesktopTabs { get; set; }
        public List<TabStripDetails> MobileTabs { get; set; }
        public int PortalId { get; set; }
        public String PortalName { get; set; }

        /// <summary>
        /// The PortalSettings Constructor encapsulates all of the logic
        /// necessary to obtain configuration settings necessary to render
        /// a Portal Tab view for a given request.
        /// These Portal Settings are stored within PortalCFG.xml, and are
        /// fetched below by calling config.GetSiteSettings().
        /// The method config.GetSiteSettings() fills the SiteConfiguration
        /// class, derived from a DataSet, which PortalSettings accesses.
        /// </summary>
        public PortalSettings(IGlobalDb globalDb, ITabDb tabDb, IModuleDb moduleDb, IModuleDefinitionDb configurationDb,
                              int tabIndex, int tabId)
        {

              ActiveTab = new TabSettings();
          DesktopTabs = new List<TabStripDetails>();
          MobileTabs = new List<TabStripDetails>();
       

            // Read the Desktop Tab Information, and sort by Tab Order
            foreach (TabItem tRow in tabDb.GetTabs())
            {
                var tabDetails = new TabStripDetails();

                tabDetails.TabId = tRow.TabId;
                tabDetails.TabName = tRow.TabName;
                tabDetails.TabOrder = tRow.TabOrder;
                tabDetails.AuthorizedRoles = tRow.AccessRoles;

                DesktopTabs.Add(tabDetails);
            }

            // If the PortalSettings.ActiveTab property is set to 0, change it to  
            // the TabID of the first tab in the DesktopTabs collection
            if (ActiveTab.TabId == 0)
                ActiveTab.TabId = (DesktopTabs[0]).TabId;


            // Read the Mobile Tab Information, and sort by Tab Order
            foreach (TabItem mRow in tabDb.GetMobileTabs())
            {
                var tabDetails = new TabStripDetails();

                tabDetails.TabId = mRow.TabId;
                tabDetails.TabName = mRow.MobileTabName;
                tabDetails.AuthorizedRoles = mRow.AccessRoles;

                MobileTabs.Add(tabDetails);
            }

            // Read the Module Information for the current (Active) tab
            TabItem activeTab = tabDb.GetSingleTabByTabId(tabId);

            // Get Modules for this Tab based on the Data Relation
            foreach (ModuleItem moduleRow in moduleDb.GetModulesByTabId(tabId))
            {
                var moduleSettings = new ModuleSettings();

                moduleSettings.ModuleTitle = moduleRow.ModuleTitle;
                moduleSettings.ModuleId = moduleRow.ModuleId;
                moduleSettings.ModuleDefId = moduleRow.ModuleDefId;
                moduleSettings.ModuleOrder = moduleRow.ModuleOrder;
                moduleSettings.TabId = tabId;
                moduleSettings.PaneName = moduleRow.PaneName;
                moduleSettings.AuthorizedEditRoles = moduleRow.EditRoles;
                moduleSettings.CacheTime = moduleRow.CacheTimeout;
                moduleSettings.ShowMobile = moduleRow.ShowMobile;

                // ModuleDefinition data
                ModuleDefinitionItem modDefRow =
                    configurationDb.GetModuleDefinitionByModuleDefId(moduleSettings.ModuleDefId);

                moduleSettings.DesktopSrc = modDefRow.DesktopSourceFile;
                moduleSettings.MobileSrc = modDefRow.MobileSourceFile;

                ActiveTab.Modules.Add(moduleSettings);
            }

            // Sort the modules in order of ModuleOrder
            ActiveTab.Modules.Sort();

            // Get the first row in the Global table
            GlobalItem globalSettings = globalDb.GetGlobalByPortalId(0);

            // Read Portal global settings 
            PortalId = globalSettings.PortalId;
            PortalName = globalSettings.PortalName;
            AlwaysShowEditButton = globalSettings.AlwaysShowEditButton;
            ActiveTab.TabIndex = tabIndex;
            ActiveTab.TabId = tabId;
            ActiveTab.TabOrder = activeTab.TabOrder;
            ActiveTab.MobileTabName = activeTab.MobileTabName;
            ActiveTab.AuthorizedRoles = activeTab.AccessRoles;
            ActiveTab.TabName = activeTab.TabName;
            ActiveTab.ShowMobile = activeTab.ShowMobile;
        }
    }
}