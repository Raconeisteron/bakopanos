namespace ASPNET.StarterKit.Portal
{
    public interface IAuthorizationDb
    {
        string GetEditRolesByModuleId(int moduleId);

        string GetAccessRolesByModuleId(int moduleId);
    }
}