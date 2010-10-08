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
            _unityContainer.RegisterViewWithRegion<EmployeeProjectsListView>(RegionNames.MainDetailsTabRegion);

            //this is an optional module. Isolate it...
            IUnityContainer container= _unityContainer.CreateChildContainer();
            container.RegisterSingleton<ProjectWorkItem, ProjectWorkItem>()
                .RegisterSingleton<IProjectWorkItem, ProjectWorkItem>()
                .RegisterSingleton<IProjectList, ProjectWorkItem>()
                .RegisterViewWithRegion<ProjectsListView>(RegionNames.NaviRegion);            
        }

        #endregion
    }
}