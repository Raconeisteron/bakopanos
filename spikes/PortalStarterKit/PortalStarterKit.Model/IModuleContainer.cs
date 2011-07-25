using System.Collections.Generic;

namespace PortalStarterKit.Model
{
    public interface IModuleContainer
    {
        List<Module> Modules { get; }

        Module NewModule();
    }
}