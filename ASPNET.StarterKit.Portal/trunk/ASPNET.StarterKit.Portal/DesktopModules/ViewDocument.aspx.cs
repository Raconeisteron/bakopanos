using System;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class ViewDocument : Page
    {
        private int _documentId = -1;
        private IDocumentsDb _documentsDb;

        [InjectionMethod]
        public void Initialize(IDocumentsDb documentsDb)
        {
            _documentsDb = documentsDb;
        }

        /// <summary>
        /// The Page_Load event handler on this Page is used to
        /// obtain obtain the contents of a document from the 
        /// Documents table, construct an HTTP Response of the
        /// correct type for the document, and then stream the 
        /// document contents to the response.  It uses the 
        /// ASPNET.StarterKit.Portal.DocumentDB() data component to encapsulate 
        /// the data access functionality.
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["DocumentId"] != null)
            {
                _documentId = Int32.Parse(Request.Params["DocumentId"]);
            }

            if (_documentId != -1)
            {
                // Obtain Document Data from Documents table
                PortalDocument dBContent = _documentsDb.GetDocumentContent(_documentId);

                // Serve up the file by name
                Response.AppendHeader("content-disposition", "filename=" + dBContent.FileFriendlyName);

                // set the content type for the Response to that of the 
                // document to display.  For example. "application/msword"
                Response.ContentType = dBContent.ContentType;

                // output the actual document contents to the response output stream
                Response.OutputStream.Write(dBContent.Content, 0, dBContent.ContentSize);

                // end the response
                Response.End();
            }
        }
    }
}