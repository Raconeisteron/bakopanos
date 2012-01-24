using System;
using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal.Oracle
{
    public class OraDocumentsDb : IDocumentsDb
    {
        #region IDocumentsDb Members

        public List<PortalDocument> GetDocuments(int moduleId)
        {
            throw new NotImplementedException();
        }

        public PortalDocument GetSingleDocument(int itemId)
        {
            throw new NotImplementedException();
        }

        public PortalDocument GetDocumentContent(int itemId)
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