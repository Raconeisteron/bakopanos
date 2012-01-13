using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using ASPNET.StarterKit.Portal.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    public partial class EditEvents : Page
    {
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected RequiredFieldValidator RequiredFieldValidator2;
        protected RequiredFieldValidator RequiredFieldValidator3;

        private int _itemId;
        private int _moduleId;

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
            var portalSecurity = ComponentManager.Resolve<IPortalSecurity>();

            // Verify that the current user has access to edit this module
            if (portalSecurity.HasEditPermissions(_moduleId) == false)
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
                    IEventsDb events = new SqlEventsDb();
                    IDataReader dr = events.GetSingleEvent(_itemId);

                    // Read first row from database
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
                    ExpireField.Text = ((DateTime) dr["ExpireDate"]).ToShortDateString();
                    CreatedBy.Text = (String) dr["CreatedByUser"];
                    WhereWhenField.Text = (String) dr["WhereWhen"];
                    CreatedDate.Text = ((DateTime) dr["CreatedDate"]).ToShortDateString();

                    dr.Close();
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
                // Create an instance of the Event DB component
                IEventsDb events = new SqlEventsDb();

                if (_itemId == 0)
                {
                    // Add the event within the Events table
                    events.AddEvent(_moduleId, Context.User.Identity.Name, TitleField.Text,
                                    DateTime.Parse(ExpireField.Text), DescriptionField.Text, WhereWhenField.Text);
                }
                else
                {
                    // Update the event within the Events table
                    events.UpdateEvent(_itemId, Context.User.Identity.Name, TitleField.Text,
                                       DateTime.Parse(ExpireField.Text), DescriptionField.Text, WhereWhenField.Text);
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
                IEventsDb events = new SqlEventsDb();
                events.DeleteEvent(_itemId);
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