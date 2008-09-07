using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Bakopanos.NW.Model;

namespace Bakopanos.NW.Service
{
    // NOTE: If you change the interface name "IProductsService" here, you must also update the reference to "IProductsService" in App.config.
    [ServiceContract]
    public interface IProductsService
    {
        [OperationContract]
        NWDataSet GetProducts();

        [OperationContract]
        NWDataSet GetCategories();

        [OperationContract]
        NWDataSet GetSingleProduct(int ProductID);

        [OperationContract]
        NWDataSet GetSingleCategory(int CategoryID);

        [OperationContract]
        NWDataSet Update(NWDataSet dataset);
    }
}
