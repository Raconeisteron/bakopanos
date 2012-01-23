using System.Collections.Generic;
using ASPNET.StarterKit.Portal.PortalDao;

namespace ASPNET.StarterKit.Portal
{
    public interface ILinksDb
    {
        List<PortalLink> GetLinks(int moduleId);
        PortalLink GetSingleLink(int itemId);
        void DeleteLink(int itemId);

        int AddLink(int moduleId, string userName, string title, string url, string mobileUrl,
                    int viewOrder, string description);

        void UpdateLink(int itemId, string userName, string title, string url, string mobileUrl,
                        int viewOrder, string description);
    }
}