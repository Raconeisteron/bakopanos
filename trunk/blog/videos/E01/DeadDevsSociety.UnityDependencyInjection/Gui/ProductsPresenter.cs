using System.Linq;
using DeadDevsSociety.UnityDependencyInjection.Business;

namespace DeadDevsSociety.UnityDependencyInjection.Gui
{
    public class ProductsPresenter : Presenter<ProductsView>
    {
        public ProductsPresenter()
        {
            View = new ProductsView();
            View.GetProducts = (sender, e) =>
                               View.DataSource = from item in new ProductsFacade().GetProducts(".")
                                                 select item.ToModel();
        }
    }
}