using Portal.Contracts;

namespace Portal.Services
{
    /// <remarks>
    /// GoF Design Pattern: Factory.
    /// </remarks>
    internal class ServiceFactory:IServiceFactory
    {
        public IAnnouncementService AnnouncementService
        {
            get { return new AnnouncementService(); }
        }

        public ILinkService LinkService
        {
            get { return new LinkService(); }
        }

        public IContactService ContactService
        {
            get { return new ContactService(); }
        }
    }
}