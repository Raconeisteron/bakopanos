using Portal.Modules.Service.Contracts;

namespace Portal.Modules.Service
{
    /// <remarks>
    /// GoF Design Pattern: Factory.
    /// </remarks>
    internal class ServiceFactory : IServiceFactory
    {
        #region IServiceFactory Members

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

        #endregion
    }
}