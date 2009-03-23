using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace Evolutil.Data.Linq
{
    public class ModuleBuilder : IModuleBuilder
    {
        public ModuleBuilder(IUnityContainer container)
        {
            container.RegisterType<IProductDAO, ProductDAO>();
        }
    }
}
