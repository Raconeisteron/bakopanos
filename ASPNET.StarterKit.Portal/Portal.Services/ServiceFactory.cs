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
    }
}