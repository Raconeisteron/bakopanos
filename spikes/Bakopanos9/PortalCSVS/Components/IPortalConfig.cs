using System;

namespace ASPNET.StarterKit.Portal
{
    public interface IPortalConfig
    {
        void UpdatePortalInfo(int portalId, String portalName, bool alwaysShow);
    }
}