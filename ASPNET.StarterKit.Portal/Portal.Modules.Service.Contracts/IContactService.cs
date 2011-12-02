using System.ServiceModel;

namespace Portal.Modules.Service
{
    [ServiceContract]
    public interface IContactService
    {
        [OperationContract]
        void CreateOrUpdate(PortalContact item);
    }
}