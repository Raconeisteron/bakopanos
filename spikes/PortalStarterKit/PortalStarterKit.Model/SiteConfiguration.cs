using System.Collections.Generic;
using System.Linq;

namespace PortalStarterKit.Model
{
    public class SiteConfiguration:IPortalContainer
    {
        private List<TabDefinition> _tabDefinitions;
        private List<ModuleDefinition> _moduleDefinitions;
        private List<Portal> _portals;

        public Portal NewPortal()
        {
            return new Portal {ParentSiteConfiguration = this};
        }


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
}