using System;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using ASPNET.StarterKit.Portal.Components;

namespace ASPNET.StarterKit.Portal
{
    public abstract class HtmlModule : PortalModuleControl
    {
        protected HtmlTableCell HtmlHolder;
        protected HtmlTable t1;

        //*******************************************************
        //
        // The Page_Load event handler on this User Control is
        // used to render a block of HTML or text to the page.  
        // The text/HTML to render is stored in the HtmlText 
        // database table.  This method uses the ASPNET.StarterKit.Portal.HtmlTextDB()
        // data component to encapsulate all data functionality.
        //
        //*******************************************************

        public HtmlModule()
        {
            Init += Page_Init;
        }

        private void Page_Load(object sender, EventArgs e)
        {
            // Obtain the selected item from the HtmlText table
            var text = new HtmlTextDB();
            SqlDataReader dr = text.GetHtmlText(ModuleId);

            if (dr.Read())
            {
                // Dynamically add the file content into the page
                String content = Server.HtmlDecode((String) dr["DesktopHtml"]);
                HtmlHolder.Controls.Add(new LiteralControl(content));
            }

            // Close the datareader
            dr.Close();
        }

        private void Page_Init(object sender, EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
        }

        #region Web Form Designer generated code

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        #endregion
    }
}