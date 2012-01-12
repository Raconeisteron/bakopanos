using System;
using Microsoft.Practices.Unity;
using Portal.Modules.Model;

namespace ASPNET.StarterKit.Portal
{
    public partial class Contacts : PortalModuleControl
    {
        [Dependency]
        public IRepository<Contact> Repo { get; set; }

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
            myDataGrid.DataSource = Repo.GetList(ModuleId);
            myDataGrid.DataBind();
        }
    }
}