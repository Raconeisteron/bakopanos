namespace ASPNET.StarterKit.Portal
{
    public interface IConfigurationDb
    {
        object GetSiteSettings();
        void SaveSiteSettings();
    }
}