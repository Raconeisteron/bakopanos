using System;
using System.IO;
using System.Web.UI;
using Portal.Components;

namespace Portal.DesktopModules
{
    public partial class XmlModule : PortalModuleControl
    {
        //*******************************************************
        //
        // The Page_Load event handler on this User Control uses
        // the Portal configuration system to obtain an xml document
        // and xsl/t transform file location.  It then sets these
        // properties on an <asp:Xml> server control.
        //
        //*******************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            var xmlsrc = (string) Settings["xmlsrc"];

            if (!string.IsNullOrEmpty(xmlsrc))
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

            var xslsrc = (string) Settings["xslsrc"];

            if (!string.IsNullOrEmpty(xslsrc))
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
    }
}