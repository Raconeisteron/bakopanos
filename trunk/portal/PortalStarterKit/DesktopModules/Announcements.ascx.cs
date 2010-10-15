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
    public partial class Announcements : PortalModuleControl<Announcements>
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
            
            label.Text=ConfigurationService.DefaultPortal.PortalName;
        }
    }
}