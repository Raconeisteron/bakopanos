using System.Collections.Generic;

namespace PortalStarterKit.Model
{
    public class SiteConfiguration
    {
        private List<TabDefinition> _tabDefinitions;
        private List<ModuleDefinition> _moduleDefinitions;
        private List<Portal> _portals;

        public List<Portal> Portals
        {
            get
            {
                if (_portals == null)
                {
                    _portals = new List<Portal>();
                }
                return _portals;
            }
        }

        public List<TabDefinition> TabDefinitions
        {
            get
            {
                if (_tabDefinitions==null)
                {
                    _tabDefinitions = new List<TabDefinition>();
                }
                return _tabDefinitions;
            }
        }
        public List<ModuleDefinition> ModuleDefinitions
        {
            get
            {
                if (_moduleDefinitions == null)
                {
                    _moduleDefinitions = new List<ModuleDefinition>();
                }
                return _moduleDefinitions;
            }
        }
    }

    public class Portal
    {
        private List<Tab> _tabs;

        public int PortalId { get; set; }
        public string PortalName { get; set; }
        public bool AlwaysShowEditButton { get; set; }
        public List<Tab> Tabs
        {
            get
            {
                if (_tabs == null)
                {
                    _tabs = new List<Tab>();
                }
                return _tabs;
            }
        }
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
        private List<Tab> _tabs;
        private List<Module> _modules;

        public int TabId { get; set; }     
        public int TabDefId { get; set; }
        public string TabName { get; set; }
        public int TabOrder { get; set; }
        public string NavigateUrl { get; set; }        
        public List<Module> Modules
        {
            get
            {
                if (_modules == null)
                {
                    _modules = new List<Module>();
                }
                return _modules;
            }
        }
        public List<Setting> Settings { get; set; }
        public List<Tab> Tabs
        {
            get
            {
                if (_tabs == null)
                {
                    _tabs = new List<Tab>();
                }
                return _tabs;
            }
        }

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