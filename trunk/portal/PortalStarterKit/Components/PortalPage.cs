using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using Microsoft.Practices.Unity;
using PortalStarterKit;

namespace ASPNET.StarterKit.Portal
{
    public abstract class PortalPage<T> : Page, IPortalPage
        where T : IPortalPage
    {
        public ISiteConfigurationService SiteConfiguration
        {
            get;
            set;
        }
        public string GetNavigateUrl(string portalId, string tabId)
        {
            return Page.GetNavigateUrl(portalId, tabId);
        }
        protected override void OnPreInit(EventArgs e)
        {
            InjectDependencies();
            base.OnPreInit(e);
        }

        public string PortalId { get; private set; }
        public string TabId { get; private set; }

        protected override void OnInit(EventArgs e)
        {
            PortalId = Page.RouteData.Values["portalId"] as string ??
                       SiteConfiguration.GetPortals()[0].PortalId;

            TabId = Page.RouteData.Values["tabId"] as string ??
                    SiteConfiguration.GetTabs(PortalId)[0].TabId;
            base.OnInit(e);
        }

        protected virtual void InjectDependencies()
        {
            var context = HttpContext.Current;
            if (context == null)
            {
                return;
            }

            var accessor = context.ApplicationInstance as IContainerAccessor;
            if (accessor == null)
            {
                return;
            }

            var container = accessor.Container;
            if (container == null)
            {
                throw new InvalidOperationException("No Unity container found");
            }

            container.BuildUp(typeof(T), this, string.Empty);
            SiteConfiguration = container.Resolve<ISiteConfigurationService>();
        }

        
    }
}