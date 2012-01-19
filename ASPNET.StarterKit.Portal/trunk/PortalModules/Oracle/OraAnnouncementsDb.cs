using System;
using System.Data;

namespace ASPNET.StarterKit.Portal.Oracle
{
    public class OraAnnouncementsDb: IAnnouncementsDb
    {
        public IDataReader GetAnnouncements(int moduleId)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetSingleAnnouncement(int itemId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAnnouncement(int itemId)
        {
            throw new NotImplementedException();
        }

        public int AddAnnouncement(int moduleId, string userName, string title, DateTime expireDate, string description, string moreLink, string mobileMoreLink)
        {
            throw new NotImplementedException();
        }

        public void UpdateAnnouncement(int itemId, string userName, string title, DateTime expireDate, string description, string moreLink, string mobileMoreLink)
        {
            throw new NotImplementedException();
        }
    }
}