using System.Linq;
using DemoApp.DataAccess;
using DemoApp.Model;
using DemoApp.Properties;
using DemoApp.ViewModel;

namespace DemoApp
{
    public class CustomerController : IController
    {
        private readonly CommandController _commands;
        private readonly ICustomerRepository _customerRepository;
        private readonly WorkspaceController _workspaces;

        public CustomerController(WorkspaceController workspaces, CommandController commands,
                                  ICustomerRepository customerRepository)
        {
            _workspaces = workspaces;
            _commands = commands;
            _customerRepository = customerRepository;
        }

        #region IController Members

        public void Run()
        {
            _commands.Add(new CommandViewModel(
                              CustomerStrings.Command_ViewAllCustomers,
                              new RelayCommand(param => ShowAllCustomers())));

            _commands.Add(new CommandViewModel(
                              CustomerStrings.Command_CreateNewCustomer,
                              new RelayCommand(param => CreateNewCustomer())));
        }

        #endregion

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