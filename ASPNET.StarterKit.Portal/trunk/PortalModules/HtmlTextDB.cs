using System;
using System.Data;
using System.Data.SqlClient;
using Framework.Data;

namespace ASPNETPortal
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// HTML/text within the Portal database.
    /// </summary>
    internal class HtmlTextDb : IHtmlTextDb
    {
        private readonly IDbHelper _db;

        public HtmlTextDb(IDbHelper db)
        {
            _db = db;
        }

        #region IHtmlTextDb Members

        /// <returns>
        /// The GetHtmlText method returns a SqlDataReader containing details
        /// about a specific item from the HtmlText database table.
        /// </returns>
        public DataTable GetHtmlText(int moduleId)
        {
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};

            return _db.GetDataTable("Portal_GetHtmlText", parameterModuleId);
        }


        /// <summary>
        /// The UpdateHtmlText method updates a specified item within
        /// the HtmlText database table.
        /// </summary>
        public void UpdateHtmlText(int moduleId, String desktopHtml, String mobileSummary, String mobileDetails)
        {
            var parameterModuleId = new SqlParameter("@ModuleID", SqlDbType.Int, 4) {Value = moduleId};
            var parameterDesktopHtml = new SqlParameter("@DesktopHtml", SqlDbType.NText) {Value = desktopHtml};
            var parameterMobileSummary = new SqlParameter("@MobileSummary", SqlDbType.NText) {Value = mobileSummary};
            var parameterMobileDetails = new SqlParameter("@MobileDetails", SqlDbType.NText) {Value = mobileDetails};

            _db.ExecuteNonQuery("Portal_UpdateHtmlText", parameterModuleId, parameterDesktopHtml, parameterMobileSummary,
                                parameterMobileDetails);
        }

        #endregion
    }
}