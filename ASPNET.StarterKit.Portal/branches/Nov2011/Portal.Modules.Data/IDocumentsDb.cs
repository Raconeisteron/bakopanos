using System.Data;

namespace Portal.Modules.Data
{
    public interface IDocumentsDb
    {
        IDataReader GetDocuments(int moduleId);
        IDataReader GetSingleDocument(int itemId);
        IDataReader GetDocumentContent(int itemId);
        void DeleteDocument(int itemID);

        void UpdateDocument(int moduleId, int itemId, string userName, string name, string url, string category,
                            byte[] content, int size, string contentType);
    }
}