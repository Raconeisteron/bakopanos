using System;
using System.Configuration;
using System.Web;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    //TODO: Derive from HttpApplication...
    public class Bootstrapper
    {
        public IUnityContainer Start()
        {
            IUnityContainer container = new UnityContainer();

            //TODO this inside the config section, hide it from the rest of the system...
            container.RegisterInstance<ConnectionStringSettings>(
                ConfigurationManager.ConnectionStrings["ConnectionString"]);

            //TODO this inside the config section, hide it from the rest of the system...
            string configFile = ConfigurationManager.AppSettings["ConfigFile"];
            container.RegisterInstance<string>("ConfigFile", HttpContext.Current.Server.MapPath(configFile));

            //TODO: list in config file...
            var components =
                new[]
                    {
                        "ASPNET.StarterKit.Portal.ContainerComponent,PortalCSVS.Components.SqlClient"
                    };

            //dynamic is ok
            var args = new object[] {container};
            foreach (var component in components)
            {
                Type type = Type.GetType(component);
                type.GetMethod("Configure").Invoke(Activator.CreateInstance(type), args);
            }

            //TODO: list from configurationmanager getsections...
            var section = ConfigurationManager.GetSection("PortalCSVS") as IContainerComponent;
            typeof (IContainerComponent).GetMethod("Configure").Invoke(section, args);


            return container;
        }
    }
}
