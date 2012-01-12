using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public interface IHtmlTextsDb
    {
        IDataReader GetHtmlText(int moduleId);
        void UpdateHtmlText(int moduleId, string desktopHtml, string mobileSummary, string mobileDetails);
    }
}