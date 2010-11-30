using DemoApp.Properties;
using DemoApp.ViewModel;

namespace DemoApp
{
    public class BuildStarterController : IController
    {
        private readonly WorkspaceController _workspaces;
        private readonly CommandController _commands;

        public BuildStarterController(WorkspaceController workspaces, CommandController commands)
        {
            _workspaces = workspaces;
            _commands = commands;
        }

        public void Run()
        {
            _commands.Add(new CommandViewModel(
                             BuildStarterStrings.Command_ViewAllCustomers,
                             new RelayCommand(param => ShowBuildTargets())));
        }

        private void ShowBuildTargets()
        {
            var workspace = new BuildTargetViewModel();
            _workspaces.Add(workspace);
            _workspaces.SetActiveWorkspace(workspace);
        }
    }
}