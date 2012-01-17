using System;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// encapsulates the detailed settings for a specific Tab 
    /// in the Portal.  ModuleSettings implements the IComparable interface 
    /// so that an ArrayList of ModuleSettings objects may be sorted by
    /// ModuleOrder, using the ArrayList's Sort() method.
    /// </summary>
    public class ModuleSettings : IComparable
    {
        public string AuthorizedEditRoles { get; set; }
        public int CacheTime { get; set; }
        public string DesktopSrc { get; set; }
        public string MobileSrc { get; set; }
        public int ModuleDefId { get; set; }
        public int ModuleId { get; set; }
        public int ModuleOrder { get; set; }
        public string ModuleTitle { get; set; }
        public string PaneName { get; set; }
        public bool ShowMobile { get; set; }
        public int TabId { get; set; }

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