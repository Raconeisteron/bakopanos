using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Data;
using DemoApp.Properties;
using Microsoft.Practices.Unity;

namespace DemoApp.ViewModel
{
    /// <summary>
    /// The ViewModel for the application's main window.
    /// </summary>
    public class MainWindowViewModel : WorkspaceViewModel
    {
        #region Fields

        private readonly IUnityContainer _container;
        private ReadOnlyCollection<CommandViewModel> _commands;
        private ObservableCollection<WorkspaceViewModel> _workspaces;

        #endregion // Fields

        #region Constructor

        public MainWindowViewModel(IUnityContainer container)
        {
            _container = container;
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
                    List<CommandViewModel> cmds = CreateCommands();
                    _commands = new ReadOnlyCollection<CommandViewModel>(cmds);
                }
                return _commands;
            }
        }

        private List<CommandViewModel> CreateCommands()
        {
            return new List<CommandViewModel>
                       {
                           new CommandViewModel(
                               Strings.MainWindowViewModel_Command_ViewAnnouncements,
                               new RelayCommand(param => ViewAnnouncements())),
                           new CommandViewModel(
                               Strings.MainWindowViewModel_Command_ViewContacts,
                               new RelayCommand(param => ViewContacts())),
                           new CommandViewModel(
                               Strings.MainWindowViewModel_Command_ViewLinks,
                               new RelayCommand(param => ViewLinks())),
                           new CommandViewModel(
                               Strings.MainWindowViewModel_Command_ViewEvents,
                               new RelayCommand(param => ViewEvents())),
                               new CommandViewModel(
                               Strings.MainWindowViewModel_Command_EditAnnouncement,
                               new RelayCommand(param => EditAnnouncement()))
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
                    _workspaces.CollectionChanged += OnWorkspacesChanged;
                }
                return _workspaces;
            }
        }

        private void OnWorkspacesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null && e.NewItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.NewItems)
                    workspace.RequestClose += OnWorkspaceRequestClose;

            if (e.OldItems != null && e.OldItems.Count != 0)
                foreach (WorkspaceViewModel workspace in e.OldItems)
                    workspace.RequestClose -= OnWorkspaceRequestClose;
        }

        private void OnWorkspaceRequestClose(object sender, EventArgs e)
        {
            var workspace = sender as WorkspaceViewModel;
            workspace.Dispose();
            Workspaces.Remove(workspace);
        }

        #endregion // Workspaces

        #region Private Helpers

        private void EditAnnouncement()
        {
            var workspace = _container.Resolve<EditAnnouncementViewModel>();
            Workspaces.Add(workspace);
            SetActiveWorkspace(workspace);
        }

        private void ViewAnnouncements()
        {
            var workspace = Workspaces.FirstOrDefault(vm => vm is AnnouncementsViewModel) as AnnouncementsViewModel;

            if (workspace == null)
            {
                workspace = _container.Resolve<AnnouncementsViewModel>();
                Workspaces.Add(workspace);
            }

            SetActiveWorkspace(workspace);
        }

        private void ViewContacts()
        {
            var workspace = Workspaces.FirstOrDefault(vm => vm is ContactsViewModel) as ContactsViewModel;

            if (workspace == null)
            {
                workspace = _container.Resolve<ContactsViewModel>();
                Workspaces.Add(workspace);
            }

            SetActiveWorkspace(workspace);
        }

        private void ViewLinks()
        {
            var workspace = Workspaces.FirstOrDefault(vm => vm is LinksViewModel) as LinksViewModel;

            if (workspace == null)
            {
                workspace = _container.Resolve<LinksViewModel>();
                Workspaces.Add(workspace);
            }

            SetActiveWorkspace(workspace);
        }

        private void ViewEvents()
        {
            var workspace = Workspaces.FirstOrDefault(vm => vm is EventsViewModel) as EventsViewModel;

            if (workspace == null)
            {
                workspace = _container.Resolve<EventsViewModel>();
                Workspaces.Add(workspace);
            }

            SetActiveWorkspace(workspace);
        }

        private void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(Workspaces.Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(Workspaces);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        #endregion // Private Helpers
    }
}