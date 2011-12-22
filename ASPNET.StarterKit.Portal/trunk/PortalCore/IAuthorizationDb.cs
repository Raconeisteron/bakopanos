namespace ASPNETPortal
{
    public interface IAuthorizationDb
    {
        string GetEditRolesByModuleId(int moduleId);

        string GetAccessRolesByModuleId(int moduleId);
    }
}