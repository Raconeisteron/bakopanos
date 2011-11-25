using System;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DAL
{
    public interface ILinkDB
    {
        SqlDataReader GetLinks(int moduleId);
        SqlDataReader GetSingleLink(int itemId);
        void DeleteLink(int itemID);

        int AddLink(int moduleId, int itemId, String userName, String title, String url, String mobileUrl,
                    int viewOrder, String description);

        void UpdateLink(int moduleId, int itemId, String userName, String title, String url, String mobileUrl,
                        int viewOrder, String description);
    }
}