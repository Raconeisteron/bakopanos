using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlLinksDb : Db, ILinksDb
    {
        private readonly string _connectionString;

        public SqlLinksDb(string connectionString)
            : base(connectionString, "System.Data.SqlClient")
        {
            _connectionString = connectionString;
        }

        #region ILinksDb Members

        public IDataReader GetLinks(int moduleId)
        {

            // Add Parameters to SPROC
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);

            // Execute method
            IDataReader result = ExecuteReader("Portal_GetLinks", CommandType.StoredProcedure, parameterModuleId);

            // Return the datareader 
            return result;
        }

        public IDataReader GetSingleLink(int itemId)
        {

            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

            // Execute method
            IDataReader result = ExecuteReader("Portal_GetSingleLink", CommandType.StoredProcedure, parameterItemId);

            // Return the datareader 
            return result;
        }

        public void DeleteLink(int itemId)
        {

            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

            //Execute Method
            ExecuteNonQuery("Portal_DeleteLink", CommandType.StoredProcedure, parameterItemId);
        }

        public int AddLink(int moduleId, string userName, string title, string url, string mobileUrl,
                           int viewOrder, string description)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID");
            parameterItemId.Direction = ParameterDirection.Output;
            DbParameter parameterModuleId = CreateParameter("@ModuleID",  moduleId);
            DbParameter parameterUserName = CreateParameter("@UserName", userName);
            DbParameter parameterTitle = CreateParameter("@Title", title);
            DbParameter parameterDescription = CreateParameter("@Description",  description);
            DbParameter parameterUrl = CreateParameter("@Url", url);
            DbParameter parameterMobileUrl = CreateParameter("@MobileUrl",  mobileUrl);
            DbParameter parameterViewOrder = CreateParameter("@ViewOrder", viewOrder);

            //Execute Method
            ExecuteNonQuery("Portal_AddLink", CommandType.StoredProcedure,
                parameterItemId,
                parameterModuleId,
                parameterUserName,
                parameterTitle,
                parameterDescription,
                parameterUrl,
                parameterMobileUrl,
                parameterViewOrder);


            return (int)parameterItemId.Value;
        }

        public void UpdateLink(int itemId, string userName, string title, string url, string mobileUrl,
                               int viewOrder, string description)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);
            DbParameter parameterUserName = CreateParameter("@UserName", userName);
            DbParameter parameterTitle = CreateParameter("@Title", title);
            DbParameter parameterDescription = CreateParameter("@Description", description);
            DbParameter parameterUrl = CreateParameter("@Url", url);
            DbParameter parameterMobileUrl = CreateParameter("@MobileUrl", mobileUrl);
            DbParameter parameterViewOrder = CreateParameter("@ViewOrder", viewOrder);

            //Execute method
            ExecuteNonQuery("Portal_UpdateLink", CommandType.StoredProcedure,
                parameterItemId,
                parameterUserName,
                parameterTitle,
                parameterDescription,
                parameterUrl,
                parameterMobileUrl,
                parameterViewOrder);
        }

        #endregion
    }
}