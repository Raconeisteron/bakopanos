using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Evolutil.Data.AdoNet;
using Microsoft.Practices.Unity;

namespace Evolutil.Data.AdoNet
{
    public class ModuleBuilder : IModuleBuilder
    {
        public ModuleBuilder(IUnityContainer container)
        {
            container.RegisterType<IProductDAO, ProductDAO>();
        }
    }
}
