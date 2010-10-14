using System;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    ///   Class that encapsulates the detailed settings for a specific Tab 
    ///   in the Portal.  
    /// </summary>
    public class ModuleSettings 
    {
        public String AuthorizedEditRoles { get; set; }

        public String DesktopSrc { get; set; }
        public int ModuleId { get; set; }
        public int ModuleOrder { get; set; }
        public String ModuleTitle { get; set; }
        public String PaneName { get; set; }
        public int TabId { get; set; }
    }
}