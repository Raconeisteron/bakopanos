namespace Portal.Modules.Service
{
    /// <remarks>
    /// GoF Design Pattern: Factory.
    /// </remarks>
    public interface IServiceFactory
    {
        IAnnouncementService AnnouncementService { get; }
        ILinkService LinkService { get; }
        IContactService ContactService { get; }
    }
}