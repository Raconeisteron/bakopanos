using NW247.Model;
using NW247.Services;

namespace NW247.Module.Views
{
    
    public class ProductsPresenter : IProductsPresenter
    {
        private readonly NorthwindDataSet dataSet;
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