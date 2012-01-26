using System;
using System.Collections;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class EditImage : Page
    {
        private IModuleConfigurationDb _moduleConfigurationDb;
        private int _moduleId;

        private IPortalSecurity _portalSecurity;

        [InjectionMethod]
        public void Initialize(IPortalSecurity portalSecurity, IModuleConfigurationDb moduleConfigurationDb)
        {
            _portalSecurity = portalSecurity;
            _moduleConfigurationDb = moduleConfigurationDb;
        }

        //****************************************************************
        //
        // The Page_Load event on this Page is used to obtain the ModuleId
        // of the image module to edit.
        //
        // It then uses the ASP.NET configuration system to populate the page's
        // edit controls with the image details.
        //
        //****************************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            // Determine ModuleId of Announcements Portal Module
            _moduleId = Int32.Parse(Request.Params["Mid"]);

            // Verify that the current user has access to edit this module
            if (_portalSecurity.HasEditPermissions(_moduleId) == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            if (Page.IsPostBack == false)
            {
                if (_moduleId > 0)
                {
                    // Get settings from the database
                    Hashtable settings = _moduleConfigurationDb.GetModuleSettings(_moduleId);

                    Src.Text = (string) settings["src"];
                    Width.Text = (string) settings["width"];
                    Height.Text = (string) settings["height"];
                }

                // Store URL Referrer to return to portal
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            }
        }

        //****************************************************************
        //
        // The UpdateBtn_Click event handler on this Page is used to save
        // the settings to the ModuleSettings database table.  It  uses the 
        // ASPNetPortalDB() data component to encapsulate the data 
        // access functionality.
        //
        //****************************************************************

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {
            // Update settings in the database
            _moduleConfigurationDb.UpdateModuleSetting(_moduleId, "src", Src.Text);
            _moduleConfigurationDb.UpdateModuleSetting(_moduleId, "height", Height.Text);
            _moduleConfigurationDb.UpdateModuleSetting(_moduleId, "width", Width.Text);

            // Redirect back to the portal home page
            Response.Redirect((String) ViewState["UrlReferrer"]);
        }

        //****************************************************************
        //
        // The CancelBtn_Click event handler on this Page is used to cancel
        // out of the page, and return the user back to the portal home
        // page.
        //
        //****************************************************************

        protected void CancelBtn_Click(Object sender, EventArgs e)
        {
            // Redirect back to the portal home page
            Response.Redirect((String) ViewState["UrlReferrer"]);
        }
    }
}