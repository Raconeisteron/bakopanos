using System;
using System.Data;

namespace ASPNETPortal
{
    public interface IEventsDb
    {
        /// <returns>
        /// The GetEvents method returns a DataSet containing all of the
        /// events for a specific portal module from the events
        /// database.
        /// </returns>
        DataTable GetEvents(int moduleId);

        /// <returns>
        /// The GetSingleEvent method returns a SqlDataReader containing details
        /// about a specific event from the events database.
        /// </returns>
        DataRow GetSingleEvent(int itemId);

        int AddEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                     String description, String wherewhen);

        void DeleteEvent(int itemId);

        void UpdateEvent(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                         String description, String wherewhen);
    }
}