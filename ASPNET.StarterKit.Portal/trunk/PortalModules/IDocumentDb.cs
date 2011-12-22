using System;
using System.Data;

namespace ASPNETPortal
{
    public interface IDocumentDb
    {
        /// <returns>
        /// The GetDocuments method returns a SqlDataReader containing all of the
        /// documents for a specific portal module from the documents
        /// database.
        /// </returns>
        DataTable GetDocuments(int moduleId);

        /// <returns>
        /// The GetSingleDocument method returns a SqlDataReader containing details
        /// about a specific document from the Documents database table.
        /// </returns>
        DataRow GetSingleDocument(int itemId);

        /// <returns>
        /// The GetDocumentContent method returns the contents of the specified
        /// document from the Documents database table.
        /// </returns>
        DataRow GetDocumentContent(int itemId);

        /// <summary>
        /// The DeleteDocument method deletes the specified document from
        /// the Documents database table.
        /// </summary>
        void DeleteDocument(int itemId);

        void UpdateDocument(int moduleId, int itemId, String userName, String name, String url,
                            String category,
                            byte[] content, int size, String contentType);
    }
}