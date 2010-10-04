// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using UIComposition.Infrastructure;
using UIComposition.Model;
using UIComposition.Project.Model;
using UIComposition.Project.Views;

namespace UIComposition.Project
{
    public class ProjectModule : IModule
    {
       
        private readonly IUnityContainer _unityContainer;

        public ProjectModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        #region IModule Members

        public void Initialize()
        {
            //this is an optional module. Isolate it...
            IUnityContainer container= _unityContainer.CreateChildContainer();
            container.RegisterSingleton<ProjectWorkItem, ProjectWorkItem>();
            container.RegisterSingleton<IProjectWorkItem, ProjectWorkItem>();
            container.RegisterSingleton<IProjectList, ProjectWorkItem>();     

            // Register a type for pull based based composition. 
            _unityContainer.RegisterViewWithRegion<ProjectsListView>(RegionNames.NaviRegion);
            _unityContainer.RegisterViewWithRegion<ProjectsListView>(RegionNames.MainDetailsTabRegion);
        }

        #endregion
    }
}