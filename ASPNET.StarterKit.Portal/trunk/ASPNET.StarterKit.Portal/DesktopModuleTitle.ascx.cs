using System;
using System.Web;
using System.Web.UI;

namespace ASPNET.StarterKit.Portal
{
    public partial class DesktopModuleTitle : UserControl
    {
        public String EditTarget;
        public String EditText;
        public String EditUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain PortalSettings from Current Context
            var portalSettings = (PortalSettings) HttpContext.Current.Items["PortalSettings"];

            // Obtain reference to parent portal module
            var portalModule = (PortalModuleControl) Parent;

            // Display Modular Title Text and Edit Buttons
            ModuleTitle.Text = portalModule.ModuleConfiguration.ModuleTitle;

            // Display the Edit button if the parent portalmodule has configured the PortalModuleTitle User Control
            // to display it -- and the current client has edit access permissions
            if (portalSettings.AlwaysShowEditButton ||
                (PortalSecurity.IsInRoles(portalModule.ModuleConfiguration.AuthorizedEditRoles)) && (EditText != null))
            {
                EditButton.Text = EditText;
                EditButton.NavigateUrl = EditUrl + "?mid=" + portalModule.ModuleId;
                EditButton.Target = EditTarget;
            }
        }
    }
}