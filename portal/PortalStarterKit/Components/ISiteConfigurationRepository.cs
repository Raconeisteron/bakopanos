using System.Collections.Generic;

namespace PortalStarterKit.Components
{
    public interface ISiteConfigurationRepository
    {
        List<PortalSettings> Read();
    }
}