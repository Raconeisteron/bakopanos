using System;
using System.Data;

namespace Portal.Modules.DAL
{
    public interface IAnnouncementsDb
    {
        IDataReader GetAnnouncements(int moduleId);
        IDataReader GetSingleAnnouncement(int itemId);
        void DeleteAnnouncement(int itemId);

        int AddAnnouncement(int moduleId, int itemId, string userName, string title, DateTime expireDate,
                            string description, string moreLink, string mobileMoreLink);

        void UpdateAnnouncement(int moduleId, int itemId, string userName, string title, DateTime expireDate,
                                string description, string moreLink, string mobileMoreLink);
    }
}