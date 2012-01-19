using System;

namespace ASPNET.StarterKit.Portal.Oracle
{
    public class OraPortalConfigurationDb : IPortalConfigurationDb
    {
        #region IPortalConfigurationDb Members

        public GlobalItem FindPortal()
        {
            throw new NotImplementedException();
        }

        public void UpdatePortalInfo(int portalId, string portalName, bool alwaysShow)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}