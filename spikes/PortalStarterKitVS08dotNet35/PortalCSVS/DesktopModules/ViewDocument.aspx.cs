using System;
using System.Data.Common;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public partial class ViewDocument : Page
    {
        [Dependency]
        public IDocumentDb DocumentDb
        {
            get;
            set;
        }

        private int documentId = -1;

        //*******************************************************
        //
        // The Page_Load event handler on this Page is used to
        // obtain obtain the contents of a document from the 
        // Documents table, construct an HTTP Response of the
        // correct type for the document, and then stream the 
        // document contents to the response.  It uses the 
        // ASPNET.StarterKit.Portal.DocumentDB() data component to encapsulate 
        // the data access functionality.
        //
        //*******************************************************

        public ViewDocument()
        {
            Page.Init += Page_Init;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["DocumentId"] != null)
            {
                documentId = Int32.Parse(Request.Params["DocumentId"]);
            }

            if (documentId != -1)
            {
                // Obtain Document Data from Documents table
                DbDataReader dBContent = DocumentDb.GetDocumentContent(documentId);
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

        protected void Page_Init(object sender, EventArgs e)
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