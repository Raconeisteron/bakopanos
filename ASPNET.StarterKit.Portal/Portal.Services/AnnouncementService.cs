using Portal.Contracts;
using Portal.Modules.DAL;

namespace Portal.Services
{
    internal class LinkService : ILinkService
    {
        public LinkService()
        {

        }
        public void CreateOrUpdate(PortalLink item)
        {
            // Create an instance of the Announcement DB component
            ILinksDb linksDb = ModulesDataAccess.LinkDb;
            if (item.ItemId == 0)
            {
                // Add the announcement within the Announcements table
                linksDb.AddLink(item.ModuleId, item.CreatedByUser, item.Title, item.Url, item.MobileUrl,
                                item.ViewOrder, item.Description);
            }
            else
            {
                // Update the announcement within the Announcements table
                linksDb.UpdateLink(item.ItemId, item.CreatedByUser, item.Title, item.Url, item.MobileUrl,
                                item.ViewOrder, item.Description);
            }
        }
    }

    internal class AnnouncementService : IAnnouncementService
    {
        public AnnouncementService()
        {
            
        }
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
    }
}
