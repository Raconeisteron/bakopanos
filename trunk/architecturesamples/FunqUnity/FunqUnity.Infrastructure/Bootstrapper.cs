using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using System.Configuration;

namespace FunqUnity.Infrastructure
{
    public static class Bootstrapper
    {
        public static IUnityContainer Configure(this IUnityContainer container, string[] configSections)
        {            
            foreach (var config in configSections)
            {
                ((IContainerConfigurator)ConfigurationManager.GetSection(config)).Configure(container);
            }
            return container;
        }
    }
}
