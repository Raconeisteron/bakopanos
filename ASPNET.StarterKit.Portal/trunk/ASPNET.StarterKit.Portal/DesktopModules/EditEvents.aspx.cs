using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class EditEvents : Page
    {
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected RequiredFieldValidator RequiredFieldValidator2;
        protected RequiredFieldValidator RequiredFieldValidator3;
        private IEventsDb _eventsDb;

        private int _itemId;
        private int _moduleId;

        private IPortalSecurity _portalSecurity;

        [InjectionMethod]
        public void Initialize(IPortalSecurity portalSecurity, IEventsDb eventsDb)
        {
            _portalSecurity = portalSecurity;
            _eventsDb = eventsDb;
        }

        //****************************************************************
        //
        // The Page_Load event on this Page is used to obtain the ModuleId
        // and ItemId of the event to edit.
        //
        // It then uses the ASPNET.StarterKit.Portal.EventsDB() data component
        // to populate the page's edit controls with the event details.
        //
        //****************************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            // Determine ModuleId of Events Portal Module
            _moduleId = Int32.Parse(Request.Params["Mid"]);

            // Verify that the current user has access to edit this module
            if (_portalSecurity.HasEditPermissions(_moduleId) == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            // Determine ItemId of Events to Update
            if (Request.Params["ItemId"] != null)
            {
                _itemId = Int32.Parse(Request.Params["ItemId"]);
            }

            // If the page is being requested the first time, determine if an
            // event itemId value is specified, and if so populate page
            // contents with the event details

            if (Page.IsPostBack == false)
            {
                if (_itemId != 0)
                {
                    // Obtain a single row of event information
                    PortalEvent dr = _eventsDb.GetSingleEvent(_itemId);

                    // Security check.  verify that itemid is within the module.
                    int dbModuleID = dr.ModuleId;
                    if (dbModuleID != _moduleId)
                        Response.Redirect("~/Admin/EditAccessDenied.aspx");


                    TitleField.Text = dr.Title;
                    DescriptionField.Text = dr.Description;
                    ExpireField.Text = dr.ExpireDate.ToShortDateString();
                    CreatedBy.Text = dr.CreatedByUser;
                    WhereWhenField.Text = dr.WhereWhen;
                    CreatedDate.Text = dr.CreatedDate.ToShortDateString();
                }

                // Store URL Referrer to return to portal
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            }
        }

        //****************************************************************
        //
        // The UpdateBtn_Click event handler on this Page is used to either
        // create or update an event.  It uses the ASPNET.StarterKit.Portal.EventsDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {
            // Only Update if the Entered Data is Valid
            if (Page.IsValid)
            {
                if (_itemId == 0)
                {
                    // Add the event within the Events table
                    _eventsDb.AddEvent(_moduleId, Context.User.Identity.Name,
                                       Server.HtmlEncode(TitleField.Text),
                                       DateTime.Parse(ExpireField.Text),
                                       Server.HtmlEncode(DescriptionField.Text),
                                       Server.HtmlEncode(WhereWhenField.Text));
                }
                else
                {
                    // Update the event within the Events table
                    _eventsDb.UpdateEvent(_itemId, Context.User.Identity.Name,
                                          TitleField.Text,
                                          DateTime.Parse(ExpireField.Text),
                                          Server.HtmlEncode(DescriptionField.Text),
                                          Server.HtmlEncode(WhereWhenField.Text));
                }

                // Redirect back to the portal home page
                Response.Redirect((String) ViewState["UrlReferrer"]);
            }
        }

        //****************************************************************
        //
        // The DeleteBtn_Click event handler on this Page is used to delete an
        // an event.  It  uses the ASPNET.StarterKit.Portal.EventsDB() data component to
        // encapsulate all data functionality.
        //
        //****************************************************************

        protected void DeleteBtn_Click(Object sender, EventArgs e)
        {
            // Only attempt to delete the item if it is an existing item
            // (new items will have "ItemId" of 0)

            if (_itemId != 0)
            {
                _eventsDb.DeleteEvent(_itemId);
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