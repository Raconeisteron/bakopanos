using System.Collections.ObjectModel;
using System.ServiceModel;

namespace Portal.Modules.Service.Contracts
{
    [ServiceContract]
    public interface ILinkService
    {
        [OperationContract]
        void CreateOrUpdate(PortalLink item);

        [OperationContract]
        Collection<PortalLink> GetLinks(int moduleId);
    }
}