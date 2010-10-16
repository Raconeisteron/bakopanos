namespace ASPNET.StarterKit.Portal
{
    public interface IPortalPage
    {
        ISiteConfigurationService SiteConfiguration { get; set; }
        string PortalId { get; }
        string TabId { get; }
        string GetNavigateUrl(string portalId, string tabId);
    }
}