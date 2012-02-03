namespace ASPNET.StarterKit.Portal
{
    public interface IPortalCacheUtility
    {
        object SiteSettings { get; }
        void InsertSiteSettings(object siteSettings, string configFile);
    }
}