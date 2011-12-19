using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
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
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};

            return _db.GetDataTable("Portal_GetLinks", parameterModuleId);
        }

        /// <returns>
        /// The GetSingleLink method returns a SqlDataReader containing details
        /// about a specific link from the Links database table.
        /// </returns>
        public DataRow GetSingleLink(int itemId)
        {
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};

            return _db.GetDataRow("Portal_GetSingleLink", parameterItemId);
        }

        /// <summary>
        /// The DeleteLink method deletes a specified link from
        /// the Links database table.
        /// </summary>
        public void DeleteLink(int itemId)
        {
            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};

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

            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Direction = ParameterDirection.Output};
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};
            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100) {Value = userName};
            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 100) {Value = title};
            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 100) {Value = description};
            var parameterUrl = new SqlParameter("@Url", SqlDbType.NVarChar, 100) {Value = url};
            var parameterMobileUrl = new SqlParameter("@MobileUrl", SqlDbType.NVarChar, 100) {Value = mobileUrl};
            var parameterViewOrder = new SqlParameter("@ViewOrder", SqlDbType.Int, 4) {Value = viewOrder};

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

            var parameterItemId = new SqlParameter("@ItemID", SqlDbType.Int, 4) {Value = itemId};
            var parameterUserName = new SqlParameter("@UserName", SqlDbType.NVarChar, 100) {Value = userName};
            var parameterTitle = new SqlParameter("@Title", SqlDbType.NVarChar, 100) {Value = title};
            var parameterDescription = new SqlParameter("@Description", SqlDbType.NVarChar, 100) {Value = description};
            var parameterUrl = new SqlParameter("@Url", SqlDbType.NVarChar, 100) {Value = url};
            var parameterMobileUrl = new SqlParameter("@MobileUrl", SqlDbType.NVarChar, 100) {Value = mobileUrl};
            var parameterViewOrder = new SqlParameter("@ViewOrder", SqlDbType.Int, 4) {Value = viewOrder};

            _db.ExecuteNonQuery("Portal_UpdateLink", parameterItemId, parameterUserName, parameterTitle,
                                parameterDescription,
                                parameterUrl, parameterMobileUrl, parameterViewOrder);
        }

        #endregion
    }
}