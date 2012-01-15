using System;
using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal
{
    public interface ITabConfigurationDb
    {
        List<TabStripDetails> FindDesktopTabs();
        List<TabStripDetails> FindMobileTabs();
        List<ModuleSettings> FindModules(int tabId);
        TabSettings FindTab(int tabId);

        int AddTab(int portalId, String tabName, int tabOrder);

        void UpdateTab(int portalId, int tabId, String tabName, int tabOrder, String authorizedRoles,
                       String mobileTabName, bool showMobile);

        void UpdateTabOrder(int tabId, int tabOrder);

        /// <summary>
        /// deletes the selected tab and its modules from 
        /// the settings which are stored in the Xml file PortalCfg.xml.  This 
        /// method also deletes any data from the database associated with all 
        /// modules within this tab.
        /// </summary>
        /// <param name="tabId"></param>
        void DeleteTab(int tabId);
    }
}