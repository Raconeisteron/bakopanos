using System;
using System.Web.UI;

namespace ASPNET.StarterKit.Portal
{
    public partial class CDefault : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.Browser["IsMobileDevice"] == "true")
            //{
            //    Response.Redirect("MobileDefault.aspx");
            //}
            //else
            //{
            //    Response.Redirect("DesktopDefault.aspx");
            //}
            Response.Redirect("DesktopDefault.aspx");
        }
    }
}