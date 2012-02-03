using System.Web;

namespace ASPNET.StarterKit.Portal.Web
{
    public class PortalHttpPrincipalUtility : IPortalPrincipalUtility
    {
        #region IPortalPrincipalUtility Members

        public bool IsInRole(string role)
        {
            return HttpContext.Current.User.IsInRole(role);
        }

        #endregion
    }
}