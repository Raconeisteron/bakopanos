using System;
using PortalStarterKit.Model;

namespace PortalStarterKit.Data
{
    public interface IComponentConfiguration
    {
        SiteConfiguration ReadSiteConfiguration(Func<string,string> serverMapPath);
    }
}