using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.Practices.Unity;

namespace ASPNETPortal
{
    /// <summary>
    /// The PortalSecurity class encapsulates two helper methods that enable
    /// developers to easily check the role status of the current browser client.
    /// </summary>
    internal class PortalSecurity : IPortalSecurity
    {
        [Dependency]
        public IModuleDb Model { get; set; }

        #region IPortalSecurity Members

        /// <summary>
        /// encrypts a clean string into a hashed string
        /// </summary>        
        public string Encrypt(string cleanString)
        {
            Byte[] clearBytes = new UnicodeEncoding().GetBytes(cleanString);
            Byte[] hashedBytes = ((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);

            return BitConverter.ToString(hashedBytes);
        }

        /*
        /// <summary>
        /// check the role
        /// status of the current browser client.
        /// </summary>        
        public bool IsInRole(String role)
        {
            return HttpContext.Current.User.IsInRole(role);
        }*/

        /// <summary>
        /// check the role
        /// status of the current browser client against an array of roles
        /// </summary>        
        public bool IsInRoles(String roles)
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

        /// <summary>
        /// check 
        /// whether the current browser client has access to edit the settings
        /// of a specified portal module
        /// </summary>        
        public bool HasEditPermissions(int moduleId)
        {
            // Find the appropriate roles for moduleid
            string editRoles = Model.GetEditRolesByModuleId(moduleId);
            string accessRoles = Model.GetAccessRolesByModuleId(moduleId);

            if (IsInRoles(accessRoles) == false || IsInRoles(editRoles) == false)
                return false;

            return true;
        }

        #endregion
    }
}