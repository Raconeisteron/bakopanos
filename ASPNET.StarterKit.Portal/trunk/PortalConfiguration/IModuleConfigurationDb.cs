using System.Collections;

namespace ASPNET.StarterKit.Portal
{
    public interface IModuleConfigurationDb
    {
        void UpdateModuleOrder(int moduleId, int moduleOrder, string pane);

        int AddModule(int tabId, int moduleOrder, string paneName, string title, int moduleDefId, int cacheTime,
                      string editRoles, bool showMobile);

        int UpdateModule(int moduleId, int moduleOrder, string paneName, string title, int cacheTime,
                         string editRoles, bool showMobile);

        void DeleteModule(int moduleId);
        void UpdateModuleSetting(int moduleId, string key, string val);
        Hashtable GetModuleSettings(int moduleId);
    }
}