
namespace Portal.Modules.DAL.SqlServer
{
    internal class PortalDb : SqlDbHelper, IPortalDb
    {
        #region IPortalDb Members

        public void DeleteModule(params int[] moduleIds)
        {
            foreach (int moduleId in moduleIds)
            {
                ExecuteNonQuery("Portal_DeleteModule", InputModuleId(moduleId));
            }
        }

        #endregion
    }
}