using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class EditHtml : Page
    {
        private IHtmlTextsDb _htmlTextsDb;
        private int _moduleId;

        private IPortalSecurity _portalSecurity;

        [InjectionMethod]
        public void Initialize(IPortalSecurity portalSecurity, IHtmlTextsDb htmlTextsDb)
        {
            _portalSecurity = portalSecurity;
            _htmlTextsDb = htmlTextsDb;
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
            _moduleId = Int32.Parse(Request.Params["Mid"]);

            // Verify that the current user has access to edit this module
            if (_portalSecurity.HasEditPermissions(_moduleId) == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            if (Page.IsPostBack == false)
            {
                // Obtain a single row of text information
                List<PortalHtmlText> dr = _htmlTextsDb.GetHtmlText(_moduleId);

                if (dr.Count>0)
                {
                    DesktopText.Text = Server.HtmlDecode(dr[0].DesktopHtml);
                    MobileSummary.Text = Server.HtmlDecode(dr[0].MobileSummary);
                    MobileDetails.Text = Server.HtmlDecode(dr[0].MobileDetails);
                }
                else
                {
                    DesktopText.Text = "Todo: Add Content...";
                    MobileSummary.Text = "Todo: Add Content...";
                    MobileDetails.Text = "Todo: Add Content...";
                }


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
            // Update the text within the HtmlText table
            _htmlTextsDb.UpdateHtmlText(_moduleId, Server.HtmlEncode(DesktopText.Text),
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