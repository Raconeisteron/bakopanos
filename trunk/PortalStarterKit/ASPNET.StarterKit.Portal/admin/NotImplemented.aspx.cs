using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace ASPNET.StarterKit.Portal
{
    public class NotImplemented : Page
    {
        protected HtmlGenericControl title;

        //****************************************************************
        //
        // The Page_Load event on this Page is used to obtain the title
        // of the fictious content item.
        //
        //****************************************************************

        public NotImplemented()
        {
            Page.Init += Page_Init;
        }

        private void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["title"] != null)
            {
                title.InnerHtml = Request.Params["title"];
            }
        }

        private void Page_Init(object sender, EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
        }

        #region Web Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        #endregion
    }
}