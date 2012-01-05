using System;
using System.Web.Security;
using System.Web.UI;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// Summary description for Register.
    /// </summary>
    public partial class Register : Page
    {
        protected void RegisterBtn_Click(object sender, EventArgs e)
        {
            // Only attempt a login if all form fields on the page are valid
            if (!Page.IsValid) return;

            // Add New User to Portal User Database
            var accountSystem = new UsersDB();

            if ((accountSystem.AddUser(Name.Text, Email.Text, PortalSecurity.Encrypt(Password.Text))) > -1)
            {
                // Set the user's authentication name to the userId
                FormsAuthentication.SetAuthCookie(Email.Text, false);

                // Redirect browser back to home page
                Response.Redirect("~/DesktopDefault.aspx");
            }
            else
            {
                Message.Text = "Registration Failed!  <" + "u" + ">" + Email.Text + "<" + "/u" +
                               "> is already registered." + "<" + "br" + ">" +
                               "Please register using a different email address.";
            }
        }
    }
}