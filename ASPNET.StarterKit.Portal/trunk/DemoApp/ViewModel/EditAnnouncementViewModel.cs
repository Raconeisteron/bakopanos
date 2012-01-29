using ASPNET.StarterKit.Portal;

namespace DemoApp.ViewModel
{
    public class EditAnnouncementViewModel : WorkspaceViewModel
    {
        private IAnnouncementsDb _db;

        public EditAnnouncementViewModel(IAnnouncementsDb db)
        {
            _db = db;
        }
    }
}