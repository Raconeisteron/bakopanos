using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    public interface IHtmlTextDB
    {
        DbDataReader GetHtmlText(int moduleId);
        void UpdateHtmlText(int moduleId, String desktopHtml, String mobileSummary, String mobileDetails);
    }
}