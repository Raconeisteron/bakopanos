namespace ASPNET.StarterKit.Portal
{
    public interface ISiteConfigurationService
    {
        PortalSettings ActivePortal (string portalId);
        TabSettings ActiveTab(string tabId);
    }
}