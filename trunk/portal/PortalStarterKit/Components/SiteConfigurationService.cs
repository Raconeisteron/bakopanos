using System.Collections.Generic;
using System.Linq;

namespace ASPNET.StarterKit.Portal
{
    public class SiteConfigurationService : ISiteConfigurationService
    {
        readonly List<PortalSettings> _deskotPortals = new List<PortalSettings>();

        public SiteConfigurationService(ISiteConfigurationRepository repository)
        {
            _deskotPortals = repository.Read();
        }

        public List<PortalSettings> GetPortals()
        {
            return _deskotPortals;
        }

        public List<TabSettings> GetTabs(string portalId)
        {
            return GetPortal(portalId).DesktopTabs;
        }


        public PortalSettings GetPortal (string portalId)
        {
            return GetPortals().Single<PortalSettings>(item => item.PortalId == portalId);
        }

        public TabSettings GetTab(string portalId, string tabId)
        {
            return GetPortal(portalId).DesktopTabs.Single<TabSettings>(item=>item.TabId==tabId);
        }
    }
}