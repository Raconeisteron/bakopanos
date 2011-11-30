
namespace Portal.Contracts
{    
    public interface IAnnouncementService
    {
        int CreateOrUpdate(PortalAnnouncement announcement);
    }
}