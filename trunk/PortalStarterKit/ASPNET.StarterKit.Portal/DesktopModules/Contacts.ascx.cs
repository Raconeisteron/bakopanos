using System;
using System.Web.UI.WebControls;
using ASPNET.StarterKit.Portal.Components;

namespace ASPNET.StarterKit.Portal
{
    public partial  class Contacts : PortalModuleControl
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


        public Contacts()
        {
            Init += Page_Init;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain contact information from Contacts table
            // and bind to the DataGrid Control
            var contacts = new ContactsDB();

            myDataGrid.DataSource = contacts.GetContacts(ModuleId);
            myDataGrid.DataBind();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
        }

        #region Web Form Designer generated code

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}