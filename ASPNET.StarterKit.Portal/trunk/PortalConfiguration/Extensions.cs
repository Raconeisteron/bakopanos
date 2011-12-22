namespace ASPNETPortal
{
    internal static class Extensions
    {
        internal static GlobalItem ToGlobalItem(this SiteConfiguration.GlobalRow row)
        {
            return new GlobalItem
                       {
                           PortalId = row.PortalId,
                           PortalName = row.PortalName,
                           AlwaysShowEditButton = row.AlwaysShowEditButton
                       };
        }

        internal static TabItem ToTabItem(this SiteConfiguration.TabRow row)
        {
            return new TabItem
                       {
                           TabId = row.TabId,
                           TabName = row.TabName,
                           TabOrder = row.TabOrder,
                           AccessRoles = row.AccessRoles,
                           MobileTabName = row.MobileTabName,
                           ShowMobile = row.ShowMobile
                       };
        }

        internal static ModuleItem ToModuleItem(this SiteConfiguration.ModuleRow row)
        {
            return new ModuleItem
                       {
                           ModuleId = row.ModuleId,
                           ModuleDefId = row.ModuleDefId,
                           ModuleOrder = row.ModuleOrder,
                           ModuleTitle = row.ModuleTitle,
                           PaneName = row.PaneName,
                           EditRoles = row.EditRoles,
                           CacheTimeout = row.CacheTimeout,
                           TabId = row.TabId,
                           ShowMobile = row.ShowMobile
                       };
        }

        internal static ModuleDefinitionItem ToModuleDefinitionItem(this SiteConfiguration.ModuleDefinitionRow row)
        {
            return new ModuleDefinitionItem
                       {
                           ModuleDefId = row.ModuleDefId,
                           DesktopSourceFile = row.DesktopSourceFile,
                           FriendlyName = row.FriendlyName,
                           MobileSourceFile = row.MobileSourceFile
                       };
        }
    }
}