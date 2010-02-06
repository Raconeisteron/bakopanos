using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public interface ILinkDB
    {
        IDataReader GetLinks();
        IDataReader GetLinks(int moduleId);
        IDataReader GetSingleLink(int itemId);
        void DeleteLink(int itemId);
        int AddLink(int moduleId, int itemId, String userName, String title, String url, String mobileUrl, int viewOrder, String description);
        void UpdateLink(int moduleId, int itemId, String userName, String title, String url, String mobileUrl, int viewOrder, String description);
    }
}