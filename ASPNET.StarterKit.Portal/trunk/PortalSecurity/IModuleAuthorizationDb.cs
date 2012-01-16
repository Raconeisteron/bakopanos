namespace ASPNET.StarterKit.Portal
{
    public interface IModuleAuthorizationDb
    {
        ModuleAuthorization FindModuleRolesByModuleId(int moduleId);
    }
}