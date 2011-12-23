using System;
using System.Configuration;
using System.Web;
using System.Web.Caching;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using NUnit.Framework;

namespace ASPNETPortal
{

    public class PortalModulesDbFake:IPortalModulesDb
    {
        public void DeletePortalModule(int moduleId)
        {
            //
        }
    }

    [TestFixture]
    public class GlobalDbFixture
    {

        [Test]
        public void GetGlobalByPortalId()
        {
            // Create a Unity container and load the Enterprise Library extension.
            IUnityContainer container = new UnityContainer();
            var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            section.Configure(container);

            // additional container initialization 
            container.RegisterInstance("ConfigFile", ConfigurationManager.AppSettings["configFile"]);
            container.RegisterType<IPortalModulesDb, PortalModulesDbFake>(new ContainerControlledLifetimeManager());

            // bootstrapp unity modules)
            foreach (ContainerRegistration containerRegistration in container.Registrations)
            {
                if (containerRegistration.RegisteredType.Name == "UnityModule")
                {
                    container.Resolve(containerRegistration.RegisteredType);
                }
            }

            
                GlobalItem item = container.Resolve<IGlobalDb>().GetGlobalByPortalId(0);
            
        }

        [Test]
        public void UpdatePortalInfo()
        {
            
        }
    }
}