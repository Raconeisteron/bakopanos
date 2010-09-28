//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================
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

            var catalog = new ModuleCatalog().
                AddModule(infrastructureModuleType).
                AddModule(servicesModuleType);
            catalog.AddModule(employeeModuleType);
            catalog.AddModule(projectModuleType);

            return catalog;
        }
    }
}