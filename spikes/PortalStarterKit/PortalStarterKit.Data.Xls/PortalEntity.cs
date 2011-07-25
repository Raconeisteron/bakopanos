using System;
using System.Data;

namespace PortalStarterKit.Data.Xls
{
    internal class PortalEntity
    {
        public PortalEntity(IDataRecord item)
        {
            PortalId = Convert.ToInt32(item["PortalId"]);
            PortalName = item["PortalName"] as string;
            AlwaysShowEditButton = Convert.ToBoolean(item["AlwaysShowEditButton"]);
        }

        public int PortalId { get; set; }
        public string PortalName { get; set; }
        public bool AlwaysShowEditButton { get; set; }
    }
}