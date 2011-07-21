using System.Collections.Generic;
using System.Xml.Serialization;

namespace PortalStarterKit.Domain
{
    public class SiteConfiguration
    {
        public Portal Portal { get; set; }

        [XmlElement("TabDefinition")]
        public List<TabDefinition> TabDefinitions { get; set; }

        [XmlElement("ModuleDefinition")]
        public List<ModuleDefinition> ModuleDefinitions { get; set; }
    }

    public class Portal
    {
        [XmlAttribute]
        public int PortalId { get; set; }

        [XmlAttribute]
        public string PortalName { get; set; }

        [XmlAttribute]
        public bool AlwaysShowEditButton { get; set; }

        [XmlElement("Tab")]
        public List<Tab> Tabs { get; set; }
    }

    public class ModuleDefinition
    {
        [XmlAttribute]
        public int ModuleDefId { get; set; }

        [XmlAttribute]
        public string FriendlyName { get; set; }

        [XmlAttribute("DesktopSourceFile")]
        public string SourceFile { get; set; }
    }

    public class TabDefinition
    {
        [XmlAttribute]
        public int TabDefId { get; set; }

        [XmlAttribute]
        public string FriendlyName { get; set; }

        [XmlAttribute("DesktopSourceFile")]
        public string SourceFile { get; set; }
    }

    public class Tab
    {
        [XmlAttribute]
        public int TabId { get; set; }

        [XmlAttribute]
        public int TabDefId { get; set; }

        [XmlAttribute]
        public string TabName { get; set; }

        [XmlAttribute]
        public int TabOrder { get; set; }

        [XmlIgnore]
        public string NavigateUrl { get; set; }

        [XmlElement("Tab")]
        public List<Tab> Tabs { get; set; }

        [XmlElement("Module")]
        public List<Module> Modules { get; set; }

        [XmlElement("Setting")]
        public List<Setting> Settings { get; set; }
    }

    public enum PaneType
    {
        LeftPane,
        ContentPane,
        RightPane
    }

    public class Module
    {
        [XmlAttribute]
        public int ModuleId { get; set; }

        [XmlAttribute]
        public int ModuleDefId { get; set; }

        [XmlAttribute]
        public int ModuleOrder { get; set; }

        [XmlAttribute]
        public string ModuleTitle { get; set; }

        [XmlAttribute]
        public PaneType PaneName { get; set; }

        [XmlElement("Setting")]
        public List<Setting> Settings { get; set; }
    }

    public class Setting
    {
        [XmlAttribute]
        public string Key { get; set; }

        [XmlAttribute]
        public string Value { get; set; }
    }
}