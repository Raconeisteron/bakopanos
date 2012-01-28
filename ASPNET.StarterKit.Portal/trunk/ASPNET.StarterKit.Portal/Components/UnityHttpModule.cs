using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// http://msdn.microsoft.com/en-us/library/ff664358(v=PandP.50).aspx
    /// </summary>
    public class UnityHttpModule : IHttpModule
    {
        private HttpApplication _context;

        #region IHttpModule Members

        public void Init(HttpApplication context)
        {
            _context = context;
            context.PreRequestHandlerExecute += OnPreRequestHandlerExecute;
        }

        public void Dispose()
        {
        }

        #endregion

        private void OnPreRequestHandlerExecute(object sender, EventArgs e)
        {
            IHttpHandler currentHandler = HttpContext.Current.Handler;
            HttpContext.Current.GetContainer().BuildUp(
                currentHandler.GetType(), currentHandler);

            // User Controls are ready to be built up after page initialization is complete
            var currentPage = HttpContext.Current.Handler as Page;
            if (currentPage != null)
            {
                currentPage.InitComplete += OnPageInitComplete;
            }
        }

        // Build up each control in the page's control tree
        private void OnPageInitComplete(object sender, EventArgs e)
        {
            var currentPage = (Page) sender;
            IUnityContainer container = HttpContext.Current.GetContainer();

            IEnumerable<Control> controls = GetControlTree(currentPage);

            foreach (Control control in controls)
            {
                if (control is IUnityControl)
                {
                    container.BuildUp(control.GetType(), control);
                }
            }
            _context.PreRequestHandlerExecute -= OnPreRequestHandlerExecute;
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