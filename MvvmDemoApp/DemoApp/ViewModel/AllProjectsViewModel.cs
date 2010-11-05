using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using DemoApp.DataAccess;
using DemoApp.Model;
using DemoApp.Properties;

namespace DemoApp.ViewModel
{
    /// <summary>
    /// Represents a container of ProjectViewModel objects
    /// that has support for staying synchronized with the
    /// ProjectRepository.  This class also provides information
    /// related to multiple selected projectss.
    /// </summary>
    public class AllProjectsViewModel : WorkspaceViewModel
    {
        #region Fields

        private readonly IProjectRepository _projectRepository;
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();

        #endregion // Fields

        #region Constructor

        public AllProjectsViewModel(IProjectRepository projectRepository)
        {
            if (projectRepository == null)
                throw new ArgumentNullException("projectRepository");

            base.DisplayName = Strings.AllProjectsViewModel_DisplayName;

            _projectRepository = projectRepository;
            
            AllProjects = new ObservableCollection<ProjectViewModel>();

            // Set up the Background Worker Events
            _backgroundWorker.DoWork += (sender, e) => e.Result = CreateAllProjects();

            _backgroundWorker.RunWorkerCompleted +=
                delegate(object sender, RunWorkerCompletedEventArgs e)
                    {
                        foreach (ProjectViewModel projectViewModel in (List<ProjectViewModel>)e.Result)
                        {
                            AllProjects.Add(projectViewModel);
                        }
                    };

            // Run the Background Worker
            _backgroundWorker.RunWorkerAsync(5000);

        }

        private List<ProjectViewModel> CreateAllProjects()
        {
            List<ProjectViewModel> all =
                (from proj in _projectRepository.Get()
                 select new ProjectViewModel(proj, _projectRepository)).ToList();

            return all;
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Returns a collection of all the ProjectViewModel objects.
        /// </summary>
        public ObservableCollection<ProjectViewModel> AllProjects { get; private set; }

        #endregion // Public Interface

        #region  Base Class Overrides

        protected override void OnDispose()
        {
            
        }

        #endregion // Base Class Overrides

        #region Event Handling Methods

        
        
        #endregion // Event Handling Methods
    }
}