using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// encapsulates the detailed settings for a specific Tab 
    /// in the Portal
    /// </summary>
    public class TabSettings
    {
        public TabSettings()
        {
            Modules = new List<ModuleSettings>();
        }

        public string AuthorizedRoles { get; set; }
        public string MobileTabName { get; set; }
        public List<ModuleSettings> Modules { get; set; }
        public bool ShowMobile { get; set; }
        public int TabId { get; set; }
        public int TabIndex { get; set; }
        public string TabName { get; set; }
        public int TabOrder { get; set; }
    }
}