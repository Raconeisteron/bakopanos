using System;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    ///   Class that encapsulates the detailed settings for a specific Tab 
    ///   in the Portal.  
    /// </summary>
    public class ModuleSettings 
    {
        public String AuthorizedEditRoles;

        public String DesktopSrc;
        public int ModuleId;
        public int ModuleOrder;
        public String ModuleTitle;
        public String PaneName;
        public int TabId;
    }
}