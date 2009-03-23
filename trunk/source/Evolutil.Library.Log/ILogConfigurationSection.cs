using Microsoft.Practices.Unity;

namespace Evolutil.Library.Log
{
    public interface ILogConfigurationSection
    {
        void Configure(IUnityContainer container);
        void Configure(IUnityContainer container, string logger);
    }
}