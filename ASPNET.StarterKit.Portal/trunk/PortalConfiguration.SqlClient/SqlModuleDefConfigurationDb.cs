using System;
using System.Data;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlModuleDefConfigurationDb : IModuleDefConfigurationDb
    {
        #region IModuleDefConfigurationDb Members

        public DataRow[] GetModuleDefinitions(int portalId)
        {
            throw new NotImplementedException();
        }

        public int AddModuleDefinition(int portalId, string name, string desktopSrc, string mobileSrc)
        {
            throw new NotImplementedException();
        }

        public void DeleteModuleDefinition(int defId)
        {
            throw new NotImplementedException();
        }

        public void UpdateModuleDefinition(int defId, string name, string desktopSrc, string mobileSrc)
        {
            throw new NotImplementedException();
        }

        public ModuleDefinitionItem GetSingleModuleDefinition(int defId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}