using System;
using System.Web;

namespace ASPNET.StarterKit.Portal.XmlFile
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// tab configuration settings, module configuration settings and module 
    /// definition configuration settings from the PortalCfg.xml file.
    /// </summary>
    public class XmlPortalConfigurationDb : IPortalConfigurationDb
    {
        private readonly IConfigurationDb _configurationDb;

        public XmlPortalConfigurationDb(IConfigurationDb configurationDb)
        {
            _configurationDb = configurationDb;
        }

        #region IPortalConfigurationDb Members

        public void UpdatePortalInfo(int portalId, String portalName, bool alwaysShow)
        {
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration) HttpContext.Current.Items["SiteSettings"];

            // Get first record of the "Global" element 
            SiteConfiguration.GlobalRow globalRow = siteSettings.Global.FindByPortalId(portalId);

            // Update the values
            globalRow.PortalId = portalId;
            globalRow.PortalName = portalName;
            globalRow.AlwaysShowEditButton = alwaysShow;

            // Save the changes 
            _configurationDb.SaveSiteSettings();
        }

        #endregion
    }
}