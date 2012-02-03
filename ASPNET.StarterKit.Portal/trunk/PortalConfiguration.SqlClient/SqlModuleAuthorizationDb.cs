using System;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlModuleAuthorizationDb : IModuleAuthorizationDb
    {
        #region IModuleAuthorizationDb Members

        public ModuleAuthorizationItem FindModuleRolesByModuleId(int moduleId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}