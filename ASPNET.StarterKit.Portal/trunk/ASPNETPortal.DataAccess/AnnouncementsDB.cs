using System;
using System.Data;
using System.Data.Common;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// announcements within the Portal database.
    /// </summary>
    internal class AnnouncementsDb : DbHelper, IAnnouncementsDb
    {
        #region IAnnouncementsDb Members

        /// <returns>
        /// The GetAnnouncements method returns a DataSet containing all of the
        /// announcements for a specific portal module from the Announcements database table.
        ///</returns>
        public DataTable GetAnnouncements(int moduleId)
        {
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);

            return GetDataTable("Portal_GetAnnouncements", parameterModuleId);
        }

        /// <returns>
        /// The GetSingleAnnouncement method returns a SqlDataReader containing details
        /// about a specific announcement from the Announcements database table.
        /// </returns>
        public DataRow GetSingleAnnouncement(int itemId)
        {
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

            return GetDataRow("Portal_GetSingleAnnouncement", parameterItemId);
        }

        /// <summary>
        /// The DeleteAnnouncement method deletes the specified announcement from
        /// the Announcements database table.
        /// </summary>
        public void DeleteAnnouncement(int itemId)
        {
            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);

            ExecuteNonQuery("Portal_DeleteAnnouncement", parameterItemId);
        }

        /// <summary>
        /// The AddAnnouncement method adds a new announcement to the
        /// Announcements database table, and returns the ItemId value as a result.
        /// </summary>
        public int AddAnnouncement(int moduleId, String userName, String title, DateTime expireDate,
                                   String description, String moreLink, String mobileMoreLink)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            DbParameter parameterItemId = CreateOutputParameter("@ItemID");
            DbParameter parameterModuleId = CreateParameter("@ModuleID", moduleId);
            DbParameter parameterUserName = CreateParameter("@UserName", userName);
            DbParameter parameterTitle = CreateParameter("@Title", title);
            DbParameter parameterMoreLink = CreateParameter("@MoreLink", moreLink);
            DbParameter parameterMobileMoreLink = CreateParameter("@MobileMoreLink", mobileMoreLink);
            DbParameter parameterExpireDate = CreateParameter("@ExpireDate", expireDate);
            DbParameter parameterDescription = CreateParameter("@Description", description);

            return ExecuteNonQuery<int>("Portal_AddAnnouncement", parameterItemId, parameterModuleId, parameterUserName,
                                        parameterTitle, parameterMoreLink, parameterMobileMoreLink,
                                        parameterExpireDate, parameterDescription);
        }

        /// <summary>
        /// The UpdateAnnouncement method updates the specified announcement within
        /// the Announcements database table.
        /// </summary>
        public void UpdateAnnouncement(int itemId, String userName, String title, DateTime expireDate,
                                       String description, String moreLink, String mobileMoreLink)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            DbParameter parameterItemId = CreateParameter("@ItemID", itemId);
            DbParameter parameterUserName = CreateParameter("@UserName", userName);
            DbParameter parameterTitle = CreateParameter("@Title", title);
            DbParameter parameterMoreLink = CreateParameter("@MoreLink", moreLink);
            DbParameter parameterMobileMoreLink = CreateParameter("@MobileMoreLink", mobileMoreLink);
            DbParameter parameterExpireDate = CreateParameter("@ExpireDate", expireDate);
            DbParameter parameterDescription = CreateParameter("@Description", description);

            ExecuteNonQuery("Portal_UpdateAnnouncement", parameterItemId, parameterUserName, parameterTitle,
                            parameterMoreLink, parameterMobileMoreLink,
                            parameterExpireDate, parameterDescription);
        }

        #endregion
    }
}