using System.Security.Principal;

namespace ASPNET.StarterKit.Portal
{
    public interface IPortalPrincipalUtility
    {

        /// <summary>
        /// enables developers to easily check the role
        /// status of the current browser client.
        /// </summary>
        bool IsInRole(string userName);
    }
}