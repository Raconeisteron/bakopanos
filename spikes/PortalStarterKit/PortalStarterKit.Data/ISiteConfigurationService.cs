using System;
using PortalStarterKit.Model;

namespace PortalStarterKit.Data
{
    public interface ISiteConfigurationService
    {
        SiteConfiguration ReadSiteConfiguration(Func<string,string> serverMapPath);
    }
}