using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Data;

namespace DemoApp.ViewModel
{
    public class WorkspaceWorkItem : ObservableCollection<WorkspaceViewModel>
    {
        #region Constructor

        public WorkspaceWorkItem()
        {
            CollectionChanged += OnWorkspacesChanged;
        }

        #endregion // Constructor

        #region Workspaces

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
            Remove(workspace);
        }

        #endregion // Workspaces

        #region Public Helpers



        public void SetActiveWorkspace(WorkspaceViewModel workspace)
        {
            Debug.Assert(Contains(workspace));

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(this);
            if (collectionView != null)
                collectionView.MoveCurrentTo(workspace);
        }

        #endregion // Private Helpers
    }
}