using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Bakopanos.Samples.NW.Model;

namespace Bakopanos.Samples.NW.Service
{
    // NOTE: If you change the interface name "IOrdersService" here, you must also update the reference to "IOrdersService" in App.config.
    [ServiceContract]
    public interface IOrdersService
    {
        [OperationContract]
        NWDataSet GetOrders();
    }
}
