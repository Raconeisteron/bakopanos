using System.Security.Principal;
using System.Web;

namespace ASPNET.StarterKit.Portal.Web
{
    public class PortalHttpPrincipalUtility : IPortalPrincipalUtility
    {
        public IPrincipal GetUser()
        {
            return HttpContext.Current.User;
        }
    }
}