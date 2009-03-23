using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using Evolutil.Domain;
using Evolutil.Library.Log;
using Evolutil.ServiceContracts;
using Microsoft.Practices.Unity;

namespace Evolutil.ServiceClient
{
    public class ProductServiceClient : ClientBase<IProductService>, IProductService
    {
        public ProductServiceClient([Dependency("ProductService")]ClientInfo info, ILogger log) :
            base(info.Binding, info.Address)
        {
            //todo behaviour
            log.Error("test");
        }

        public List<ProductBO> Read()
        {
            return Channel.Read();
        }
    }
}
