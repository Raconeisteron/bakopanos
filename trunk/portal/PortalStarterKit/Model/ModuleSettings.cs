using System;
using System.Collections.Generic;

namespace PortalStarterKit.Model
{
    /// <summary>
    ///   Class that encapsulates the detailed settings for a specific Tab 
    ///   in the Portal.
    /// </summary>
    public class ModuleSettings
    {
        public String AuthorizedEditRoles { get; set; }

        public ModuleDefSettings ModuleDef { get; set; }
        
        public string ModuleId { get; set; }
        public int ModuleOrder { get; set; }
        public List<string> EditRoles { get; set; }
        
        public String ModuleTitle { get; set; }
        public PortalPane PaneName { get; set; }
        public string TabId { get; set; }
    }
}