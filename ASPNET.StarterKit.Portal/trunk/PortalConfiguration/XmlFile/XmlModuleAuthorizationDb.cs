namespace ASPNET.StarterKit.Portal.XmlFile
{
    public class XmlModuleAuthorizationDb : IModuleAuthorizationDb
    {
        private readonly IConfigurationDb _configurationDb;

        public XmlModuleAuthorizationDb(IConfigurationDb configurationDb)
        {
            _configurationDb = configurationDb;
        }

        public ModuleAuthorization FindModuleRolesByModuleId(int moduleId)
        {
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration)_configurationDb.GetSiteSettings();

            SiteConfiguration.ModuleRow moduleRow = siteSettings.Module.FindByModuleId(moduleId);

            return new ModuleAuthorization
                       {
                           AccessRoles = moduleRow.TabRow.AccessRoles,
                           EditRoles = moduleRow.EditRoles
                       };
        }

    }
}