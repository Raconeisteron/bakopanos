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
using System.Windows.Controls;
using Microsoft.Practices.Composite.Events;
using Microsoft.Practices.Composite.Presentation;
using Microsoft.Practices.Composite.Presentation.Regions;
using UIComposition.BusinessEntities;

namespace UIComposition.Modules.Employee.Views
{
    /// <summary>
    /// Interaction logic for EmployeesListView.xaml
    /// </summary>
    public partial class EmployeesListView : UserControl
    {
        public EmployeesListView(EmployeesListViewModel viewModel)
        {
            InitializeComponent();

            EmployeeSelected += delegate(object sender, DataEventArgs<EmployeeItem> e)
                                    {
                                        // Update the value of the regioncontext. This regioncontext is two way bound to the 
                                        // SelectedEmployee property of the EmployeePresentationModel. So the control that's hosting the region
                                        // (EmployeesView) will get notified of the selection change through databinding. 
                                        ObservableRegionContext.Value = e.Value;
                                    };

            DataContext = viewModel;
        }

        /// <summary>
        /// This object will hold the regioncontext for the view. You can also subscribe
        /// to change notifications to detect when this property changes
        /// </summary>
        public ObservableObject<object> ObservableRegionContext
        {
            get
            {
                // The BindRegionContextToDependencyObjectBehavior will forward an observableobject that
                // holds the regioncontext value to this dependencyproperty. 
                return RegionContext.GetObservableContext(this);
            }
        }

        public event EventHandler<DataEventArgs<EmployeeItem>> EmployeeSelected = delegate { };

        private void EmployeesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count <= 0) return;
            var selected = e.AddedItems[0] as EmployeeItem;
            if (selected != null)
            {
                // Raise an event to notify the presenter that the selected employee has changed
                EmployeeSelected(this, new DataEventArgs<EmployeeItem>(selected));
            }
        }
    }
}