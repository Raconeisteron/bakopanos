// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Unity;

namespace UIComposition.Infrastructure.Services
{
    internal class UnityService : IUnityService
    {
        private readonly IUnityContainer _container;

        public UnityService(IUnityContainer container)
        {
            _container = container;
        }

        #region IUnityService Members

        public void RegisterSingleton<TTo, TFrom>()
            where TFrom : TTo
        {
            _container.RegisterType<TTo, TFrom>(new ContainerControlledLifetimeManager());
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public void RegisterInstance<T>(T instance)
        {
            _container.RegisterInstance(instance);
        }

        #endregion
    }
}