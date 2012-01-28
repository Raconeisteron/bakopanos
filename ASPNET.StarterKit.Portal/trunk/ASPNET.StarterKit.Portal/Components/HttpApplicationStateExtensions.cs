using System.Configuration;
using System.Security.Principal;
using System.Web;
using ASPNET.StarterKit.Portal.Web;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace ASPNET.StarterKit.Portal
{
    public static class HttpApplicationStateExtensions
    {
        private const string GlobalContainerKey = "PortalContainer";

        public static IUnityContainer GetContainer(this HttpContext context)
        {
            context.Application.Lock();
            try
            {
                var container = context.Application[GlobalContainerKey] as IUnityContainer;
                if (container == null)
                {
                    container = new UnityContainer();
                    var section = (UnityConfigurationSection) ConfigurationManager.GetSection("unity");
                    section.Configure(container);

                    // additional container initialization 
                    container.RegisterInstance(ConfigurationManager.ConnectionStrings["connectionString"]);

                    container.RegisterInstance<IPortalServerUtility>(new PortalHttpServerUtility());
                    container.RegisterInstance<IPortalPrincipalUtility>(new PortalHttpPrincipalUtility());

                    context.Application[GlobalContainerKey] = container;
                }
                return container;
            }
            finally
            {
                context.Application.UnLock();
            }
        }
    }
}