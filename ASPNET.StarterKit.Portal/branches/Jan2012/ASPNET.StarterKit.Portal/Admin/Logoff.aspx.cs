using System;
using System.Web.Security;
using System.Web.UI;

namespace ASPNET.StarterKit.Portal
{
    public partial class Logoff : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Log User Off from Cookie Authentication System
            FormsAuthentication.SignOut();

            // Invalidate roles token
            Response.Cookies["portalroles"].Value = null;
            Response.Cookies["portalroles"].Expires = new DateTime(1999, 10, 12);
            Response.Cookies["portalroles"].Path = "/";

            // Redirect user back to the Portal Home Page
            Response.Redirect(Global.GetApplicationPath(Request) + "/DesktopDefault.aspx");
        }
    }
}