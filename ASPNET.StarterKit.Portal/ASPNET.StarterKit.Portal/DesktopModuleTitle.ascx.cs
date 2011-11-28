using System;
using System.Web;
using System.Web.UI;

namespace ASPNET.StarterKit.Portal
{
    public partial class DesktopModuleTitle : UserControl
    {
        public string EditTarget;
        public string EditText;
        public string EditUrl;

        public DesktopModuleTitle()
        {
            Init += Page_Init;
        }

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

        protected void Page_Init(object sender, EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
        }

        #region Web Form Designer generated code

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}