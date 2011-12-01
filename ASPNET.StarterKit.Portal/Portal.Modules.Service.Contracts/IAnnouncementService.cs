namespace Portal.Modules.Service.Contracts
{
    public interface IAnnouncementService
    {
        void CreateOrUpdate(PortalAnnouncement announcement);
    }
}