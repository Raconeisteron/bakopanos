using System.Collections.Generic;
using PortalStarterKit.Model;

namespace PortalStarterKit.Core
{
    public interface ISiteEnvironment
    {
        string DataPhysicalPath { get; set; }
    }
    public class SiteEnvironment:ISiteEnvironment
    {
        public string DataPhysicalPath { get; set; }
    }

    public interface ISiteConfigurationService
    {
        List<PortalSettings> GetPortals();
        List<TabSettings> GetTabs(string portalId);

        PortalSettings GetPortal(string portalId);
        TabSettings GetTab(string portalId, string tabId);
    }
}