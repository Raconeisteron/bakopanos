using Portal.Modules.Contracts;
using Portal.Modules.DAL;

namespace Portal.Modules.Services
{
    internal class AnnouncementService : IAnnouncementService
    {
        #region IAnnouncementService Members

        public void CreateOrUpdate(PortalAnnouncement announcement)
        {
            // Create an instance of the Announcement DB component
            IAnnouncementsDb announcementDb = ModulesDataAccess.AnnouncementsDb;
            if (announcement.ItemId == 0)
            {
                // Add the announcement within the Announcements table
                announcementDb.AddAnnouncement(announcement.ModuleId, announcement.CreatedByUser, announcement.Title,
                                               announcement.ExpireDate, announcement.Description,
                                               announcement.MoreLink, announcement.MobileMoreLink);
            }
            else
            {
                // Update the announcement within the Announcements table
                announcementDb.UpdateAnnouncement(announcement.ItemId, announcement.CreatedByUser, announcement.Title,
                                                  announcement.ExpireDate, announcement.Description,
                                                  announcement.MoreLink, announcement.MobileMoreLink);
            }
        }

        #endregion
    }
}