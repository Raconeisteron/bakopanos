using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using ASPNET.StarterKit.Portal;
using ASPNET.StarterKit.Portal.SqlClient;
using ASPNET.StarterKit.Portal.XmlFile;
using DemoApp.Properties;

namespace DemoApp.ViewModel
{
    /// <summary>
    /// The ViewModel for the application's main window.
    /// </summary>
    public class MainWindowViewModel : WorkspaceViewModel
    {
        #region Fields
                
        ReadOnlyCollection<CommandViewModel> _commands;
        ObservableCollection<WorkspaceViewModel> _workspaces;

        #endregion // Fields

        #region Constructor

        public MainWindowViewModel(string customerDataFile)
        {
            base.DisplayName = Strings.MainWindowViewModel_DisplayName;

        }

        #endregion // Constructor

        #region Commands

        /// <summary>
        /// Returns a read-only list of commands 
        /// that the UI can display and execute.
        /// </summary>
        public ReadOnlyCollection<CommandViewModel> Commands
        {
            get
            {
                if (_commands == null)
                {
                    List<CommandViewModel> cmds = this.CreateCommands();
                    _commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _commands;
            }
        }

        List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
            {
                new CommandViewModel(
                    Strings.MainWindowViewModel_Command_ViewAnnouncements,
                    new RelayCommand(param => this.ViewAnnouncements())),

                new CommandViewModel(
                    Strings.MainWindowViewModel_Command_EditAnnouncement,
                    new RelayCommand(param => this.EditAnnouncement()))
            };
        }

        #endregion // Commands

        #region Workspaces

        /// <summary>
        /// Returns the collection of available workspaces to display.
        /// A 'workspace' is a ViewModel that can request to be closed.
        /// </summary>
        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                if (_workspaces == null)
                {
                    _workspaces = new ObservableCollection<WorkspaceViewModel>();
                    _workspaces.CollectionChanged += this.OnWorkspacesChanged;
                }
                return _workspaces;
            }
        }

        void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += this.OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= this.OnWorkspaceRequestClose;
        }

        void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            WorkspaceViewModel workspace = sender as WorkspaceViewModel;
            workspace.Dispose();
            this.Workspaces.Remove(workspace);
        }

        #endregion // Workspaces

        #region Private Helpers

        void EditAnnouncement()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            IAnnouncementsDb db = new SqlAnnouncementsDb(connectionString);
            var workspace = new EditAnnouncementViewModel(db);
            Workspaces.Add(workspace);
            SetActiveWorkspace(workspace);
        }

        void ViewAnnouncements()
        {
            var workspace = Workspaces.FirstOrDefault(vm => vm is AnnouncementsViewModel) as AnnouncementsViewModel;

            if (workspace == null)
            {
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                string configFile = ConfigurationManager.AppSettings["ConfigFile"];

                IAnnouncementsDb db = new SqlAnnouncementsDb(connectionString);
                IConfigurationDb configurationDb = new XmlConfigurationDb(configFile);
                ITabConfigurationDb tabConfigurationDb = new XmlTabConfigurationDb(configurationDb);

                workspace = new AnnouncementsViewModel(db, tabConfigurationDb);
                Workspaces.Add(workspace);
            }

            this.SetActiveWorkspace(workspace);
        }

        void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(this.Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this.Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        #endregion // Private Helpers
    }
}