// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using UIComposition.Infrastructure;
using UIComposition.Employee.Controllers;
using UIComposition.Employee.Model;
using UIComposition.Employee.Views;
using UIComposition.Model;
using UIComposition.Services;

namespace UIComposition.Employee
{
    public class EmployeeModule : IModule
    {
        private readonly IUnityContainer _unityContainer;
        private readonly IRegionViewRegistry _regionViewRegistry;

        public EmployeeModule(IUnityContainer unityContainer,IRegionViewRegistry regionViewRegistry)
        {
            _unityContainer = unityContainer;
            _regionViewRegistry = regionViewRegistry;

        }

        #region IModule Members

        public void Initialize()
        {
            _unityContainer.RegisterSingleton<IEmployeeWorkItem, EmployeeWorkItem>();

            _unityContainer.RegisterSingleton<IEmployeesController, EmployeesController>();
            _unityContainer.Resolve<IEmployeesController>();

            _regionViewRegistry.RegisterViewWithRegion(RegionNames.ToolBarRegion,
                                                       () =>
                                                       new ToolBarView(
                                                           _unityContainer.Resolve<ToolBarViewModel>()));
            
        }

        #endregion
    }
}