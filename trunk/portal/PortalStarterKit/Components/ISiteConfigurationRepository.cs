using System.Collections.Generic;
using PortalStarterKit.Model;

namespace PortalStarterKit.Components
{
    public interface ISiteConfigurationRepository
    {
        List<PortalSettings> Read();
    }
}