using System;
using System.Data;
using System.Web.UI;
using Portal.Contracts;
using Portal.Modules.DAL;
using Portal.Services;

namespace ASPNET.StarterKit.Portal
{
    public partial class EditLinks : Page
    {
        private int itemId;
        private int moduleId;

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
            moduleId = Int32.Parse(Request.Params["Mid"]);

            // Verify that the current user has access to edit this module
            if (PortalSecurity.HasEditPermissions(moduleId) == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            // Determine ItemId of Link to Update
            if (Request.Params["ItemId"] != null)
            {
                itemId = Int32.Parse(Request.Params["ItemId"]);
            }

            // If the page is being requested the first time, determine if an
            // link itemId value is specified, and if so populate page
            // contents with the link details

            if (Page.IsPostBack == false)
            {
                if (itemId != 0)
                {
                    // Obtain a single row of link information
                    ILinksDb links = ModulesDataAccess.LinkDb;
                    IDataReader dr = links.GetSingleLink(itemId);

                    // Read in first row from database
                    dr.Read();

                    // Security check.  verify that itemid is within the module.
                    int dbModuleID = Convert.ToInt32(dr["ModuleID"]);
                    if (dbModuleID != moduleId)
                    {
                        dr.Close();
                        Response.Redirect("~/Admin/EditAccessDenied.aspx");
                    }

                    TitleField.Text = (string) dr["Title"];
                    DescriptionField.Text = (string) dr["Description"];
                    UrlField.Text = (string) dr["Url"];
                    MobileUrlField.Text = (string) dr["MobileUrl"];
                    ViewOrderField.Text = dr["ViewOrder"].ToString();
                    CreatedBy.Text = (string) dr["CreatedByUser"];
                    CreatedDate.Text = ((DateTime) dr["CreatedDate"]).ToShortDateString();

                    // Close datareader
                    dr.Close();
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
                // Create an instance of the Link DB component
                ILinkService links = ServiceAccess.LinkService;

                var link = new PortalLink
                               {
                                   ItemId = itemId,
                                   ModuleId = moduleId,
                                   CreatedByUser = Context.User.Identity.Name,
                                   Title = TitleField.Text,
                                   Url = UrlField.Text,
                                   MobileUrl = MobileUrlField.Text,
                                   ViewOrder = Int32.Parse(ViewOrderField.Text),
                                   Description = DescriptionField.Text
                               };

                links.CreateOrUpdate(link);
                
                // Redirect back to the portal home page
                Response.Redirect((string) ViewState["UrlReferrer"]);
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

            if (itemId != 0)
            {
                ILinksDb links = ModulesDataAccess.LinkDb;
                links.DeleteLink(itemId);
            }

            // Redirect back to the portal home page
            Response.Redirect((string) ViewState["UrlReferrer"]);
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
            Response.Redirect((string) ViewState["UrlReferrer"]);
        }
    }
}