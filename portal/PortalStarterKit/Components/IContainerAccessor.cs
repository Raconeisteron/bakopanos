using Microsoft.Practices.Unity;

namespace PortalStarterKit.Components
{
    public interface IContainerAccessor
    {
        IUnityContainer Container { get; }
    }
}