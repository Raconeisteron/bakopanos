using System;
using System.Collections.Generic;
using System.IO;
using DemoApp.Model;

namespace DemoApp.DataAccess.Fake
{
    /// <summary>
    /// Represents a source of customers in the application.
    /// </summary>
    internal class ProjectRepository : IProjectRepository
    {
        public event EventHandler<ItemAddedEventArgs<Project>> ItemAdded;
        public void Add(Project project)
        {
            
        }

        public bool Contains(Project project)
        {
            return true;
        }

        public List<Project> Get()
        {
            var projects = new List<Project>();

            foreach (string projectFile in Directory.GetFiles(@"C:\svn\chinook2.trunk\source","*.csproj",SearchOption.AllDirectories))
            {
                var project = Project.CreateProject(Path.GetFileNameWithoutExtension(projectFile));
                projects.Add(project);
            }

            return projects;
        }
    }
}