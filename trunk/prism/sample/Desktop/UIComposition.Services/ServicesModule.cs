// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Modularity;
using UIComposition.Infrastructure.Services;
using UIComposition.Services.Employee;
using UIComposition.Services.Project;

namespace UIComposition.Services
{
    internal class ServicesModule : IModule
    {
        private readonly IUnityService _unityService;

        public ServicesModule(IUnityService unityService)
        {
            _unityService = unityService;
        }

        #region IModule Members

        public void Initialize()
        {
            _unityService.RegisterSingleton<IEmployeeService, FakeEmployeeService>();
            _unityService.RegisterSingleton<IProjectService, FakeProjectService>();
            _unityService.RegisterSingleton<ISelectedEmployeeWorkItem, SelectedEmployeeWorkItem>();
        }

        #endregion
    }
}