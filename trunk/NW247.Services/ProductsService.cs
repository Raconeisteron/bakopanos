using Microsoft.Practices.Unity;
using NW247.Data.NorthwindDataSetTableAdapters;
using NW247.Model;

namespace NW247.Services
{
    internal class ProductsService : IProductsService
    {
        private ProductsTableAdapter tableAdapter;

        [Dependency]
        public ProductsTableAdapter TableAdapter
        {
            set { tableAdapter = value; }
        }

        #region IProductsService Members

        public NorthwindDataSet.ProductsDataTable GetProducts()
        {
            var dataset = new NorthwindDataSet();
            tableAdapter.Fill(dataset.Products);
            return dataset.Products;
        }

        public NorthwindDataSet.ProductsDataTable UpdateAll(NorthwindDataSet.ProductsDataTable products)
        {
            tableAdapter.Update(products);
            return products;
        }

        #endregion
    }
}