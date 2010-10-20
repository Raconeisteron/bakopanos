using Microsoft.Practices.Unity;

namespace PortalStarterKit.Core
{
    internal interface IContainerAccessor
    {
        IUnityContainer Container { get; }
    }
}