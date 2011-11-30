namespace Portal.Modules.Contracts
{
    public interface IAnnouncementService
    {
        void CreateOrUpdate(PortalAnnouncement announcement);
    }
}