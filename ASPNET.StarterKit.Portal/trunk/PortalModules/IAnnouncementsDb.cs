using System;
using System.Collections.Generic;
using ASPNET.StarterKit.Portal.PortalDao;

namespace ASPNET.StarterKit.Portal
{
    public interface IAnnouncementsDb
    {
        List<PortalAnnouncement> GetAnnouncements(int moduleId);
        PortalAnnouncement GetSingleAnnouncement(int itemId);
        void DeleteAnnouncement(int itemId);

        int AddAnnouncement(int moduleId, string userName, string title, DateTime expireDate,
                            string description, string moreLink, string mobileMoreLink);

        void UpdateAnnouncement(int itemId, string userName, string title, DateTime expireDate,
                                string description, string moreLink, string mobileMoreLink);
    }
}