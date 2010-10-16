using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal
{
    public interface ISiteConfigurationService
    {
        List<PortalSettings> GetPortals();
        List<TabSettings> GetTabs(string portalId);

        PortalSettings GetPortal (string portalId);
        TabSettings GetTab(string portalId, string tabId);
    }
}