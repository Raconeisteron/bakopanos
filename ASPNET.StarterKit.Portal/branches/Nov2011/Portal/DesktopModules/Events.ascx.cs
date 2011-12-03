using System;
using Portal.Components;
using Portal.Modules.Data;

namespace Portal.DesktopModules
{
    public partial class Events : PortalModuleControl
    {
        //*******************************************************
        //
        // The Page_Load event handler on this User Control is used to
        // obtain a DataReader of event information from the Events
        // table, and then databind the results to a templated DataList
        // server control.  It uses the ASPNET.StarterKit.Portal.EventDB()
        // data component to encapsulate all data functionality.
        //
        //*******************************************************


        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain the list of events from the Events table
            // and bind to the DataList Control
            IEventsDb events = ModulesDataAccess.EventsDb;

            myDataList.DataSource = events.GetEvents(ModuleId);
            myDataList.DataBind();
        }
    }
}