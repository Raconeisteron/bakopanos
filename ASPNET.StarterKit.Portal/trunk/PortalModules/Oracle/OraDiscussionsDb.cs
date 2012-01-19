using System;
using System.Data;

namespace ASPNET.StarterKit.Portal.Oracle
{
    public class OraDiscussionsDb : IDiscussionsDb
    {
        #region IDiscussionsDb Members

        public IDataReader GetTopLevelMessages(int moduleId)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetThreadMessages(string parent)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetSingleMessage(int itemId)
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