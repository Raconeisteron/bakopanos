using System;
using System.Data;

namespace Portal.Modules.Data
{
    public interface IAnnouncementDb
    {
        IDataReader GetAnnouncements(int moduleId);
        IDataReader GetSingleAnnouncement(int itemId);
        void DeleteAnnouncement(int itemId);

        int AddAnnouncement(int moduleId, String userName, String title, DateTime expireDate,
                            String description, String moreLink, String mobileMoreLink);

        void UpdateAnnouncement(int itemId, String userName, String title, DateTime expireDate,
                                String description, String moreLink, String mobileMoreLink);
    }
}