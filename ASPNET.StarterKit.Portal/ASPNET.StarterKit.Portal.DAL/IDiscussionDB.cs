using System;
using System.Data;

namespace ASPNET.StarterKit.Portal.DAL
{
    public interface IDiscussionDB
    {
        IDataReader GetTopLevelMessages(int moduleId);
        IDataReader GetThreadMessages(String parent);
        IDataReader GetSingleMessage(int itemId);
        int AddMessage(int moduleId, int parentId, String userName, String title, String body);
    }
}