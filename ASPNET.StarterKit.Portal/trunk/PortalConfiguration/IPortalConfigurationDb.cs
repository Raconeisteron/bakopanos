using System;

namespace ASPNET.StarterKit.Portal
{
    public interface IPortalConfigurationDb
    {
        void UpdatePortalInfo(int portalId, String portalName, bool alwaysShow);
    }
}