namespace PortalStarterKit.Components
{
    public interface IPortalPage
    {
        ISiteConfigurationService SiteConfiguration { get; set; }
        PortalSecurity PortalSecurity { get; set; }

        string PortalId { get; }
        string TabId { get; }
        string GetNavigateUrl(string portalId, string tabId);
    }
}