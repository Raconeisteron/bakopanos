using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal
{
    public interface IHtmlTextsDb
    {
        List<PortalHtmlText> GetHtmlText(int moduleId);
        void UpdateHtmlText(int moduleId, string desktopHtml, string mobileSummary, string mobileDetails);
    }
}