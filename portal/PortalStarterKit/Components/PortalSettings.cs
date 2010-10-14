using System;
using System.Collections;
using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    ///   This class encapsulates all of the settings for the Portal, as well
    ///   as the configuration settings required to execute the current tab
    ///   view within the portal.
    /// </summary>
    public class PortalSettings
    {
        public PortalSettings()
        {
            ActiveTab = new TabSettings();
            DesktopTabs = new List<TabSettings>();
        }

        public TabSettings ActiveTab { get; set; }
        public List<TabSettings> DesktopTabs { get; set; }

        public bool AlwaysShowEditButton { get; set; }
        public int PortalId { get; set; }
        public String PortalName { get; set; }

    }
}