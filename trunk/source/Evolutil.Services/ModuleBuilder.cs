using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolutil.ServiceContracts;
using Microsoft.Practices.Unity;

namespace Evolutil.Services
{
    public class ModuleBuilder : IModuleBuilder
    {
        public ModuleBuilder(IUnityContainer container)
        {
            container.RegisterType<IProductService, ProductService>();
        }
    }
}
