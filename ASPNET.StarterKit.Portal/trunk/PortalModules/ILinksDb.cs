using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public interface ILinksDb
    {
        IDataReader GetLinks(int moduleId);
        IDataReader GetSingleLink(int itemId);
        void DeleteLink(int itemId);

        int AddLink(int moduleId, String userName, String title, String url, String mobileUrl,
                                    int viewOrder, String description);

        void UpdateLink(int itemId, String userName, String title, String url, String mobileUrl,
                                        int viewOrder, String description);
    }
}