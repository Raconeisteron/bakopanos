using System;
using System.Data;
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
            var parameterModuleId = _db.CreateParameter("@ModuleID", moduleId);

            return _db.GetDataTable("Portal_GetEvents", parameterModuleId);
        }

        /// <returns>
        /// The GetSingleEvent method returns a SqlDataReader containing details
        /// about a specific event from the events database.
        /// </returns>
        public DataRow GetSingleEvent(int itemId)
        {
            var parameterItemId = _db.CreateParameter("@ItemID",  itemId);

            return _db.GetDataRow("Portal_GetSingleEvent", parameterItemId);
        }

        /// <summary>
        /// The DeleteEvent method deletes a specified event from
        /// the events database.
        /// </summary>
        public void DeleteEvent(int itemId)
        {
            var parameterItemId = _db.CreateParameter("@ItemID",  itemId);

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
            var parameterItemId = _db.CreateIdentityParameter("@ItemID");

            var parameterModuleId = _db.CreateParameter("@ModuleID", moduleId);
            var parameterUserName = _db.CreateParameter("@UserName", userName);
            var parameterTitle = _db.CreateParameter("@Title",  title);
            var parameterWhereWhen = _db.CreateParameter("@WhereWhen", wherewhen);
            var parameterExpireDate = _db.CreateParameter("@ExpireDate", expireDate);
            var parameterDescription = _db.CreateParameter("@Description", description);

            _db.ExecuteNonQuery<int>("Portal_AddEvent", parameterItemId, parameterDescription, parameterModuleId, parameterUserName,
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

            var parameterItemId =  _db.CreateParameter("@ItemID",  itemId);
           
            var parameterUserName = _db.CreateParameter("@UserName",  userName);
            var parameterTitle = _db.CreateParameter("@Title",  title);
            var parameterWhereWhen = _db.CreateParameter("@WhereWhen",  wherewhen);
            var parameterExpireDate = _db.CreateParameter("@ExpireDate",  expireDate);
            var parameterDescription = _db.CreateParameter("@Description", description);

            _db.ExecuteNonQuery("Portal_UpdateEvent", parameterItemId, parameterUserName, parameterTitle, parameterWhereWhen, parameterExpireDate, parameterDescription);
        }

        #endregion
    }
}