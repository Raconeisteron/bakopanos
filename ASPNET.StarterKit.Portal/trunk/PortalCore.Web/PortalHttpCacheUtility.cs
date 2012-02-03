using System.Web;
using System.Web.Caching;

namespace ASPNET.StarterKit.Portal.Web
{
    public class PortalHttpCacheUtility : IPortalCacheUtility
    {
        #region IPortalCacheUtility Members

        public void InsertSiteSettings(object siteSettings, string configFile)
        {
            HttpContext.Current.Cache.Insert("SiteSettings", siteSettings, new CacheDependency(configFile));
        }

        public object SiteSettings
        {
            get { return HttpContext.Current.Cache["SiteSettings"]; }
        }

        #endregion
    }
}