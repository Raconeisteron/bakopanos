using System;
using System.Collections.ObjectModel;

namespace ASPNET.StarterKit.Portal.Oracle
{
    public class OraDiscussionsDb : IDiscussionsDb
    {
        #region IDiscussionsDb Members

        public Collection<PortalDiscussion> GetTopLevelMessages(int moduleId)
        {
            throw new NotImplementedException();
        }

        public Collection<PortalDiscussion> GetThreadMessages(string parent)
        {
            throw new NotImplementedException();
        }

        public PortalDiscussion GetSingleMessage(int itemId)
        {
            throw new NotImplementedException();
        }

        public int AddMessage(int moduleId, int parentId, string userName, string title, string body)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}