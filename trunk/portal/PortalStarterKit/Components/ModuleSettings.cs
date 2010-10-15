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
        public string ModuleId { get; set; }
        public int ModuleOrder { get; set; }
        public String ModuleTitle { get; set; }
        public String PaneName { get; set; }
        public string TabId { get; set; }
    }
}