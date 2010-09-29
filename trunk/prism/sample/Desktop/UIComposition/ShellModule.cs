// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Unity;
using UIComposition.Controllers;

namespace UIComposition
{
    public class ShellModule : IModule
    {
        private readonly IUnityContainer _unityContainer;

        public ShellModule(IUnityContainer unityContainer)
        {
            _unityContainer = unityContainer;
        }

        #region IModule Members

        public void Initialize()
        {
            _unityContainer.RegisterSingleton<IShellController, ShellController>();
            _unityContainer.Resolve<IShellController>();
        }

        #endregion
    }
}