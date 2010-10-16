using System.Web.UI;

namespace ASPNET.StarterKit.Portal
{
    public static class PortalExtensions
    {
        public static string GetNavigateUrl(this Page page, string portalId, string tabId)
        {
            return page.GetRouteUrl("default-route", new {PortalId = portalId, TabId = tabId});
        }
    }
}