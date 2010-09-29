// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using UIComposition.Modules.Employee.Controllers;

namespace UIComposition.Modules.Employee
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
            _unityContainer.RegisterSingleton<IEmployeesController, EmployeesController>();

            _unityContainer.Resolve<IEmployeesController>();
        }

        #endregion
    }
}