using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ASPNET.StarterKit.Portal.Oracle
{
    public class OraAnnouncementsDb : IAnnouncementsDb
    {
        #region IAnnouncementsDb Members

        public Collection<PortalAnnouncement> GetAnnouncements(int moduleId)
        {
            throw new NotImplementedException();
        }

        public PortalAnnouncement GetSingleAnnouncement(int itemId)
        {
            throw new NotImplementedException();
        }

        public void DeleteAnnouncement(int itemId)
        {
            throw new NotImplementedException();
        }

        public int AddAnnouncement(int moduleId, string userName, string title, DateTime expireDate, string description,
                                   string moreLink, string mobileMoreLink)
        {
            throw new NotImplementedException();
        }

        public void UpdateAnnouncement(int itemId, string userName, string title, DateTime expireDate,
                                       string description, string moreLink, string mobileMoreLink)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}