using System.Configuration;
using DemoApp.ViewModel;
using Microsoft.Practices.Unity;

namespace DemoApp
{
    public class Bootstrapper
    {
        public static IUnityContainer CreateContainer(string[] modules)
        {
            IUnityContainer container = new UnityContainer();
            container.RegisterType<WorkspaceController, WorkspaceController>(new ContainerControlledLifetimeManager());
            container.RegisterType<CommandController, CommandController>(new ContainerControlledLifetimeManager());

            if (modules != null)
            {
                foreach (string module in modules)
                {
                    var section = (IModule) ConfigurationManager.GetSection(module);
                    section.Initialize(container);
                }
            }

            return container;
        }
    }
}