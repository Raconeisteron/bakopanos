using System;
using System.Collections.ObjectModel;
using Portal.Modules.Data;

namespace Portal.Modules.Service
{
    internal class AnnouncementService : IAnnouncementService
    {
        #region IAnnouncementService Members

        public PortalAnnouncement CreateOrUpdate(PortalAnnouncement announcement)
        {
            // Create an instance of the Announcement DB component
            IAnnouncementsDb announcementDb = ModulesDataAccess.AnnouncementsDb;
            if (announcement.ItemId == 0)
            {
                // Add the announcement within the Announcements table
                announcement.ItemId = announcementDb.AddAnnouncement(announcement.ModuleId, announcement.CreatedByUser,
                                                                     announcement.Title,
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
            return announcement;
        }

        public Collection<PortalAnnouncement> GetAnnouncements(int moduleId)
        {
            IAnnouncementsDb db = ModulesDataAccess.AnnouncementsDb;
            var reader = db.GetAnnouncements(moduleId);

            var items = new Collection<PortalAnnouncement>();
    
            while (reader.Read())
            {
                var item = new PortalAnnouncement
                               {
                                   ItemId = Convert.ToInt32(reader["ItemId"]),
                                   ModuleId = moduleId,
                                   Title = (string) reader["Title"],
                                   Description = (string) reader["Description"],
                                   CreatedByUser = (string) reader["CreatedByUser"],
                                   CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                   MoreLink = (string) reader["MoreLink"],
                                   MobileMoreLink = (string) reader["MobileMoreLink"],
                                   ExpireDate = Convert.ToDateTime(reader["ExpireDate"])
                               };
                items.Add(item);
            }

            reader.Close();

            return items;
        }
        #endregion
    }
}