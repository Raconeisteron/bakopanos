using System;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// This class encapsulates the basic attributes of a Module, and is used
    /// by the administration pages when manipulating modules.  ModuleItem implements 
    /// the IComparable interface so that an ArrayList of ModuleItems may be sorted
    /// by ModuleOrder, using the ArrayList's Sort() method.
    /// </summary>
    public class ModuleItem : IComparable
    {
        public int ModuleOrder { get; set; }

        public String ModuleTitle { get; set; }

        public String PaneName { get; set; }

        public int ModuleId { get; set; }

        public int ModuleDefId { get; set; }

        public string EditRoles { get; set; }

        public int CacheTimeout { get; set; }

        public int TabId { get; set; }

        public bool ShowMobile { get; set; }

        #region IComparable Members

        public int CompareTo(object value)
        {
            if (value == null) return 1;

            int compareOrder = ((ModuleItem) value).ModuleOrder;

            if (ModuleOrder == compareOrder) return 0;
            if (ModuleOrder < compareOrder) return -1;
            if (ModuleOrder > compareOrder) return 1;
            return 0;
        }

        #endregion
    }
}