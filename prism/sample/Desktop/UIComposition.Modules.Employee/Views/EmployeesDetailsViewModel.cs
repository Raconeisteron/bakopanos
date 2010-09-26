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
using System.Globalization;
using UIComposition.BusinessEntities;
using UIComposition.Modules.Employee.Controllers;

namespace UIComposition.Modules.Employee.Views
{
    public class EmployeesDetailsViewModel
    {
        private readonly IEmployeesController _employeeController;
        public EmployeesDetailsViewModel(IEmployeesController employeeController)
        {
            _employeeController=employeeController;
        }

        private const string mapUrlFormat = "http://maps.msn.com/home.aspx?strt1={0}&city1={1}&stnm1={2}";

        public EmployeeItem SelectedEmployee
        {
            get { return _employeeController.SelectedEmployee; }
            set { _employeeController.SelectedEmployee = value; }
        }

        public string AddressMapUrl
        {
            get
            {
                if (SelectedEmployee != null)
                {
                    return string.Format(CultureInfo.InvariantCulture, mapUrlFormat, SelectedEmployee.Address,
                                         SelectedEmployee.City, SelectedEmployee.State);
                }

                return string.Empty;
            }
        }

    }
}