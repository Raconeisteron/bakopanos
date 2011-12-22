using System;
using System.Data;

namespace ASPNETPortal
{
    public interface IAnnouncementsDb
    {
        /// <returns>
        /// The GetAnnouncements method returns a DataSet containing all of the
        /// announcements for a specific portal module from the Announcements database table.
        ///</returns>
        DataTable GetAnnouncements(int moduleId);

        /// <returns>
        /// The GetSingleAnnouncement method returns a SqlDataReader containing details
        /// about a specific announcement from the Announcements database table.
        /// </returns>
        DataRow GetSingleAnnouncement(int itemId);

        /// <summary>
        /// The DeleteAnnouncement method deletes the specified announcement from
        /// the Announcements database table.
        /// </summary>
        void DeleteAnnouncement(int itemId);

        /// <summary>
        /// The AddAnnouncement method adds a new announcement to the
        /// Announcements database table, and returns the ItemId value as a result.
        /// </summary>
        int AddAnnouncement(int moduleId, String userName, String title, DateTime expireDate,
                            String description, String moreLink, String mobileMoreLink);

        /// <summary>
        /// The UpdateAnnouncement method updates the specified announcement within
        /// the Announcements database table.
        /// </summary>
        void UpdateAnnouncement(int itemId, String userName, String title, DateTime expireDate,
                                String description, String moreLink, String mobileMoreLink);
    }
}