// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
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
        private readonly IUnityContainer _unityContainer;

        public EmployeesController(IUnityContainer unityContainer, ILogService logService, IRegionManager regionManager,
                                   IRegionViewRegistry regionViewRegistry,
                                   IEventAggregator eventAggregator)
        {
            _unityContainer = unityContainer;
            _regionManager = regionManager;
            _logService = logService;
            eventAggregator.GetEvent<SelectedEmployeeEvent>().Subscribe(NavigateSelectedEmployee);

            regionViewRegistry.RegisterViewWithRegion(RegionNames.ToolBarRegion,
                                                       () =>
                                                       new ToolBarView(
                                                           _unityContainer.Resolve<ToolBarViewModel>()));

            regionViewRegistry.RegisterViewWithRegion(RegionNames.SelectionRegion,
                                                      () =>
                                                      new EmployeesListView(
                                                          _unityContainer.Resolve<EmployeesListViewModel>()));

            _regionManager.RegisterViewWithRegion(Infrastructure.RegionNames.MainRegion,
                                                  () => new EmployeesView());
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
                var detailsView = _unityContainer.Resolve<EmployeesDetailsView>();

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