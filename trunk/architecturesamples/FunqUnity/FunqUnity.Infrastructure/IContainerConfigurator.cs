using Microsoft.Practices.Unity;
using System.Configuration;

namespace FunqUnity.Infrastructure
{
    public interface IContainerConfigurator
    {
        IUnityContainer Configure(IUnityContainer container);
    }
   
}
