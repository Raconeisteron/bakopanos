using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ASPNET.StarterKit.Portal
{
    public abstract class XmlModule : PortalModuleControl
    {
        protected Xml xml1;


        //*******************************************************
        //
        // The Page_Load event handler on this User Control uses
        // the Portal configuration system to obtain an xml document
        // and xsl/t transform file location.  It then sets these
        // properties on an <asp:Xml> server control.
        //
        //*******************************************************

        public XmlModule()
        {
            Init += Page_Init;
        }

        private void Page_Load(object sender, EventArgs e)
        {
            var xmlsrc = (String) Settings["xmlsrc"];

            if ((xmlsrc != null) && (xmlsrc != ""))
            {
                if (File.Exists(Server.MapPath(xmlsrc)))
                {
                    xml1.DocumentSource = xmlsrc;
                }
                else
                {
                    Controls.Add(
                        new LiteralControl("<" + "br" + "><" + "span class=NormalRed" + ">" + "File " + xmlsrc +
                                           " not found.<" + "br" + ">"));
                }
            }

            var xslsrc = (String) Settings["xslsrc"];

            if ((xslsrc != null) && (xslsrc != ""))
            {
                if (File.Exists(Server.MapPath(xslsrc)))
                {
                    xml1.TransformSource = xslsrc;
                }
                else
                {
                    Controls.Add(
                        new LiteralControl("<" + "br" + "><" + "span class=NormalRed>File " + xslsrc + " not found.<" +
                                           "br" + ">"));
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

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }

        #endregion
    }
}