using Microsoft.Practices.Composite.Modularity;
using UIComposition.Infrastructure.Services;
using UIComposition.Services.Employee;
using UIComposition.Services.Project;

namespace UIComposition.Services
{
    [Module(ModuleName = "Services")]
    public class ServicesModule : IModule
    {
        private readonly IUnityService _unityService;

        public ServicesModule(IUnityService unityService)
        {
            _unityService = unityService;
        }

        #region IModule Members

        public void Initialize()
        {
            RegisterTypesAndServices();
        }

        #endregion

        protected void RegisterTypesAndServices()
        {
            _unityService.RegisterSingleton<IEmployeeService, EmployeeService>();
            _unityService.RegisterSingleton<IProjectService, ProjectService>();
        }
    }
}