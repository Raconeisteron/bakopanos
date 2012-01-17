namespace ASPNET.StarterKit.Portal
{
    public interface IPortalConfigurationDb
    {
        GlobalItem FindPortal();

        void UpdatePortalInfo(int portalId, string portalName, bool alwaysShow);
    }
}