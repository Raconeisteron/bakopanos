using System;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.Data
{
    public interface IHtmlTextDB
    {
        SqlDataReader GetHtmlText(int moduleId);
        void UpdateHtmlText(int moduleId, String desktopHtml, String mobileSummary, String mobileDetails);
    }
}