using System;
using System.Collections;

namespace ASPNET.StarterKit.Portal
{
    public interface IModuleConfigurationDb
    {
        ModuleAuthorization FindModuleRolesByModuleId(int moduleId);
        void UpdateModuleOrder(int moduleId, int moduleOrder, String pane);

        int AddModule(int tabId, int moduleOrder, String paneName, String title, int moduleDefId, int cacheTime,
                                      String editRoles, bool showMobile);

        int UpdateModule(int moduleId, int moduleOrder, String paneName, String title, int cacheTime,
                                         String editRoles, bool showMobile);

        void DeleteModule(int moduleId);
        void UpdateModuleSetting(int moduleId, String key, String val);
        Hashtable GetModuleSettings(int moduleId);
    }
}