using System;

namespace Portal.Modules.Data
{
    public interface IAnnouncementDb : IDb
    {
        int AddAnnouncement(int moduleId, string userName, string title, DateTime expireDate,
                            string description, string moreLink, string mobileMoreLink);

        void UpdateAnnouncement(int itemId, string userName, string title, DateTime expireDate,
                                string description, string moreLink, string mobileMoreLink);
    }
}