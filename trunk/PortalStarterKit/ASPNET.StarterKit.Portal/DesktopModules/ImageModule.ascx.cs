using System;
using System.Web.UI.WebControls;
using ASPNET.StarterKit.Portal.Components;

namespace ASPNET.StarterKit.Portal
{
    public partial  class ImageModule : PortalModuleControl
    {


        //*******************************************************
        //
        // The Page_Load event handler on this User Control uses
        // the Portal configuration system to obtain image details.
        // It then sets these properties on an <asp:Image> server control.
        //
        //*******************************************************

        public ImageModule()
        {
            Init += Page_Init;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var imageSrc = (String) Settings["src"];
            var imageHeight = (String) Settings["height"];
            var imageWidth = (String) Settings["width"];

            // Set Image Source, Width and Height Properties
            if ((imageSrc != null) && (imageSrc != ""))
            {
                Image1.ImageUrl = imageSrc;
            }

            if ((imageWidth != null) && (imageWidth != ""))
            {
                Image1.Width = Int32.Parse(imageWidth);
            }

            if ((imageHeight != null) && (imageHeight != ""))
            {
                Image1.Height = Int32.Parse(imageHeight);
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