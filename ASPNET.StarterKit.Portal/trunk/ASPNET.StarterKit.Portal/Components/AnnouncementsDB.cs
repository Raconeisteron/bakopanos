using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// announcements within the Portal database.
    /// </summary>
    public class AnnouncementsDb : DbHelper
    {
        /// <returns>
        /// The GetAnnouncements method returns a DataSet containing all of the
        /// announcements for a specific portal module from the Announcements database table.
        ///</returns>
        public static DataTable GetAnnouncements(int moduleId)
        {
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};

            return GetDataTable("Portal_GetAnnouncements", parameterModuleId);
        }

        /// <returns>
        /// The GetSingleAnnouncement method returns a SqlDataReader containing details
        /// about a specific announcement from the Announcements database table.
        /// </returns>
        public static DataRow GetSingleAnnouncement(int itemId)
        {   
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};

            return GetDataRow("Portal_GetSingleAnnouncement", parameterItemId);
        }

        /// <summary>
        /// The DeleteAnnouncement method deletes the specified announcement from
        /// the Announcements database table.
        /// </summary>
        public static void DeleteAnnouncement(int itemId)
        {
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};

            ExecuteNonQuery("Portal_DeleteAnnouncement", parameterItemId);
        }

        /// <summary>
        /// The AddAnnouncement method adds a new announcement to the
        /// Announcements database table, and returns the ItemId value as a result.
        /// </summary>
        public static int AddAnnouncement(int moduleId, String userName, String title, DateTime expireDate,
                                   String description, String moreLink, String mobileMoreLink)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Direction = ParameterDirection.Output};
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};
            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100) {Value = userName};
            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 150) {Value = title};
            var parameterMoreLink = new SqlParameter("@MoreLink", SqlDbType.NVarChar, 150) {Value = moreLink};
            var parameterMobileMoreLink = new SqlParameter("@MobileMoreLink", SqlDbType.NVarChar, 150)
                                              {Value = mobileMoreLink};
            var parameterExpireDate = new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8) {Value = expireDate};
            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 2000) {Value = description};

            return ExecuteNonQuery<int>("Portal_AddAnnouncement", parameterItemId, parameterModuleId, parameterUserName,
                                        parameterTitle, parameterMoreLink, parameterMobileMoreLink,
                                        parameterExpireDate, parameterDescription);
        }

        /// <summary>
        /// The UpdateAnnouncement method updates the specified announcement within
        /// the Announcements database table.
        /// </summary>
        public static void UpdateAnnouncement(int itemId, String userName, String title, DateTime expireDate,
                                       String description, String moreLink, String mobileMoreLink)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }
            
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};
            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100) {Value = userName};
            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 150) {Value = title};
            var parameterMoreLink = new SqlParameter("@MoreLink", SqlDbType.NVarChar, 150) {Value = moreLink};
            var parameterMobileMoreLink = new SqlParameter("@MobileMoreLink", SqlDbType.NVarChar, 150)
                                              {Value = mobileMoreLink};
            var parameterExpireDate = new SqlParameter("@ExpireDate", SqlDbType.DateTime, 8) {Value = expireDate};
            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 2000) {Value = description};

            ExecuteNonQuery("Portal_UpdateAnnouncement", parameterItemId, parameterUserName, parameterTitle,
                            parameterMoreLink, parameterMobileMoreLink,
                            parameterExpireDate, parameterDescription);
        }
    }
}