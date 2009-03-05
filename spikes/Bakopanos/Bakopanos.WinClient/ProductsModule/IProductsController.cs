using System.Windows.Forms;
using Bakopanos.Framework.Composite;
using Bakopanos.WinClient.ProductsModule.Views;

namespace Bakopanos.WinClient.ProductsModule
{
    public interface IProductsController : IController
    {
        Panel Workspace { set; }
        ProductListView View1 { set; }
        TestView View2 { set; }
    }
}