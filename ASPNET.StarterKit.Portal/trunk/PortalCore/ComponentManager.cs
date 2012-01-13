using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace ASPNET.StarterKit.Portal
{
    public interface IUnit
    {
    }
    public static class ComponentManager
    {
        static IUnityContainer _container;

        public static T Resolve<T>()
        {
            if (_container == null)
            {
                _container = new UnityContainer();
                var section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
                section.Configure(_container);

                _container.Resolve<IUnit>("PortalConfigurationUnit");
                _container.Resolve<IUnit>("PortalModulesUnit");
                _container.Resolve<IUnit>("PortalSecurityUnit");

                // additional container initialization 
                _container.RegisterInstance(ConfigurationManager.ConnectionStrings["connectionString"]);
            }
            return _container.Resolve<T>();
        }
    }
}