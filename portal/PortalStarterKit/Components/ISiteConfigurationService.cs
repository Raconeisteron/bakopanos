namespace ASPNET.StarterKit.Portal
{
    public interface ISiteConfigurationService
    {
        PortalSettings ActivePortal (int portalId);
        TabSettings ActiveTab(int tabId);
    }
}