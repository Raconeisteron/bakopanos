using System;

namespace Portal.Modules.Data
{
    public interface IAnnouncementDb : IDb
    {
        int AddAnnouncement(int moduleId, String userName, String title, DateTime expireDate,
                            String description, String moreLink, String mobileMoreLink);

        void UpdateAnnouncement(int itemId, String userName, String title, DateTime expireDate,
                                String description, String moreLink, String mobileMoreLink);
    }
}