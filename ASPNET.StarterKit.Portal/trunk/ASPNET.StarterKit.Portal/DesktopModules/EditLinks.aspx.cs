using System;
using System.Data;
using System.Web.UI;
using ASPNET.StarterKit.Portal.PortalDao;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class EditLinks : Page
    {
        private int _itemId;
        private ILinksDb _linksDb;
        private int _moduleId;

        private IPortalSecurity _portalSecurity;

        [InjectionMethod]
        public void Initialize(IPortalSecurity portalSecurity, ILinksDb linksDb)
        {
            _portalSecurity = portalSecurity;
            _linksDb = linksDb;
        }

        //****************************************************************
        //
        // The Page_Load event on this Page is used to obtain the 
        // ItemId of the link to edit.
        //
        // It then uses the ASPNET.StarterKit.Portal.LinkDB() data component
        // to populate the page's edit controls with the links details.
        //
        //****************************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            // Determine ModuleId of Links Portal Module
            _moduleId = Int32.Parse(Request.Params["Mid"]);

            // Verify that the current user has access to edit this module
            if (_portalSecurity.HasEditPermissions(_moduleId) == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            // Determine ItemId of Link to Update
            if (Request.Params["ItemId"] != null)
            {
                _itemId = Int32.Parse(Request.Params["ItemId"]);
            }

            // If the page is being requested the first time, determine if an
            // link itemId value is specified, and if so populate page
            // contents with the link details

            if (Page.IsPostBack == false)
            {
                if (_itemId != 0)
                {
                    // Obtain a single row of link information
                    PortalLink dr = _linksDb.GetSingleLink(_itemId);

                    // Security check.  verify that itemid is within the module.
                    int dbModuleID = dr.ModuleId;
                    if (dbModuleID != _moduleId)
                        Response.Redirect("~/Admin/EditAccessDenied.aspx");


                    TitleField.Text = dr.Title;
                    DescriptionField.Text = dr.Description;
                    UrlField.Text = dr.Url;
                    MobileUrlField.Text = dr.MobileUrl;
                    ViewOrderField.Text = dr.ViewOrder.ToString();
                    CreatedBy.Text = dr.CreatedByUser;
                    CreatedDate.Text = dr.CreatedDate.ToShortDateString();

                }

                // Store URL Referrer to return to portal
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            }
        }

        //****************************************************************
        //
        // The UpdateBtn_Click event handler on this Page is used to either
        // create or update a link.  It  uses the ASPNET.StarterKit.Portal.LinkDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (_itemId == 0)
                {
                    // Add the link within the Links table
                    _linksDb.AddLink(_moduleId, Context.User.Identity.Name, TitleField.Text, UrlField.Text,
                                     MobileUrlField.Text, Int32.Parse(ViewOrderField.Text), DescriptionField.Text);
                }
                else
                {
                    // Update the link within the Links table
                    _linksDb.UpdateLink(_itemId, Context.User.Identity.Name, TitleField.Text, UrlField.Text,
                                        MobileUrlField.Text, Int32.Parse(ViewOrderField.Text), DescriptionField.Text);
                }

                // Redirect back to the portal home page
                Response.Redirect((String)ViewState["UrlReferrer"]);
            }
        }

        //****************************************************************
        //
        // The DeleteBtn_Click event handler on this Page is used to delete 
        // a link.  It  uses the ASPNET.StarterKit.Portal.LinksDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        protected void DeleteBtn_Click(Object sender, EventArgs e)
        {
            // Only attempt to delete the item if it is an existing item
            // (new items will have "ItemId" of 0)

            if (_itemId != 0)
            {
                _linksDb.DeleteLink(_itemId);
            }

            // Redirect back to the portal home page
            Response.Redirect((String)ViewState["UrlReferrer"]);
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
            Response.Redirect((String)ViewState["UrlReferrer"]);
        }
    }
}