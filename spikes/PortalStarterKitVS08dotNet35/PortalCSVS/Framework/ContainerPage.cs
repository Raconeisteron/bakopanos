using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal.Framework
{
    public abstract class ContainerPage<T> : Page
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