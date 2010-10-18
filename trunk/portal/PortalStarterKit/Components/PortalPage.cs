using System;
using System.Web;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace PortalStarterKit.Components
{
    public abstract class PortalPage<T> : Page, IPortalPage
        where T : IPortalPage
    {
        #region IPortalPage Members

        public ISiteConfigurationService SiteConfiguration { get; set; }
        public IPortalSecurity PortalSecurity { get; set; }

        public string NavigateUrl(string portalId, string tabId)
        {
                return this.GetNavigateUrl(portalId,tabId);
            
        }

        public string PortalId { get; private set; }
        public string TabId { get; private set; }

        #endregion

        protected override void OnPreInit(EventArgs e)
        {
            InjectDependencies();
            base.OnPreInit(e);
        }

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
            PortalSecurity = container.Resolve<IPortalSecurity>();
        }

        public static UserControl GetPortalModule(PortalPage<T> page, ModuleSettings module)
        {
            var portalModule = (IPortalControl)page.LoadControl(module.DesktopSrc);
            portalModule.PortalId = page.PortalId;
            portalModule.TabId = page.TabId;
            portalModule.ModuleConfiguration = module;
            portalModule.PortalSecurity = page.PortalSecurity;

            return (UserControl)portalModule;
        }
    }
}