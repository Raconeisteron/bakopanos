using System.Web.UI;

namespace ASPNET.StarterKit.Portal
{
    public class PortalPage<T> : Page
    {
        protected T Model { get; private set; }

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            Model = DataAccess.Resolve<T>();
        }
    }
}