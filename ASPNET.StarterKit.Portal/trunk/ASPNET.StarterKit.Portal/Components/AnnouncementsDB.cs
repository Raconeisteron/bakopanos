using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// announcements within the Portal database.
    /// </summary>
    public class AnnouncementsDb : DbHelper, IAnnouncementsDb
    {        
        /// <returns>
        /// The GetAnnouncements method returns a DataSet containing all of the
        /// announcements for a specific portal module from the Announcements database table.
        ///</returns>
        public DataTable GetAnnouncements(int moduleId)
        {
            var parameterModuleId = CreateParameter("@ModuleID", moduleId);

            return GetDataTable("Portal_GetAnnouncements", parameterModuleId);
        }

        /// <returns>
        /// The GetSingleAnnouncement method returns a SqlDataReader containing details
        /// about a specific announcement from the Announcements database table.
        /// </returns>
        public DataRow GetSingleAnnouncement(int itemId)
        {
            var parameterItemId = CreateParameter("@ItemID", itemId);

            return GetDataRow("Portal_GetSingleAnnouncement", parameterItemId);
        }

        /// <summary>
        /// The DeleteAnnouncement method deletes the specified announcement from
        /// the Announcements database table.
        /// </summary>
        public void DeleteAnnouncement(int itemId)
        {
            var parameterItemId = CreateParameter("@ItemID", itemId);

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

            var parameterItemId = CreateOutputParameter("@ItemID");
            var parameterModuleId = CreateParameter("@ModuleID", moduleId);
            var parameterUserName = CreateParameter("@UserName", userName);
            var parameterTitle = CreateParameter("@Title", title);
            var parameterMoreLink = CreateParameter("@MoreLink", moreLink);
            var parameterMobileMoreLink = CreateParameter("@MobileMoreLink", mobileMoreLink);
            var parameterExpireDate = CreateParameter("@ExpireDate", expireDate);
            var parameterDescription = CreateParameter("@Description", description);

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

            var parameterItemId = CreateParameter("@ItemID", itemId);
            var parameterUserName = CreateParameter("@UserName", userName);
            var parameterTitle = CreateParameter("@Title", title);
            var parameterMoreLink = CreateParameter("@MoreLink", moreLink);
            var parameterMobileMoreLink = CreateParameter("@MobileMoreLink", mobileMoreLink);
            var parameterExpireDate = CreateParameter("@ExpireDate", expireDate);
            var parameterDescription = CreateParameter("@Description", description);

            ExecuteNonQuery("Portal_UpdateAnnouncement", parameterItemId, parameterUserName, parameterTitle,
                            parameterMoreLink, parameterMobileMoreLink,
                            parameterExpireDate, parameterDescription);
        }
    }
}