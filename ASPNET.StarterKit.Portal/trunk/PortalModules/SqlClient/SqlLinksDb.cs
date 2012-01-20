using System.Data;
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
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;

            // Execute method
            IDataReader result = ExecuteReader("Portal_GetLinks", CommandType.StoredProcedure, parameterModuleId);

            // Return the datareader 
            return result;
        }

        public IDataReader GetSingleLink(int itemId)
        {

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemId;

            // Execute method
            IDataReader result = ExecuteReader("Portal_GetSingleLink", CommandType.StoredProcedure, parameterItemId);

            // Return the datareader 
            return result;
        }

        public void DeleteLink(int itemID)
        {

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemID;

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
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Direction = ParameterDirection.Output;

            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;

            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 100);
            parameterTitle.Value = title;

            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 100);
            parameterDescription.Value = description;

            var parameterUrl = new SqlParameter("@Url", SqlDbType.NVarChar, 100);
            parameterUrl.Value = url;

            var parameterMobileUrl = new SqlParameter("@MobileUrl", SqlDbType.NVarChar, 100);
            parameterMobileUrl.Value = mobileUrl;

            var parameterViewOrder = new SqlParameter("@ViewOrder", SqlDbType.Int, 4);
            parameterViewOrder.Value = viewOrder;

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
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemId;

            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100);
            parameterUserName.Value = userName;

            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 100);
            parameterTitle.Value = title;

            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 100);
            parameterDescription.Value = description;

            var parameterUrl = new SqlParameter("@Url", SqlDbType.NVarChar, 100);
            parameterUrl.Value = url;

            var parameterMobileUrl = new SqlParameter("@MobileUrl", SqlDbType.NVarChar, 100);
            parameterMobileUrl.Value = mobileUrl;

            var parameterViewOrder = new SqlParameter("@ViewOrder", SqlDbType.Int, 4);
            parameterViewOrder.Value = viewOrder;

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