// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Regions;

namespace Microsoft.Practices.Unity
{
    public static class UnityExtensions
    {
        public static void RegisterSingleton<TTo, TFrom>(this IUnityContainer container)
            where TFrom : TTo
        {
            container.RegisterType<TTo, TFrom>(new ContainerControlledLifetimeManager());
        }

        public static void RegisterSingleton<TTo, TFrom>(this IUnityContainer container,string name)
           where TFrom : TTo
        {
            container.RegisterType<TTo, TFrom>(name, new ContainerControlledLifetimeManager());
        }

        public static void RegisterViewWithRegion<TView>(this IUnityContainer unityContainer,string regionName)
        {
            var regionViewRegistry = unityContainer.Resolve<IRegionViewRegistry>();
            regionViewRegistry.RegisterViewWithRegion(regionName,
                                                      () => unityContainer.Resolve<TView>());
        }
    }
}