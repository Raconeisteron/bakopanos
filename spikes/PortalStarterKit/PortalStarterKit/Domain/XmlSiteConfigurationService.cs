using System.ComponentModel.Composition;
using System.Configuration;
using System.IO;
using System.Web;
using System.Xml.Serialization;

namespace PortalStarterKit.Domain
{
    [Export]
    public class XmlSiteConfigurationService : SiteConfigurationService
    {
        private readonly string _xmlSiteConfigurationFile;

        public XmlSiteConfigurationService()
        {
            _xmlSiteConfigurationFile =
                HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["XmlSiteConfigurationFile"]);

            // Code that runs on application startup            
            var serializer = new XmlSerializer(typeof (SiteConfiguration));
            var fs = new FileStream(_xmlSiteConfigurationFile, FileMode.Open, FileAccess.Read, FileShare.Read);
            SiteConfiguration = (SiteConfiguration) serializer.Deserialize(fs);

            //
            InitializeSiteConfiguration(SiteConfiguration.Portal.Tabs);
        }
    }
}