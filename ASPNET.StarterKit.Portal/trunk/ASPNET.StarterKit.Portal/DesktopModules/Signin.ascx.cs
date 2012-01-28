using System;
using System.Web.Security;
using System.Web.UI;
using ASPNET.StarterKit.Portal.Security.Cryptography;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class Signin : PortalModuleControl
    {
        private IUsersDb _usersDb;

        [InjectionMethod]
        public void Initialize(IUsersDb usersDb)
        {
            _usersDb = usersDb;
        }

        protected void LoginBtn_Click(Object sender, ImageClickEventArgs e)
        {
            // Attempt to Validate User Credentials using UsersDB
            string userId = _usersDb.Login(email.Text, Encryption.Encrypt(password.Text));

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