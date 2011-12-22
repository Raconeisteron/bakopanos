using System;

namespace ASPNETPortal
{
    /// <summary>
    /// This class encapsulates the basic attributes of a Tab, and is used
    /// by the administration pages when manipulating tabs.  TabItem implements 
    /// the IComparable interface so that an ArrayList of TabItems may be sorted
    /// by TabOrder, using the ArrayList's Sort() method.
    /// </summary>
    public class TabItem : IComparable
    {
        public int TabOrder { get; set; }

        public String TabName { get; set; }

        public int TabId { get; set; }

        public string AccessRoles { get; set; }

        public bool ShowMobile { get; set; }

        public string MobileTabName { get; set; }

        #region IComparable Members

        public int CompareTo(object value)
        {
            if (value == null) return 1;

            int compareOrder = ((TabItem) value).TabOrder;

            if (TabOrder == compareOrder) return 0;
            if (TabOrder < compareOrder) return -1;
            if (TabOrder > compareOrder) return 1;
            return 0;
        }

        #endregion
    }
}