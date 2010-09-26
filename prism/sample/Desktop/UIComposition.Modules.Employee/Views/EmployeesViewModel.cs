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
using UIComposition.BusinessEntities;
using UIComposition.Modules.Employee.Controllers;

namespace UIComposition.Modules.Employee.Views
{
    public class EmployeesViewModel : INotifyPropertyChanged
    {
        private readonly IEmployeesController _employeeController;
        private EmployeeItem _selectedEmployee;

        public EmployeesViewModel(IEmployeesController employeeController)
        {
            _employeeController = employeeController;
        }


        public EmployeeItem SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                if (_selectedEmployee == value) return;
                _selectedEmployee = value;

                _employeeController.OnEmployeeSelected(SelectedEmployee);

                OnPropertyChanged("SelectedEmployee");
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}