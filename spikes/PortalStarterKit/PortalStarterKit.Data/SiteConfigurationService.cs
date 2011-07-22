using System.Collections.Generic;
using System.Linq;
using PortalStarterKit.Model;

namespace PortalStarterKit.Data
{
    public abstract class SiteConfigurationService : ISiteConfigurationService
    {
        #region ISiteConfigurationService Members

        public SiteConfiguration SiteConfiguration { get; protected set; }

        #endregion

        public void InitializeSiteConfiguration(IEnumerable<Tab> tabs)
        {
            foreach (Tab tab in tabs)
            {
                string desktopSrc =
                    SiteConfiguration.TabDefinitions.Single(item => item.TabDefId == tab.TabDefId).SourceFile;
                tab.NavigateUrl = desktopSrc + "?tabid=" + tab.TabId;

                InitializeSiteConfiguration(tab.Tabs);
            }
        }
    }
}