using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ASPNET.StarterKit.Portal
{
    public interface IAnnouncementsDb
    {
        Collection<PortalAnnouncement> GetAnnouncements(int moduleId);
        PortalAnnouncement GetSingleAnnouncement(int itemId);
        void DeleteAnnouncement(int itemId);

        int AddAnnouncement(int moduleId, string userName, string title, DateTime expireDate,
                            string description, string moreLink, string mobileMoreLink);

        void UpdateAnnouncement(int itemId, string userName, string title, DateTime expireDate,
                                string description, string moreLink, string mobileMoreLink);
    }
}