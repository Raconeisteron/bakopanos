using System;
using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal.Oracle
{
    public class OraLinksDb : ILinksDb
    {
        #region ILinksDb Members

        public List<PortalLink> GetLinks(int moduleId)
        {
            throw new NotImplementedException();
        }

        public PortalLink GetSingleLink(int itemId)
        {
            throw new NotImplementedException();
        }

        public void DeleteLink(int itemId)
        {
            throw new NotImplementedException();
        }

        public int AddLink(int moduleId, string userName, string title, string url, string mobileUrl, int viewOrder,
                           string description)
        {
            throw new NotImplementedException();
        }

        public void UpdateLink(int itemId, string userName, string title, string url, string mobileUrl, int viewOrder,
                               string description)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}