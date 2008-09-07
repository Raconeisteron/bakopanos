using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using Bakopanos.NW.Data.NWDataSetTableAdapters;
using Bakopanos.NW.Model;

namespace Bakopanos.NW.Service
{
    // NOTE: If you change the class name "ProductsService" here, you must also update the reference to "ProductsService" in App.config.
    public class ProductsService : IProductsService
    {        
        public NWDataSet GetProducts()
        {
            var ds = new NWDataSet();
            new ProductsTableAdapter().Fill(ds.Products);
            return ds;
        }

        public NWDataSet GetCategories()
        {
            var ds = new NWDataSet();
            new CategoriesTableAdapter().Fill(ds.Categories);
            return ds;
        }

        public NWDataSet GetSingleProduct(int ProductID)
        {
            throw new NotImplementedException();
        }

        public NWDataSet GetSingleCategory(int CategoryID)
        {
            throw new NotImplementedException();
        }
        
        public NWDataSet Update(NWDataSet dataset)
        {
            var tam = new TableAdapterManager
                          {
                              ProductsTableAdapter = new ProductsTableAdapter(),
                              CategoriesTableAdapter = new CategoriesTableAdapter()
                          };
            tam.UpdateAll(dataset);
            return dataset;
        }
    }
}
