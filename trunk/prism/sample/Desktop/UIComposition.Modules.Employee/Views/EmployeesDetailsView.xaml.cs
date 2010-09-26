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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace UIComposition.Modules.Employee.Views
{
    /// <summary>
    /// Interaction logic for EmployeesDetailsView.xaml
    /// </summary>
    public partial class EmployeesDetailsView : UserControl
    {
        public EmployeesDetailsView(EmployeesDetailsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Frame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            ErrorText.Visibility = Visibility.Visible;
            ErrorText.Text = e.Exception.Message;
            e.Handled = true;
        }

        private void Frame_Navigated(object sender, NavigationEventArgs e)
        {
            ErrorText.Visibility = Visibility.Hidden;
            ErrorText.Text = string.Empty;
        }
    }
}