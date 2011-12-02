namespace Portal.Modules.Service
{
    /// <remarks>
    /// GoF Design Patterns: Factory, Singleton, Proxy.
    /// 
    /// This class makes extensive use of the Factory pattern in determining which 
    /// database specific services to return.
    /// 
    /// This class is like a Singleton -- it is a static class and 
    /// therefore only one 'instance' will ever exist.
    /// 
    /// This class is a Proxy as it 'stands in' for the actual Data Access Object Factory.
    /// </remarks>
    public static class ServiceAccess
    {
        private static readonly IServiceFactory Factory;

        static ServiceAccess()
        {
            Factory = new ServiceFactory();
        }

        public static IAnnouncementService AnnouncementService
        {
            get { return Factory.AnnouncementService; }
        }

        public static ILinkService LinkService
        {
            get { return Factory.LinkService; }
        }

        public static IContactService ContactService
        {
            get { return Factory.ContactService; }
        }
    }
}