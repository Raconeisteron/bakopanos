using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using Evolutil.Library;
using Evolutil.Library.Log;
using Evolutil.ServiceContracts;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Evolutil.WebForms
{
    public class Global : HttpApplication, IContainerAccessor
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Container = new Bootstraper().Start();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            if (Container != null)
            {
                Container.Dispose();
            }

        }
        /// <summary>
        /// Returns the Unity container of the application 
        /// </summary>
        IUnityContainer IContainerAccessor.Container
        {
            get { return Container; }
        }

        /// <summary>
        /// The Unity container for the current application
        /// </summary>
        public static IUnityContainer Container
        {
            get;
            set;
        }

        public static void BuildItemWithCurrentContext<T>(Control ctrl)
        {
            Container.BuildUp(typeof(T), ctrl);
        }

    }

    public interface IContainerAccessor
    {
        IUnityContainer Container { get; }
    }
    public abstract class BasePage<T> : Page
        where T : class
    {
        private IContainerAccessor accessor;

        protected override void OnPreInit(EventArgs e)
        {
            InjectDependencies();
            base.OnPreInit(e);
        }

        protected virtual void InjectDependencies()
        {
            var context = HttpContext.Current;
            if (context == null)
            {
                return;
            }

            accessor = context.ApplicationInstance as IContainerAccessor;
            if (accessor == null)
            {
                return;
            }

            var container = accessor.Container;
            if (container == null)
            {
                throw new InvalidOperationException("No Unity container found");
            }

            container.BuildUp(this as T);
        }

        public void BuildUpControl(Control ctrl)
        {
            accessor.Container.BuildUp(ctrl);
        }

    }


}