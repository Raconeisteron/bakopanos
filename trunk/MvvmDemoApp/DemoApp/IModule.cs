using DemoApp.ViewModel;
using Microsoft.Practices.Unity;

namespace DemoApp
{
    public interface IModule
    {
        void Initialize(IUnityContainer container);
    }
}