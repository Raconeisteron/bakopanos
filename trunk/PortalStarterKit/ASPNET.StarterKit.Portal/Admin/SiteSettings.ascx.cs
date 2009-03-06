using System;
using System.Web.UI.WebControls;
using ASPNET.StarterKit.Portal.Components;

namespace ASPNET.StarterKit.Portal.Admin
{
    public partial  class SiteSettings : PortalModuleControl
    {

        public SiteSettings()
        {
            Init += Page_Init;
        }


        //*******************************************************
        //
        // The Page_Load server event handler on this user control is used
        // to populate the current site settings from the config system
        //
        //*******************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            // Verify that the current user has access to access this page
            if (PortalSecurity.IsInRoles("Admins") == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            // If this is the first visit to the page, populate the site data
            if (Page.IsPostBack == false)
            {
                // Obtain PortalSettings from Current Context
                var portalSettings = (PortalSettings) Context.Items["PortalSettings"];

                siteName.Text = portalSettings.PortalName;
                showEdit.Checked = portalSettings.AlwaysShowEditButton;
            }
        }

        //*******************************************************
        //
        // The Apply_Click server event handler is used
        // to update the Site Name within the Portal Config System
        //
        //*******************************************************

        protected void Apply_Click(Object sender, EventArgs e)
        {
            // Obtain PortalSettings from Current Context
            var portalSettings = (PortalSettings) Context.Items["PortalSettings"];

            // update Tab info in the database
            var config = new Configuration();
            config.UpdatePortalInfo(portalSettings.PortalId, siteName.Text, showEdit.Checked);

            // Redirect to this site to refresh
            Response.Redirect(Request.RawUrl);
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