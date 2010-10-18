using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace PortalStarterKit.Components
{
    public class FakeSiteConfigurationRepository : ISiteConfigurationRepository
    {
        #region ISiteConfigurationRepository Members

        public List<PortalSettings> Read()
        {
            var deskotPortals = new List<PortalSettings>();

            var portalItem = new PortalSettings();

            portalItem.AlwaysShowEditButton = true;
            portalItem.PortalName = "fake dev portal";
            portalItem.PortalId = "fake";
            deskotPortals.Add(portalItem);

            {
                var tabItem = new TabSettings();

                tabItem.TabName = "Home";
                tabItem.TabId = "Home";
                tabItem.TabOrder = 1;
                portalItem.DesktopTabs.Add(tabItem);

            }

            {
                var tabItem = new TabSettings();

                tabItem.TabName = "Announcements";
                tabItem.TabId = "Announcements";
                tabItem.TabOrder = 1;
                portalItem.DesktopTabs.Add(tabItem);

                var moduleItem = new ModuleSettings();

                moduleItem.ModuleId = "Announcements";
                moduleItem.ModuleTitle = "Announcements";
                moduleItem.ModuleOrder = 0;
                moduleItem.DesktopSrc = "DesktopModules/Announcements.ascx";
                moduleItem.FriendlyName = "Announcements";

                tabItem.Modules.Add(moduleItem);

            }

            {
                var tabItem = new TabSettings();

                tabItem.TabName = "SiteInfo";
                tabItem.TabId = "SiteInfo";
                tabItem.TabOrder = 1;
                portalItem.DesktopTabs.Add(tabItem);

                var moduleItem = new ModuleSettings();

                moduleItem.ModuleId = "SiteInfo";
                moduleItem.ModuleOrder = 0;
                moduleItem.DesktopSrc = "DesktopModules/SiteInfo.ascx";
                moduleItem.FriendlyName = "SiteInfo";

                tabItem.Modules.Add(moduleItem);
            }

            return deskotPortals;
        }

        #endregion
    }
}