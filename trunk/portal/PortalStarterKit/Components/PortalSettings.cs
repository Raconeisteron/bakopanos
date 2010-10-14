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
        public TabSettings ActiveTab = new TabSettings();
        public List<TabSettings> DesktopTabs = new List<TabSettings>();

        public bool AlwaysShowEditButton;
        public int PortalId;
        public String PortalName;

    }
}