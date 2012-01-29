namespace ASPNET.StarterKit.Portal
{
    public interface IPortalCacheUtility
    {
        void InsertSiteSettings(object siteSettings, string configFile);
        object SiteSettings
        { 
            get;
        }
    }
}