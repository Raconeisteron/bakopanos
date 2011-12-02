using System.Collections.ObjectModel;
using System.ServiceModel;

namespace Portal.Modules.Service
{
    [ServiceContract]
    public interface IAnnouncementService
    {
        [OperationContract]
        PortalAnnouncement CreateOrUpdate(PortalAnnouncement announcement);

        [OperationContract]
        Collection<PortalAnnouncement> GetAnnouncements(int moduleId);
    }
}