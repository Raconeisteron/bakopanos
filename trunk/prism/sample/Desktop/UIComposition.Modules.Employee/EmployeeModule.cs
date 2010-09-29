// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using UIComposition.Modules.Employee.Controllers;
using UIComposition.Modules.Employee.Views;

namespace UIComposition.Modules.Employee
{
    public class EmployeeModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IRegionViewRegistry _regionViewRegistry;
        private readonly IUnityContainer _unityContainer;

        public EmployeeModule(IUnityContainer unityContainer, IRegionViewRegistry regionViewRegistry,
                              IRegionManager regionManager)
        {
            _unityContainer = unityContainer;
            _regionViewRegistry = regionViewRegistry;
            _regionManager = regionManager;
        }

        #region IModule Members

        public void Initialize()
        {
            _unityContainer.RegisterSingleton<IEmployeesController, EmployeesController>();

            RegisterViewsWithRegions();
        }

        #endregion

        private void RegisterViewsWithRegions()
        {
            _regionViewRegistry.RegisterViewWithRegion(RegionNames.SelectionRegion,
                                                       () =>
                                                       new EmployeesListView(
                                                           _unityContainer.Resolve<EmployeesListViewModel>()));

            _regionManager.RegisterViewWithRegion(Infrastructure.RegionNames.MainRegion,
                                                  () => new EmployeesView());

            _unityContainer.Resolve<IEmployeesController>();
        }
    }
}