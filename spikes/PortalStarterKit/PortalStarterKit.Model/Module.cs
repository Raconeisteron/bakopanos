using System.Collections.Generic;

namespace PortalStarterKit.Model
{
    public class Module
    {
        internal Module()
        {

        }

        public int ModuleId { get; set; }
        public int ModuleDefId { get; set; }
        public int ModuleOrder { get; set; }
        public string ModuleTitle { get; set; }
        public PaneType PaneName { get; set; }
        public List<Setting> Settings { get; set; }

        public ModuleDefinition ModuleDefinition { get; set; }
        public Portal ParentPortal { get; internal set; }        
        public Tab ParentTab { get; set; }
    }
}