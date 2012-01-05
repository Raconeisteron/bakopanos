using System;
using System.Web.UI;

namespace ASPNET.StarterKit.Portal
{
    public partial class NotImplemented : Page
    {
        //****************************************************************
        //
        // The Page_Load event on this Page is used to obtain the title
        // of the fictious content item.
        //
        //****************************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["title"] != null)
            {
                title.InnerHtml = Request.Params["title"];
            }
        }
    }
}