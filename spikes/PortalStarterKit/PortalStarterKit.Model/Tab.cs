using System;
using System.Collections.Generic;
using System.Linq;

namespace PortalStarterKit.Model
{
    public class Tab : ITabContainer, IModuleContainer
    {
        private List<Tab> _tabs;
        private List<Module> _modules;
        
        internal Tab()
        {

        }

        public Tab NewTab(Guid tabDefId)
        {
            var tab = new Tab {ParentTab = this, ParentPortal = ParentPortal};

            if (ParentPortal!=null)
            {
                tab.TabDefinition =
                    ParentPortal.ParentSiteConfiguration.TabDefinitions.Single<TabDefinition>(
                        item => item.TabDefId == tabDefId);    
            }
            
            return tab;
        }

        public Module NewModule()
        {
            var module = new Module { ParentTab = this, ParentPortal = ParentPortal };

            return module;
        }

        public int TabId { get; set; }     
        public string TabName { get; set; }
        public int TabOrder { get; set; }
        
        public string NavigateUrl
        {
            get
            {                
                return TabDefinition.SourceFile + "?tabid=" + TabId;
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

        public TabDefinition TabDefinition { get; internal set; }

        public Portal ParentPortal { get; internal set; }
        public Tab ParentTab { get; internal set; }
    }
}