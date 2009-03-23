using System;
using System.Data;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.Data
{
    public interface IAnnouncementsDB
    {
        DataSet GetAnnouncements(int moduleId);
        SqlDataReader GetSingleAnnouncement(int itemId);
        void DeleteAnnouncement(int itemID);

        int AddAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                            String description, String moreLink, String mobileMoreLink);

        void UpdateAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                                String description, String moreLink, String mobileMoreLink);
    }
}