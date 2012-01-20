using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlEventsDb : Db, IEventsDb
    {
        private readonly string _connectionString;

        public SqlEventsDb(string connectionString)
            : base(connectionString, "System.Data.SqlClient")
        {
            _connectionString = connectionString;
        }

        #region IEventsDb Members

        public IDataReader GetEvents(int moduleId)
        {

            // Add Parameters to SPROC
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4);
            parameterModuleId.Value = moduleId;

            // Execute method
            IDataReader result = ExecuteReader("Portal_GetEvents", CommandType.StoredProcedure, parameterModuleId);

            // Return the datareader 
            return result;
        }

        public IDataReader GetSingleEvent(int itemId)
        {

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemId;

            // Execute method
            IDataReader result = ExecuteReader("Portal_GetSingleEvent", CommandType.StoredProcedure, parameterItemId);

            // Return the datareader 
            return result;
        }

        public void DeleteEvent(int itemID)
        {

            // Add Parameters to SPROC
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4);
            parameterItemId.Value = itemID;

            //Execute method
            ExecuteNonQuery("Portal_DeleteEvent", CommandType.StoredProcedure, parameterItemId);
        }

        public int AddEvent(int moduleId, string userName, string title, DateTime expireDate,
                            string description, string wherewhen)
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

            var parameterWhereWhen = new SqlParameter("@WhereWhen", SqlDbType.NVarChar, 100);
            parameterWhereWhen.Value = wherewhen;

            var parameterExpireDate = new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8);
            parameterExpireDate.Value = expireDate;

            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 2000);
            parameterDescription.Value = description;

            //Execute Method
            ExecuteNonQuery("Portal_AddEvent", CommandType.StoredProcedure,
                parameterItemId,
                parameterModuleId,
                parameterUserName,
                parameterTitle,
                parameterWhereWhen,
                parameterExpireDate,
                parameterDescription);

            // Return the new Event ItemID
            return (int)parameterItemId.Value;
        }

        public void UpdateEvent(int itemId, string userName, string title, DateTime expireDate,
                                string description, string wherewhen)
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

            var parameterWhereWhen = new SqlParameter("@WhereWhen", SqlDbType.NVarChar, 100);
            parameterWhereWhen.Value = wherewhen;

            var parameterExpireDate = new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8);
            parameterExpireDate.Value = expireDate;

            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 2000);
            parameterDescription.Value = description;

            //Execute Method
            ExecuteNonQuery("Portal_UpdateEvent", CommandType.StoredProcedure,
                parameterItemId,
                parameterUserName,
                parameterTitle,
                parameterWhereWhen,
                parameterExpireDate,
                parameterDescription);
        }

        #endregion
    }
}