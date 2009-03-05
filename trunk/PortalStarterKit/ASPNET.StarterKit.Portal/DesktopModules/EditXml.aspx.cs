using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPNET.StarterKit.Portal
{
    public class EditXml : Page
    {
        protected LinkButton cancelButton;

        private int moduleId;
        protected LinkButton updateButton;
        protected TextBox XmlDataSrc;
        protected TextBox XslTransformSrc;

        public EditXml()
        {
            Page.Init += Page_Init;
        }

        //****************************************************************
        //
        // The Page_Load event on this Page is used to obtain the ModuleId
        // xml module to edit.
        //
        // It then uses the ASP.NET configuration system to populate the page's
        // edit controls with the xml details.
        //
        //****************************************************************

        private void Page_Load(object sender, EventArgs e)
        {
            // Determine ModuleId of Announcements Portal Module
            moduleId = Int32.Parse(Request.Params["Mid"]);

            // Verify that the current user has access to edit this module
            if (PortalSecurity.HasEditPermissions(moduleId) == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            if (Page.IsPostBack == false)
            {
                if (moduleId > 0)
                {
                    // Get settings from the database
                    Hashtable settings = Configuration.GetModuleSettings(moduleId);

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

        private void UpdateBtn_Click(Object sender, EventArgs e)
        {
            // Update settings in the database
            var config = new Configuration();

            config.UpdateModuleSetting(moduleId, "xmlsrc", XmlDataSrc.Text);
            config.UpdateModuleSetting(moduleId, "xslsrc", XslTransformSrc.Text);

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

        private void CancelBtn_Click(Object sender, EventArgs e)
        {
            // Redirect back to the portal home page
            Response.Redirect((String) ViewState["UrlReferrer"]);
        }

        private void Page_Init(object sender, EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
        }

        #region Web Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.updateButton.Click += new System.EventHandler(this.UpdateBtn_Click);
            this.cancelButton.Click += new System.EventHandler(this.CancelBtn_Click);
            this.Load += new System.EventHandler(this.Page_Load);
        }

        #endregion
    }
}