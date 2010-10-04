// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using UIComposition.Infrastructure;
using UIComposition.Model;
using UIComposition.Project.Model;
using UIComposition.Project.Views;
using UIComposition.Services;

namespace UIComposition.Project
{
    public class ProjectModule : IModule
    {
        private readonly IRegionViewRegistry _regionViewRegistry;
        private readonly IEmployeeWorkItem _employeeWorkItem;
        private readonly IUnityContainer _unityContainer;

        public ProjectModule(IEmployeeWorkItem employeeWorkItem,
            IRegionViewRegistry regionViewRegistry,IUnityContainer unityContainer)
        {
            _employeeWorkItem = employeeWorkItem;            
            _regionViewRegistry = regionViewRegistry;
            _unityContainer = unityContainer;
        }

        #region IModule Members

        public void Initialize()
        {
            //this is an optional module. Isolate it...
            IUnityContainer container= _unityContainer.CreateChildContainer();
            container.RegisterSingleton<IProjectWorkItem, ProjectWorkItem>();     

            // Register a type for pull based based composition. 
            _regionViewRegistry.RegisterViewWithRegion(RegionNames.NaviRegion,
                                                       () =>
                                                       new ProjectsListView(new ProjectsListViewModel(container.Resolve<IProjectWorkItem>())));

            _regionViewRegistry.RegisterViewWithRegion(RegionNames.MainDetailsTabRegion,
                                                       () =>
                                                       new ProjectsListView(new ProjectsListViewModel(_employeeWorkItem)));
        }

        #endregion
    }
}