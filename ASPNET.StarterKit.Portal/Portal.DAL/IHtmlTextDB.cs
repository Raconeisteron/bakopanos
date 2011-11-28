using System;
using System.Data;

namespace ASPNET.StarterKit.Portal.DAL
{
    public interface IHtmlTextDb
    {
        IDataReader GetHtmlText(int moduleId);
        void UpdateHtmlText(int moduleId, string desktopHtml, string mobileSummary, string mobileDetails);
    }
}