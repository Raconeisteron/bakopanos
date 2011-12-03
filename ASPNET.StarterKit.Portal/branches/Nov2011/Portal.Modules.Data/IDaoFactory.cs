namespace Portal.Modules.Data
{
    /// <remarks>
    /// GoF Design Pattern: Factory.
    /// </remarks>
    public interface IDaoFactory
    {
        IPortalDb PortalDb { get; }
        IAnnouncementsDb AnnouncementsDb { get; }
        IContactsDb ContactsDb { get; }
        IDiscussionsDb DiscussionDb { get; }
        IDocumentsDb DocumentDb { get; }
        IEventsDb EventsDb { get; }
        IHtmlTextDb HtmlTextDb { get; }
        ILinksDb LinkDb { get; }
    }
}