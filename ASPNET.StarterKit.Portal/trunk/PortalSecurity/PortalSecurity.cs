using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using ASPNET.StarterKit.Portal.XmlFile;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// The PortalSecurity class encapsulates two helper methods that enable
    /// developers to easily check the role status of the current browser client.
    /// </summary>
    public class PortalSecurity : IPortalSecurity
    {
        private readonly IModuleConfigurationDb _moduleConfigurationDb;
        public PortalSecurity(IModuleConfigurationDb moduleConfigurationDb)
        {
            _moduleConfigurationDb = moduleConfigurationDb;
        }

        //*********************************************************************
        //
        // Security.Encrypt() Method
        //
        // The Encrypt method encrypts a clean string into a hashed string
        //
        //*********************************************************************

        public string Encrypt(string cleanString)
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

        public bool IsInRole(String role)
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

        public bool IsInRoles(String roles)
        {
            HttpContext context = HttpContext.Current;

            foreach (String role in roles.Split(new[] {';'}))
            {
                if (role != "" && role != null && ((role == "All Users") || (context.User.IsInRole(role))))
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

        public bool HasEditPermissions(int moduleId)
        {
            // Find the appropriate Module in the Module table
            ModuleAuthorization moduleRoles = _moduleConfigurationDb.FindModuleRolesByModuleId(moduleId);

            if (IsInRoles(moduleRoles.AccessRoles) == false || IsInRoles(moduleRoles.EditRoles) == false)
                return false;
            else
                return true;
        }
    }
}