using ASPNET.StarterKit.Portal.XmlFile;

namespace ASPNET.StarterKit.Portal
{
    public interface IConfigurationDb
    {
        SiteConfiguration GetSiteSettings();
        void SaveSiteSettings();
    }
}