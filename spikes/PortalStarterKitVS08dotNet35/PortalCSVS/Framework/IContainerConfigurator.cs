
using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public interface IContainerConfigurator
    {
        void Configure(IUnityContainer container);        
    }   
}