namespace ASPNET.StarterKit.Portal.XmlFile
{
    public class XmlModuleAuthorizationDb : IModuleAuthorizationDb
    {
        private readonly IConfigurationDb _configurationDb;

        public XmlModuleAuthorizationDb(IConfigurationDb configurationDb)
        {
            _configurationDb = configurationDb;
        }

        #region IModuleAuthorizationDb Members

        public ModuleAuthorizationItem FindModuleRolesByModuleId(int moduleId)
        {
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration) _configurationDb.GetSiteSettings();

            SiteConfiguration.ModuleRow moduleRow = siteSettings.Module.FindByModuleId(moduleId);

            return new ModuleAuthorizationItem
                       {
                           AccessRoles = moduleRow.TabRow.AccessRoles,
                           EditRoles = moduleRow.EditRoles
                       };
        }

        #endregion
    }
}