﻿using System.Web;
using Microsoft.Practices.Unity;

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