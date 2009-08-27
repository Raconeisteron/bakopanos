using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    public interface ILinkDB
    {
        DbDataReader GetLinks(int moduleId);
        DbDataReader GetSingleLink(int itemId);
        void DeleteLink(int itemID);

        int AddLink(int moduleId, int itemId, String userName, String title, String url, String mobileUrl,
                                    int viewOrder, String description);

        void UpdateLink(int moduleId, int itemId, String userName, String title, String url, String mobileUrl,
                                        int viewOrder, String description);
    }
}