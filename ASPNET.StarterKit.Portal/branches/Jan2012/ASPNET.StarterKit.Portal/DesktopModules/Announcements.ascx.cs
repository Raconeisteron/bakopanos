using System;
using Microsoft.Practices.Unity;
using Portal.Modules.Model;

namespace ASPNET.StarterKit.Portal
{
    public partial class Announcements : PortalModuleControl
    {
        [Dependency]
        public IRepository<Announcement> AnnouncementsRepo { get; set; }

        /// <summary>
        /// The Page_Load event handler on this User Control is used to
        /// obtain a DataSet of announcement information from the Announcements
        /// table, and then databind the results to a templated DataList
        /// server control.  It uses the ASPNET.StarterKit.Portal.AnnouncementsDB()
        /// data component to encapsulate all data functionality.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain announcement information from Announcements table
            // and bind to the datalist control
            // DataBind Announcements to DataList Control
            myDataList.DataSource = AnnouncementsRepo.GetList(ModuleId);
            myDataList.DataBind();
        }
    }
}