using System;
using System.Collections.Generic;
using System.Linq;

namespace PortalStarterKit.Model
{
    public class Portal : ITabContainer
    {
        private List<Tab> _tabs;

        internal Portal()
        {

        }

        public Tab NewTab(Guid tabDefId)
        {
            var tab = new Tab
                          {
                              ParentTab = null, 
                              ParentPortal = this                              
                          };

            tab.TabDefinition =
                ParentSiteConfiguration.TabDefinitions.Single<TabDefinition>(item => item.TabDefId == tabDefId);

            return tab;
        }

        public int PortalId { get; set; }
        public string PortalName { get; set; }
        public bool AlwaysShowEditButton { get; set; }

        public SiteConfiguration ParentSiteConfiguration { get; internal set; }

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
}