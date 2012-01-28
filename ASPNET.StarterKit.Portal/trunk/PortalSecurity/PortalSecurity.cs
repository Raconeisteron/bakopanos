using System;
using System.Web;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// <see cref="IPortalSecurity"/>
    /// </summary>
    public class PortalSecurity : IPortalSecurity
    {
        private readonly IModuleAuthorizationDb _moduleAuthorizationDb;

        public PortalSecurity(IModuleAuthorizationDb moduleAuthorizationDb)
        {
            _moduleAuthorizationDb = moduleAuthorizationDb;
        }

        #region IPortalSecurity Members

        /// <summary>
        /// <see cref="IPortalSecurity.IsInRole"/>
        /// </summary>
        public bool IsInRole(String role)
        {
            return HttpContext.Current.User.IsInRole(role);
        }

        /// <summary>
        /// <see cref="IPortalSecurity.IsInRoles"/>
        /// </summary>
        public bool IsInRoles(String roles)
        {
            foreach (String role in roles.Split(new[] {';'}))
            {
                if (!string.IsNullOrEmpty(role) && ((role == "All Users") || (IsInRole(role))))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// <see cref="IPortalSecurity.HasEditPermissions"/>
        /// </summary>
        public bool HasEditPermissions(int moduleId)
        {
            // Find the appropriate Module in the Module table
            ModuleAuthorizationItem moduleRoles = _moduleAuthorizationDb.FindModuleRolesByModuleId(moduleId);

            if (IsInRoles(moduleRoles.AccessRoles) == false || IsInRoles(moduleRoles.EditRoles) == false)
                return false;
            else
                return true;
        }

        #endregion
    }
}