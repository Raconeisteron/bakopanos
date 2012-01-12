using System;
using System.Data;
using System.Web.UI;

namespace ASPNET.StarterKit.Portal
{
    public partial class EditAnnouncements : Page
    {
        private int _itemId;
        private int _moduleId;


        //****************************************************************
        //
        // The Page_Load event on this Page is used to obtain the ModuleId
        // and ItemId of the announcement to edit.
        //
        // It then uses the ASPNET.StarterKit.Portal.AnnouncementsDB() data component
        // to populate the page's edit controls with the annoucement details.
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

            // Determine ItemId of Announcement to Update
            if (Request.Params["ItemId"] != null)
            {
                _itemId = Int32.Parse(Request.Params["ItemId"]);
            }

            // If the page is being requested the first time, determine if an
            // announcement itemId value is specified, and if so populate page
            // contents with the announcement details

            if (Page.IsPostBack) return;

            if (_itemId != 0)
            {
                // Obtain a single row of announcement information
                var announcementDb = new SqlAnnouncementsDb();
                IDataReader dr = announcementDb.GetSingleAnnouncement(_itemId);

                // Load first row into DataReader
                dr.Read();

                // Security check.  verify that itemid is within the module.
                int dbModuleId = Convert.ToInt32(dr["ModuleID"]);
                if (dbModuleId != _moduleId)
                {
                    dr.Close();
                    Response.Redirect("~/Admin/EditAccessDenied.aspx");
                }

                TitleField.Text = (String) dr["Title"];
                MoreLinkField.Text = (String) dr["MoreLink"];
                MobileMoreField.Text = (String) dr["MobileMoreLink"];
                DescriptionField.Text = (String) dr["Description"];
                ExpireField.Text = ((DateTime) dr["ExpireDate"]).ToShortDateString();
                CreatedBy.Text = (String) dr["CreatedByUser"];
                CreatedDate.Text = ((DateTime) dr["CreatedDate"]).ToShortDateString();

                // Close the datareader
                dr.Close();
            }

            // Store URL Referrer to return to portal
            ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
        }

        //****************************************************************
        //
        // The UpdateBtn_Click event handler on this Page is used to either
        // create or update an announcement.  It  uses the ASPNET.StarterKit.Portal.AnnouncementsDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {
            // Only Update if the Entered Data is Valid
            if (Page.IsValid)
            {
                // Create an instance of the Announcement DB component
                var announcementDb = new SqlAnnouncementsDb();

                if (_itemId == 0)
                {
                    // Add the announcement within the Announcements table
                    announcementDb.AddAnnouncement(_moduleId, _itemId, Context.User.Identity.Name, TitleField.Text,
                                                   DateTime.Parse(ExpireField.Text), DescriptionField.Text,
                                                   MoreLinkField.Text, MobileMoreField.Text);
                }
                else
                {
                    // Update the announcement within the Announcements table
                    announcementDb.UpdateAnnouncement(_moduleId, _itemId, Context.User.Identity.Name, TitleField.Text,
                                                      DateTime.Parse(ExpireField.Text), DescriptionField.Text,
                                                      MoreLinkField.Text, MobileMoreField.Text);
                }

                // Redirect back to the portal home page
                Response.Redirect((String) ViewState["UrlReferrer"]);
            }
        }

        //****************************************************************
        //
        // The DeleteBtn_Click event handler on this Page is used to delete an
        // an announcement.  It  uses the ASPNET.StarterKit.Portal.AnnouncementsDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        protected void DeleteBtn_Click(Object sender, EventArgs e)
        {
            // Only attempt to delete the item if it is an existing item
            // (new items will have "ItemId" of 0)

            if (_itemId != 0)
            {
                var announcementDb = new SqlAnnouncementsDb();
                announcementDb.DeleteAnnouncement(_itemId);
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