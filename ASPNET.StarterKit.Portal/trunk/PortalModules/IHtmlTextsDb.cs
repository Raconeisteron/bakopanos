using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public interface IHtmlTextsDb
    {
        IDataReader GetHtmlText(int moduleId);
        void UpdateHtmlText(int moduleId, String desktopHtml, String mobileSummary, String mobileDetails);
    }
}