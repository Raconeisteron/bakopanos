using System;
using System.Data;
using System.IO;
using System.Web.UI;
using Portal.Modules.DAL;

namespace ASPNET.StarterKit.Portal
{
    public partial class EditDocs : Page
    {
        private int itemId;
        private int moduleId;

        public EditDocs()
        {
            Page.Init += Page_Init;
        }

        //****************************************************************
        //
        // The Page_Load event on this Page is used to obtain the ModuleId
        // and ItemId of the document to edit.
        //
        // It then uses the ASPNET.StarterKit.Portal.DocumentDB() data component
        // to populate the page's edit controls with the document details.
        //
        //****************************************************************

        protected void Page_Load(object sender, EventArgs e)
        {
            // Determine ModuleId of Announcements Portal Module
            moduleId = Int32.Parse(Request.Params["Mid"]);

            // Verify that the current user has access to edit this module
            if (PortalSecurity.HasEditPermissions(moduleId) == false)
            {
                Response.Redirect("~/Admin/EditAccessDenied.aspx");
            }

            // Determine ItemId of Document to Update
            if (Request.Params["ItemId"] != null)
            {
                itemId = Int32.Parse(Request.Params["ItemId"]);
            }

            // If the page is being requested the first time, determine if an
            // document itemId value is specified, and if so populate page
            // contents with the document details

            if (Page.IsPostBack == false)
            {
                if (itemId != 0)
                {
                    // Obtain a single row of document information
                    IDocumentsDb documents = ModulesDataAccess.DocumentDb;
                    IDataReader dr = documents.GetSingleDocument(itemId);

                    // Load first row into Datareader
                    dr.Read();

                    // Security check.  verify that itemid is within the module.
                    int dbModuleID = Convert.ToInt32(dr["ModuleID"]);
                    if (dbModuleID != moduleId)
                    {
                        dr.Close();
                        Response.Redirect("~/Admin/EditAccessDenied.aspx");
                    }

                    NameField.Text = (string) dr["FileFriendlyName"];
                    PathField.Text = (string) dr["FileNameUrl"];
                    CategoryField.Text = (string) dr["Category"];
                    CreatedBy.Text = (string) dr["CreatedByUser"];
                    CreatedDate.Text = ((DateTime) dr["CreatedDate"]).ToShortDateString();

                    dr.Close();
                }

                // Store URL Referrer to return to portal
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            }
        }

        //****************************************************************
        //
        // The UpdateBtn_Click event handler on this Page is used to either
        // create or update an document.  It  uses the ASPNET.StarterKit.Portal.DocumentDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        protected void UpdateBtn_Click(Object sender, EventArgs e)
        {
            // Only Update if Input Data is Valid
            if (Page.IsValid)
            {
                // Create an instance of the Document DB component
                IDocumentsDb documents = ModulesDataAccess.DocumentDb;

                // Determine whether a file was uploaded

                if (storeInDatabase.Checked && (FileUpload.PostedFile != null))
                {
                    // for web farm support
                    var length = (int) FileUpload.PostedFile.InputStream.Length;
                    string contentType = FileUpload.PostedFile.ContentType;
                    var content = new byte[length];

                    FileUpload.PostedFile.InputStream.Read(content, 0, length);

                    // Update the document within the Documents table
                    documents.UpdateDocument(moduleId, itemId, Context.User.Identity.Name, NameField.Text,
                                             PathField.Text, CategoryField.Text, content, length, contentType);
                }
                else
                {
                    if (Upload.Checked && (FileUpload.PostedFile != null))
                    {
                        // Calculate virtualPath of the newly uploaded file
                        string virtualPath = "~/uploads/" + Path.GetFileName(FileUpload.PostedFile.FileName);

                        // Calculate physical path of the newly uploaded file
                        string phyiscalPath = Server.MapPath(virtualPath);

                        // Save file to uploads directory
                        FileUpload.PostedFile.SaveAs(phyiscalPath);

                        // Update PathFile with uploaded virtual file location
                        PathField.Text = virtualPath;
                    }
                    documents.UpdateDocument(moduleId, itemId, Context.User.Identity.Name, NameField.Text,
                                             PathField.Text, CategoryField.Text, new byte[0], 0, "");
                }

                // Redirect back to the portal home page
                Response.Redirect((string) ViewState["UrlReferrer"]);
            }
        }

        //****************************************************************
        //
        // The DeleteBtn_Click event handler on this Page is used to delete an
        // a document.  It  uses the ASPNET.StarterKit.Portal.DocumentsDB()
        // data component to encapsulate all data functionality.
        //
        //****************************************************************

        protected void DeleteBtn_Click(Object sender, EventArgs e)
        {
            // Only attempt to delete the item if it is an existing item
            // (new items will have "ItemId" of 0)

            if (itemId != 0)
            {
                IDocumentsDb documents = ModulesDataAccess.DocumentDb;
                documents.DeleteDocument(itemId);
            }

            // Redirect back to the portal home page
            Response.Redirect((string) ViewState["UrlReferrer"]);
        }

        //****************************************************************
        //
        // The CancelBtn_Click event handler on this Page is used to cancel
        // out of the page, and return the user back to the portal home
        // page.
        //
        //****************************************************************

        protected void CancelBtn_Click(Object sender, EventArgs e)
        {
            // Redirect back to the portal home page
            Response.Redirect((string) ViewState["UrlReferrer"]);
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