using System;

namespace ASPNET.StarterKit.Portal
{
    public interface IPortalSecurity
    {
        string Encrypt(string cleanString);
        bool IsInRole(String role);
        bool IsInRoles(String roles);
        bool HasEditPermissions(int moduleId);
    }
}