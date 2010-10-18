using System;
using System.Collections.Generic;

namespace PortalStarterKit.Components
{
    /// <summary>
    ///   Class that encapsulates the detailed settings for a specific Tab 
    ///   in the Portal
    /// </summary>
    public class TabSettings
    {
        public TabSettings()
        {
            Modules = new List<ModuleSettings>();
        }

        public List<String> AccessRoles { get; set; }
        public List<ModuleSettings> Modules { get; set; }

        public string TabId { get; set; }
        public String TabName { get; set; }
        public int TabOrder { get; set; }
    }
}