using System;
using System.Data;

namespace ASPNET.StarterKit.Portal.DAL
{
    public interface IAnnouncementsDb
    {
        DataSet GetAnnouncements(int moduleId);
        IDataReader GetSingleAnnouncement(int itemId);
        void DeleteAnnouncement(int itemId);

        int AddAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                            String description, String moreLink, String mobileMoreLink);

        void UpdateAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                                String description, String moreLink, String mobileMoreLink);
    }
}