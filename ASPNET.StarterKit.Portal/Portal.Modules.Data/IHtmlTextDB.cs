using System.Data;

namespace Portal.Modules.Data
{
    public interface IHtmlTextDb
    {
        IDataReader GetHtmlText(int moduleId);
        void UpdateHtmlText(int moduleId, string desktopHtml, string mobileSummary, string mobileDetails);
    }
}