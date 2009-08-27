using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    public interface IDiscussionDB
    {
        DbDataReader GetTopLevelMessages(int moduleId);
        DbDataReader GetThreadMessages(String parent);
        DbDataReader GetSingleMessage(int itemId);
        int AddMessage(int moduleId, int parentId, String userName, String title, String body);
    }
}