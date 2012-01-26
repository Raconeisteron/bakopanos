using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlEventsDb : Db, IEventsDb
    {
        public SqlEventsDb(string connectionString)
            : base(connectionString, "System.Data.SqlClient")
        {
        }

        #region IEventsDb Members

        public List<PortalEvent> GetEvents(int moduleId)
        {
            // Add Parameters to SPROC
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);

            // Execute method
            IDataReader reader = ExecuteReader("Portal_GetEvents", CommandType.StoredProcedure, parameterModuleId);

            var eventList = new List<PortalEvent>();

            while (reader.Read())
                eventList.Add(reader.ToPortalEvent());

            // Return list
            return eventList;
        }

        public PortalEvent GetSingleEvent(int itemId)
        {
            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

            // Execute method
            IDataReader reader = ExecuteReader("Portal_GetSingleEvent", CommandType.StoredProcedure, parameterItemId);

            //Read once, since we have only one result (itemId is Unique)
            reader.Read();
            PortalEvent singleEvent = reader.ToPortalEvent();

            // Return the item
            return singleEvent;
        }

        public void DeleteEvent(int itemId)
        {
            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

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
            parameterItemId.Size = 4;
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);
            DbParameter parameterUserName = CreateParameter("@UserName", userName);
            DbParameter parameterTitle = CreateParameter("@Title", title);
            DbParameter parameterWhereWhen = CreateParameter("@WhereWhen", wherewhen);
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
            return Convert.ToInt32(parameterItemId.Value);
        }

        public void UpdateEvent(int itemId, string userName, string title, DateTime expireDate,
                                string description, string wherewhen)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }


            // Add Parameters to SPROC
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);
            DbParameter parameterUserName = CreateParameter("@UserName", userName);
            DbParameter parameterTitle = CreateParameter("@Title", title);
            DbParameter parameterWhereWhen = CreateParameter("@WhereWhen", wherewhen);
            DbParameter parameterExpireDate = CreateParameter("@ExpireDate", expireDate);
            DbParameter parameterDescription = CreateParameter("@Description", description);

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