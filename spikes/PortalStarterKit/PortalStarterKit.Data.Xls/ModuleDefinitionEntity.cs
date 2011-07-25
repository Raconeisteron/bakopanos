using System;
using System.Data;

namespace PortalStarterKit.Data.Xls
{
    internal class ModuleDefinitionEntity
    {
        public ModuleDefinitionEntity(IDataRecord item)
        {
            ModuleDefId = Convert.ToInt32(item["ModuleDefId"]);
            FriendlyName = item["FriendlyName"] as string;
            SourceFile = item["DesktopSourceFile"] as string;
        }

        public int ModuleDefId { get; set; }
        public string FriendlyName { get; set; }
        public string SourceFile { get; set; }

    }
}