namespace ASPNET.StarterKit.Portal.DAL
{
    /// <remarks>
    /// GoF Design Pattern: Factory.
    /// </remarks>
    public interface IDaoFactory
    {
        IPortalDB PortalDB { get; }
        IAnnouncementsDB AnnouncementsDB { get; }
        IContactsDB ContactsDB { get; }
        IDiscussionDB DiscussionDB { get; }
        IDocumentDB DocumentDB { get; }
        IEventsDB EventsDB { get; }
        IHtmlTextDB HtmlTextDB { get; }
        ILinkDB LinkDB { get; }
        IRolesDB RolesDB { get; }
        IUsersDB UsersDB { get; }
    }
}