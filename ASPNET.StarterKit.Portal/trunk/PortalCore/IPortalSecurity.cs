using System;

namespace ASPNETPortal
{
    public interface IPortalSecurity
    {
        string Encrypt(string cleanString);
        bool HasEditPermissions(int moduleId);
        //bool IsInRole(String role);
        bool IsInRoles(String roles);
    }
}