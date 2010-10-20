using System;
using Microsoft.Practices.Unity;
using PortalStarterKit.Core;

namespace PortalStarterKit.DesktopModules
{
    public partial class SiteInfo : PortalModule<SiteInfo>
    {
        [Dependency]
        public ISiteConfigurationService ConfigurationService { private
            get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                tabList.DataSource = ConfigurationService.GetPortal(PortalId).DesktopTabs;
                tabList.DataBind();
            }
        }
    }
}