using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public interface IAnnouncementsDb
    {
        IDataReader GetAnnouncements(int moduleId);
        IDataReader GetSingleAnnouncement(int itemId);
        void DeleteAnnouncement(int itemId);

        int AddAnnouncement(int moduleId, string userName, string title, DateTime expireDate,
                            string description, string moreLink, string mobileMoreLink);

        void UpdateAnnouncement(int itemId, string userName, string title, DateTime expireDate,
                                string description, string moreLink, string mobileMoreLink);
    }
}