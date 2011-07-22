using PortalStarterKit.Model;

namespace PortalStarterKit.Presentation
{
    public interface IPortalTemplateControl
    {
        Tab ActiveTab { get; }
        SiteConfiguration SiteConfiguration { get; }
    }
}