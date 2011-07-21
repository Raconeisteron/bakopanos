using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalStarterKit.Domain;

namespace PortalStarterKit
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var portalSettings = (SiteConfiguration) Context.Cache["SiteConfiguration"];

            AddMenus(NavigationMenu.Items, portalSettings.Portal.Tabs);
        }

        private static void AddMenus(MenuItemCollection menuitems, IEnumerable<Tab> tabs)
        {
            foreach (Tab tab in tabs)
            {
                var item = new MenuItem {Text = tab.TabName, NavigateUrl = tab.NavigateUrl};
                menuitems.Add(item);

                AddMenus(item.ChildItems, tab.Tabs);
            }
        }
    }
}