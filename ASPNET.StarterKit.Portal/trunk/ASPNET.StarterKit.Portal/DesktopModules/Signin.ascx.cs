using System;
using System.Web.Security;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class Signin : PortalModuleControl
    {
        [Dependency]
        public IUsersDb Model { private get; set; }


        protected void LoginBtn_Click(Object sender, ImageClickEventArgs e)
        {
            // Attempt to Validate User Credentials using UsersDB
            String userId = Model.Login(email.Text, PortalSecurity.Encrypt(password.Text));

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