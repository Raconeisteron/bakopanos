using System;
using System.Web.UI;

namespace ASPNET.StarterKit.Portal
{
    public class EditAccessDenied : Page
    {
        public EditAccessDenied()
        {
            Page.Init += Page_Init;
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
        }

        #endregion
    }
}