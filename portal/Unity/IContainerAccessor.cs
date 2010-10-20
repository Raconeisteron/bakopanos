using Microsoft.Practices.Unity;

namespace PortalStarterKit
{
    public interface IContainerAccessor
    {
        IUnityContainer Container { get; }
    }
}