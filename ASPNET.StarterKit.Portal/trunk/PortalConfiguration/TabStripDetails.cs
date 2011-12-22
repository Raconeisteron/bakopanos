using System;

namespace ASPNETPortal
{
    /// <summary>
    /// Class that encapsulates the just the tabstrip details -- TabName, TabId and TabOrder 
    /// -- for a specific Tab in the Portal
    /// </summary>
    public class TabStripDetails
    {
        public String AuthorizedRoles;
        public int TabId;
        public String TabName;
        public int TabOrder;
    }
}