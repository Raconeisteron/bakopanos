// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using UIComposition.Infrastructure;
using UIComposition.Employee.Controllers;
using UIComposition.Employee.Model;
using UIComposition.Employee.Views;
using UIComposition.Model;

namespace UIComposition.Employee
{
    public class EmployeeModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public EmployeeModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        #region IModule Members

        public void Initialize()
        {
            _unityContainer.RegisterSingleton<EmployeeWorkItem, EmployeeWorkItem>();
            _unityContainer.RegisterSingleton<IEmployeeWorkItem, EmployeeWorkItem>();
            _unityContainer.RegisterSingleton<IProjectList, EmployeeWorkItem>();
            _unityContainer.RegisterSingleton<IEmployeeList, EmployeeWorkItem>();
            _unityContainer.RegisterSingleton<IEmployeeInfo, EmployeeWorkItem>();

            _unityContainer.RegisterSingleton<IEmployeesController, EmployeesController>();
            _unityContainer.Resolve<IEmployeesController>();

            _unityContainer.RegisterViewWithRegion<ToolBarView>(RegionNames.ToolBarRegion);
            
        }

        #endregion
    }
}