using System;
using System.Data;
using System.Data.Common;

namespace ASPNET.StarterKit.Portal
{
    public interface IAnnouncementsDb
    {
        DataSet GetAnnouncements(int moduleId);
        AnnouncementItem GetSingleAnnouncement(int itemId);
        void DeleteAnnouncement(int itemID);

        int AddAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                            String description, String moreLink, String mobileMoreLink);

        void UpdateAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                                String description, String moreLink, String mobileMoreLink);
    }
}