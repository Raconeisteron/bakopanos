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
using UIComposition.Modules.Project.Views;

namespace UIComposition.Modules.Project
{
    [Module(ModuleName = "Modules.Project")]
    [ModuleDependency("Services")]
    public class ProjectModule : IModule
    {
        private readonly IRegionViewRegistry _regionViewRegistry;
        private readonly IUnityService _unityService;

        public ProjectModule(IUnityService unityService, IRegionViewRegistry regionViewRegistry)
        {
            _unityService = unityService;
            _regionViewRegistry = regionViewRegistry;
        }

        #region IModule Members

        public void Initialize()
        {
            // Register a type for pull based based composition. 
            _regionViewRegistry.RegisterViewWithRegion("TabRegion", _unityService.ResolveLazy<ProjectsListView>());
        }

        #endregion
    }
}