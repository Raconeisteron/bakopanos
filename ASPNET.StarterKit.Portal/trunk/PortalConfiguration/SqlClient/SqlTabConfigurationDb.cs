using System;
using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlTabConfigurationDb : ITabConfigurationDb
    {
        #region ITabConfigurationDb Members

        public List<TabStripDetails> FindDesktopTabs()
        {
            throw new NotImplementedException();
        }

        public List<TabStripDetails> FindMobileTabs()
        {
            throw new NotImplementedException();
        }

        public List<ModuleSettings> FindModules(int tabId)
        {
            throw new NotImplementedException();
        }

        public TabSettings FindTab(int tabId)
        {
            throw new NotImplementedException();
        }

        public int AddTab(int portalId, string tabName, int tabOrder)
        {
            throw new NotImplementedException();
        }

        public void UpdateTab(int portalId, int tabId, string tabName, int tabOrder, string authorizedRoles,
                              string mobileTabName, bool showMobile)
        {
            throw new NotImplementedException();
        }

        public void UpdateTabOrder(int tabId, int tabOrder)
        {
            throw new NotImplementedException();
        }

        public void DeleteTab(int tabId)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}