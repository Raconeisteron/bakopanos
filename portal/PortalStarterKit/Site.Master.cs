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
            foreach (TabSettings tab in ConfigurationService.ActivePortal(0).DesktopTabs)
            {
                NavigationMenu.Items.Add(new MenuItem(tab.TabName+"...temmorary..."));
            }
        }
    }
}