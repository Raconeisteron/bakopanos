using System.ServiceModel;
using Portal.Modules.Service.Contracts;

namespace Portal
{
    public class ContactServiceClient : ClientBase<IContactService>, IContactService
    {
        public void CreateOrUpdate(PortalContact item)
        {
            Channel.CreateOrUpdate(item);
        }
    }

    public class AnnouncementServiceClient : ClientBase<IAnnouncementService>, IAnnouncementService
    {
        public void CreateOrUpdate(PortalAnnouncement item)
        {
            Channel.CreateOrUpdate(item);
        }
    }

    public class LinkServiceClient : ClientBase<ILinkService>, ILinkService
    {
        public void CreateOrUpdate(PortalLink item)
        {
            Channel.CreateOrUpdate(item);
        }
    }
}