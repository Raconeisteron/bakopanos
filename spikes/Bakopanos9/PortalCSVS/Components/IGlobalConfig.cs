namespace ASPNET.StarterKit.Portal
{
    public interface IGlobalConfig
    {
        SiteConfiguration GetSiteSettings();
        void SaveSiteSettings();
    }
}