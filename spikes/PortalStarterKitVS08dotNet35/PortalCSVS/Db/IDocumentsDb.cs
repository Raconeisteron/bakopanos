using System;
using System.Data.Common;

namespace ASPNET.StarterKit.Portal
{
    public interface IDocumentsDb
    {
        DbDataReader GetDocuments(int moduleId);
        DbDataReader GetSingleDocument(int itemId);
        DbDataReader GetDocumentContent(int itemId);
        void DeleteDocument(int itemID);

        void UpdateDocument(int moduleId, int itemId, String userName, String name, String url, String category,
                            byte[] content, int size, String contentType);
    }
}