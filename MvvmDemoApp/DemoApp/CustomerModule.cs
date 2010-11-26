using System.Configuration;
using System.Linq;
using DemoApp.DataAccess;
using DemoApp.Model;
using DemoApp.Properties;
using DemoApp.ViewModel;

namespace DemoApp
{
    public class CustomerModule : ConfigurationSection, IModule
    {
        #region Fields

        private ICustomerRepository _customerRepository;
        private Workspaces _workspaces;

        #endregion // Fields

        public void Initialize(Workspaces workspaces, Commands commands)
        {
            _customerRepository = new CustomerRepository(CustomerDataFile);

            _workspaces = workspaces;
            
            commands.Add(new CommandViewModel(
                             Strings.MainWindowViewModel_Command_ViewAllCustomers,
                             new RelayCommand(param => ShowAllCustomers())));

            commands.Add(new CommandViewModel(
                             Strings.MainWindowViewModel_Command_CreateNewCustomer,
                             new RelayCommand(param => CreateNewCustomer())));
        }

        private void CreateNewCustomer()
        {
            Customer newCustomer = Customer.CreateNewCustomer();
            var workspace = new CustomerViewModel(newCustomer, _customerRepository);
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
                workspace = new AllCustomersViewModel(_customerRepository);
                _workspaces.Add(workspace);
            }

            _workspaces.SetActiveWorkspace(workspace);
        }

        [ConfigurationProperty(("customerDataFile"))]
        public string CustomerDataFile
        {
            get 
            { 
                return this["customerDataFile"] as string;
            }
            internal set
            {
                this["customerDataFile"] = value;
            }
        }

    }
}