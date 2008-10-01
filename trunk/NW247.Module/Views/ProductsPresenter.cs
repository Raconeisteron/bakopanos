using Model;
using Services;
using NW247.Module.Controllers;

namespace NW247.Module.Views
{
    public interface IProductsPresenter
    {
        IProductsView View { get; set; }
    }

    public class ProductsPresenter : IProductsPresenter
    {
        private readonly NorthwindDataSet dataSet;
        private IProductsController controller;
        private ProductsPresenter presenter;
        private IProductsService service;

        public ProductsPresenter(
            IProductsView view,
            IProductsController controller,
            IProductsService service)
        {
            View = view;
            this.controller = controller;
            this.service = service;

            dataSet = service.GetProducts();
            View.Model = dataSet.Products;

            View.Save += delegate
                             {
                                 if (dataSet.HasChanges())
                                 {
                                     var changes = dataSet.GetChanges() as NorthwindDataSet;
                                     dataSet.Merge(service.UpdateAll(changes));
                                 }
                             };
        }

        #region IProductsPresenter Members

        public IProductsView View { get; set; }

        #endregion
    }
}