// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using UIComposition.Infrastructure;

namespace UIComposition
{
    public class ShellModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IRegionViewRegistry _regionViewRegistry;
        private readonly IUnityContainer _unityContainer;

        public ShellModule(IUnityContainer unityContainer, IRegionViewRegistry regionViewRegistry,
                              IRegionManager regionManager)
        {
            _unityContainer = unityContainer;
            _regionViewRegistry = regionViewRegistry;
            _regionManager = regionManager;
        }

        #region IModule Members

        public void Initialize()
        {
            RegisterViewsWithRegions();
        }

        #endregion

        private void RegisterViewsWithRegions()
        {
            
        }
    }
}