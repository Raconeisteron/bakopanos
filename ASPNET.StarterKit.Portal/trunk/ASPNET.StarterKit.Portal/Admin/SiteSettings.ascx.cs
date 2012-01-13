using System;

namespace ASPNET.StarterKit.Portal
{
    public partial class SiteSettings : PortalModuleControl
    {
        //*******************************************************
        //
        // The Page_Load server event handler on this user control is used
        // to populate the current site settings from the config system
        //
        //*******************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            var portalSecurity = ComponentManager.Resolve<IPortalSecurity>();

            // Verify that the current user has access to access this page
            if (portalSecurity.IsInRoles("Admins") == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            // If this is the first visit to the page, populate the site data
            if (Page.IsPostBack == false)
            {
                // Obtain PortalSettings from Current Context
                var portalSettings = (PortalSettings) Context.Items["PortalSettings"];

                siteName.Text = portalSettings.Portal.PortalName;
                showEdit.Checked = portalSettings.Portal.AlwaysShowEditButton;
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
            var config = ComponentManager.Resolve<IPortalConfigurationDb>();
            config.UpdatePortalInfo(portalSettings.Portal.PortalId, siteName.Text, showEdit.Checked);

            // Redirect to this site to refresh
            Response.Redirect(Request.RawUrl);
        }
    }
}