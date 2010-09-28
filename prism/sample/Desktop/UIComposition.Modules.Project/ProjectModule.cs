// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using UIComposition.Infrastructure.Services;
using UIComposition.Modules.Project.Views;

namespace UIComposition.Modules.Project
{
    public class ProjectModule : IModule
    {
        private readonly IRegionViewRegistry _regionViewRegistry;
        private readonly IUnityService _unityService;

        public ProjectModule(IUnityService unityService, IRegionViewRegistry regionViewRegistry)
        {
            _unityService = unityService;
            _regionViewRegistry = regionViewRegistry;
        }

        #region IModule Members

        public void Initialize()
        {
            // Register a type for pull based based composition. 
            _regionViewRegistry.RegisterViewWithRegion(RegionNames.TabRegion,
                                                       () =>
                                                       new ProjectsListView(
                                                           _unityService.Resolve<ProjectsListViewModel>()));
        }

        #endregion
    }
}