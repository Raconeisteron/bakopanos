using System.Configuration;
using System.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace ASPNET.StarterKit.Portal
{
    public static class HttpApplicationStateExtensions
    {
        private const string GlobalContainerKey = "PortalContainer";

        public static IUnityContainer GetContainer(this HttpApplicationState appState)
        {
            appState.Lock();
            try
            {
                var container = appState[GlobalContainerKey] as IUnityContainer;
                if (container == null)
                {
                    container = new UnityContainer();
                    var section = (UnityConfigurationSection) ConfigurationManager.GetSection("unity");
                    section.Configure(container);

                    // additional container initialization 
                    container.RegisterInstance(ConfigurationManager.ConnectionStrings["connectionString"]);

                    appState[GlobalContainerKey] = container;
                }
                return container;
            }
            finally
            {
                appState.UnLock();
            }
        }
    }
}