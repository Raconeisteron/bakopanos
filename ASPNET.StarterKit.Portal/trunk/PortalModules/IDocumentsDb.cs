using System.Collections.Generic;
using ASPNET.StarterKit.Portal.PortalDao;

namespace ASPNET.StarterKit.Portal
{
    public interface IDocumentsDb
    {
        List<PortalDocument> GetDocuments(int moduleId);
        PortalDocument GetSingleDocument(int itemId);
        PortalDocument GetDocumentContent(int itemId);
        void DeleteDocument(int itemId);

        void UpdateDocument(int moduleId, int itemId, string userName, string name, string url, string category,
                            byte[] content, int size, string contentType);
    }
}