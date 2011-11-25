using System;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL
{
    public interface IHtmlTextDB
    {
        SqlDataReader GetHtmlText(int moduleId);
        void UpdateHtmlText(int moduleId, String desktopHtml, String mobileSummary, String mobileDetails);
    }
}