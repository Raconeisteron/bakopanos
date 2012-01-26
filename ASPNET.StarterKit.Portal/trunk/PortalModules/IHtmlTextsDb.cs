namespace ASPNET.StarterKit.Portal
{
    public interface IHtmlTextsDb
    {
        PortalHtmlText GetHtmlText(int moduleId);
        void UpdateHtmlText(int moduleId, string desktopHtml, string mobileSummary, string mobileDetails);
    }
}