using System;
using System.Web.UI;

namespace Portal
{
    public partial class CDefault : Page
    {
        public CDefault()
        {
            Page.Init += Page_Init;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Browser["IsMobileDevice"] == "true")
            {
                Response.Redirect("MobileDefault.aspx");
            }
            else
            {
                Response.Redirect("DesktopDefault.aspx");
            }
        }

        protected void Page_Init(object sender, EventArgs e)
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
        }

        #endregion
    }
}