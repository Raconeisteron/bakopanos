using System.Security.Principal;
using System.Web;

namespace ASPNET.StarterKit.Portal.Web
{
    public class PortalHttpPrincipalUtility : IPortalPrincipalUtility
    {
        public bool IsInRole(string role)
        {
            return HttpContext.Current.User.IsInRole(role);
        }
    }
}