using System;
using System.Data;
using System.Web.UI;
using Portal.Components;
using Portal.Modules.Data;
using Portal.Modules.Service;
using Portal.Modules.Service.Contracts;

namespace Portal.DesktopModules
{
    public partial class EditAnnouncements : Page
    {
        private int itemId;
        private int moduleId;

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
            moduleId = Int32.Parse(Request.Params["Mid"]);

            // Verify that the current user has access to edit this module
            if (PortalSecurity.HasEditPermissions(moduleId) == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            // Determine ItemId of Announcement to Update
            if (Request.Params["ItemId"] != null)
            {
                itemId = Int32.Parse(Request.Params["ItemId"]);
            }

            // If the page is being requested the first time, determine if an
            // announcement itemId value is specified, and if so populate page
            // contents with the announcement details

            if (Page.IsPostBack == false)
            {
                if (itemId != 0)
                {
                    // Obtain a single row of announcement information
                    IAnnouncementsDb announcementDB = ModulesDataAccess.AnnouncementsDb;
                    IDataReader dr = announcementDB.GetSingleAnnouncement(itemId);

                    // Load first row into DataReader
                    dr.Read();

                    // Security check.  verify that itemid is within the module.
                    int dbModuleID = Convert.ToInt32(dr["ModuleID"]);
                    if (dbModuleID != moduleId)
                    {
                        dr.Close();
                        Response.Redirect("~/Admin/EditAccessDenied.aspx");
                    }

                    TitleField.Text = (string) dr["Title"];
                    MoreLinkField.Text = (string) dr["MoreLink"];
                    MobileMoreField.Text = (string) dr["MobileMoreLink"];
                    DescriptionField.Text = (string) dr["Description"];
                    ExpireField.Text = ((DateTime) dr["ExpireDate"]).ToShortDateString();
                    CreatedBy.Text = (string) dr["CreatedByUser"];
                    CreatedDate.Text = ((DateTime) dr["CreatedDate"]).ToShortDateString();

                    // Close the datareader
                    dr.Close();
                }

                // Store URL Referrer to return to portal
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            }
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
                IAnnouncementService announcementService = ServiceAccess.AnnouncementService;

                var announcement = new PortalAnnouncement
                                       {
                                           ItemId = itemId,
                                           ModuleId = moduleId,
                                           CreatedByUser = Context.User.Identity.Name,
                                           Title = TitleField.Text,
                                           ExpireDate = DateTime.Parse(ExpireField.Text),
                                           Description = DescriptionField.Text,
                                           MoreLink = MoreLinkField.Text,
                                           MobileMoreLink = MobileMoreField.Text
                                       };

                announcementService.CreateOrUpdate(announcement);

                // Redirect back to the portal home page
                Response.Redirect((string) ViewState["UrlReferrer"]);
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

            if (itemId != 0)
            {
                IAnnouncementsDb announcementDB = ModulesDataAccess.AnnouncementsDb;
                announcementDB.DeleteAnnouncement(itemId);
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