using System;
using System.Data;
using System.Data.Common;
using Framework.Data;

namespace ASPNETPortal
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// events within the Portal database.
    /// </summary>
    internal class EventsDb : IEventsDb
    {
        private readonly IDbHelper _db;

        public EventsDb(IDbHelper db)
        {
            _db = db;
        }

        #region IEventsDb Members

        /// <returns>
        /// The GetEvents method returns a DataSet containing all of the
        /// events for a specific portal module from the events
        /// database.
        /// </returns>
        public DataTable GetEvents(int moduleId)
        {
            DbParameter parameterModuleId = _db.CreateParameter("@ModuleID", moduleId);

            return _db.GetDataTable("Portal_GetEvents", parameterModuleId);
        }

        /// <returns>
        /// The GetSingleEvent method returns a SqlDataReader containing details
        /// about a specific event from the events database.
        /// </returns>
        public DataRow GetSingleEvent(int itemId)
        {
            DbParameter parameterItemId = _db.CreateParameter("@ItemID", itemId);

            return _db.GetDataRow("Portal_GetSingleEvent", parameterItemId);
        }

        /// <summary>
        /// The DeleteEvent method deletes a specified event from
        /// the events database.
        /// </summary>
        public void DeleteEvent(int itemId)
        {
            DbParameter parameterItemId = _db.CreateParameter("@ItemID", itemId);

            _db.ExecuteNonQuery("Portal_DeleteEvent", parameterItemId);
        }

        /// <summary>
        /// adds a new event within the Events database table, and returns the ItemID value as a result.
        /// </summary>
        public int AddEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                            String description, String wherewhen)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }
            DbParameter parameterItemId = _db.CreateIdentityParameter("@ItemID");

            DbParameter parameterModuleId = _db.CreateParameter("@ModuleID", moduleId);
            DbParameter parameterUserName = _db.CreateParameter("@UserName", userName);
            DbParameter parameterTitle = _db.CreateParameter("@Title", title);
            DbParameter parameterWhereWhen = _db.CreateParameter("@WhereWhen", wherewhen);
            DbParameter parameterExpireDate = _db.CreateParameter("@ExpireDate", expireDate);
            DbParameter parameterDescription = _db.CreateParameter("@Description", description);

            _db.ExecuteNonQuery<int>("Portal_AddEvent", parameterItemId, parameterDescription, parameterModuleId,
                                     parameterUserName,
                                     parameterTitle, parameterWhereWhen, parameterExpireDate);

            // Return the new Event ItemID)
            return (int) parameterItemId.Value;
        }

        /// <summary>
        /// updates the specified event within the Events database table.
        /// </summary>
        public void UpdateEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                                String description, String wherewhen)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            DbParameter parameterItemId = _db.CreateParameter("@ItemID", itemId);

            DbParameter parameterUserName = _db.CreateParameter("@UserName", userName);
            DbParameter parameterTitle = _db.CreateParameter("@Title", title);
            DbParameter parameterWhereWhen = _db.CreateParameter("@WhereWhen", wherewhen);
            DbParameter parameterExpireDate = _db.CreateParameter("@ExpireDate", expireDate);
            DbParameter parameterDescription = _db.CreateParameter("@Description", description);

            _db.ExecuteNonQuery("Portal_UpdateEvent", parameterItemId, parameterUserName, parameterTitle,
                                parameterWhereWhen, parameterExpireDate, parameterDescription);
        }

        #endregion
    }
}