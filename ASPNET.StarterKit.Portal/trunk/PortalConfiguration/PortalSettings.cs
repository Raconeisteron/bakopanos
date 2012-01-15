using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// encapsulates all of the settings for the Portal, as well
    /// as the configuration settings required to execute the current tab
    /// view within the portal.
    /// </summary>
    public class PortalSettings
    {
        private readonly IPortalConfigurationDb _portalConfigurationDb;
        private readonly ITabConfigurationDb _tabConfigurationDb;

        /// <summary>
        /// encapsulates all of the logic
        /// necessary to obtain configuration settings necessary to render
        /// a Portal Tab view for a given request.
        ///
        /// These Portal Settings are stored within PortalCFG.xml, and are
        /// fetched below by calling config.GetSiteSettings().
        /// The method config.GetSiteSettings() fills the SiteConfiguration
        /// class, derived from a DataSet, which PortalSettings accesses.
        /// </summary>
        public PortalSettings(int tabIndex, int tabId, IPortalConfigurationDb portalConfigurationDb,
                              ITabConfigurationDb tabConfigurationDb)
        {
            _portalConfigurationDb = portalConfigurationDb;
            _tabConfigurationDb = tabConfigurationDb;

            // Get the configuration data

            DesktopTabs = _tabConfigurationDb.FindDesktopTabs();
            MobileTabs = _tabConfigurationDb.FindMobileTabs();

            // If the PortalSettings.ActiveTab property is set to 0, change it to  
            // the TabID of the first tab in the DesktopTabs collection
            if (tabId == 0)
                tabId = (DesktopTabs[0]).TabId;

            // Read the Module Information for the current (Active) tab
            ActiveTab = _tabConfigurationDb.FindTab(tabId);

            ActiveTab.TabIndex = tabIndex;

            foreach (ModuleSettings moduleSettings in _tabConfigurationDb.FindModules(tabId))
            {
                ActiveTab.Modules.Add(moduleSettings);
            }

            // Sort the modules in order of ModuleOrder
            ActiveTab.Modules.Sort();

            Portal = _portalConfigurationDb.FindPortal();
        }

        public TabSettings ActiveTab { get; set; }
        public List<TabStripDetails> DesktopTabs { get; set; }
        public List<TabStripDetails> MobileTabs { get; set; }
        public GlobalItem Portal { get; set; }
    }
}