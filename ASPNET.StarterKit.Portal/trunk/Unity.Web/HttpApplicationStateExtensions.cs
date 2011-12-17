using System.Web;
using Microsoft.Practices.Unity;

namespace Unity.Web
{
    /// <summary>
    /// http://blogs.msdn.com/b/mpuleio/archive/2008/07/17/proof-of-concept-a-simple-di-solution-for-asp-net-webforms.aspx
    /// </summary>
    public static class HttpApplicationStateExtensions
    {
        private const string GlobalContainerKey = "Your global Unity container";

        public static IUnityContainer GetContainer(this HttpApplicationState application)
        {
            application.Lock();
            try
            {
                IUnityContainer container = application[GlobalContainerKey] as IUnityContainer;
                if (container == null)
                {
                    container = new UnityContainer();
                    application[GlobalContainerKey] = container;
                }
                return container;
            }
            finally
            {
                application.UnLock();
            }
        }
    }
}