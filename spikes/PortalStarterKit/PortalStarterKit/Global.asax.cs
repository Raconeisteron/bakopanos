using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Web;
using PortalStarterKit.Domain;

namespace PortalStarterKit
{
    public class Global : HttpApplication
    {
        [Import]
        private XmlSiteConfigurationService _service;

        private void Application_Start(object sender, EventArgs e)
        {
            //Find the assembly (.dll) that has the stuff we need             
            var catalog = new DirectoryCatalog(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"));

            //To do anything with the stuff in the catalog, 
            //we need to put into a container (Which has methods to do the magic stuff)
            var container = new CompositionContainer(catalog);

            //Now lets do the magic bit - Wiring everything up
            container.ComposeParts(this);

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