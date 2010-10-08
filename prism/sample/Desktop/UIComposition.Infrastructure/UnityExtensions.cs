// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Regions;

namespace Microsoft.Practices.Unity
{
    public static class UnityExtensions
    {
        public static IUnityContainer RegisterSingleton<TTo, TFrom>(this IUnityContainer container)
            where TFrom : TTo
        {
            container.RegisterType<TTo, TFrom>(new ContainerControlledLifetimeManager());
            return container;
        }

        public static void RegisterSingleton<TTo, TFrom>(this IUnityContainer container, string name)
           where TFrom : TTo
        {
            container.RegisterType<TTo, TFrom>(name, new ContainerControlledLifetimeManager());
        }

        public static IUnityContainer RegisterViewWithRegion<TView>(this IUnityContainer unityContainer, string regionName)
        {
            var regionViewRegistry = unityContainer.Resolve<IRegionViewRegistry>();
            regionViewRegistry.RegisterViewWithRegion(regionName,
                                                      () => unityContainer.Resolve<TView>());
            return unityContainer;
        }

        static IRegion GetRegion(this IUnityContainer unityContainer, string regionName)
        {
            var regionManager = unityContainer.Resolve<IRegionManager>();
            return regionManager.Regions[regionName];
        }

        public static void ActivateView<T>(this IUnityContainer unityContainer, string regionName, string viewName)
        {
            IRegion region = unityContainer.GetRegion(regionName);
            object existingView = region.GetView(viewName);

            // See if the view already exists in the region. 
            if (existingView == null)
            {
                // the view does not exist yet. Create it and push it into the region
                var view = unityContainer.Resolve<T>();

                // the details view should receive it's own scoped region manager, therefore Add overload using 'true' (see notes below).
                region.Add(view, viewName, true);

                region.Activate(view);
            }
            else
            {
                // The view already exists. Just show it. 
                region.Activate(existingView);
            }
        }
    }
}