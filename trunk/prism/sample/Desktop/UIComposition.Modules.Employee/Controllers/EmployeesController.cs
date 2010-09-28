// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Regions;
using UIComposition.BusinessEntities;
using UIComposition.Infrastructure.Services;
using UIComposition.Modules.Employee.Views;
using UIComposition.Services;

namespace UIComposition.Modules.Employee.Controllers
{
    internal class EmployeesController : IEmployeesController
    {
        private readonly ILogService _logService;
        private readonly IRegionManager _regionManager;
        private readonly IUnityService _unityService;

        public EmployeesController(IUnityService unityService, ILogService logService, IRegionManager regionManager,
                                   IEventAggregator eventAggregator)
        {
            _unityService = unityService;
            _regionManager = regionManager;
            _logService = logService;
            eventAggregator.GetEvent<SelectedEmployeeEvent>().Subscribe(NavigateSelectedEmployee);
        }

        private void NavigateSelectedEmployee(EmployeeItem employee)
        {
            _logService.WriteInfo("EmployeesController::OnEmployeeSelected");
            IRegion detailsRegion = _regionManager.Regions[RegionNames.DetailsRegion];
            object existingView = detailsRegion.GetView("EmployeesDetailsView");

            // See if the view already exists in the region. 
            if (existingView == null)
            {
                // the view does not exist yet. Create it and push it into the region
                var detailsView = _unityService.Resolve<EmployeesDetailsView>();

                // the details view should receive it's own scoped region manager, therefore Add overload using 'true' (see notes below).
                detailsRegion.Add(detailsView, "EmployeesDetailsView", true);

                detailsRegion.Activate(detailsView);
            }
            else
            {
                // The view already exists. Just show it. 
                detailsRegion.Activate(existingView);
            }
        }
    }
}