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
            _unityContainer.RegisterSingleton<EmployeeWorkItem, EmployeeWorkItem>()
                .RegisterSingleton<IEmployeeWorkItem, EmployeeWorkItem>()
                .RegisterSingleton<IProjectList, EmployeeWorkItem>()
                .RegisterSingleton<IEmployeeList, EmployeeWorkItem>()
                .RegisterSingleton<IEmployeeInfo, EmployeeWorkItem>()
                .RegisterSingleton<IEmployeesController, EmployeesController>()
                .RegisterViewWithRegion<ToolBarView>(RegionNames.ToolBarRegion);

            _unityContainer.Resolve<IEmployeesController>();
        }

        #endregion
    }
}