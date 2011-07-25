using System;
using System.Data;
using PortalStarterKit.Model;

namespace PortalStarterKit.Data.Xls
{
    internal class ModuleEntity
    {
        public ModuleEntity(IDataRecord item)
        {
            ModuleId = Convert.ToInt32(item["ModuleId"]);
            ModuleTitle = item["ModuleTitle"] as string;
        }

        public int TabId { get; set; }
        public int ModuleId { get; set; }
        public int ModuleDefId { get; set; }
        public int ModuleOrder { get; set; }
        public string ModuleTitle { get; set; }
        public PaneType PaneName { get; set; }
    }
}