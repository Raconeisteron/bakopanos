using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public interface IHtmlTextDb
    {
        /// <returns>
        /// The GetHtmlText method returns a SqlDataReader containing details
        /// about a specific item from the HtmlText database table.
        /// </returns>
        DataTable GetHtmlText(int moduleId);

        /// <summary>
        /// The UpdateHtmlText method updates a specified item within
        /// the HtmlText database table.
        /// </summary>
        void UpdateHtmlText(int moduleId, String desktopHtml, String mobileSummary, String mobileDetails);
    }
}