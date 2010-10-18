using System;
using Microsoft.Practices.Unity;
using PortalStarterKit.Components;

namespace PortalStarterKit.DesktopModules
{
    public partial class Announcements : PortalModule<Announcements>
    {
        [Dependency]
        public ISiteConfigurationService ConfigurationService { private
            get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            label.Text = ConfigurationService.GetPortal(PortalId).PortalName;
        }
    }
}