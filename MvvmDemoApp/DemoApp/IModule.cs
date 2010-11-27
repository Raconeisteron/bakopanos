using DemoApp.ViewModel;

namespace DemoApp
{
    public interface IModule
    {
        void Initialize(WorkspaceWorkItem workspaces, CommandWorkItem commands);
    }
}