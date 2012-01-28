using System.Collections.ObjectModel;

namespace ASPNET.StarterKit.Portal
{
    public interface ITabConfigurationDb
    {
        Collection<TabStripDetails> FindDesktopTabs();
        Collection<TabStripDetails> FindMobileTabs();
        Collection<ModuleSettings> FindModules(int tabId);
        TabSettings FindTab(int tabId);

        int AddTab(int portalId, string tabName, int tabOrder);

        void UpdateTab(int portalId, int tabId, string tabName, int tabOrder, string authorizedRoles,
                       string mobileTabName, bool showMobile);

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