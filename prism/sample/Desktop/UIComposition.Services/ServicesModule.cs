// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;

namespace UIComposition.Services
{

    internal class ServicesModule : IModule
    {
        private readonly IUnityContainer _container;

        public ServicesModule(IUnityContainer container)
        {
            _container = container;
            //_container.RegisterInstance(new ProjectServiceClient());
            //_container.RegisterInstance(new EmployeeServiceClient());
        }

        #region IModule Members

        public void Initialize()
        {
            _container.RegisterSingleton<IEmployeeService, FakeEmployeeService>();
            _container.RegisterSingleton<IProjectService, FakeProjectService>();

            //_container.RegisterSingleton<IEmployeeService, WcfEmployeeService>();
            //_container.RegisterSingleton<IProjectService, WcfProjectService>();

                   
        }

        #endregion
    }
}