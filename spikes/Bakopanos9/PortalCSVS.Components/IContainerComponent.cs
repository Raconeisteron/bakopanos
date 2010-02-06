using Microsoft.Practices.Unity;

namespace ASPNET.StarterKit.Portal
{
    public interface IContainerComponent 
    {
        void Configure(IUnityContainer container);
    }
}