using System.Data;

namespace Portal.Modules.DAL
{
    public interface IDiscussionsDb
    {
        IDataReader GetTopLevelMessages(int moduleId);
        IDataReader GetThreadMessages(string parent);
        IDataReader GetSingleMessage(int itemId);
        int AddMessage(int moduleId, int parentId, string userName, string title, string body);
    }
}