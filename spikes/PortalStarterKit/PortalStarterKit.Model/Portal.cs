using System.Collections.Generic;

namespace PortalStarterKit.Model
{
    public class Portal : ITabContainer
    {
        private List<Tab> _tabs;

        internal Portal()
        {

        }

        public Tab NewTab()
        {
            return new Tab {ParentPortal = this};
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