using System;
using System.Data;
using System.Web.UI;
using ASPNET.StarterKit.Portal.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    public partial class HtmlModule : PortalModuleControl
    {
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
            IHtmlTextsDb text = ComponentManager.Resolve<IHtmlTextsDb>();
            IDataReader dr = text.GetHtmlText(ModuleId);

            if (dr.Read())
            {
                // Dynamically add the file content into the page
                string content = Server.HtmlDecode((String) dr["DesktopHtml"]);
                HtmlHolder.Controls.Add(new LiteralControl(content));
            }

            // Close the datareader
            dr.Close();
        }
    }
}