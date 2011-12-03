using System.Collections.ObjectModel;
using System.ServiceModel;

namespace Portal.Modules.Service
{
    public class AnnouncementServiceClient : ClientBase<IAnnouncementService>, IAnnouncementService
    {
        #region IAnnouncementService Members

        public PortalAnnouncement CreateOrUpdate(PortalAnnouncement item)
        {
            return Channel.CreateOrUpdate(item);
        }

        public Collection<PortalAnnouncement> GetAnnouncements(int moduleId)
        {
            return Channel.GetAnnouncements(moduleId);
        }

        #endregion
    }
}