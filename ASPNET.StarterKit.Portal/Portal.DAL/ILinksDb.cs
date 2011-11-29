using System.Data;

namespace ASPNET.StarterKit.Portal.Modules.DAL
{
    public interface ILinksDb
    {
        IDataReader GetLinks(int moduleId);
        IDataReader GetSingleLink(int itemId);
        void DeleteLink(int itemId);

        int AddLink(int moduleId, int itemId, string userName, string title, string url, string mobileUrl,
                    int viewOrder, string description);

        void UpdateLink(int moduleId, int itemId, string userName, string title, string url, string mobileUrl,
                        int viewOrder, string description);
    }
}