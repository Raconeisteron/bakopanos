using System;
using System.Data;
using System.Data.Common;
using Framework.Data;

namespace ASPNETPortal
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// announcements within the Portal database.
    /// </summary>
    internal class AnnouncementsDb : IAnnouncementsDb
    {
        private readonly IDbHelper _db;

        public AnnouncementsDb(IDbHelper db)
        {
            _db = db;
        }

        #region IAnnouncementsDb Members

        /// <returns>
        /// The GetAnnouncements method returns a DataSet containing all of the
        /// announcements for a specific portal module from the Announcements database table.
        ///</returns>
        public DataTable GetAnnouncements(int moduleId)
        {
            DbParameter parameterModuleId = _db.CreateParameter("@ModuleID", moduleId);

            return _db.GetDataTable("Portal_GetAnnouncements", parameterModuleId);
        }

        /// <returns>
        /// The GetSingleAnnouncement method returns a SqlDataReader containing details
        /// about a specific announcement from the Announcements database table.
        /// </returns>
        public DataRow GetSingleAnnouncement(int itemId)
        {
            DbParameter parameterItemId = _db.CreateParameter("@ItemID", itemId);

            return _db.GetDataRow("Portal_GetSingleAnnouncement", parameterItemId);
        }

        /// <summary>
        /// The DeleteAnnouncement method deletes the specified announcement from
        /// the Announcements database table.
        /// </summary>
        public void DeleteAnnouncement(int itemId)
        {
            DbParameter parameterItemId = _db.CreateParameter("@ItemID", itemId);

            _db.ExecuteNonQuery("Portal_DeleteAnnouncement", parameterItemId);
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

            DbParameter parameterItemId = _db.CreateOutputParameter("@ItemID", DbType.Int32, 4);

            DbParameter parameterModuleId = _db.CreateParameter("@ModuleID", moduleId);
            DbParameter parameterUserName = _db.CreateParameter("@UserName", userName);
            DbParameter parameterTitle = _db.CreateParameter("@Title", title);
            DbParameter parameterMoreLink = _db.CreateParameter("@MoreLink", moreLink);
            DbParameter parameterMobileMoreLink = _db.CreateParameter("@MobileMoreLink", mobileMoreLink);
            DbParameter parameterExpireDate = _db.CreateParameter("@ExpireDate", expireDate);
            DbParameter parameterDescription = _db.CreateParameter("@Description", description);

            return _db.ExecuteNonQuery<int>("Portal_AddAnnouncement", parameterItemId, parameterModuleId,
                                            parameterUserName,
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

            DbParameter parameterItemId = _db.CreateParameter("@ItemID", itemId);
            DbParameter parameterUserName = _db.CreateParameter("@UserName", userName);
            DbParameter parameterTitle = _db.CreateParameter("@Title", title);
            DbParameter parameterMoreLink = _db.CreateParameter("@MoreLink", moreLink);
            DbParameter parameterMobileMoreLink = _db.CreateParameter("@MobileMoreLink", mobileMoreLink);
            DbParameter parameterExpireDate = _db.CreateParameter("@ExpireDate", expireDate);
            DbParameter parameterDescription = _db.CreateParameter("@Description", description);

            _db.ExecuteNonQuery("Portal_UpdateAnnouncement", parameterItemId, parameterUserName, parameterTitle,
                                parameterMoreLink, parameterMobileMoreLink,
                                parameterExpireDate, parameterDescription);
        }

        #endregion
    }
}