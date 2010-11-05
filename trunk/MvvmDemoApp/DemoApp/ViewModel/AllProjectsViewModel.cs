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

        #endregion // Fields

        #region Constructor

        public AllProjectsViewModel(IProjectRepository projectRepository)
        {
            if (projectRepository == null)
                throw new ArgumentNullException("projectRepository");

            base.DisplayName = Strings.AllProjectsViewModel_DisplayName;

            _projectRepository = projectRepository;
        }

        #endregion // Constructor

        #region Public Interface

        

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