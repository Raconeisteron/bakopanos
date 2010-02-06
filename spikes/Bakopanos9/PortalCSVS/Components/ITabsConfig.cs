using System;

namespace ASPNET.StarterKit.Portal
{
    public interface ITabsConfig
    {
        int AddTab(int portalId, String tabName, int tabOrder);
        void UpdateTab(int portalId, int tabId, String tabName, int tabOrder, String authorizedRoles, String mobileTabName, bool showMobile);
        void UpdateTabOrder(int tabId, int tabOrder);
        void DeleteTab(int tabId);
    }
}