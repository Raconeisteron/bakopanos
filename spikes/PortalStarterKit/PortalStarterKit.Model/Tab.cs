using System.Collections.Generic;
using System.Linq;

namespace PortalStarterKit.Model
{
    public class Tab : ITabContainer
    {
        private List<Tab> _tabs;
        private List<Module> _modules;

        internal Tab()
        {

        }

        public Tab NewTab()
        {
            var tab = new Tab {ParentTab = this, ParentPortal = ParentPortal};
            
            return tab;
        }

        public int TabId { get; set; }     
        public int TabDefId { get; set; }
        public string TabName { get; set; }
        public int TabOrder { get; set; }
        
        public string NavigateUrl
        {
            get
            {
                string desktopSrc = ParentPortal.ParentSiteConfiguration.TabDefinitions.Single(item => item.TabDefId == TabDefId).SourceFile;
                return desktopSrc + "?tabid=" + TabId;
            }
        }

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

        public Portal ParentPortal { get; internal set; }
        public Tab ParentTab { get; internal set; }
    }
}