using System;

namespace ASPNET.StarterKit.Portal
{
    public interface IPortalConfigurationDb
    {
        GlobalItem FindPortal();

        void UpdatePortalInfo(int portalId, String portalName, bool alwaysShow);
    }
}