using System;
using Portal.Components;
using Portal.Modules.Data;

namespace Portal.DesktopModules
{
    public partial class Contacts : PortalModuleControl
    {
        //*******************************************************
        //
        // The Page_Load event handler on this User Control is used to
        // obtain a DataReader of contact information from the Contacts
        // table, and then databind the results to a DataGrid
        // server control.  It uses the ASPNET.StarterKit.Portal.ContactsDB()
        // data component to encapsulate all data functionality.
        //
        //*******************************************************


        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain contact information from Contacts table
            // and bind to the DataGrid Control
            IContactsDb contacts = ModulesDataAccess.ContactsDb;

            myDataGrid.DataSource = contacts.GetContacts(ModuleId);
            myDataGrid.DataBind();
        }
    }
}