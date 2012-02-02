using System.Collections.ObjectModel;
using ASPNET.StarterKit.Portal;
using DemoApp.Properties;

namespace DemoApp.ViewModel
{
    public class EventsViewModel : WorkspaceViewModel
    {
        public EventsViewModel(IEventsDb eventsDb, ITabConfigurationDb tabConfigurationDb)
        {
            base.DisplayName = Strings.EventsViewModel_DisplayName;

            Events = new Collection<PortalEvent>();

            foreach (TabStripDetails tab in tabConfigurationDb.FindDesktopTabs())
            {
                foreach (ModuleSettings module in tabConfigurationDb.FindModules(tab.TabId))
                {
                    foreach (PortalEvent portalEvent in eventsDb.GetEvents(module.ModuleId))
                    {
                        Events.Add(portalEvent);
                    }
                }
            }
        }

        public Collection<PortalEvent> Events { get; private set; }
    }
}