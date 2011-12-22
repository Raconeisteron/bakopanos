using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// The PortalSecurity class encapsulates two helper methods that enable
    /// developers to easily check the role status of the current browser client.
    /// </summary>
    internal class PortalSecurity : IPortalSecurity
    {
        [Dependency]
        public IAuthorizationDb Model { get; set; }

        //*********************************************************************
        //
        // Security.Encrypt() Method
        //
        // The Encrypt method encrypts a clean string into a hashed string
        //
        //*********************************************************************

        #region IPortalSecurity Members

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