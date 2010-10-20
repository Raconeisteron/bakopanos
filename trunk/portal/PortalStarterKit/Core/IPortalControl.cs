using PortalStarterKit.Model;

namespace PortalStarterKit.Core
{
    public interface IPortalControl 
    {
        string PortalId { get; set; }

        string TabId { get; set; }

        ModuleSettings ModuleSettings { get; set; }
        
        IPortalSecurity PortalSecurity { get; set; }
    }
}