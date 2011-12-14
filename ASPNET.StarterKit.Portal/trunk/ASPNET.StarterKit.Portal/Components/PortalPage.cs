using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPNET.StarterKit.Portal
{

    public class PortalDataSource : ObjectDataSource
    {
        public PortalDataSource()
        {
            
        }
    }

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