using System.Collections.ObjectModel;
using ASPNET.StarterKit.Portal;
using DemoApp.Properties;

namespace DemoApp.ViewModel
{
    public class AnnouncementsViewModel : WorkspaceViewModel
    {
        private IAnnouncementsDb _db;
        public AnnouncementsViewModel(IAnnouncementsDb db)
        {
            _db = db;
            base.DisplayName = Strings.AnnouncementsViewModel_DisplayName;
            Announcements = db.GetAnnouncements(13);
        }

        public Collection<PortalAnnouncement> Announcements { get; private set; }

    }
}