using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Evolutil.Domain;

namespace Evolutil.ServiceContracts
{
    [ServiceContract]
    public interface IProductService
    {
        [OperationContract]
        List<ProductBO> Read();
    }
}
