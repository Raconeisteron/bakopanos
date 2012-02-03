using System.Web;

namespace ASPNET.StarterKit.Portal.Web
{
    public class PortalHttpServerUtility : IPortalServerUtility
    {
        #region IPortalServerUtility Members

        public string MapPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }

        #endregion
    }
}