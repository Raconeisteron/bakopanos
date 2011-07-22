using System.Collections.Generic;
using System.Linq;
using PortalStarterKit.Model;

namespace PortalStarterKit.Data
{
    public abstract class SiteConfigurationService : ISiteConfigurationService
    {
        #region ISiteConfigurationService Members

        public abstract SiteConfiguration ReadSiteConfiguration();

        #endregion
    }
}