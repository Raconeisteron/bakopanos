using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public class UnityPageHandlerFactory : PageHandlerFactory
    {
        readonly IUnityContainer _container = HttpContext.Current.Application.GetContainer();

        public override IHttpHandler GetHandler(HttpContext context, string requestType, string virtualPath, string path)
        {
            var page = (Page)base.GetHandler(context, requestType, virtualPath, path);
            
            if (page != null)
            {
                _container.BuildUp(page);

                page.InitComplete += page_InitComplete;
            }          
            return page; 
        }

        void page_InitComplete(object sender, EventArgs e)
        {
            IEnumerable<Control> controls = GetControlTree((Page)sender);

            foreach (Control control in controls)
            {
                if (control is PortalModuleControl)
                {
                    _container.BuildUp(control.GetType(), control);
                }
            }
        }

        // Get the controls in the page's control tree excluding the page itself
        private static IEnumerable<Control> GetControlTree(Control root)
        {
            foreach (Control child in root.Controls)
            {
                yield return child;
                foreach (Control c in GetControlTree(child))
                {
                    yield return c;
                }
            }
        }
    }
}