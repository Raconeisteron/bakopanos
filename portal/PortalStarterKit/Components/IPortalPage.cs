namespace PortalStarterKit.Components
{
    public interface IPortalPage
    {
        ISiteConfigurationService SiteConfiguration { get; set; }
        IPortalSecurity PortalSecurity { get; set; }

        string PortalId { get; }
        string TabId { get; }
        string NavigateUrl(string portalId, string tabId);
    }
}