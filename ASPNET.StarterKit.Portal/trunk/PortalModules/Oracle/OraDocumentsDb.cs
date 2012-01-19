using System;
using System.Data;

namespace ASPNET.StarterKit.Portal.Oracle
{
    public class OraDocumentsDb : IDocumentsDb
    {
        #region IDocumentsDb Members

        public IDataReader GetDocuments(int moduleId)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetSingleDocument(int itemId)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetDocumentContent(int itemId)
        {
            throw new NotImplementedException();
        }

        public void DeleteDocument(int itemId)
        {
            throw new NotImplementedException();
        }

        public void UpdateDocument(int moduleId, int itemId, string userName, string name, string url, string category,
                                   byte[] content, int size, string contentType)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}