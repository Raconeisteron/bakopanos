// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System;
using System.Windows;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.UnityExtensions;
using Microsoft.Practices.Unity;

namespace UIComposition
{
    internal class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            var shell = Container.Resolve<Shell>();
            shell.Show();
            return shell;
        }

        protected override IModuleCatalog GetModuleCatalog()
        {
            //Type shellModuleType = Type.GetType("UIComposition.ShellModule,UIComposition");

            //Type infrastructureModuleType =
            //    Type.GetType("UIComposition.Infrastructure.InfrastructureModule,UIComposition.Infrastructure");
            //Type servicesModuleType = Type.GetType("UIComposition.Services.ServicesModule,UIComposition.Services");
            
            //var catalog = new ModuleCatalog();
            //catalog.AddModule(infrastructureModuleType).
            //    AddModule(servicesModuleType);

            ////modules
            //Type employeeModuleType =
            //    Type.GetType("UIComposition.Employee.EmployeeModule,UIComposition.Employee");

            //Type projectModuleType =
            //   Type.GetType("UIComposition.Project.ProjectModule,UIComposition.Project");
            //catalog.AddModule(employeeModuleType);
            //catalog.AddModule(projectModuleType);


            //catalog.AddModule(shellModuleType);

            return new ConfigurationModuleCatalog();
        }
    }
}