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
        public String AuthorizedRoles;
        public List<ModuleSettings> Modules = new List<ModuleSettings>();

        public int TabId;
        public int TabIndex;
        public String TabName;
        public int TabOrder;
    }
}