using System;
using System.Data;

namespace ASPNET.StarterKit.Portal.DAL
{
    public interface IHtmlTextDb
    {
        IDataReader GetHtmlText(int moduleId);
        void UpdateHtmlText(int moduleId, String desktopHtml, String mobileSummary, String mobileDetails);
    }
}