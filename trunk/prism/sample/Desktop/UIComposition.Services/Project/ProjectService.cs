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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UIComposition.BusinessEntities;

namespace UIComposition.Services.Project
{
    internal class ProjectService : IProjectService
    {
        private readonly Dictionary<int, ObservableCollection<ProjectItem>> _projects;

        public ProjectService()
        {
            _projects = new Dictionary<int, ObservableCollection<ProjectItem>>();

            var projectsEmployee1 = new ObservableCollection<ProjectItem>();

            var project1 = new ProjectItem {ProjectName = "Project 1", Role = "Architect"};
            var project2 = new ProjectItem {ProjectName = "Project 2", Role = "Developer"};

            projectsEmployee1.Add(project1);
            projectsEmployee1.Add(project2);

            _projects.Add(1, projectsEmployee1);

            var projectsEmployee2 = new ObservableCollection<ProjectItem>
                                        {
                                            project1,
                                            project2,
                                            new ProjectItem
                                                {ProjectName = "Project 3", Role = "Dev Lead"}
                                        };

            _projects.Add(2, projectsEmployee2);

            var projectsEmployee3 = new ObservableCollection<ProjectItem>();

            _projects.Add(3, projectsEmployee3);
        }

        #region IProjectService Members

        public ObservableCollection<ProjectItem> RetrieveProjects(int employeeId)
        {
            return _projects.ContainsKey(employeeId) ? _projects[employeeId] : null;
        }

        #endregion
    }
}