using System;
using System.Web.Security;
using System.Web.UI;
using ASPNET.StarterKit.Portal.Components;
using ASPNET.StarterKit.Portal.Data;
using ASPNET.StarterKit.Portal.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.DesktopModules
{
    public partial class Signin : PortalModuleControl<Signin>
    {
        public Signin()
        {
            Init += Page_Init;
        }

        protected void LoginBtn_Click(Object sender, ImageClickEventArgs e)
        {
            // Attempt to Validate User Credentials using UsersDB
            IUsersDB accountSystem = new UsersDB();
            String userId = accountSystem.Login(email.Text, PortalSecurity.Encrypt(password.Text));

            if ((userId != null) && (userId != ""))
            {
                // Use security system to set the UserID within a client-side Cookie
                FormsAuthentication.SetAuthCookie(email.Text, RememberCheckbox.Checked);

                // Redirect browser back to originating page
                Response.Redirect(Request.ApplicationPath);
            }
            else
            {
                Message.Text = "<" + "br" + ">Login Failed!" + "<" + "br" + ">";
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

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        #endregion
    }
}