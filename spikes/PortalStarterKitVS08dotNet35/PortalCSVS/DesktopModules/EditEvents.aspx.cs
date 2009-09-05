using System;
using System.Data.Common;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class EditEvents : ContainerPage<EditEvents>
    {
        [Dependency]
        public IEventsDb EventsDb
        {
            get;
            set;
        }

        private int itemId;
        private int moduleId;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected RequiredFieldValidator RequiredFieldValidator2;
        protected RequiredFieldValidator RequiredFieldValidator3;

        public EditEvents()
        {
            Page.Init += Page_Init;
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
            moduleId = Int32.Parse(Request.Params["Mid"]);

            // Verify that the current user has access to edit this module
            if (PortalSecurity.HasEditPermissions(moduleId) == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            // Determine ItemId of Events to Update
            if (Request.Params["ItemId"] != null)
            {
                itemId = Int32.Parse(Request.Params["ItemId"]);
            }

            // If the page is being requested the first time, determine if an
            // event itemId value is specified, and if so populate page
            // contents with the event details

            if (Page.IsPostBack == false)
            {
                if (itemId != 0)
                {
                    // Obtain a single row of event information
                    PortalEvent item = EventsDb.GetSingleEvent(itemId);

                    // Security check.  verify that itemid is within the module.
                    if (Convert.ToInt32(item.ModuleID) != moduleId)
                    {
                        Response.Redirect("~/Admin/EditAccessDenied.aspx");
                    }

                    TitleField.Text = item.Title;
                    DescriptionField.Text = item.Description;
                    ExpireField.Text = item.ExpireDate.Value.ToShortDateString();
                    CreatedBy.Text = item.CreatedByUser.Email;
                    WhereWhenField.Text = item.WhereWhen;
                    CreatedDate.Text = item.CreatedDate.Value.ToShortDateString();
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
                if (itemId == 0)
                {
                    // Add the event within the Events table
                    EventsDb.AddEvent(moduleId, itemId, Context.User.Identity.Name, TitleField.Text,
                                    DateTime.Parse(ExpireField.Text), DescriptionField.Text, WhereWhenField.Text);
                }
                else
                {
                    // Update the event within the Events table
                    EventsDb.UpdateEvent(moduleId, itemId, Context.User.Identity.Name, TitleField.Text,
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

            if (itemId != 0)
            {
                EventsDb.DeleteEvent(itemId);
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