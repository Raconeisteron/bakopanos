using System;

namespace PortalStarterKit.Model
{
    /// <summary>
    ///   Class that encapsulates the detailed settings for a specific module definition 
    ///   in the Portal.
    /// </summary>
    public class ModuleDefSettings
    {
        public String DesktopSrc { get; set; }
        public String FriendlyName { get; set; }

        public string ModuleDefId { get; set; }
    }
}