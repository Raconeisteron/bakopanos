using System;
using System.Web;

namespace PortalStarterKit.Components
{
    /// <summary>
    /// encapsulates helper methods that enable
    /// developers to easily check the role status of the current browser client.
    /// </summary>
    public class PortalSecurity : IPortalSecurity
    {
        /// <summary>
        /// enables developers to easily check the role
        /// status of the current browser client.
        /// </summary>
        public bool IsInRole(String role)
        {
            return HttpContext.Current.User.IsInRole(role);
        }

       /// <summary>
       /// enables developers to easily check the role
       /// status of the current browser client against an array of roles
       /// </summary>
        public bool IsInRoles(String roles)
        {
            /*HttpContext context = HttpContext.Current;

            foreach (String role in roles.Split(new[] { ';' }))
            {
                if (!string.IsNullOrEmpty(role) && ((role == "All Users") || (context.User.IsInRole(role))))
                {
                    return true;
                }
            }
            */
            return true;
        }

        /// <summary>
        /// enables developers to easily check 
        /// whether the current browser client has access to edit the settings
        /// of a specified portal module
        /// </summary>
        public bool HasEditPermissions(PortalSettings module)
        {
            /*
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration)HttpContext.Current.Items["SiteSettings"];

            // Find the appropriate Module in the Module table
            SiteConfiguration.ModuleRow moduleRow = siteSettings.Module.FindByModuleId(moduleId);

            string editRoles = moduleRow.EditRoles;
            string accessRoles = moduleRow.TabRow.AccessRoles;

            if (IsInRoles(accessRoles) == false || IsInRoles(editRoles) == false)
            {
                return false;
            }
             */
            return true;
        }
    }
}