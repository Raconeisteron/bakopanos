using Microsoft.Practices.Unity;

namespace UIComposition.Infrastructure
{
    internal static class UnityExtensions
    {
        public static void RegisterSingleton<TTo, TFrom>(this IUnityContainer container)
            where TFrom : TTo
        {
            container.RegisterType<TTo, TFrom>(new ContainerControlledLifetimeManager());
        }
    }
}