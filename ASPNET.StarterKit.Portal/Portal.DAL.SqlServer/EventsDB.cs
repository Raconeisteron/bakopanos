using System;
using System.Data;

namespace Portal.Modules.DAL.SqlServer
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// events within the Portal database.
    /// </summary>
    internal class EventsDb : SqlDbHelper, IEventsDb
    {
        #region IEventsDb Members

        /// <summary>
        /// The GetEvents method returns a DataSet containing all of the
        /// events for a specific portal module from the events
        /// database.
        /// </summary>        
        public IDataReader GetEvents(int moduleId)
        {
            return GetItems("Portal_GetEvents", InputModuleId(moduleId));
        }

        /// <summary>
        /// The GetSingleEvent method returns a IDataReader containing details
        /// about a specific event from the events database.
        /// </summary>
        public IDataReader GetSingleEvent(int itemId)
        {
            // Return the datareader 
            return GetSingleItem("Portal_GetSingleEvent", itemId);
        }

        /// <summary>
        /// The DeleteEvent method deletes a specified event from
        /// the events database.
        /// </summary>        
        public void DeleteEvent(int itemId)
        {
            ExecuteNonQuery("Portal_DeleteEvent", InputItemId(itemId));
        }

        /// <summary>
        /// // The AddEvent method adds a new event within the Events database table, 
        /// and returns the ItemID value as a result.
        /// </summary>        
        public int AddEvent(int moduleId, int itemId, string userName, string title, DateTime expireDate,
                            string description, string wherewhen)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            return CreateItem("Portal_AddEvent", ReturnValueItemId(), InputModuleId(moduleId),
                              InputUserName(userName),
                              InputTitle(title),
                              InputWhereWhen(wherewhen),
                              InputExpireDate(expireDate),
                              InputDescription(description));
        }

        /// <summary>
        /// The UpdateEvent method updates the specified event within
        /// the Events database table.
        /// </summary>        
        public void UpdateEvent(int moduleId, int itemId, string userName, string title, DateTime expireDate,
                                string description, string wherewhen)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            ExecuteNonQuery("Portal_UpdateEvent", 
                InputItemId(itemId),
                       InputUserName(userName),
                       InputTitle(title),
                       InputWhereWhen(wherewhen),
                       InputExpireDate(expireDate),
                       InputDescription(description));
        }

        #endregion
    }
}