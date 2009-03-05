using System;
using System.Web.UI.WebControls;
using ASPNET.StarterKit.Portal.Components;

namespace ASPNET.StarterKit.Portal
{
    public abstract class Announcements : PortalModuleControl
    {
        protected DataList myDataList;


        //*******************************************************
        //
        // The Page_Load event handler on this User Control is used to
        // obtain a DataSet of announcement information from the Announcements
        // table, and then databind the results to a templated DataList
        // server control.  It uses the ASPNET.StarterKit.Portal.AnnouncementsDB()
        // data component to encapsulate all data functionality.
        //
        //*******************************************************

        public Announcements()
        {
            Init += Page_Init;
        }

        private void Page_Load(object sender, EventArgs e)
        {
            // Obtain announcement information from Announcements table
            // and bind to the datalist control
            var announcements = new AnnouncementsDB();

            // DataBind Announcements to DataList Control
            myDataList.DataSource = announcements.GetAnnouncements(ModuleId);
            myDataList.DataBind();
        }

        private void Page_Init(object sender, EventArgs e)
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
            this.Load += new System.EventHandler(this.Page_Load);
        }

        #endregion
    }
}