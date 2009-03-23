using System;
using System.Data.SqlClient;

namespace ASPNET.StarterKit.Portal.Data
{
    public interface IDocumentDB
    {
        SqlDataReader GetDocuments(int moduleId);
        SqlDataReader GetSingleDocument(int itemId);
        SqlDataReader GetDocumentContent(int itemId);
        void DeleteDocument(int itemID);

        void UpdateDocument(int moduleId, int itemId, String userName, String name, String url, String category,
                            byte[] content, int size, String contentType);
    }
}