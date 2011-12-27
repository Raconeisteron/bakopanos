using System;

namespace ASPNETPortal
{
    /// <summary>
    /// Class that encapsulates the detailed settings for a specific Tab 
    /// in the Portal.  ModuleSettings implements the IComparable interface 
    /// so that an ArrayList of ModuleSettings objects may be sorted by
    /// ModuleOrder, using the ArrayList's Sort() method.
    /// </summary>
    public class ModuleSettings : IComparable
    {
        public String AuthorizedEditRoles;
        public int CacheTime;
        public String DesktopSrc;
        public String MobileSrc;
        public int ModuleDefId;
        public int ModuleId;
        public int ModuleOrder;
        public String ModuleTitle;
        public String PaneName;
        public bool ShowMobile;
        public int TabId;

        #region IComparable Members

        public int CompareTo(object value)
        {
            if (value == null) return 1;

            int compareOrder = ((ModuleSettings) value).ModuleOrder;

            if (ModuleOrder == compareOrder) return 0;
            if (ModuleOrder < compareOrder) return -1;
            if (ModuleOrder > compareOrder) return 1;
            return 0;
        }

        #endregion
    }
}