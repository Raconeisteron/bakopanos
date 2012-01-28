using System;

namespace ASPNET.StarterKit.Portal
{
    /// <summary>
    /// Encapsulates helper methods that enable
    /// developers to easily check the role status of the current browser client.
    /// </summary>
    public interface IPortalSecurity
    {
        /// <summary>
        /// enables developers to easily check the role
        /// status of the current browser client.
        /// </summary>
        bool IsInRole(String role);

        /// <summary>
        /// enables developers to easily check the role
        /// status of the current browser client against an array of roles
        /// </summary>
        bool IsInRoles(String roles);

        /// <summary>
        /// enables developers to easily check 
        /// whether the current browser client has access to edit the settings
        /// of a specified portal module
        /// </summary>
        bool HasEditPermissions(int moduleId);
    }
}