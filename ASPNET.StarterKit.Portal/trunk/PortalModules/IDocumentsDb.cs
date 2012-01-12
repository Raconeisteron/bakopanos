using System;
using System.Data;

namespace ASPNET.StarterKit.Portal
{
    public interface IDocumentsDb
    {
        IDataReader GetDocuments(int moduleId);
        IDataReader GetSingleDocument(int itemId);
        IDataReader GetDocumentContent(int itemId);
        void DeleteDocument(int itemId);

        void UpdateDocument(int moduleId, int itemId, String userName, String name, String url, String category,
                                            byte[] content, int size, String contentType);
    }
}