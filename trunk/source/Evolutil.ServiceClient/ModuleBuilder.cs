using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using Evolutil.ServiceContracts;
using Microsoft.Practices.Unity;

namespace Evolutil.ServiceClient
{
    public class ModuleBuilder : IModuleBuilder
    {
        public ModuleBuilder(IUnityContainer container)
        {
            container.RegisterType<IProductService, ProductServiceClient>();

            const string uri = "http://localhost:11418/ProductService.svc";
            var info = new ClientInfo
                           {
                               Binding = new WebHttpBinding(),
                               Address = new EndpointAddress(uri)
                           };
            container.RegisterInstance<ClientInfo>("ProductService", info);
        }
    }

    public class ClientInfo
    {
        public WebHttpBinding Binding { get; set; }
        public EndpointAddress Address { get; set; }
    }
}
