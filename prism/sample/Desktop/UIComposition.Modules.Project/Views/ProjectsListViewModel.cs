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
using System.Collections.ObjectModel;
using System.ComponentModel;
using UIComposition.BusinessEntities;
using UIComposition.Services;
using UIComposition.Services.Project;

namespace UIComposition.Modules.Project.Views
{
    public class ProjectsListViewModel : INotifyPropertyChanged
    {
        private readonly IProjectService _projectService;
        private int _employeeId;
        private ObservableCollection<ProjectItem> _projects;

        public ProjectsListViewModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ObservableCollection<ProjectItem> Projects
        {
            get { return _projects; }
            set
            {
                if (_projects != value)
                {
                    _projects = value;
                    OnPropertyChanged("Projects");
                }
            }
        }

        public int EmployeeId
        {
            get { return _employeeId; }
            set
            {
                if (_employeeId == value) return;
                _employeeId = value;
                Projects = _projectService.RetrieveProjects(EmployeeId);
                OnPropertyChanged("EmployeeId");
            }
        }

        public static string HeaderInfo
        {
            get { return "Current Projects"; }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}