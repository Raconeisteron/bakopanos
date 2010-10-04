// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using System.Collections.ObjectModel;
using System.ComponentModel;
using UIComposition.BusinessEntities;
using UIComposition.Infrastructure;
using UIComposition.Services;

namespace UIComposition.Modules.Project.Model
{
    internal class ProjectWorkItem : IProjectWorkItem, IProjectList
    {        
        private ObservableCollection<ProjectItem> _projects;

        public ProjectWorkItem(IProjectService projectService)
        {            
            Projects = projectService.RetrieveProjects();
        }

        #region IEmployeeWorkItem Members

        public ObservableCollection<ProjectItem> Projects
        {
            get { return _projects; }
            set
            {
                _projects = value;
                PropertyChanged.OnPropertyChanged(this, "Projects");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion        
       
    }
}