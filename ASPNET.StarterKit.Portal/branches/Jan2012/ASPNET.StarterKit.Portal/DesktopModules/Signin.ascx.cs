using System;
using System.Web.Security;
using System.Web.UI;

namespace ASPNET.StarterKit.Portal
{
    public partial class Signin : PortalModuleControl
    {
        protected void LoginBtn_Click(Object sender, ImageClickEventArgs e)
        {
            // Attempt to Validate User Credentials using UsersDB
            var accountSystem = new UsersDB();
            string userId = accountSystem.Login(email.Text, PortalSecurity.Encrypt(password.Text));

            if (!string.IsNullOrEmpty(userId))
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
    }
}