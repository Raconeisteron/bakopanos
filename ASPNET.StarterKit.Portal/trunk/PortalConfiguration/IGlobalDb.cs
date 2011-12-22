using System;

namespace ASPNETPortal
{
    public interface IGlobalDb
    {
        GlobalItem GetGlobalByPortalId(int portalId);

        /// <summary>
        /// The UpdatePortalInfo method updates the name and access settings for the portal.
        /// These settings are stored in the Xml file PortalCfg.xml.
        /// </summary>        
        void UpdatePortalInfo(int portalId, String portalName, bool alwaysShow);
    }
}