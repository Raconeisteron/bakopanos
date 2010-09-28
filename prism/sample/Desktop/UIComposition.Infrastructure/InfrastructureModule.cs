// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using UIComposition.Infrastructure.Services;

namespace UIComposition.Infrastructure
{
    internal class InfrastructureModule : IModule
    {
        private readonly IUnityContainer _container;

        public InfrastructureModule(IUnityContainer container)
        {
            _container = container;
        }

        #region IModule Members

        public void Initialize()
        {
            _container.RegisterSingleton<IUnityService, UnityService>();
            _container.RegisterSingleton<ILogService, LogService>();
        }

        #endregion
    }
}