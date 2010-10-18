using System;
using System.Web;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace PortalStarterKit.Components
{
    public abstract class PortalMasterPage<T> : MasterPage, IPortalPage
        where T : IPortalPage
    {
        #region IPortalPage Members

        public ISiteConfigurationService SiteConfiguration { get; set; }
        public PortalSecurity PortalSecurity { get; set; }

        public string PortalId { get; private set; }
        public string TabId { get; private set; }

        public string GetNavigateUrl(string portalId, string tabId)
        {
            return Page.GetNavigateUrl(portalId, tabId);
        }

        #endregion

        protected override void OnInit(EventArgs e)
        {
            InjectDependencies();

            PortalId = Page.RouteData.Values["portalId"] as string ??
                       SiteConfiguration.GetPortals()[0].PortalId;

            TabId = Page.RouteData.Values["tabId"] as string ??
                    SiteConfiguration.GetTabs(PortalId)[0].TabId;

            base.OnInit(e);
        }

        protected virtual void InjectDependencies()
        {
            HttpContext context = HttpContext.Current;
            if (context == null)
            {
                return;
            }

            var accessor = context.ApplicationInstance as IContainerAccessor;
            if (accessor == null)
            {
                return;
            }

            IUnityContainer container = accessor.Container;
            if (container == null)
            {
                throw new InvalidOperationException("No Unity container found");
            }

            container.BuildUp(typeof (T), this, string.Empty);
            SiteConfiguration = container.Resolve<ISiteConfigurationService>();
            PortalSecurity = container.Resolve<PortalSecurity>();
        }
    }
}