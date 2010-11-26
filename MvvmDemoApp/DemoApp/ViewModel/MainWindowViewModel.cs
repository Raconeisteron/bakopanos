
using System.Collections.ObjectModel;
using DemoApp.Properties;

namespace DemoApp.ViewModel
{
    /// <summary>
    ///   The ViewModel for the application's main window.
    /// </summary>
    public class MainWindowViewModel : WorkspaceViewModel
    {
        #region Fields

        private readonly Workspaces _workspaces;
        private readonly Commands _commands;

        #endregion // Fields

        #region Constructor

        public MainWindowViewModel(Workspaces workspaces,Commands commands)
        {
            base.DisplayName = Strings.MainWindowViewModel_DisplayName;

            _workspaces = workspaces;
            _commands = commands;
        }

        #endregion // Constructor

        public ObservableCollection<WorkspaceViewModel> Workspaces
        {
            get
            {
                return _workspaces;
            }
        }

        /// <summary>
        ///   Returns a read-only list of commands 
        ///   that the UI can display and execute.
        /// </summary>
        public ObservableCollection<CommandViewModel> Commands
        {
            get
            {
                return _commands;
            }
        }

    }
}