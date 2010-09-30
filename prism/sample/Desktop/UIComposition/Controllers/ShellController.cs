// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using UIComposition.Infrastructure;
using UIComposition.Views;

namespace UIComposition.Controllers
{
    internal class ShellController : IShellController
    {
        private readonly IRegionViewRegistry _regionViewRegistry;
        private readonly IUnityContainer _unityContainer;

        public ShellController(IUnityContainer unityContainer, IRegionViewRegistry regionViewRegistry)
        {
            _unityContainer = unityContainer;
            _regionViewRegistry = regionViewRegistry;

            _regionViewRegistry.RegisterViewWithRegion(RegionNames.ToolBarRegion,
                                                       () =>
                                                       new ToolBarView(
                                                           _unityContainer.Resolve<ToolBarViewModel>()));
        }
    }
}