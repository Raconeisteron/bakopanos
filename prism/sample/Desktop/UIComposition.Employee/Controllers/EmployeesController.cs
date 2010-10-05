// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation.Commands;
using Microsoft.Practices.Unity;
using UIComposition.Infrastructure;
using UIComposition.Infrastructure.Services;
using UIComposition.Employee.Views;
using UIComposition.Model;

namespace UIComposition.Employee.Controllers
{
    internal class EmployeesController : IEmployeesController
    {
        private readonly ILogService _logService;
        private readonly IUnityContainer _unityContainer;
        private bool areViewsRegistered;

        public EmployeesController(IUnityContainer unityContainer, ILogService logService, IEventAggregator eventAggregator)
        {
            _unityContainer = unityContainer;            
            _logService = logService;
            eventAggregator.GetEvent<SelectedEmployeeEvent>().Subscribe(NavigateSelectedEmployee);

            ShowModuleCommand = new DelegateCommand<object>(ShowModule, CanShowModule);            
        }
        
        public DelegateCommand<object> ShowModuleCommand { get; private set; }

        private void ShowModule(object arg)
        {
            if (!areViewsRegistered)
            {
                _unityContainer.RegisterViewWithRegion<NaviBarView>(RegionNames.NaviRegion);

                _unityContainer.RegisterViewWithRegion<EmployeesListView>(RegionNames.MainSelectionRegion);

                _unityContainer.RegisterViewWithRegion<EmployeesView>(RegionNames.MainRegion);

            }
            areViewsRegistered = true;
        }

        private static bool CanShowModule(object arg)
        {
            return true;
        }

        private void NavigateSelectedEmployee(EmployeeItem employee)
        {
            _logService.WriteInfo("EmployeesController::OnEmployeeSelected");
            _unityContainer.ActivateView<EmployeesDetailsView>(RegionNames.MainDetailsRegion, "EmployeesDetailsView");            
        }
        
    }
}