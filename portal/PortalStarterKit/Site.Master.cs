using System;
using System.Web.UI.WebControls;
using PortalStarterKit.Components;

namespace PortalStarterKit
{
    public partial class SiteMaster : PortalMasterPage<SiteMaster>
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            portalName.Text = SiteConfiguration.GetPortal(PortalId).PortalName;

            foreach (TabSettings tab in SiteConfiguration.GetPortal(PortalId).DesktopTabs)
            {
                var menuItem = new MenuItem(tab.TabName);
                menuItem.NavigateUrl = NavigateUrl(PortalId,tab.TabId);
                NavigationMenu.Items.Add(menuItem);
            }
        }
    }
}