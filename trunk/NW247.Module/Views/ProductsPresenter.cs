using NW247.Services;

namespace NW247.Module.Views
{
    public class ProductsPresenter : IProductsPresenter
    {
        private readonly IProductsService service;

        //private IProductsController controller;        

        //[Dependency]
        //private IProductsController Controller
        //{
        //    set { controller = value; }
        //}

        public ProductsPresenter(IProductsView view, IProductsService Service)
        {
            View = view;
            service = Service;

            View.Model = service.GetProducts();
        }

        #region IProductsPresenter Members

        public IProductsView View { get; set; }

        #endregion
    }
}