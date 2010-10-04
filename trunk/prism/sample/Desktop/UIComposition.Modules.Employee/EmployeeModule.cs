// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using UIComposition.Infrastructure;
using UIComposition.Modules.Employee.Controllers;
using UIComposition.Modules.Employee.Model;
using UIComposition.Modules.Employee.Views;
using UIComposition.Services;
using UIComposition.Services.Employee;

namespace UIComposition.Modules.Employee
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