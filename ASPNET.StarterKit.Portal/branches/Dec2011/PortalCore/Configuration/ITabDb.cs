using System;
using System.Collections.Generic;

namespace ASPNETPortal.Configuration
{
    public interface ITabDb
    {
        /// <summary>
        /// Returns a list of all tabs for the portal.
        /// </summary>        
        IEnumerable<TabItem> GetTabs();

        /// <summary>
        /// Returns a list of all mobile tabs for the portal.
        /// </summary>        
        IEnumerable<TabItem> GetMobileTabs();

        /// <summary>
        /// Returns a tab by tabid
        /// </summary>        
        TabItem GetSingleTabByTabId(int tabId);

        /// <summary>
        /// The AddTab method adds a new tab to the portal.  These settings are 
        /// stored in the Xml file PortalCfg.xml.
        /// </summary>        
        int AddTab(int portalId, String tabName, int tabOrder);

        /// <summary>
        /// The UpdateTab method updates the settings for the specified tab. 
        /// These settings are stored in the Xml file PortalCfg.xml.
        /// </summary>        
        void UpdateTab(int portalId, int tabId, String tabName, int tabOrder, String authorizedRoles,
                       String mobileTabName, bool showMobile);

        /// <summary>
        /// The UpdateTabOrder method changes the position of the tab with respect
        /// to other tabs in the portal.  These settings are stored in the Xml 
        /// file PortalCfg.xml.
        /// </summary>        
        void UpdateTabOrder(int tabId, int tabOrder);

        /// <summary>
        /// The DeleteTab method deletes the selected tab and its modules from 
        /// the settings which are stored in the Xml file PortalCfg.xml.  This 
        /// method also deletes any data from the database associated with all 
        /// modules within this tab.
        /// </summary>
        void DeleteTab(int tabId);
    }
}