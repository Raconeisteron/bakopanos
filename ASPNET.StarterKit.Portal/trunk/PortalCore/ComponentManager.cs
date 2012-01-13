using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace ASPNET.StarterKit.Portal
{
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

            }
            return _container.Resolve<T>();
        }
    }
}