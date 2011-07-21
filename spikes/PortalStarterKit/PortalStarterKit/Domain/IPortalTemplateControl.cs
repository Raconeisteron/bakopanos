namespace PortalStarterKit.Domain
{
    public interface IPortalTemplateControl
    {
        Tab ActiveTab { get; }
        SiteConfiguration SiteConfiguration { get; }
    }
}