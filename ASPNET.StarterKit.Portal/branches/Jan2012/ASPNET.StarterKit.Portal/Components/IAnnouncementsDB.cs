using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public interface IAnnouncementsDb
    {
        DataSet GetAnnouncements(int moduleId);
        SqlDataReader GetSingleAnnouncement(int itemId);
        void DeleteAnnouncement(int itemID);

        int AddAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                                            String description, String moreLink, String mobileMoreLink);

        void UpdateAnnouncement(int moduleId, int itemId, String userName, String title, DateTime expireDate,
                                                String description, String moreLink, String mobileMoreLink);

        [InjectionMethod]
        void Initialize(ConnectionStringSettings connectionStringSettings);
    }
}