using System.Web.UI;

namespace ASPNET.StarterKit.Portal
{
    public class PortalPage<T> : Page
    {
        protected T DataAccess { get; private set; }

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            DataAccess = StarterKit.Portal.DataAccess.Resolve<T>();
        }
    }
}