namespace ASPNET.StarterKit.Portal
{
    public interface IPortalSecurity
    {
        bool HasEditPermissions(int moduleId);
    }
}