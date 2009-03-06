using System;
using ASPNET.StarterKit.Portal.Components;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal.DesktopModules
{
    public partial class Announcements : PortalModuleControl<Announcements>
    {
        /// <summary>
        /// announcement information from Announcements table 
        /// </summary>
        [Dependency]
        public IAnnouncementsDB DBAnnouncements { private get; set; }        
        
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

        protected void Page_Load(object sender, EventArgs e)
        {            
            // DataBind Announcements to DataList Control
            myDataList.DataSource = DBAnnouncements.GetAnnouncements(ModuleId);
            myDataList.DataBind();
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