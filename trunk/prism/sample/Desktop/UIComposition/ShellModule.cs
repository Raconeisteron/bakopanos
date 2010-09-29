// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using UIComposition.Infrastructure;
using UIComposition.Views;

namespace UIComposition
{
    public class ShellModule : IModule
    {
        private readonly IRegionViewRegistry _regionViewRegistry;
        private readonly IUnityContainer _unityContainer;

        public ShellModule(IUnityContainer unityContainer, IRegionViewRegistry regionViewRegistry)
        {
            _unityContainer = unityContainer;
            _regionViewRegistry = regionViewRegistry;            
        }

        #region IModule Members

        public void Initialize()
        {
            RegisterViewsWithRegions();
        }

        #endregion

        private void RegisterViewsWithRegions()
        {
            _regionViewRegistry.RegisterViewWithRegion(RegionNames.ToolBarRegion,
                                                       () =>
                                                       new ToolBarView(
                                                           _unityContainer.Resolve<ToolBarViewModel>()));
        }
    }
}