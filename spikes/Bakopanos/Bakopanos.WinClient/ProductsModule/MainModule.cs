using Microsoft.Practices.Unity;

namespace Bakopanos.WinClient.ProductsModule
{
    public class MainModule : IMainModule
    {
        private IProductsController _ProductsController;

        public MainModule(IUnityContainer container)
        {
            container.RegisterType<IProductsController, ProductsController>();
        }

        #region IMainModule Members

        [Dependency]
        public IProductsController ProductsController
        {
            set { _ProductsController = value; }
        }


        public void Run()
        {
            _ProductsController.Run();
        }

        #endregion
    }
}