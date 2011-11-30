using Portal.Contracts;

namespace Portal.Services
{
    /// <remarks>
    /// GoF Design Pattern: Factory.
    /// </remarks>
    public interface IServiceFactory
    {
        IAnnouncementService AnnouncementService { get; }
        
    }
}