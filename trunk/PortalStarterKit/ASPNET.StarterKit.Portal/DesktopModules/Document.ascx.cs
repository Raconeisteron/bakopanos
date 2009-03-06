using System;
using System.Web.UI.WebControls;
using ASPNET.StarterKit.Portal.Components;

namespace ASPNET.StarterKit.Portal
{
    public partial  class Document : PortalModuleControl
    {

        public Document()
        {
            Init += Page_Init;
        }


        //*******************************************************
        //
        // The Page_Load event handler on this User Control is used to
        // obtain a SqlDataReader of document information from the 
        // Documents table, and then databind the results to a DataGrid
        // server control.  It uses the ASPNET.StarterKit.Portal.DocumentDB()
        // data component to encapsulate all data functionality.
        //
        //*******************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtain Document Data from Documents table
            // and bind to the datalist control
            var documents = new DocumentDB();

            myDataGrid.DataSource = documents.GetDocuments(ModuleId);
            myDataGrid.DataBind();
        }

        //*******************************************************
        //
        // GetBrowsePath() is a helper method used to create the url   
        // to the document.  If the size of the content stored in the   
        // database is non-zero, it creates a path to browse that.   
        // Otherwise, the FileNameUrl value is used.
        //
        // This method is used in the databinding expression for
        // the browse Hyperlink within the DataGrid, and is called 
        // for each row when DataGrid.DataBind() is called.  It is 
        // defined as a helper method here (as opposed to inline 
        // within the template) to improve code organization and
        // avoid embedding logic within the content template.
        //
        //*******************************************************

        protected String GetBrowsePath(String url, object size, int documentId)
        {
            if (size != DBNull.Value && (int) size > 0)
            {
                // if there is content in the database, create an 
                // url to browse it

                return "~/DesktopModules/ViewDocument.aspx?DocumentID=" + documentId;
            }
            else
            {
                // otherwise, return the FileNameUrl
                return url;
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