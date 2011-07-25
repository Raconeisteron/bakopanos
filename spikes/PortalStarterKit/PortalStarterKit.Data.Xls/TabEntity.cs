using System;
using System.Data;

namespace PortalStarterKit.Data.Xls
{
    internal class TabEntity
    {
        public TabEntity(IDataRecord item)
        {
            PortalId = Convert.ToInt32(item["PortalId"]);
            TabId = Convert.ToInt32(item["TabId"]);
            ParentTabId = Convert.ToInt32(item["ParentTabId"]);
            TabDefId = Convert.ToInt32(item["TabDefId"]);
            TabName = item["TabName"] as string;
        }

        public int PortalId { get; set; }
        public int TabId { get; set; }
        public int ParentTabId { get; set; }
        public int TabDefId { get; set; }
        public string TabName { get; set; }
        public int TabOrder { get; set; }

    }
}