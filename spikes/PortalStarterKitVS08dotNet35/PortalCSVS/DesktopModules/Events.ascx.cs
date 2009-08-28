using System;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class Events : PortalModuleControl<Events>
    {

        [Dependency]
        public IEventsDb EventsDb
        {
            get;
            set;
        }

        //*******************************************************
        //
        // The Page_Load event handler on this User Control is used to
        // obtain a DataReader of event information from the Events
        // table, and then databind the results to a templated DataList
        // server control.  It uses the ASPNET.StarterKit.Portal.EventDB()
        // data component to encapsulate all data functionality.
        //
        //*******************************************************

        public Events()
        {
            Init += Page_Init;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain the list of events from the Events table
            // and bind to the DataList Control
            myDataList.DataSource = EventsDb.GetEvents(ModuleId);
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