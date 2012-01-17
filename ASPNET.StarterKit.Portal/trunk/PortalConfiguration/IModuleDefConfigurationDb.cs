using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public interface IModuleDefConfigurationDb
    {
        DataRow[] GetModuleDefinitions(int portalId);
        int AddModuleDefinition(int portalId, string name, string desktopSrc, string mobileSrc);
        void DeleteModuleDefinition(int defId);
        void UpdateModuleDefinition(int defId, string name, string desktopSrc, string mobileSrc);
        ModuleDefinitionItem GetSingleModuleDefinition(int defId);
    }
}