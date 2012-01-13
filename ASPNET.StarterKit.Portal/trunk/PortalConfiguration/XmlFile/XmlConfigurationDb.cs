using System.Web;
using System.Web.Caching;

namespace ASPNET.StarterKit.Portal.XmlFile
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// tab configuration settings, module configuration settings and module 
    /// definition configuration settings from the PortalCfg.xml file.
    /// </summary>
    internal class XmlConfigurationDb : IConfigurationDb
    {
        private string _configFile;

        public XmlConfigurationDb(string configFile)
        {
            _configFile = configFile;
        }

        /// <summary>
        /// This method is used in Global.asax to
        /// push the settings into the current HttpContext, so that all of the 
        /// pages, content modules and classes throughout the rest of the request
        /// may access them.
        ///
        /// The SiteConfiguration object is cached using the ASP.NET Cache API,
        /// with a file-change dependency on the XML configuration file.  Normallly,
        /// this method just returns a copy of the object in the cache.  When the
        /// configuration is updated and changes are saved to the the XML file,
        /// the SiteConfiguration object is evicted from the cache.  The next time 
        /// this method runs, it will read from the XML file again and insert a
        /// fresh copy of the SiteConfiguration into the cache.
        /// </summary>
        /// <returns>
        /// returns a typed dataset of the all of the site configuration settings from the
        /// XML configuration file.
        ///</returns>
        public SiteConfiguration GetSiteSettings()
        {
            var siteSettings = (SiteConfiguration) HttpContext.Current.Cache["SiteSettings"];

            // If the SiteConfiguration isn't cached, load it from the XML file and add it into the cache.
            if (siteSettings == null)
            {
                // Create the dataset
                siteSettings = new SiteConfiguration();

                // Retrieve the location of the XML configuration file
                string configFile = HttpContext.Current.Server.MapPath(_configFile);

                // Set the AutoIncrement property to true for easier adding of rows
                siteSettings.Tab.TabIdColumn.AutoIncrement = true;
                siteSettings.Module.ModuleIdColumn.AutoIncrement = true;
                siteSettings.ModuleDefinition.ModuleDefIdColumn.AutoIncrement = true;

                // Load the XML data into the DataSet
                siteSettings.ReadXml(configFile);

                // Store the dataset in the cache
                HttpContext.Current.Cache.Insert("SiteSettings", siteSettings, new CacheDependency(configFile));
            }

            return siteSettings;
        }

        /// <summary>
        /// overwrites the the XML file with the
        /// settings in the SiteConfiguration object in context.  The object will in 
        /// turn be evicted from the cache and be reloaded from the XML file the next
        /// time GetSiteSettings() is called.
        /// </summary>
        public void SaveSiteSettings()
        {
            // Obtain SiteSettings from the Cache
            var siteSettings = (SiteConfiguration) HttpContext.Current.Cache["SiteSettings"];

            // Check the object
            if (siteSettings == null)
            {
                // If SaveSiteSettings() is called once, the cache is cleared.  If it is
                // then called again before Global.Application_BeginRequest is called, 
                // which reloads the cache, the siteSettings object will be Null 
                siteSettings = GetSiteSettings();
            }
            string configFile = HttpContext.Current.Server.MapPath(_configFile);

            // Object is evicted from the Cache here.  
            siteSettings.WriteXml(configFile);
        }
    }
}