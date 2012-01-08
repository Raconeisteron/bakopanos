using System;
using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal
{
    public interface IAnnouncementsRepository
    {
        List<Announcement> GetAnnouncements(int moduleId);
        Announcement GetSingleAnnouncement(int itemId);

        void DeleteSingleAnnouncement(int itemId);

        Announcement SaveAnnouncement(int itemId, int moduleId, String userName, String title, DateTime expireDate,
                            String description, String moreLink, String mobileMoreLink);
    }
}