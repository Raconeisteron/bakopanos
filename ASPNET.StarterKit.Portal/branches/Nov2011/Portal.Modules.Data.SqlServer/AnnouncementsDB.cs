using System;
using System.Data;

namespace Portal.Modules.Data.SqlServer
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// announcements within the Portal database.
    /// </summary>
    internal class AnnouncementsDb : SqlDbHelper, IAnnouncementsDb
    {
        #region IAnnouncementsDb Members

        /// <summary>
        /// The GetAnnouncements method returns a DataSet containing all of the
        /// announcements for a specific portal module from the Announcements
        /// database table.
        /// </summary>        
        public IDataReader GetAnnouncements(int moduleId)
        {
            return GetItems("Portal_GetAnnouncements", InputModuleId(moduleId));
        }

        /// <summary>
        /// The GetSingleAnnouncement method returns a IDataReader containing details
        /// about a specific announcement from the Announcements database table.
        /// </summary>        
        public IDataReader GetSingleAnnouncement(int itemId)
        {
            return GetSingleItem("Portal_GetSingleAnnouncement", itemId);
        }

        /// <summary>
        /// The DeleteAnnouncement method deletes the specified announcement from
        /// the Announcements database table.
        /// </summary>        
        public void DeleteAnnouncement(int itemId)
        {
            ExecuteNonQuery("Portal_DeleteAnnouncement", InputItemId(itemId));
        }

        /// <summary>
        /// The AddAnnouncement method adds a new announcement to the
        /// Announcements database table, and returns the ItemId value as a result.
        /// </summary>        
        public int AddAnnouncement(int moduleId, string userName, string title, DateTime expireDate,
                                   string description, string moreLink, string mobileMoreLink)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            // Add Parameters to SPROC
            return CreateItem("Portal_AddAnnouncement", OutputItemId(), InputModuleId(moduleId),
                              InputUserName(userName),
                              InputTitle(title),
                              InputMoreLink(moreLink),
                              InputMobileMoreLink(mobileMoreLink),
                              InputExpireDate(expireDate),
                              InputDescription(description));
        }

        /// <summary>
        /// The UpdateAnnouncement method updates the specified announcement within
        /// the Announcements database table.
        /// </summary>        
        public void UpdateAnnouncement(int itemId, string userName, string title, DateTime expireDate,
                                       string description, string moreLink, string mobileMoreLink)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            ExecuteNonQuery("Portal_UpdateAnnouncement", InputItemId(itemId),
                            InputUserName(userName),
                            InputTitle(title),
                            InputMoreLink(moreLink),
                            InputMobileMoreLink(mobileMoreLink),
                            InputExpireDate(expireDate),
                            InputDescription(description));
        }

        #endregion
    }
}