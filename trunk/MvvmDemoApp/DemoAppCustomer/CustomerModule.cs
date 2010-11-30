using System;
using System.Configuration;
using System.Linq;
using System.Windows;
using DemoApp.DataAccess;
using DemoApp.Model;
using DemoApp.Properties;
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
                var resources = new ResourceDictionary { Source = new Uri("pack://application:,,,/DemoAppCustomer;component/DataTemplates.xaml") };
                Application.Current.Resources.MergedDictionaries.Add(resources);
            }

            _container = container;
            container.RegisterType<ICustomerRepository, CustomerRepository>(new ContainerControlledLifetimeManager());
            container.RegisterInstance<ICustomerModule>(this);

            //todo: isolate this in a service... then 4 failing the tests would work again...
            {
                _workspaces = container.Resolve<WorkspaceController>();
                var commands = container.Resolve<CommandController>();

                commands.Add(new CommandViewModel(
                                 CustomerStrings.MainWindowViewModel_Command_ViewAllCustomers,
                                 new RelayCommand(param => ShowAllCustomers())));

                commands.Add(new CommandViewModel(
                                 CustomerStrings.MainWindowViewModel_Command_CreateNewCustomer,
                                 new RelayCommand(param => CreateNewCustomer())));
            }
        }

        #endregion

        //todo: isolate this in a service... then 4 failing the tests would work again...
        private void CreateNewCustomer()
        {
            Customer newCustomer = Customer.CreateNewCustomer();
            var workspace = new CustomerViewModel(newCustomer, _container.Resolve<ICustomerRepository>());
            _workspaces.Add(workspace);
            _workspaces.SetActiveWorkspace(workspace);
        }

        //todo: isolate this in a service... then 4 failing the tests would work again...
        private void ShowAllCustomers()
        {
            var workspace =
                _workspaces.FirstOrDefault(vm => vm is AllCustomersViewModel)
                as AllCustomersViewModel;

            if (workspace == null)
            {
                workspace = new AllCustomersViewModel(_container.Resolve<ICustomerRepository>());
                _workspaces.Add(workspace);
            }

            _workspaces.SetActiveWorkspace(workspace);
        }
    }
}