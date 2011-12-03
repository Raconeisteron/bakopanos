using System.Collections.ObjectModel;
using System.ServiceModel;

namespace Portal.Modules.Service
{
    [ServiceContract]
    public interface IDocumentService
    {
        [OperationContract]
        Collection<PortalDocument> GetDocuments(int moduleId);

        [OperationContract]
        PortalDocument PortalDocument(int itemId);

        [OperationContract]
        byte[] GetDocumentContent(int itemId);

        [OperationContract]
        void DeleteDocument(int itemId);

        [OperationContract]
        void UpdateDocument(int moduleId, int itemId, string userName, string name, string url, string category,
                            byte[] content, int size, string contentType);
    }
}