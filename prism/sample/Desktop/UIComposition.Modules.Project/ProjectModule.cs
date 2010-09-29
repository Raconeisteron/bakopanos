// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using Microsoft.Practices.Unity;
using UIComposition.Modules.Project.Views;

namespace UIComposition.Modules.Project
{
    public class ProjectModule : IModule
    {
        private readonly IRegionViewRegistry _regionViewRegistry;
        private readonly IUnityContainer _unityContainer;

        public ProjectModule(IUnityContainer unityContainer, IRegionViewRegistry regionViewRegistry)
        {
            _unityContainer = unityContainer;
            _regionViewRegistry = regionViewRegistry;
        }

        #region IModule Members

        public void Initialize()
        {
            // Register a type for pull based based composition. 
            _regionViewRegistry.RegisterViewWithRegion(RegionNames.TabRegion,
                                                       () =>
                                                       new ProjectsListView(
                                                           _unityContainer.Resolve<ProjectsListViewModel>()));
        }

        #endregion
    }
}