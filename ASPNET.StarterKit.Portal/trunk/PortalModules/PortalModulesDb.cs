using System.Data.Common;
using Framework.Data;

namespace ASPNETPortal
{
    internal class PortalModulesDb : IPortalModulesDb
    {
        private readonly IDbHelper _db;

        public PortalModulesDb(IDbHelper db)
        {
            _db = db;
        }

        #region IPortalModulesDb Members

        public void DeletePortalModule(int moduleId)
        {
            DbParameter parameterModuleId = _db.CreateParameter("@ModuleID", moduleId);
            _db.ExecuteNonQuery("Portal_DeleteModule", parameterModuleId);
        }

        #endregion
    }
}