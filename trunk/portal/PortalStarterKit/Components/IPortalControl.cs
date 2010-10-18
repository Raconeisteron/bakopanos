using System.Web.UI;

namespace PortalStarterKit.Components
{
    public interface IPortalControl 
    {
        string PortalId { get; set; }

        string TabId { get; set; }

        ModuleSettings ModuleConfiguration { get; set; }
        
        IPortalSecurity PortalSecurity { get; set; }
    }
}