using System;
using Portal.Components;
using Portal.Modules.Data;

namespace Portal.DesktopModules
{
    public partial class Announcements : PortalModuleControl
    {
        //*******************************************************
        //
        // The Page_Load event handler on this User Control is used to
        // obtain a DataSet of announcement information from the Announcements
        // table, and then databind the results to a templated DataList
        // server control.  It uses the ASPNET.StarterKit.Portal.AnnouncementsDB()
        // data component to encapsulate all data functionality.
        //
        //*******************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain announcement information from Announcements table
            // and bind to the datalist control
            IAnnouncementsDb announcements = ModulesDataAccess.AnnouncementsDb;

            // DataBind Announcements to DataList Control
            myDataList.DataSource = announcements.GetAnnouncements(ModuleId);
            myDataList.DataBind();
        }
    }
}