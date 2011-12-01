using System;
using System.Web.Security;
using System.Web.UI;

namespace Portal.Admin
{
    public partial class Logoff : Page
    {
        public Logoff()
        {
            Page.Init += Page_Init;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Log User Off from Cookie Authentication System
            FormsAuthentication.SignOut();

            // Invalidate roles token
            Response.Cookies["portalroles"].Value = null;
            Response.Cookies["portalroles"].Expires = new DateTime(1999, 10, 12);
            Response.Cookies["portalroles"].Path = "/";

            // Redirect user back to the Portal Home Page
            Response.Redirect(Global.GetApplicationPath(Request));
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