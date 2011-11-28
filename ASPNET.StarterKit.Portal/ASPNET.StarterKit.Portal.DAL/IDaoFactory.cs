namespace ASPNET.StarterKit.Portal.DAL
{
    /// <remarks>
    /// GoF Design Pattern: Factory.
    /// </remarks>
    public interface IDaoFactory
    {
        IPortalDb PortalDB { get; }
        IAnnouncementsDb AnnouncementsDB { get; }
        IContactsDb ContactsDB { get; }
        IDiscussionsDb DiscussionDB { get; }
        IDocumentsDb DocumentDB { get; }
        IEventsDb EventsDB { get; }
        IHtmlTextDb HtmlTextDB { get; }
        ILinksDb LinkDB { get; }
        IRolesDb RolesDB { get; }
        IUsersDb UsersDB { get; }
    }
}