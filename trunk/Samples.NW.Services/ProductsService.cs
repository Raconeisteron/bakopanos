using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Bakopanos.Samples.NW.Model;

namespace Bakopanos.Samples.NW.Service
{
    // NOTE: If you change the class name "ProductsService" here, you must also update the reference to "ProductsService" in App.config.
    public class ProductsService : IProductsService
    {
        public NWDataSet GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}
