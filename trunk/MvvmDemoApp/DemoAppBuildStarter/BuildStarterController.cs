using DemoApp.Properties;
using DemoApp.ViewModel;

namespace DemoApp
{
    public class BuildStarterController : IController
    {
        private readonly CommandController _commands;
        private readonly WorkspaceController _workspaces;

        public BuildStarterController(WorkspaceController workspaces, CommandController commands)
        {
            _workspaces = workspaces;
            _commands = commands;
        }

        #region IController Members

        public void Run()
        {
            _commands.Add(new CommandViewModel(
                              BuildStarterStrings.Command_ViewAllCustomers,
                              new RelayCommand(param => ShowBuildTargets())));
        }

        #endregion

        private void ShowBuildTargets()
        {
            var workspace = new BuildTargetViewModel();
            _workspaces.Add(workspace);
            _workspaces.SetActiveWorkspace(workspace);
        }
    }
}