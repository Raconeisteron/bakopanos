using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class HtmlModule : PortalModuleControl
    {
        private IHtmlTextsDb _htmlTextsDb;

        [InjectionMethod]
        public void Initialize(IHtmlTextsDb htmlTextsDb)
        {
            _htmlTextsDb = htmlTextsDb;
        }

        /// <summary>
        /// The Page_Load event handler on this User Control is
        /// used to render a block of HTML or text to the page.  
        /// The text/HTML to render is stored in the HtmlText 
        /// database table.  This method uses the ASPNET.StarterKit.Portal.HtmlTextDB()
        /// data component to encapsulate all data functionality.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain the selected item from the HtmlText table
            List<PortalHtmlText> dr = _htmlTextsDb.GetHtmlText(ModuleId);

            if (dr.Count>0)
            {
                // Dynamically add the file content into the page
                string content = Server.HtmlDecode(dr[0].DesktopHtml);
                HtmlHolder.Controls.Add(new LiteralControl(content));
            }

        }
    }
}