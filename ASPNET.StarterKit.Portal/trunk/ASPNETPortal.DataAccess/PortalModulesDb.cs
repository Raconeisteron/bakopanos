namespace ASPNET.StarterKit.Portal
{
    internal class PortalModulesDb : IPortalModulesDb
    {
        private readonly IDbHelper _db;

        public PortalModulesDb(IDbHelper db)
        {
            _db = db;
        }

        public void DeletePortalModule(int moduleId)
        {
            var parameterModuleId = _db.CreateParameter("@ModuleID", moduleId);
            _db.ExecuteNonQuery("Portal_DeleteModule", parameterModuleId);
        }
    }
}