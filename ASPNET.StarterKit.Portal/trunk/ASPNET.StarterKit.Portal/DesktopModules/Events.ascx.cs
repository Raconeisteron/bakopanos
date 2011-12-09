using System;

namespace ASPNET.StarterKit.Portal
{
    public partial class Events : PortalModuleControl
    {
        /// <summary>
        /// The Page_Load event handler on this User Control is used to
        /// obtain a DataReader of event information from the Events
        /// table, and then databind the results to a templated DataList
        /// server control.  It uses the ASPNET.StarterKit.Portal.EventDB()
        /// data component to encapsulate all data functionality.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain the list of events from the Events table
            // and bind to the DataList Control
            var events = new EventsDb();

            myDataList.DataSource = EventsDb.GetEvents(ModuleId);
            myDataList.DataBind();
        }
    }
}