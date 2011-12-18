using System;
using System.Data;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class EditContacts : Page
    {
        private int _itemId;
        private int _moduleId;

        [Dependency]
        public IContactsDb Model { private get; set; }

        //****************************************************************
        //
        // The Page_Load event on this Page is used to obtain the ModuleId
        // and ItemId of the contact to edit.
        //
        // It then uses the ASPNET.StarterKit.Portal.ContactsDB() data component
        // to populate the page's edit controls with the contact details.
        //
        //****************************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            // Determine ModuleId of Contacts Portal Module
            _moduleId = Int32.Parse(Request.Params["Mid"]);

            // Verify that the current user has access to edit this module
            if (PortalSecurity.HasEditPermissions(_moduleId) == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            // Determine ItemId of Contacts to Update
            if (Request.Params["ItemId"] != null)
            {
                _itemId = Int32.Parse(Request.Params["ItemId"]);
            }

            // If the page is being requested the first time, determine if an
            // contact itemId value is specified, and if so populate page
            // contents with the contact details

            if (Page.IsPostBack) return;

            if (_itemId != 0)
            {
                // Obtain a single row of contact information
                DataRow row = Model.GetSingleContact(_itemId);

                // Security check.  verify that itemid is within the module.
                int dbModuleId = Convert.ToInt32(row["ModuleID"]);
                if (dbModuleId != _moduleId)
                {
                    Response.Redirect("~/Admin/EditAccessDenied.aspx");
                }

                NameField.Text = (String) row["Name"];
                RoleField.Text = (String) row["Role"];
                EmailField.Text = (String) row["Email"];
                Contact1Field.Text = (String) row["Contact1"];
                Contact2Field.Text = (String) row["Contact2"];
                CreatedBy.Text = (String) row["CreatedByUser"];
                CreatedDate.Text = ((DateTime) row["CreatedDate"]).ToShortDateString();
            }

            // Store URL Referrer to return to portal
            ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
        }

        //****************************************************************
        //
        // The UpdateBtn_Click event handler on this Page is used to either
        // create or update a contact.  It  uses the ASPNET.StarterKit.Portal.ContactsDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {
            // Only Update if Entered data is Valid
            if (Page.IsValid)
            {
                // Create an instance of the ContactsDB component

                if (_itemId == 0)
                {
                    // Add the contact within the contacts table
                    Model.AddContact(_moduleId, Context.User.Identity.Name, NameField.Text, RoleField.Text,
                                     EmailField.Text, Contact1Field.Text, Contact2Field.Text);
                }
                else
                {
                    // Update the contact within the contacts table
                    Model.UpdateContact(_itemId, Context.User.Identity.Name, NameField.Text,
                                        RoleField.Text,
                                        EmailField.Text, Contact1Field.Text, Contact2Field.Text);
                }

                // Redirect back to the portal home page
                Response.Redirect((String) ViewState["UrlReferrer"]);
            }
        }

        //****************************************************************
        //
        // The DeleteBtn_Click event handler on this Page is used to delete an
        // a contact.  It  uses the ASPNET.StarterKit.Portal.ContactsDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        protected void DeleteBtn_Click(Object sender, EventArgs e)
        {
            // Only attempt to delete the item if it is an existing item
            // (new items will have "ItemId" of 0)

            if (_itemId != 0)
            {
                Model.DeleteContact(_itemId);
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