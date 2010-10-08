// ===================================================================================
// Bakopanos Konstantinos
// http://www.deaddevssociety.com
// ===================================================================================
using Microsoft.Practices.Unity;
using UIComposition.Infrastructure;
using UIComposition.Views;

namespace UIComposition.Controllers
{
    internal class ShellController : IShellController
    {
        public ShellController(IUnityContainer unityContainer)
        {
            unityContainer.RegisterViewWithRegion<ToolBarView>(RegionNames.ToolBarRegion);
        }
    }
}