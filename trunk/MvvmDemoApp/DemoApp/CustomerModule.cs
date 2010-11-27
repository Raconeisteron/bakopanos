using System.Configuration;
using System.Linq;
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

        private WorkspaceController _workspaces;
        private IUnityContainer _container;

        #endregion // Fields

        [ConfigurationProperty(("customerDataFile"))]
        public string CustomerDataFile
        {
            get { return this["customerDataFile"] as string; }
            internal set { this["customerDataFile"] = value; }
        }

        #region IModule Members

        public void Initialize(IUnityContainer container)
        {
            _container = container;
            container.RegisterType<ICustomerRepository, CustomerRepository>(new ContainerControlledLifetimeManager());
            container.RegisterInstance<ICustomerModule>(this);

            _workspaces = container.Resolve<WorkspaceController>();
            var commands = container.Resolve<CommandController>();

            commands.Add(new CommandViewModel(
                             Strings.MainWindowViewModel_Command_ViewAllCustomers,
                             new RelayCommand(param => ShowAllCustomers())));

            commands.Add(new CommandViewModel(
                             Strings.MainWindowViewModel_Command_CreateNewCustomer,
                             new RelayCommand(param => CreateNewCustomer())));
        }

        #endregion

        private void CreateNewCustomer()
        {
            Customer newCustomer = Customer.CreateNewCustomer();
            var workspace = new CustomerViewModel(newCustomer, _container.Resolve<ICustomerRepository>());
            _workspaces.Add(workspace);
            _workspaces.SetActiveWorkspace(workspace);
        }

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