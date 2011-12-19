using System;
using System.Collections;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class EditXml : Page
    {
        private int _moduleId;

        [Dependency]
        public IConfigurationDb ConfigModel { get; set; }

        /// <summary>
        /// The Page_Load event on this Page is used to obtain the ModuleId
        /// xml module to edit.
        ///
        /// It then uses the ASP.NET configuration system to populate the page's
        /// edit controls with the xml details.
        /// </summary>
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
                    Hashtable settings = ConfigModel.GetModuleSettings(_moduleId);

                    XmlDataSrc.Text = (String) settings["xmlsrc"];
                    XslTransformSrc.Text = (String) settings["xslsrc"];
                }

                // Store URL Referrer to return to portal
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            }
        }

        //****************************************************************
        //
        // The UpdateBtn_Click event handler on this Page is used to save
        // the settings to the configuration file.
        //
        //****************************************************************

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {
            // Update settings in the database
            ConfigModel.UpdateModuleSetting(_moduleId, "xmlsrc", XmlDataSrc.Text);
            ConfigModel.UpdateModuleSetting(_moduleId, "xslsrc", XslTransformSrc.Text);

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