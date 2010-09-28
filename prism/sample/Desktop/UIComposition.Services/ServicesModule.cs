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

        public void Initialize()
        {
            _unityService.RegisterSingleton<IEmployeeService, EmployeeService>();
            _unityService.RegisterSingleton<IProjectService, ProjectService>();
            _unityService.RegisterSingleton<ISelectedEmployeeContext, SelectedEmployeeContext>();
        }
    }
}