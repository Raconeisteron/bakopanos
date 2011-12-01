using System.ServiceModel;

namespace Portal.Modules.Service.Contracts
{
    [ServiceContract]
    public interface IAnnouncementService
    {
        [OperationContract]
        void CreateOrUpdate(PortalAnnouncement announcement);
    }
}