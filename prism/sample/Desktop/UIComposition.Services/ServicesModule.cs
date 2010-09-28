// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
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
            _unityService.RegisterInstance(EnterpriseLibraryContainer.Current.GetInstance<Database>("Db"));
            //_unityService.RegisterInstance(new ProjectServiceClient());
            //_unityService.RegisterInstance(new EmployeeServiceClient());
        }

        #region IModule Members

        public void Initialize()
        {
            _unityService.RegisterSingleton<IEmployeeService, EmployeeService>();
            _unityService.RegisterSingleton<IProjectService, ProjectService>();
            _unityService.RegisterSingleton<ISelectedEmployeeWorkItem, SelectedEmployeeWorkItem>();
        }

        #endregion
    }
}