using System;
using System.Collections.Generic;
using DemoApp.Model;

namespace DemoApp.DataAccess.Fake
{
    /// <summary>
    /// Represents a source of customers in the application.
    /// </summary>
    internal class ProjectRepository : IProjectRepository
    {
        public event EventHandler<ItemAddedEventArgs<Project>> ItemAdded;
        public void AddProject(Project project)
        {
            
        }

        public bool ContainsProject(Project project)
        {
            return true;
        }

        public List<Project> GetProjects()
        {
            return default(List<Project>);
        }
    }
}