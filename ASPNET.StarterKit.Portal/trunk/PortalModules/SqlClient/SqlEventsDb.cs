using System;
using System.Data;
using System.Data.Common;
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
            DbParameter parameterModuleId = CreateParameter("@ModuleID",moduleId);

            // Execute method
            IDataReader result = ExecuteReader("Portal_GetEvents", CommandType.StoredProcedure, parameterModuleId);

            // Return the datareader 
            return result;
        }

        public IDataReader GetSingleEvent(int itemId)
        {

            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

            // Execute method
            IDataReader result = ExecuteReader("Portal_GetSingleEvent", CommandType.StoredProcedure, parameterItemId);

            // Return the datareader 
            return result;
        }

        public void DeleteEvent(int itemID)
        {

            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID",itemID);

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
            DbParameter parameterItemId = CreateParameter("@ItemID");
            parameterItemId.Direction = ParameterDirection.Output;
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);
            DbParameter parameterUserName = CreateParameter("@UserName", userName);
            DbParameter parameterTitle = CreateParameter("@Title", title);
            DbParameter parameterWhereWhen = CreateParameter("@WhereWhen",  wherewhen);
            DbParameter parameterExpireDate = CreateParameter("@ExpireDate", expireDate);
            DbParameter parameterDescription = CreateParameter("@Description", description);

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
            DbParameter parameterItemId = CreateParameter("@ItemID",itemId);
            DbParameter parameterUserName = CreateParameter("@UserName", userName);
            DbParameter parameterTitle = CreateParameter("@Title",  title);
            DbParameter parameterWhereWhen = CreateParameter("@WhereWhen", wherewhen);
            DbParameter parameterExpireDate = CreateParameter("@ExpireDate",expireDate);
            DbParameter parameterDescription = CreateParameter("@Description",description);

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