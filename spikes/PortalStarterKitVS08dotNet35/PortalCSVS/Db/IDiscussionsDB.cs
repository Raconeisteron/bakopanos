using System;
using System.Data.Common;

namespace ASPNET.StarterKit.Portal
{
    public interface IDiscussionsDb
    {
        DbDataReader GetTopLevelMessages(int moduleId);
        DbDataReader GetThreadMessages(String parent);
        DbDataReader GetSingleMessage(int itemId);
        int AddMessage(int moduleId, int parentId, String userName, String title, String body);
    }
}