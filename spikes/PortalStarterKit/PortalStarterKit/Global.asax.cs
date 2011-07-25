using System;
using System.Configuration;
using System.Web;
using PortalStarterKit.Data;

namespace PortalStarterKit
{
    public class Global : HttpApplication
    {       
        private void Application_Start(object sender, EventArgs e)
        {
            var configuration = (IComponentConfiguration)ConfigurationManager.GetSection("DataComponentConfiguration");            
            //deal with cache...
            Context.Cache.Insert("SiteConfiguration", configuration.ReadSiteConfiguration(Server.MapPath));
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