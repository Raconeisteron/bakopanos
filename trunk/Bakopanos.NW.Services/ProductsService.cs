using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Bakopanos.NW.Data.NWDataSetTableAdapters;
using Bakopanos.NW.Model;

namespace Bakopanos.NW.Service
{
    // NOTE: If you change the class name "ProductsService" here, you must also update the reference to "ProductsService" in App.config.
    public class ProductsService : IProductsService
    {
        ProductsTableAdapter pta = new ProductsTableAdapter();
        TableAdapterManager tam = new TableAdapterManager();

        public NWDataSet GetProducts()
        {
            var ds = new NWDataSet();
            pta.Fill(ds.Products);
            return ds;
        }
    }
}
