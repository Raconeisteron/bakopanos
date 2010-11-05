using System;
using System.Collections.Generic;
using DemoApp.Model;

namespace DemoApp.DataAccess
{    
    public interface IProjectRepository
    {
        /// <summary>
        /// Raised when a project is placed into the repository.
        /// </summary>
        event EventHandler<ItemAddedEventArgs<Project>> ItemAdded;

        /// <summary>
        /// Places the specified project into the repository.
        /// If the project is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        void AddProject(Project project);

        /// <summary>
        /// Returns true if the specified project exists in the
        /// repository, or false if it is not.
        /// </summary>
        bool ContainsProject(Project project);

        /// <summary>
        /// Returns a shallow-copied list of all projects in the repository.
        /// </summary>
        List<Project> GetProjects();
    }
}