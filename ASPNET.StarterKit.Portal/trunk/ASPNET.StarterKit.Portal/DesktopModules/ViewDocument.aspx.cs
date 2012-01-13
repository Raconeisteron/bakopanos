using System;
using System.Data;
using System.Web.UI;
using ASPNET.StarterKit.Portal.SqlClient;

namespace ASPNET.StarterKit.Portal
{
    public partial class ViewDocument : Page
    {
        private int _documentId = -1;

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
                IDocumentsDb documents = new SqlDocumentsDb();

                IDataReader dBContent = documents.GetDocumentContent(_documentId);
                dBContent.Read();

                // Serve up the file by name
                Response.AppendHeader("content-disposition", "filename=" + (String) dBContent["FileName"]);

                // set the content type for the Response to that of the 
                // document to display.  For example. "application/msword"
                Response.ContentType = (String) dBContent["ContentType"];

                // output the actual document contents to the response output stream
                Response.OutputStream.Write((byte[]) dBContent["Content"], 0, (int) dBContent["ContentSize"]);

                // end the response
                Response.End();
            }
        }
    }
}