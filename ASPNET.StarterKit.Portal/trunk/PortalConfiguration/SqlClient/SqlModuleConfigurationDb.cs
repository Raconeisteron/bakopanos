using System;
using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlModuleConfigurationDb : IModuleConfigurationDb
    {
        #region IModuleConfigurationDb Members

        public void UpdateModuleOrder(int moduleId, int moduleOrder, string pane)
        {
            throw new NotImplementedException();
        }

        public int AddModule(int tabId, int moduleOrder, string paneName, string title, int moduleDefId, int cacheTime,
                             string editRoles, bool showMobile)
        {
            throw new NotImplementedException();
        }

        public int UpdateModule(int moduleId, int moduleOrder, string paneName, string title, int cacheTime,
                                string editRoles, bool showMobile)
        {
            throw new NotImplementedException();
        }

        public void DeleteModule(int moduleId)
        {
            throw new NotImplementedException();
        }

        public void UpdateModuleSetting(int moduleId, string key, string val)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, string> GetModuleSettings(int moduleId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}