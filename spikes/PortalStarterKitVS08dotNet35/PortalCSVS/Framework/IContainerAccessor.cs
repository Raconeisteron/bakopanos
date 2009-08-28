using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal.Framework
{
    public interface IContainerAccessor
    {
        IUnityContainer Container { get; }
    }    

}