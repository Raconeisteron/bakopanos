using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPNET.StarterKit.Portal;
using Microsoft.Practices.Unity;

namespace PortalStarterKit
{
    public partial class SiteMaster : PortalMasterPage<SiteMaster>
    {
        [Dependency]
        public ISiteConfigurationService ConfigurationService
        {
            private
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            portalName.Text = ConfigurationService.GetPortal("main").PortalName;

            foreach (TabSettings tab in ConfigurationService.GetPortal("main").DesktopTabs)
            {
                var menuItem = new MenuItem(tab.TabName);
                menuItem.NavigateUrl = "~/main/" + tab.TabId;
                NavigationMenu.Items.Add(menuItem);
            }
        }
    }
}