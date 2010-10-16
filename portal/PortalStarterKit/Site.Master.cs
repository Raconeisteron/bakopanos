using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPNET.StarterKit.Portal;
using Microsoft.Practices.Unity;

namespace PortalStarterKit
{
    public partial class SiteMaster : PortalMasterPage<SiteMaster>
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            portalName.Text = SiteConfiguration.GetPortal(PortalId).PortalName;

            foreach (TabSettings tab in SiteConfiguration.GetPortal(PortalId).DesktopTabs)
            {
                var menuItem = new MenuItem(tab.TabName);
                menuItem.NavigateUrl = GetNavigateUrl(PortalId, tab.TabId);
                NavigationMenu.Items.Add(menuItem);
            }
        }
    }
}