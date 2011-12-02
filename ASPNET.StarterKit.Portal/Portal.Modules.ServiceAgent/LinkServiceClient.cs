using System.Collections.ObjectModel;
using System.ServiceModel;

namespace Portal.Modules.Service
{
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