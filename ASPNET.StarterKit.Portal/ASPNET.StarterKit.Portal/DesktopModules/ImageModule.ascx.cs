using System;

namespace ASPNET.StarterKit.Portal
{
    public partial class ImageModule : PortalModuleControl
    {
        //*******************************************************
        //
        // The Page_Load event handler on this User Control uses
        // the Portal configuration system to obtain image details.
        // It then sets these properties on an <asp:Image> server control.
        //
        //*******************************************************


        protected void Page_Load(object sender, EventArgs e)
        {
            var imageSrc = (string) Settings["src"];
            var imageHeight = (string) Settings["height"];
            var imageWidth = (string) Settings["width"];

            // Set Image Source, Width and Height Properties
            if (!string.IsNullOrEmpty(imageSrc))
            {
                Image1.ImageUrl = imageSrc;
            }

            if (!string.IsNullOrEmpty(imageWidth))
            {
                Image1.Width = Int32.Parse(imageWidth);
            }

            if (!string.IsNullOrEmpty(imageHeight))
            {
                Image1.Height = Int32.Parse(imageHeight);
            }
        }
    }
}