using System;
using System.Data.SqlClient;
using System.Web.UI;

namespace ASPNET.StarterKit.Portal
{
    public partial class EditHtml : Page
    {
        private int _moduleId;

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
            _moduleId = Int32.Parse(Request.Params["Mid"]);

            // Verify that the current user has access to edit this module
            if (PortalSecurity.HasEditPermissions(_moduleId) == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            if (Page.IsPostBack == false)
            {
                // Obtain a single row of text information
                var text = new HtmlTextDb();
                SqlDataReader dr = HtmlTextDb.GetHtmlText(_moduleId);

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
            var text = new HtmlTextDb();

            // Update the text within the HtmlText table
            HtmlTextDb.UpdateHtmlText(_moduleId, Server.HtmlEncode(DesktopText.Text),
                                      Server.HtmlEncode(MobileSummary.Text),
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
    }
}