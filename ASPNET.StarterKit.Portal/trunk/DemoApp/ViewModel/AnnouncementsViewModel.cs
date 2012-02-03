using System.Collections.ObjectModel;
using ASPNET.StarterKit.Portal;
using DemoApp.Properties;

namespace DemoApp.ViewModel
{
    public class AnnouncementsViewModel : WorkspaceViewModel
    {
        public AnnouncementsViewModel(IAnnouncementsDb announcementsDb, ITabConfigurationDb tabConfigurationDb)
        {
            base.DisplayName = Strings.AnnouncementsViewModel_DisplayName;

            Announcements = new Collection<PortalAnnouncement>();

            foreach (TabStripDetails tab in tabConfigurationDb.FindDesktopTabs())
            {
                foreach (ModuleSettings module in tabConfigurationDb.FindModules(tab.TabId))
                {
                    foreach (PortalAnnouncement portalAnnouncement in announcementsDb.GetAnnouncements(module.ModuleId))
                    {
                        Announcements.Add(portalAnnouncement);
                    }
                }
            }
        }

        public Collection<PortalAnnouncement> Announcements { get; private set; }
    }
}