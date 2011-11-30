using System.Data;

namespace Portal.Modules.DAL.SqlServer
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// documents within the Portal database.
    /// </summary>
    internal class DocumentsDb : SqlDbHelper, IDocumentsDb
    {
        #region IDocumentsDb Members

        /// <summary>
        /// The GetDocuments method returns a IDataReader containing all of the
        /// documents for a specific portal module from the documents
        /// database.
        /// </summary>        
        public IDataReader GetDocuments(int moduleId)
        {
            return GetItems("Portal_GetDocuments", InputModuleId(moduleId));
        }

        /// <summary>
        /// The GetSingleDocument method returns a IDataReader containing details
        /// about a specific document from the Documents database table.
        /// </summary>        
        public IDataReader GetSingleDocument(int itemId)
        {
            return GetSingleItem("Portal_GetSingleDocument", itemId);
        }

        /// <summary>
        /// The GetDocumentContent method returns the contents of the specified
        /// document from the Documents database table.
        /// </summary>        
        public IDataReader GetDocumentContent(int itemId)
        {
            return GetSingleItem("Portal_GetDocumentContent", itemId);
        }

        /// <summary>
        /// // The DeleteDocument method deletes the specified document from
        /// the Documents database table.
        /// </summary>        
        public void DeleteDocument(int itemId)
        {
            ExecuteNonQuery("Portal_DeleteDocument", InputItemId(itemId));
        }

        /// <summary>
        /// The UpdateDocument method updates the specified document within
        /// the Documents database table.
        /// </summary>        
        public void UpdateDocument(int moduleId, int itemId, string userName, string name, string url, string category,
                                   byte[] content, int size, string contentType)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            ExecuteNonQuery("Portal_UpdateDocument", InputItemId(itemId), 
                InputModuleId(moduleId),
                       InputUserName(userName),
                       InputFileFriendlyName(name),
                       InputFileNameUrl(url),
                       InputCategory(category),
                       InputContent(content),
                       InputContentType(contentType),
                       InputContentSize(size));
        }

        #endregion
    }
}