using System.Data;

namespace Portal.Modules.DAL.SqlServer
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// HTML/text within the Portal database.
    /// </summary>
    internal class HtmlTextDb : SqlDbHelper, IHtmlTextDb
    {
        #region IHtmlTextDb Members

        /// <summary>
        /// The GetHtmlText method returns a IDataReader containing details
        /// about a specific item from the HtmlText database table.
        /// </summary>        
        public IDataReader GetHtmlText(int moduleId)
        {
            return GetItems("Portal_GetHtmlText", InputModuleId(moduleId));
        }

        /// <summary>
        /// The UpdateHtmlText method updates a specified item within
        /// the HtmlText database table.
        /// </summary>        
        public void UpdateHtmlText(int moduleId, string desktopHtml, string mobileSummary, string mobileDetails)
        {
            ExecuteNonQuery("Portal_UpdateHtmlText", OutputItemId(), InputModuleId(moduleId),
                            InputDesktopHtml(desktopHtml),
                            InputMobileSummary(mobileSummary),
                            InputMobileDetails(mobileDetails));
        }

        #endregion
    }
}