using Portal.Contracts;
using Portal.Modules.DAL;

namespace Portal.Services
{
    internal class AnnouncementService : IAnnouncementService
    {
        public AnnouncementService()
        {
            
        }
        public int CreateOrUpdate(PortalAnnouncement announcement)
        {
            // Create an instance of the Announcement DB component
            IAnnouncementsDb announcementDb = ModulesDataAccess.AnnouncementsDb;
            int itemId = announcement.ItemId;
            if (itemId == 0)
            {
                // Add the announcement within the Announcements table
                itemId = announcementDb.AddAnnouncement(announcement.ModuleId, announcement.CreatedByUser, announcement.Title,
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
            return itemId;
        }
    }
}
