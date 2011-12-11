using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// The PortalSecurity class encapsulates two helper methods that enable
    /// developers to easily check the role status of the current browser client.
    /// </summary>
    public static class PortalSecurity
    {
        //*********************************************************************
        //
        // Security.Encrypt() Method
        //
        // The Encrypt method encrypts a clean string into a hashed string
        //
        //*********************************************************************

        public static string Encrypt(string cleanString)
        {
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(cleanString);
            Byte[] hashedBytes = ((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);

            return BitConverter.ToString(hashedBytes);
        }

        //*********************************************************************
        //
        // PortalSecurity.IsInRole() Method
        //
        // The IsInRole method enables developers to easily check the role
        // status of the current browser client.
        //
        //*********************************************************************

        public static bool IsInRole(String role)
        {
            return HttpContext.Current.User.IsInRole(role);
        }

        //*********************************************************************
        //
        // PortalSecurity.IsInRoles() Method
        //
        // The IsInRoles method enables developers to easily check the role
        // status of the current browser client against an array of roles
        //
        //*********************************************************************

        public static bool IsInRoles(String roles)
        {
            HttpContext context = HttpContext.Current;

            foreach (String role in roles.Split(new[] {';'}))
            {
                if (!string.IsNullOrEmpty(role) && ((role == "All Users") || (context.User.IsInRole(role))))
                {
                    return true;
                }
            }

            return false;
        }

        //*********************************************************************
        //
        // PortalSecurity.HasEditPermissions() Method
        //
        // The HasEditPermissions method enables developers to easily check 
        // whether the current browser client has access to edit the settings
        // of a specified portal module
        //
        //*********************************************************************

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