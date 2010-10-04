// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UIComposition.Model;

namespace UIComposition.Services
{
    internal class FakeProjectService : IProjectService
    {
        private readonly Dictionary<int, ObservableCollection<ProjectItem>> _projects;

        public FakeProjectService()
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

        public ObservableCollection<ProjectItem> RetrieveProjects()
        {
            var projects = new List<ProjectItem>();
            projects.Add( new ProjectItem { ProjectName = "Project 1", Role = "Architect" });
            projects.Add( new ProjectItem { ProjectName = "Project 2", Role = "Developer" });
            projects.Add( new ProjectItem { ProjectName = "Project 3", Role = "Dev Lead" });

            return new ObservableCollection<ProjectItem>(projects);
        }

        public ObservableCollection<ProjectItem> RetrieveProjects(int employeeId)
        {
            return _projects.ContainsKey(employeeId) ? _projects[employeeId] : null;
        }

        #endregion
    }
}