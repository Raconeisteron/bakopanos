// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using UIComposition.Infrastructure;
using UIComposition.Modules.Project.Views;
using UIComposition.Services;

namespace UIComposition.Modules.Project
{
    public class ProjectModule : IModule
    {
        private readonly IRegionViewRegistry _regionViewRegistry;
        private IEmployeeWorkItem _employeeWorkItem;
        private IProjectWorkItem _projectWorkItem;
        public ProjectModule(IEmployeeWorkItem employeeWorkItem,IProjectWorkItem projectWorkItem, IRegionViewRegistry regionViewRegistry)
        {
            _employeeWorkItem = employeeWorkItem;
            _projectWorkItem = projectWorkItem;
            _regionViewRegistry = regionViewRegistry;
        }

        #region IModule Members

        public void Initialize()
        {
            // Register a type for pull based based composition. 
            _regionViewRegistry.RegisterViewWithRegion(RegionNames.NaviRegion,
                                                       () =>
                                                       new ProjectsListView(new ProjectsListViewModel(_projectWorkItem)));

            _regionViewRegistry.RegisterViewWithRegion(RegionNames.MainDetailsTabRegion,
                                                       () =>
                                                       new ProjectsListView(new ProjectsListViewModel(_employeeWorkItem)));
        }

        #endregion
    }
}