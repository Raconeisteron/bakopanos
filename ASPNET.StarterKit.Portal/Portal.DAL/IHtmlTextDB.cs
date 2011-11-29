using System.Data;

namespace Portal.Modules.DAL
{
    public interface IHtmlTextDb
    {
        IDataReader GetHtmlText(int moduleId);
        void UpdateHtmlText(int moduleId, string desktopHtml, string mobileSummary, string mobileDetails);
    }
}