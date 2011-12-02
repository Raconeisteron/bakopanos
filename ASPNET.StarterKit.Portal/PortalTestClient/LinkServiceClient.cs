using System;
using System.Collections.ObjectModel;
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
        public PortalAnnouncement CreateOrUpdate(PortalAnnouncement item)
        {
            return Channel.CreateOrUpdate(item);
        }

        public Collection<PortalAnnouncement> GetAnnouncements(int moduleId)
        {
            return Channel.GetAnnouncements(moduleId);
        }
    }

    public class PortalLinks : Collection<PortalLink>
    {

    }

    public class LinkServiceClient : ClientBase<ILinkService>, ILinkService
    {
        public void CreateOrUpdate(PortalLink item)
        {
            Channel.CreateOrUpdate(item);
        }

        public Collection<PortalLink> GetLinks(int moduleId)
        {
            return Channel.GetLinks(moduleId);
        }
    }
}