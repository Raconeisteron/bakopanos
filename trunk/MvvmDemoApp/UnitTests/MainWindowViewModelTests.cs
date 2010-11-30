using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using DemoApp.DataAccess;
using DemoApp.Properties;
using DemoApp.ViewModel;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoApp
{
    [TestClass]
    public class MainWindowViewModelTests
    {
        readonly WorkspaceController _workspaces = new WorkspaceController();

        private MainWindowViewModel GetTarget()
        {            
            var commands = new CommandController();
            //todo: fake this with rhino mocks...
            var repo = new CustomerRepository(new CustomerModule
            {
                CustomerDataFile =
                    Constants.CUSTOMER_DATA_FILE
            });

            new CustomerController(_workspaces, commands, repo).Run();

            return new MainWindowViewModel(_workspaces, commands);
        }

        [TestMethod]
        public void TestViewAllCustomers()
        {
            var target = GetTarget();
            
            CommandViewModel commandVM =
                target.Commands.First(cvm => cvm.DisplayName == CustomerStrings.Command_ViewAllCustomers);
            commandVM.Command.Execute(null);

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(_workspaces);
            Assert.IsTrue(collectionView.CurrentItem is AllCustomersViewModel, "Invalid current item.");
        }

        [TestMethod]
        public void TestCreateNewCustomer()
        {
            var target = GetTarget();

            CommandViewModel commandVM =
                target.Commands.First(cvm => cvm.DisplayName == CustomerStrings.Command_CreateNewCustomer);
            commandVM.Command.Execute(null);

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(_workspaces);
            Assert.IsTrue(collectionView.CurrentItem is CustomerViewModel, "Invalid current item.");
        }

        [TestMethod]
        public void TestCannotViewAllCustomersTwice()
        {
            var target = GetTarget();

            CommandViewModel commandVM =
                target.Commands.First(cvm => cvm.DisplayName == CustomerStrings.Command_ViewAllCustomers);
            // Tell the ViewModel to show all customers twice.
            commandVM.Command.Execute(null);
            commandVM.Command.Execute(null);

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(_workspaces);
            Assert.IsTrue(collectionView.CurrentItem is AllCustomersViewModel, "Invalid current item.");
            Assert.IsTrue(_workspaces.Count == 1, "Invalid number of view models.");
        }

        [TestMethod]
        public void TestCloseAllCustomersWorkspace()
        {
            // Create the MainWindowViewModel, but not the MainWindow.
            var target = GetTarget();

            Assert.AreEqual(0, _workspaces.Count, "Workspaces isn't empty.");

            // Find the command that opens the "All Customers" workspace.
            CommandViewModel commandVM =
                target.Commands.First(cvm => cvm.DisplayName == CustomerStrings.Command_ViewAllCustomers);

            // Open the "All Customers" workspace.
            commandVM.Command.Execute(null);
            Assert.AreEqual(1, _workspaces.Count, "Did not create viewmodel.");

            // Ensure the correct type of workspace was created.
            var allCustomersVM = _workspaces[0] as AllCustomersViewModel;
            Assert.IsNotNull(allCustomersVM, "Wrong viewmodel type created.");

            // Tell the "All Customers" workspace to close.
            allCustomersVM.CloseCommand.Execute(null);
            Assert.AreEqual(0, _workspaces.Count, "Did not close viewmodel.");
        }
    }
}