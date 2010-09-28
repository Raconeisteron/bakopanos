// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using UIComposition.Infrastructure.Services;
using UIComposition.Modules.Employee.Controllers;
using UIComposition.Modules.Employee.Views;

namespace UIComposition.Modules.Employee
{
    public class EmployeeModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IRegionViewRegistry _regionViewRegistry;
        private readonly IUnityService _unityService;

        public EmployeeModule(IUnityService unityService, IRegionViewRegistry regionViewRegistry,
                              IRegionManager regionManager)
        {
            _unityService = unityService;
            _regionViewRegistry = regionViewRegistry;
            _regionManager = regionManager;
        }

        #region IModule Members

        public void Initialize()
        {
            _unityService.RegisterSingleton<IEmployeesController, EmployeesController>();

            RegisterViewsWithRegions();
        }

        #endregion

        private void RegisterViewsWithRegions()
        {
            _regionViewRegistry.RegisterViewWithRegion(RegionNames.SelectionRegion,
                                                       () =>
                                                       new EmployeesListView(
                                                           _unityService.Resolve<EmployeesListViewModel>()));

            _regionManager.RegisterViewWithRegion(Infrastructure.RegionNames.MainRegion,
                                                  () => new EmployeesView());

            _unityService.Resolve<IEmployeesController>();
        }
    }
}