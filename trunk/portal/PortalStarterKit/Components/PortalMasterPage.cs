using System;
using System.Web;
using System.Web.UI;
using PortalStarterKit;

namespace ASPNET.StarterKit.Portal
{
    public abstract class PortalMasterPage<T> : MasterPage
        where T : class
    {
        private IContainerAccessor _accessor;

        public string PortalId { get; private set; }
        public string TabId { get; private set; }

        protected override void OnInit(EventArgs e)
        {
            InjectDependencies();

            PortalId = Page.RouteData.Values["portalId"] as string;
            TabId = Page.RouteData.Values["tabId"] as string;

            base.OnInit(e);
        }
        protected virtual void InjectDependencies()
        {
            var context = HttpContext.Current;
            if (context == null)
            {
                return;
            }

            _accessor = context.ApplicationInstance as IContainerAccessor;
            if (_accessor == null)
            {
                return;
            }

            var container = _accessor.Container;
            if (container == null)
            {
                throw new InvalidOperationException("No Unity container found");
            }

            container.BuildUp(typeof(T), this, string.Empty);
        }

       
    }
}