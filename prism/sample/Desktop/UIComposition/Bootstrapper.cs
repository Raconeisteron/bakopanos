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
            Type infrastructureModuleType =
                Type.GetType("UIComposition.Infrastructure.InfrastructureModule,UIComposition.Infrastructure");
            Type servicesModuleType = Type.GetType("UIComposition.Services.ServicesModule,UIComposition.Services");
            Type employeeModuleType =
                Type.GetType("UIComposition.Modules.Employee.EmployeeModule,UIComposition.Modules.Employee");
            Type projectModuleType =
                Type.GetType("UIComposition.Modules.Project.ProjectModule,UIComposition.Modules.Project");

            ModuleCatalog catalog = new ModuleCatalog().
                AddModule(infrastructureModuleType).
                AddModule(servicesModuleType);
            catalog.AddModule(employeeModuleType);
            catalog.AddModule(projectModuleType);

            return catalog;
        }
    }
}