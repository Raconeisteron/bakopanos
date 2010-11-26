using System.Linq;
using DemoApp.DataAccess;
using DemoApp.Model;
using DemoApp.Properties;
using DemoApp.ViewModel;

namespace DemoApp.Services
{
    public class CustomerService
    {
        #region Fields

        private readonly ICustomerRepository _customerRepository;
        private readonly Workspaces _workspaces;

        #endregion // Fields

        #region Constructor

        public CustomerService(ICustomerRepository customerRepository, Workspaces workspaces, Commands commands)
        {
            _customerRepository = customerRepository;
            _workspaces = workspaces;
            
            commands.Add(new CommandViewModel(
                             Strings.MainWindowViewModel_Command_ViewAllCustomers,
                             new RelayCommand(param => ShowAllCustomers())));

            commands.Add(new CommandViewModel(
                             Strings.MainWindowViewModel_Command_CreateNewCustomer,
                             new RelayCommand(param => CreateNewCustomer())));
        }

        #endregion // Constructor

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
    }
}