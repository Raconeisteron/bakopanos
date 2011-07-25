using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalStarterKit.Model;

namespace PortalStarterKit
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var portalSettings = (SiteConfiguration) Context.Cache["SiteConfiguration"];

            AddMenus(NavigationMenu.Items, portalSettings.Portals[0]);
        }

        private static void AddMenus(MenuItemCollection menuitems, ITabContainer tabContainer)
        {
            foreach (Tab tab in tabContainer.Tabs)
            {
                var item = new MenuItem {Text = tab.TabName, NavigateUrl = tab.NavigateUrl};
                menuitems.Add(item);

                AddMenus(item.ChildItems, tab);
            }
        }
    }
}