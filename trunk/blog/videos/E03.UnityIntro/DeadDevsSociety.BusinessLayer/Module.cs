using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using DeadDevsSociety.Framework;
using Microsoft.Practices.Unity;

namespace DeadDevsSociety.BusinessLayer
{
    public class Module : ConfigurationSection//,IModule
    {
        public IUnityContainer Configure(IUnityContainer container)
        {
            container.RegisterType<IProductsFacade,ProductsFacade>();            
            return container;
        }
    }
}
