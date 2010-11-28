using System.Configuration;
using DemoApp.ViewModel;
using Microsoft.Practices.Unity;

namespace DemoApp
{
    public class BuildStarterModule : ConfigurationSection, IModule
    {
        #region Fields

        private IUnityContainer _container;
        private WorkspaceController _workspaces;

        #endregion // Fields

        #region IModule Members

        public void Initialize(IUnityContainer container)
        {
            _container = container;

            _workspaces = container.Resolve<WorkspaceController>();
            var commands = container.Resolve<CommandController>();

            commands.Add(new CommandViewModel(
                             "BuilTarget",
                             new RelayCommand(param => ShowBuildTarget())));
        }

        #endregion

        private void ShowBuildTarget()
        {
            var workspace = new BuildTargetViewModel();
            _workspaces.Add(workspace);
            _workspaces.SetActiveWorkspace(workspace);
        }
    }
}