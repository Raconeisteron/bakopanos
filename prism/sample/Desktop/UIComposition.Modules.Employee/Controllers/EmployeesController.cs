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
using System.ComponentModel;
using System.Globalization;
using Microsoft.Practices.Composite.Regions;
using UIComposition.BusinessEntities;
using UIComposition.Infrastructure.Services;
using UIComposition.Modules.Employee.Views;
using UIComposition.Services;

namespace UIComposition.Modules.Employee.Controllers
{
    public class EmployeesController : IEmployeesController, IEmployeeContext
    {
        private EmployeeItem _selectedEmployee;
        private readonly IRegionManager _regionManager;
        private readonly IUnityService _unityService;
        private readonly ILogService _logService;

        public EmployeesController(IUnityService unityService,ILogService logService, IRegionManager regionManager)
        {
            _unityService = unityService;
            _regionManager = regionManager;
            _logService = logService;
        }
       
        public EmployeeItem SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                if (_selectedEmployee == value) return;
                _selectedEmployee = value;                
                OnEmployeeSelected(SelectedEmployee);
            }
        }
        
        private void OnEmployeeSelected(EmployeeItem employee)
        {
            _logService.WriteInfo("EmployeesController::OnEmployeeSelected");
            OnPropertyChanged("SelectedEmployee");

            IRegion detailsRegion = _regionManager.Regions[RegionNames.DetailsRegion];
            object existingView = detailsRegion.GetView(employee.EmployeeId.ToString(CultureInfo.InvariantCulture));

            // See if the view already exists in the region. 
            if (existingView == null)
            {
                // the view does not exist yet. Create it and push it into the region
                var detailsView = _unityService.Resolve<EmployeesDetailsView>();
               
                // the details view should receive it's own scoped region manager, therefore Add overload using 'true' (see notes below).
                detailsRegion.Add(detailsView, employee.EmployeeId.ToString(CultureInfo.InvariantCulture),
                                  true);

                detailsRegion.Activate(detailsView);
            }
            else
            {
                // The view already exists. Just show it. 
                detailsRegion.Activate(existingView);
            }           
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}