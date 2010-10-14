using System;
using System.Web;
using System.Web.UI;
using ASPNET.StarterKit.Portal;
using Microsoft.Practices.Unity;

namespace PortalStarterKit
{
    public class Global : HttpApplication, IContainerAccessor
    {
        /// <summary>
        /// The Unity container for the current application
        /// </summary>
        public static IUnityContainer Container { get; set; }

        /// <summary>
        /// Returns the Unity container of the application 
        /// </summary>
        IUnityContainer IContainerAccessor.Container
        {
            get
            {
                return Container;
            }
        }

        protected void Application_Start(object sender, EventArgs e)
        {

           BuildContainer();

        }

        protected void Application_End(object sender, EventArgs e)
        {

           CleanUp();

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

        public static void BuildItemWithCurrentContext<T>(Control ctrl)
        {
            Container.BuildUp(typeof(T), ctrl);
        }

        private static void BuildContainer()
        {
            IUnityContainer container = new UnityContainer();

            // Register the relevant types for the 
            // container here through classes or configuration
            // register the container in the container property

            container.RegisterType<ISiteConfigurationService, XmlSiteConfigurationService>(
                new ContainerControlledLifetimeManager());

            Container = container;

        }

        private static void CleanUp()
        {
            if (Container != null)
            {
                Container.Dispose();
            }
        }


    }
}