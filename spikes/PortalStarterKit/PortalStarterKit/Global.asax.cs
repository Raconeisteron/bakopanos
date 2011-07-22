using System;
using System.Configuration;
using System.Web;
using PortalStarterKit.Data;

namespace PortalStarterKit
{
    public class Global : HttpApplication
    {
        private ISiteConfigurationService _service;

        private void Application_Start(object sender, EventArgs e)
        {
            switch (ConfigurationManager.AppSettings["SiteConfigurationService"])
            {
                case "Xls":
                    Type xlsType = Type.GetType("PortalStarterKit.Data.Xls.XlsSiteConfigurationService,PortalStarterKit.Data.Xls");
                string xlsFile =
                    HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["XlsSiteConfigurationFile"]);
                _service = (ISiteConfigurationService)Activator.CreateInstance(xlsType, new object[] { xlsFile });
                    break;
                case "Xml":
                    Type xmlType = Type.GetType("PortalStarterKit.Data.Xml.XmlSiteConfigurationService,PortalStarterKit.Data.Xml");
                string xmlFile =
                    HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["XmlSiteConfigurationFile"]);
                _service = (ISiteConfigurationService) Activator.CreateInstance(xmlType, new object[] {xmlFile});
                    break;
            }
            
            //deal with cache...
            Context.Cache.Insert("SiteConfiguration", _service.SiteConfiguration);
        }

        private void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown
        }

        private void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs
        }

        private void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
        }

        private void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.
        }
    }
}