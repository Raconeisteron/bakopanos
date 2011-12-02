using System.ServiceModel;

namespace Portal.Modules.Service
{
    public class ContactServiceClient : ClientBase<IContactService>, IContactService
    {
        public void CreateOrUpdate(PortalContact item)
        {
            Channel.CreateOrUpdate(item);
        }
    }
}