using NW247.Data.NorthwindDataSetTableAdapters;
using NW247.Model;

namespace NW247.Services
{
    internal class ProductsService : IProductsService
    {
        private readonly TableAdapterManager Manager;
        private readonly ProductsTableAdapter TableAdapter;

        public ProductsService(ProductsTableAdapter TableAdapter, TableAdapterManager Manager)
        {
            this.TableAdapter = TableAdapter;
            this.Manager = Manager;
            Manager.ProductsTableAdapter = TableAdapter;
        }

        #region IProductsService Members

        public NorthwindDataSet GetProducts()
        {
            var dataset = new NorthwindDataSet();
            TableAdapter.Fill(dataset.Products);
            return dataset;
        }

        public NorthwindDataSet UpdateAll(NorthwindDataSet products)
        {
            Manager.UpdateAll(products);
            return products;
        }

        #endregion
    }
}