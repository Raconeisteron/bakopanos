namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// TODO: null cache for now....
    /// </summary>
    public class PortalCacheUtility : IPortalCacheUtility
    {
        private object _siteSettings;

        #region IPortalCacheUtility Members

        public void InsertSiteSettings(object siteSettings, string configFile)
        {
            _siteSettings = siteSettings;
        }

        public object SiteSettings
        {
            get { return _siteSettings; }
        }

        #endregion
    }
}