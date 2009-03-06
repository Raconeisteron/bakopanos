using System;
using System.Data;
using System.Web.UI;
using ASPNET.StarterKit.Portal.Components;

namespace ASPNET.StarterKit.Portal.DesktopModules
{
    public partial class EditHtml : Page
    {
        private int moduleId;

        public EditHtml()
        {
            Page.Init += Page_Init;
        }

        //****************************************************************
        //
        // The Page_Load event on this Page is used to obtain the ModuleId
        // of the xml module to edit.
        //
        // It then uses the ASPNET.StarterKit.Portal.HtmlTextDB() data component
        // to populate the page's edit controls with the text details.
        //
        //****************************************************************

        protected void Page_Load(object sender, EventArgs e)
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
                // Obtain a single row of text information
                IHtmlTextDB text = new HtmlTextDB();
                IDataReader dr = text.GetHtmlText(moduleId);

                if (dr.Read())
                {
                    DesktopText.Text = Server.HtmlDecode((String) dr["DesktopHtml"]);
                    MobileSummary.Text = Server.HtmlDecode((String) dr["MobileSummary"]);
                    MobileDetails.Text = Server.HtmlDecode((String) dr["MobileDetails"]);
                }
                else
                {
                    DesktopText.Text = "Todo: Add Content...";
                    MobileSummary.Text = "Todo: Add Content...";
                    MobileDetails.Text = "Todo: Add Content...";
                }

                dr.Close();

                // Store URL Referrer to return to portal
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            }
        }

        //****************************************************************
        //
        // The UpdateBtn_Click event handler on this Page is used to save
        // the text changes to the database.
        //
        //****************************************************************

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {
            // Create an instance of the HtmlTextDB component
            IHtmlTextDB text = new HtmlTextDB();

            // Update the text within the HtmlText table
            text.UpdateHtmlText(moduleId, Server.HtmlEncode(DesktopText.Text), Server.HtmlEncode(MobileSummary.Text),
                                Server.HtmlEncode(MobileDetails.Text));

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

        protected void Page_Init(object sender, EventArgs e)
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
        }

        #endregion
    }
}