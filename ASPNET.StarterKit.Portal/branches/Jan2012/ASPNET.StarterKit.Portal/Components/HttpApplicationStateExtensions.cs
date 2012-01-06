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
                var myContainer = appState[GlobalContainerKey] as IUnityContainer;
                if (myContainer == null)
                {
                    myContainer = new UnityContainer();
                    var section = (UnityConfigurationSection) ConfigurationManager.GetSection("unity");
                    section.Configure(myContainer);

                    // additional container initialization 
                    myContainer.RegisterInstance(ConfigurationManager.ConnectionStrings["connectionString"]);

                    appState[GlobalContainerKey] = myContainer;
                }
                return myContainer;
            }
            finally
            {
                appState.UnLock();
            }
        }
    }
}