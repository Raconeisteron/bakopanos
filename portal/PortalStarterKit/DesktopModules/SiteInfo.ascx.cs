using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPNET.StarterKit.Portal;
using Microsoft.Practices.Unity;

namespace PortalStarterKit.DesktopModules
{
    public partial class SiteInfo : PortalModuleControl<SiteInfo>
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
            if (!Page.IsPostBack)
            {
                tabList.DataSource = ConfigurationService.DefaultPortal.DesktopTabs;                
                tabList.DataBind();
            }
            
        }
    }
}