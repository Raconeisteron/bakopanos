using System;
using System.Data;
using Framework.Data;

namespace ASPNETPortal
{
    /// <summary>
    /// Class that encapsulates all data logic necessary to add/query/delete
    /// documents within the Portal database.
    /// </summary>
    internal class DocumentDb : IDocumentDb
    {
        private readonly IDbHelper _db;

        public DocumentDb(IDbHelper db)
        {
            _db = db;
        }

        #region IDocumentDb Members

        /// <returns>
        /// The GetDocuments method returns a SqlDataReader containing all of the
        /// documents for a specific portal module from the documents
        /// database.
        /// </returns>
        public DataTable GetDocuments(int moduleId)
        {
            var parameterModuleId = _db.CreateParameter("@ModuleID", moduleId);

            return _db.GetDataTable("Portal_GetDocuments", parameterModuleId);
        }

        /// <returns>
        /// The GetSingleDocument method returns a SqlDataReader containing details
        /// about a specific document from the Documents database table.
        /// </returns>
        public DataRow GetSingleDocument(int itemId)
        {
            var parameterItemId = _db.CreateParameter("@ItemID", itemId);

            return _db.GetDataRow("Portal_GetSingleDocument", parameterItemId);
        }

        /// <returns>
        /// The GetDocumentContent method returns the contents of the specified
        /// document from the Documents database table.
        /// </returns>
        public DataRow GetDocumentContent(int itemId)
        {
            var parameterItemId = _db.CreateParameter("@ItemID", itemId);

            return _db.GetDataRow("Portal_GetDocumentContent", parameterItemId);
        }


        /// <summary>
        /// The DeleteDocument method deletes the specified document from
        /// the Documents database table.
        /// </summary>
        public void DeleteDocument(int itemId)
        {
            var parameterItemId = _db.CreateParameter("@ItemID", itemId);

            _db.ExecuteNonQuery("Portal_DeleteDocument", parameterItemId);
        }


        /// <summary>
        ///  updates the specified document within the Documents database table.
        /// </summary>
        public void UpdateDocument(int moduleId, int itemId, String userName, String name, String url,
                                   String category,
                                   byte[] content, int size, String contentType)
        {
            if (userName.Length < 1)
            {
                userName = "unknown";
            }

            var parameterItemId = _db.CreateParameter("@ItemID", itemId);
            var parameterModuleId = _db.CreateParameter("@ModuleID", moduleId);
            var parameterUserName = _db.CreateParameter("@UserName", userName);
            var parameterName = _db.CreateParameter("@FileFriendlyName", name);
            var parameterFileUrl = _db.CreateParameter("@FileNameUrl", url);
            var parameterCategory = _db.CreateParameter("@Category", category);
            var parameterContent = _db.CreateParameter("@Content", content);
            var parameterContentType = _db.CreateParameter("@ContentType", contentType);
            var parameterContentSize = _db.CreateParameter("@ContentSize", size);

            _db.ExecuteNonQuery("Portal_UpdateDocument", parameterItemId, parameterModuleId, parameterUserName, parameterName,
                                parameterFileUrl, parameterCategory, parameterContent, parameterContentType,
                                parameterContentSize);
        }

        #endregion
    }
}