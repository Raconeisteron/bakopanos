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
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using UIComposition.Infrastructure.Services;
using UIComposition.Modules.Employee.Controllers;
using UIComposition.Modules.Employee.Views;

namespace UIComposition.Modules.Employee
{
    public class EmployeeModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IRegionViewRegistry _regionViewRegistry;
        private readonly IUnityService _unityService;

        public EmployeeModule(IUnityService unityService, IRegionViewRegistry regionViewRegistry,
                              IRegionManager regionManager)
        {
            _unityService = unityService;
            _regionViewRegistry = regionViewRegistry;
            _regionManager = regionManager;
        }

        #region IModule Members

        public void Initialize()
        {
            RegisterTypesAndServices();

            RegisterViewsWithRegions();
        }

        #endregion

        private void RegisterViewsWithRegions()
        {
            _regionViewRegistry.RegisterViewWithRegion(RegionNames.SelectionRegion,
                                                       _unityService.ResolveLazy<EmployeesListView>());
            _regionManager.RegisterViewWithRegion(Infrastructure.RegionNames.MainRegion,
                                                  _unityService.ResolveLazy<EmployeesView>());
        }

        protected void RegisterTypesAndServices()
        {
            _unityService.RegisterSingleton<IEmployeesController, EmployeesController>();
        }
    }
}