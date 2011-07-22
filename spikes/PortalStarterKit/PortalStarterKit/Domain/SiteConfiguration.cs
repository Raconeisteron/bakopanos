using System.Collections.Generic;

namespace PortalStarterKit.Domain
{
    public class SiteConfiguration
    {
        public List<Portal> Portals { get; set; }
        public List<TabDefinition> TabDefinitions { get; set; }
        public List<ModuleDefinition> ModuleDefinitions { get; set; }
    }

    public class Portal
    {
        public int PortalId { get; set; }
        public string PortalName { get; set; }
        public bool AlwaysShowEditButton { get; set; }
        public List<Tab> Tabs { get; set; }
    }

    public class ModuleDefinition
    {
        public int ModuleDefId { get; set; }
        public string FriendlyName { get; set; }
        public string SourceFile { get; set; }
    }

    public class TabDefinition
    {
        public int TabDefId { get; set; }
        public string FriendlyName { get; set; }
        public string SourceFile { get; set; }
    }

    public class Tab
    {        
        public int TabId { get; set; }     
        public int TabDefId { get; set; }
        public string TabName { get; set; }
        public int TabOrder { get; set; }
        public string NavigateUrl { get; set; }        
        public List<Module> Modules { get; set; }
        public List<Setting> Settings { get; set; }
        public List<Tab> Tabs { get; set; }

        public TabDefinition TabDefinition { get; set; }
        public Portal ParentPortal { get; set; }
        public Tab ParentTab { get; set; }
    }

    public enum PaneType
    {
        LeftPane,
        ContentPane,
        RightPane
    }

    public class Module
    {
        public int ModuleId { get; set; }
        public int ModuleDefId { get; set; }
        public int ModuleOrder { get; set; }
        public string ModuleTitle { get; set; }
        public PaneType PaneName { get; set; }
        public List<Setting> Settings { get; set; }

        public ModuleDefinition ModuleDefinition { get; set; }
        public Tab ParentTab { get; set; }
    }

    public class Setting
    {        
        public string Key { get; set; }
        public string Value { get; set; }
    }
}