using System;
using System.Data.Common;

namespace ASPNET.StarterKit.Portal
{
    public interface IHtmlTextsDb
    {
        DbDataReader GetHtmlText(int moduleId);
        void UpdateHtmlText(int moduleId, String desktopHtml, String mobileSummary, String mobileDetails);
    }
}