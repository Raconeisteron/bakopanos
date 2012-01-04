using System;
using System.Collections.Generic;

namespace ASPNETPortal.Configuration
{
    /// <summary>
    /// Class that encapsulates the detailed settings for a specific Tab 
    /// in the Portal
    /// </summary>
    public class TabSettings
    {
        public String AuthorizedRoles;
        public String MobileTabName;
        public List<ModuleSettings> Modules = new List<ModuleSettings>();
        public bool ShowMobile;
        public int TabId;
        public int TabIndex;
        public String TabName;
        public int TabOrder;
    }
}