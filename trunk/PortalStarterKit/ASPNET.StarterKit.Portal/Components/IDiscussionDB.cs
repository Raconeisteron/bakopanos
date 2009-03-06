using System;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.Components
{
    public interface IDiscussionDB
    {
        SqlDataReader GetTopLevelMessages(int moduleId);
        SqlDataReader GetThreadMessages(String parent);
        SqlDataReader GetSingleMessage(int itemId);
        int AddMessage(int moduleId, int parentId, String userName, String title, String body);
    }
}