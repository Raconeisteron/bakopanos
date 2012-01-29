using System.Collections.ObjectModel;
using ASPNET.StarterKit.Portal;
using DemoApp.Properties;

namespace DemoApp.ViewModel
{
    public class LinksViewModel : WorkspaceViewModel
    {
        public LinksViewModel(ILinksDb linksDb, ITabConfigurationDb tabConfigurationDb)
        {
            base.DisplayName = Strings.LinksViewModel_DisplayName;

            Links = new Collection<PortalLink>();

            foreach (TabStripDetails tab in tabConfigurationDb.FindDesktopTabs())
            {
                foreach (ModuleSettings module in tabConfigurationDb.FindModules(tab.TabId))
                {
                    foreach (PortalLink portalLink in linksDb.GetLinks(module.ModuleId))
                    {
                        Links.Add(portalLink);
                    }
                }
            }
        }

        public Collection<PortalLink> Links { get; private set; }
    }
}