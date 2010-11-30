using System;
using System.Configuration;
using System.Windows;
using DemoApp.DataAccess;
using DemoApp.ViewModel;
using Microsoft.Practices.Unity;

namespace DemoApp
{
    public class CustomerModule : ConfigurationSection, IModule, ICustomerModule
    {
        #region Fields

        private IUnityContainer _container;
        private WorkspaceController _workspaces;

        #endregion // Fields

        #region ICustomerModule Members

        [ConfigurationProperty(("customerDataFile"))]
        public string CustomerDataFile
        {
            get { return this["customerDataFile"] as string; }
            internal set { this["customerDataFile"] = value; }
        }

        #endregion

        #region IModule Members

        public void Initialize(IUnityContainer container)
        {
            {
                // Info: http://msdn.microsoft.com/en-us/library/aa970069.aspx
                var resources = new ResourceDictionary
                                    {
                                        Source =
                                            new Uri(
                                            "pack://application:,,,/DemoAppCustomer;component/DataTemplates.xaml")
                                    };
                Application.Current.Resources.MergedDictionaries.Add(resources);
            }

            _container = container;
            container.RegisterType<ICustomerRepository, CustomerRepository>(new ContainerControlledLifetimeManager());
            container.RegisterInstance<ICustomerModule>(this);

            container.Resolve<CustomerController>().Run();
        }

        #endregion
    }
}