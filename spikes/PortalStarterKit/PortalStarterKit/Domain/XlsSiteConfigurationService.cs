using System.ComponentModel.Composition;
using System.Configuration;
using System.Web;

namespace PortalStarterKit.Domain
{
    [Export]
    public class XlsSiteConfigurationService : SiteConfigurationService
    {
        private readonly string _xlsSiteConfigurationFile;

        public XlsSiteConfigurationService()
        {
            _xlsSiteConfigurationFile =
                HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["XlsSiteConfigurationFile"]);
            //TODO:
            //
            InitializeSiteConfiguration(SiteConfiguration.Portal.Tabs);
        }
    }
}