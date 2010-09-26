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

        protected override void InitializeModules()
        {
            Type infrastructureModuleType = Type.GetType("UIComposition.Infrastructure.InfrastructureModule,UIComposition.Infrastructure");
            var infrastructureModule = (IModule)Container.Resolve(infrastructureModuleType);
            infrastructureModule.Initialize();

            Type servicesModuleType = Type.GetType("UIComposition.Services.ServicesModule,UIComposition.Services");
            var servicesModule = (IModule)Container.Resolve(servicesModuleType);
            servicesModule.Initialize();

            Type employeeModuleType =
                Type.GetType("UIComposition.Modules.Employee.EmployeeModule,UIComposition.Modules.Employee");
            var employeeModule = (IModule)Container.Resolve(employeeModuleType);
            employeeModule.Initialize();

            Type projectModuleType =
                Type.GetType("UIComposition.Modules.Project.ProjectModule,UIComposition.Modules.Project");
            var projectModule = (IModule)Container.Resolve(projectModuleType);
            projectModule.Initialize();
        }
    }
}