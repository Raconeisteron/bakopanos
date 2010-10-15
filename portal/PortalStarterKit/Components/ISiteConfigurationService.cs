using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal
{
    public interface ISiteConfigurationService
    {
        List<PortalSettings> DesktopPortals { get; }

        PortalSettings DefaultPortal{ get;}
        TabSettings DefaultTab { get; }
        PortalSettings ActivePortal (string portalId);
        TabSettings ActiveTab(string portalId, string tabId);
    }
}