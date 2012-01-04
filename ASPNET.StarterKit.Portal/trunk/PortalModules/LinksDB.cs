using System;
using System.Data;
using Framework.Data;

namespace ASPNETPortal
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// links within the Portal database.
    /// </summary>
    internal class LinkDb : ILinkDb
    {
        private readonly IDbHelper _db;

        public LinkDb(IDbHelper db)
        {
            _db = db;
        }

        #region ILinkDb Members

        /// <returns>
        /// The GetLinks method returns a SqlDataReader containing all of the
        /// links for a specific portal module from the announcements
        /// database.
        /// </returns>
        public DataTable GetLinks(int moduleId)
        {
            var parameterModuleId = _db.CreateParameter("@ModuleID", moduleId);

            return _db.GetDataTable("Portal_GetLinks", parameterModuleId);
        }

        /// <returns>
        /// The GetSingleLink method returns a SqlDataReader containing details
        /// about a specific link from the Links database table.
        /// </returns>
        public DataRow GetSingleLink(int itemId)
        {
            var parameterItemId = _db.CreateParameter("@ItemID", itemId);

            return _db.GetDataRow("Portal_GetSingleLink", parameterItemId);
        }

        /// <summary>
        /// The DeleteLink method deletes a specified link from
        /// the Links database table.
        /// </summary>
        public void DeleteLink(int itemId)
        {
            var parameterItemId = _db.CreateParameter("@ItemID",  itemId);

            _db.ExecuteNonQuery("Portal_DeleteLink", parameterItemId);
        }

        /// <summary>
        /// The AddLink method adds a new link within the
        /// links database table, and returns ItemID value as a result.
        /// </summary>
        public int AddLink(int moduleId, String userName, String title, String url, String mobileUrl,
                           int viewOrder, String description)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            var parameterItemId = _db.CreateOutputParameter("@ItemID", DbType.Int32);
            var parameterModuleId = _db.CreateParameter("@ModuleID", moduleId);
            var parameterUserName = _db.CreateParameter("@UserName", userName);
            var parameterTitle = _db.CreateParameter("@Title", title);
            var parameterDescription = _db.CreateParameter("@Description",  description);
            var parameterUrl = _db.CreateParameter("@Url",  url);
            var parameterMobileUrl = _db.CreateParameter("@MobileUrl", mobileUrl);
            var parameterViewOrder = _db.CreateParameter("@ViewOrder", viewOrder);

            return _db.ExecuteNonQuery<int>("Portal_AddLink", parameterItemId, parameterModuleId, parameterUserName,
                                            parameterTitle, parameterDescription, parameterUrl, parameterMobileUrl,
                                            parameterViewOrder);
        }

        /// <summary>
        /// The UpdateLink method updates a specified link within
        /// the Links database table.
        /// </summary>
        public void UpdateLink(int itemId, String userName, String title, String url,
                               String mobileUrl, int viewOrder, String description)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            var parameterItemId = _db.CreateParameter("@ItemID",  itemId);
            var parameterUserName = _db.CreateParameter("@UserName", userName);
            var parameterTitle = _db.CreateParameter("@Title",  title);
            var parameterDescription = _db.CreateParameter("@Description",  description);
            var parameterUrl = _db.CreateParameter("@Url",  url);
            var parameterMobileUrl = _db.CreateParameter("@MobileUrl",  mobileUrl);
            var parameterViewOrder = _db.CreateParameter("@ViewOrder", viewOrder);

            _db.ExecuteNonQuery("Portal_UpdateLink", parameterItemId, parameterUserName, parameterTitle,
                                parameterDescription,
                                parameterUrl, parameterMobileUrl, parameterViewOrder);
        }

        #endregion
    }
}