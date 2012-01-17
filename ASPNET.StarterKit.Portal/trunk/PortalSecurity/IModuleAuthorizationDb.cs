namespace ASPNET.StarterKit.Portal
{
    public interface IModuleAuthorizationDb
    {
        ModuleAuthorizationItem FindModuleRolesByModuleId(int moduleId);
    }
}