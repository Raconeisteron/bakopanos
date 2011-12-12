using System;
using System.Collections;
using System.Web.UI;

namespace ASPNET.StarterKit.Portal
{
    public partial class EditImage : Page
    {
        private int _moduleId;


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
            if (PortalSecurity.HasEditPermissions(_moduleId) == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            if (Page.IsPostBack == false)
            {
                if (_moduleId > 0)
                {
                    // Get settings from the database
                    Hashtable settings = ConfigurationDb.GetModuleSettings(_moduleId);

                    Src.Text = (String) settings["src"];
                    Width.Text = (String) settings["width"];
                    Height.Text = (String) settings["height"];
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
            ConfigurationDb.UpdateModuleSetting(_moduleId, "src", Src.Text);
            ConfigurationDb.UpdateModuleSetting(_moduleId, "height", Height.Text);
            ConfigurationDb.UpdateModuleSetting(_moduleId, "width", Width.Text);

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