using System;
using System.Collections;
using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal
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

        public String AuthorizedRoles { get; set; }
        public List<ModuleSettings> Modules { get; set; }

        public string TabId { get; set; }
        public String TabName { get; set; }
        public int TabOrder { get; set; }
    }
}