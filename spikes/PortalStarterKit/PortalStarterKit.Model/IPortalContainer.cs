using System.Collections.Generic;

namespace PortalStarterKit.Model
{
    public interface IPortalContainer
    {
        List<Portal> Portals { get; }

        Portal NewPortal();
    }
}