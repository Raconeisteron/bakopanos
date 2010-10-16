using System.Collections.Generic;
using System.Linq;

namespace PortalStarterKit.Components
{
    public class SiteConfigurationService : ISiteConfigurationService
    {
        private readonly List<PortalSettings> _deskotPortals = new List<PortalSettings>();

        public SiteConfigurationService(ISiteConfigurationRepository repository)
        {
            _deskotPortals = repository.Read();
        }

        #region ISiteConfigurationService Members

        public List<PortalSettings> GetPortals()
        {
            return _deskotPortals;
        }

        public List<TabSettings> GetTabs(string portalId)
        {
            return GetPortal(portalId).DesktopTabs;
        }


        public PortalSettings GetPortal(string portalId)
        {
            return GetPortals().Single(item => item.PortalId == portalId);
        }

        public TabSettings GetTab(string portalId, string tabId)
        {
            return GetPortal(portalId).DesktopTabs.Single(item => item.TabId == tabId);
        }

        #endregion
    }
}