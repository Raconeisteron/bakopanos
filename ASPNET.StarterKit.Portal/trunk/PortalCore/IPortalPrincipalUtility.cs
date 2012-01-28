using System.Security.Principal;

namespace ASPNET.StarterKit.Portal
{
    public interface IPortalPrincipalUtility
    {
        IPrincipal GetUser();
    }
}