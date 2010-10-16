using System.Web.UI;

namespace PortalStarterKit.Components
{
    public abstract class PortalModuleUserControl : UserControl
    {
        public string PortalId { get; set; }

        public string TabId { get; set; }

        public ModuleSettings ModuleConfiguration { get; set; }
    }
}