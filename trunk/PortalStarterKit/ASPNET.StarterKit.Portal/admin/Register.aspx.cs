using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// Summary description for Register.
    /// </summary>
    public class Register : Page
    {
        protected CompareValidator CompareValidator1;
        protected TextBox ConfirmPassword;
        protected TextBox Email;
        protected Label Message;
        protected TextBox Name;
        protected TextBox Password;
        protected LinkButton RegisterBtn;
        protected RegularExpressionValidator RegularExpressionValidator1;
        protected RequiredFieldValidator RequiredFieldValidator1;
        protected RequiredFieldValidator RequiredFieldValidator2;
        protected RequiredFieldValidator RequiredFieldValidator3;
        protected RequiredFieldValidator RequiredFieldValidator4;

        public Register()
        {
            Page.Init += Page_Init;
        }

        private void RegisterBtn_Click(object sender, EventArgs e)
        {
            // Only attempt a login if all form fields on the page are valid
            if (Page.IsValid)
            {
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
            this.RegisterBtn.Click += new System.EventHandler(this.RegisterBtn_Click);
        }

        #endregion
    }
}