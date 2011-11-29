using System.Data;

namespace ASPNET.StarterKit.Portal.Modules.DAL
{
    public interface IHtmlTextDb
    {
        IDataReader GetHtmlText(int moduleId);
        void UpdateHtmlText(int moduleId, string desktopHtml, string mobileSummary, string mobileDetails);
    }
}