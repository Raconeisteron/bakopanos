using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PortalStarterKit.Core;

namespace PortalStarterKit.DesktopModules
{
    public partial class HtmlModule : PortalModule<HtmlModule>
    {
        /// <summary>
        /// Used to render a block of HTML or text to the page.
        /// The text/HTML to render is stored in the HtmlText
        /// database table.  This method uses the ASPNET.StarterKit.Portal.HtmlTextDB()
        /// data component to encapsulate all data functionality. 
        /// </summary>
        protected void Page_Load(Object sender, EventArgs e)
        {
            // Dynamically add the file content into the page
            String content = Server.HtmlDecode("<strong>ok</strong>");
            HtmlHolder.Controls.Add(new LiteralControl(content));

        }
    }
}