using System.Collections.Generic;

namespace ASPNET.StarterKit.Portal
{
    public interface ISiteConfigurationRepository
    {
        List<PortalSettings> Read();
    }
}