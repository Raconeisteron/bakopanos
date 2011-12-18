using System.Web;
using Microsoft.Practices.Unity;

namespace Unity.Web
{
    /// <summary>
    /// http://blogs.msdn.com/b/mpuleio/archive/2008/07/17/proof-of-concept-a-simple-di-solution-for-asp-net-webforms.aspx
    /// </summary>
    public static class HttpApplicationStateExtensions
    {
        private const string GlobalContainerKey = "EntLibContainer";

        public static IUnityContainer GetContainer(this HttpApplicationState appState)
        {
            appState.Lock();
            try
            {
                var myContainer = appState[GlobalContainerKey] as IUnityContainer;
                if (myContainer == null)
                {
                    myContainer = new UnityContainer();
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