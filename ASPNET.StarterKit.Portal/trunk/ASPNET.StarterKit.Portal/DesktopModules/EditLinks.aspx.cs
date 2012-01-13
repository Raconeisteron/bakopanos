using System;
using System.Data;
using System.Web.UI;
using ASPNET.StarterKit.Portal.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    public partial class EditLinks : Page
    {
        private int _itemId;
        private int _moduleId;

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

            var portalSecurity = ComponentManager.Resolve<IPortalSecurity>();

            // Verify that the current user has access to edit this module
            if (portalSecurity.HasEditPermissions(_moduleId) == false)
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
                    ILinksDb links = new SqlLinksDb();
                    IDataReader dr = links.GetSingleLink(_itemId);

                    // Read in first row from database
                    dr.Read();

                    // Security check.  verify that itemid is within the module.
                    int dbModuleID = Convert.ToInt32(dr["ModuleID"]);
                    if (dbModuleID != _moduleId)
                    {
                        dr.Close();
                        Response.Redirect("~/Admin/EditAccessDenied.aspx");
                    }

                    TitleField.Text = (String) dr["Title"];
                    DescriptionField.Text = (String) dr["Description"];
                    UrlField.Text = (String) dr["Url"];
                    MobileUrlField.Text = (String) dr["MobileUrl"];
                    ViewOrderField.Text = dr["ViewOrder"].ToString();
                    CreatedBy.Text = (String) dr["CreatedByUser"];
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
                ILinksDb links = new SqlLinksDb();

                if (_itemId == 0)
                {
                    // Add the link within the Links table
                    links.AddLink(_moduleId, Context.User.Identity.Name, TitleField.Text, UrlField.Text,
                                  MobileUrlField.Text, Int32.Parse(ViewOrderField.Text), DescriptionField.Text);
                }
                else
                {
                    // Update the link within the Links table
                    links.UpdateLink(_itemId, Context.User.Identity.Name, TitleField.Text, UrlField.Text,
                                     MobileUrlField.Text, Int32.Parse(ViewOrderField.Text), DescriptionField.Text);
                }

                // Redirect back to the portal home page
                Response.Redirect((String) ViewState["UrlReferrer"]);
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
                ILinksDb links = new SqlLinksDb();
                links.DeleteLink(_itemId);
            }

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