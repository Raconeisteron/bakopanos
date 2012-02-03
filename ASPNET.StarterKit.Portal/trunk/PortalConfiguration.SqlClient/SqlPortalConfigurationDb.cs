using System;

namespace ASPNET.StarterKit.Portal.SqlClient
{
    public class SqlPortalConfigurationDb : IPortalConfigurationDb
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