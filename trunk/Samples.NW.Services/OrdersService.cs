using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Bakopanos.Samples.NW.Model;

namespace Bakopanos.Samples.NW.Service
{
    // NOTE: If you change the class name "OrdersService" here, you must also update the reference to "OrdersService" in App.config.
    public class OrdersService : IOrdersService
    {        

        public NWDataSet GetOrders()
        {
            throw new NotImplementedException();
        }

    }
}
