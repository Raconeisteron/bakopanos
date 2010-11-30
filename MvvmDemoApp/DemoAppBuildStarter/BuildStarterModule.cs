using System;
using System.Configuration;
using System.Windows;
using Microsoft.Practices.Unity;

namespace DemoApp
{
    public class BuildStarterModule : ConfigurationSection, IModule
    {
        #region IModule Members

        public void Initialize(IUnityContainer container)
        {
            {                
                // Info: http://msdn.microsoft.com/en-us/library/aa970069.aspx
                var resources = new ResourceDictionary { Source = new Uri("pack://application:,,,/DemoAppBuildStarter;component/DataTemplates.xaml") };
                Application.Current.Resources.MergedDictionaries.Add(resources);
            }

            container.Resolve<BuildStarterController>().Run();

        }

        #endregion

        
    }
}