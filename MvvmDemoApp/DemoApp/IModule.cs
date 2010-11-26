using DemoApp.ViewModel;

namespace DemoApp
{
    public interface IModule
    {
        void Initialize(Workspaces workspaces, Commands commands);
    }
}