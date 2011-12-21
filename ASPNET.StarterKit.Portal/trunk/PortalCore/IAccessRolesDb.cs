namespace ASPNET.StarterKit.Portal
{
    public interface IAccessRolesDb
    {
        
        ModuleItem GetModuleByModuleId(int moduleId);

        TabItem GetSingleTabByTabId(int tabId);
    }
}