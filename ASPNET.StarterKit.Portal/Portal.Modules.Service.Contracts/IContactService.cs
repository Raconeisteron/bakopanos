using System.ServiceModel;

namespace Portal.Modules.Service.Contracts
{
    [ServiceContract]
    public interface IContactService
    {
        [OperationContract]
        void CreateOrUpdate(PortalContact item);
    }
}