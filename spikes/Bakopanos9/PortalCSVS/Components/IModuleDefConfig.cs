using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public interface IModuleDefConfig
    {
        DataRow[] GetModuleDefinitions(int portalId);
        int AddModuleDefinition(int portalId, String name, String desktopSrc, String mobileSrc);
        void DeleteModuleDefinition(int defId);
        void UpdateModuleDefinition(int defId, String name, String desktopSrc, String mobileSrc);
        SiteConfiguration.ModuleDefinitionRow GetSingleModuleDefinition(int defId);
    }
}