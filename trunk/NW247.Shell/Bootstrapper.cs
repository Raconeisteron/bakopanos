using System.Windows;
using Infrastructure;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.UnityExtensions;
using Microsoft.Practices.Unity.Configuration;

namespace NW247.Shell
{
    internal class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            RegisterServices();

            var shell = new Shell();
            shell.Show();
            return shell;
        }

        private void RegisterServices()
        {
            var config = ConfigManager.GetConfigSettings<UnityConfigurationSection>("DI.config", "unity");
            config.Containers.Default.Configure(Container);
        }

        protected override IModuleEnumerator GetModuleEnumerator()
        {
            return new StaticModuleEnumerator().AddModule(typeof(Module.Module));
        }
    }
}