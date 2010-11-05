using System;
using DemoApp.DataAccess;
using DemoApp.Model;
using DemoApp.Properties;

namespace DemoApp.ViewModel
{
    public class ProjectViewModel : WorkspaceViewModel
    {
        #region Fields

        private Project _project;
        private IProjectRepository _projectRepository;
        
        #endregion

        #region Constructor

        public ProjectViewModel(Project project, IProjectRepository projectRepository)
        {
            if (project == null)
                throw new ArgumentNullException("project");

            if (projectRepository == null)
                throw new ArgumentNullException("projectRepository");

            _project = project;
            _projectRepository = projectRepository;            
        }

        #endregion // Constructor

        #region Customer Properties

        public string ProjectName
        {
            get { return _project.ProjectName; }            
        }

        #endregion

    }
}