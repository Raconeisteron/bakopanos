using System;
using System.Data;

namespace ASPNET.StarterKit.Portal.DAL
{
    public interface IAnnouncementsDB
    {
        DataSet GetAnnouncements(int moduleId);
        IDataReader GetSingleAnnouncement(int itemId);
        void DeleteAnnouncement(int itemID);

        int AddAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                            String description, String moreLink, String mobileMoreLink);

        void UpdateAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                                String description, String moreLink, String mobileMoreLink);
    }
}