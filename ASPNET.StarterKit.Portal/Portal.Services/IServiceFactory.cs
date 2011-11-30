using Portal.Modules.Contracts;

namespace Portal.Modules.Services
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