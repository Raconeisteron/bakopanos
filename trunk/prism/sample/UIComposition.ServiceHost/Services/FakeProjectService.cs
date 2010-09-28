// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UIComposition.Contracts;

namespace UIComposition.Services
{
    internal class FakeProjectService : IProjectService
    {
        private readonly Dictionary<int, ObservableCollection<Project>> _projects;

        public FakeProjectService()
        {
            _projects = new Dictionary<int, ObservableCollection<Project>>();

            var projectsEmployee1 = new ObservableCollection<Project>();

            var project1 = new Project {ProjectName = "Project 1", Role = "Architect"};
            var project2 = new Project {ProjectName = "Project 2", Role = "Developer"};

            projectsEmployee1.Add(project1);
            projectsEmployee1.Add(project2);

            _projects.Add(1, projectsEmployee1);

            var projectsEmployee2 = new ObservableCollection<Project>
                                        {
                                            project1,
                                            project2,
                                            new Project
                                                {ProjectName = "Project 3", Role = "Dev Lead"}
                                        };

            _projects.Add(2, projectsEmployee2);

            var projectsEmployee3 = new ObservableCollection<Project>();

            _projects.Add(3, projectsEmployee3);
        }

        #region IProjectService Members

        public ObservableCollection<Project> RetrieveProjects(int employeeId)
        {
            return _projects.ContainsKey(employeeId) ? _projects[employeeId] : null;
        }

        #endregion
    }
}