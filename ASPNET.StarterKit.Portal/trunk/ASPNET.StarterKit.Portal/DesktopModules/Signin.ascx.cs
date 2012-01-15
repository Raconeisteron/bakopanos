using System;
using System.Web.Security;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class Signin : PortalModuleControl
    {
        private IPortalSecurity _portalSecurity;
        private IUsersDb _usersDb;

        [InjectionMethod]
        public void Initialize(IPortalSecurity portalSecurity, IUsersDb usersDb)
        {
            _portalSecurity = portalSecurity;
            _usersDb = usersDb;
        }

        protected void LoginBtn_Click(Object sender, ImageClickEventArgs e)
        {
            // Attempt to Validate User Credentials using UsersDB
            string userId = _usersDb.Login(email.Text, _portalSecurity.Encrypt(password.Text));

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