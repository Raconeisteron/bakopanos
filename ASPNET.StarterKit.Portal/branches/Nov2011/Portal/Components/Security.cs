using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Portal.Components
{
    /// <summary>
    /// The PortalSecurity class encapsulates two helper methods that enable
    /// developers to easily check the role status of the current browser client.
    /// </summary>
    public class PortalSecurity
    {
        /// <summary>
        /// The Encrypt method encrypts a clean string into a hashed string
        /// </summary>
        public static string Encrypt(string cleanString)
        {
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(cleanString);
            Byte[] hashedBytes = ((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);

            return BitConverter.ToString(hashedBytes);
        }

        /// <summary>
        /// The IsInRole method enables developers to easily check the role
        /// status of the current browser client.
        /// </summary>        
        public static bool IsInRole(string role)
        {
            return HttpContext.Current.User.IsInRole(role);
        }

        /// <summary>
        /// The IsInRoles method enables developers to easily check the role
        /// status of the current browser client against an array of roles
        /// </summary>        
        public static bool IsInRoles(string roles)
        {
            HttpContext context = HttpContext.Current;
            string[] list = roles.Split(new[] {';'});

            foreach (string role in list)
            {
                if (!string.IsNullOrEmpty(role) && ((role == "All Users") || (context.User.IsInRole(role))))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// The HasEditPermissions method enables developers to easily check 
        /// whether the current browser client has access to edit the settings
        /// of a specified portal module
        /// </summary>        
        public static bool HasEditPermissions(int moduleId)
        {
            // Obtain SiteSettings from Current Context
            var siteSettings = (SiteConfiguration) HttpContext.Current.Items["SiteSettings"];

            // Find the appropriate Module in the Module table
            SiteConfiguration.ModuleRow moduleRow = siteSettings.Module.FindByModuleId(moduleId);

            string editRoles = moduleRow.EditRoles;
            string accessRoles = moduleRow.TabRow.AccessRoles;

            if (IsInRoles(accessRoles) == false || IsInRoles(editRoles) == false)
                return false;
            return true;
        }
    }
}