using System.Configuration;
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

                    // additional container initialization 
                    container.RegisterInstance<IPortalServerUtility>(new PortalHttpServerUtility());
                    container.RegisterType<IPortalPrincipalUtility, PortalHttpPrincipalUtility>();
                    container.RegisterInstance<IPortalCacheUtility>(new PortalHttpCacheUtility());

                    var section = (UnityConfigurationSection) ConfigurationManager.GetSection("unity");
                    section.Configure(container);

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