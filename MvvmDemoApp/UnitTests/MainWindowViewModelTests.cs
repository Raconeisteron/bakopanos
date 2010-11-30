using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using DemoApp.Properties;
using DemoApp.ViewModel;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoApp
{
    [TestClass]
    public class MainWindowViewModelTests
    {
        //
        // NOTE: In order to access the auto-generated Strings class in the main assembly, I had to change the
        // Custom Tool associated with Strings.resx from ResXFileCodeGenerator to PublicResXFileCodeGenerator.
        // You can specify the custom tool by selecting that resx file in Solution Explorer, and then view its 
        // properties by opening the Properties Window for that file.
        //

        [TestMethod]
        public void TestViewAllCustomers()
        {
            IUnityContainer container = Bootstrapper.CreateContainer(default(string[]));
            var module = new CustomerModule();
            module.CustomerDataFile = Constants.CUSTOMER_DATA_FILE;
            module.Initialize(container);

            var target = container.Resolve<MainWindowViewModel>();
            var workspaces = container.Resolve<WorkspaceController>();

            CommandViewModel commandVM =
                target.Commands.First(cvm => cvm.DisplayName == CustomerStrings.MainWindowViewModel_Command_ViewAllCustomers);
            commandVM.Command.Execute(null);

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(workspaces);
            Assert.IsTrue(collectionView.CurrentItem is AllCustomersViewModel, "Invalid current item.");
        }

        [TestMethod]
        public void TestCreateNewCustomer()
        {
            IUnityContainer container = Bootstrapper.CreateContainer(default(string[]));
            var module = new CustomerModule();
            module.CustomerDataFile = Constants.CUSTOMER_DATA_FILE;
            module.Initialize(container);

            var target = container.Resolve<MainWindowViewModel>();
            var workspaces = container.Resolve<WorkspaceController>();

            CommandViewModel commandVM =
                target.Commands.First(cvm => cvm.DisplayName == CustomerStrings.MainWindowViewModel_Command_CreateNewCustomer);
            commandVM.Command.Execute(null);

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(workspaces);
            Assert.IsTrue(collectionView.CurrentItem is CustomerViewModel, "Invalid current item.");
        }

        [TestMethod]
        public void TestCannotViewAllCustomersTwice()
        {
            IUnityContainer container = Bootstrapper.CreateContainer(default(string[]));
            var module = new CustomerModule();
            module.CustomerDataFile = Constants.CUSTOMER_DATA_FILE;
            module.Initialize(container);

            var target = container.Resolve<MainWindowViewModel>();
            var workspaces = container.Resolve<WorkspaceController>();

            CommandViewModel commandVM =
                target.Commands.First(cvm => cvm.DisplayName == CustomerStrings.MainWindowViewModel_Command_ViewAllCustomers);
            // Tell the ViewModel to show all customers twice.
            commandVM.Command.Execute(null);
            commandVM.Command.Execute(null);

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(workspaces);
            Assert.IsTrue(collectionView.CurrentItem is AllCustomersViewModel, "Invalid current item.");
            Assert.IsTrue(workspaces.Count == 1, "Invalid number of view models.");
        }

        [TestMethod]
        public void TestCloseAllCustomersWorkspace()
        {
            // Create the MainWindowViewModel, but not the MainWindow.
            IUnityContainer container = Bootstrapper.CreateContainer(default(string[]));
            var module = new CustomerModule();
            module.CustomerDataFile = Constants.CUSTOMER_DATA_FILE;
            module.Initialize(container);

            var target = container.Resolve<MainWindowViewModel>();
            var workspaces = container.Resolve<WorkspaceController>();

            Assert.AreEqual(0, workspaces.Count, "Workspaces isn't empty.");

            // Find the command that opens the "All Customers" workspace.
            CommandViewModel commandVM =
                target.Commands.First(cvm => cvm.DisplayName == CustomerStrings.MainWindowViewModel_Command_ViewAllCustomers);

            // Open the "All Customers" workspace.
            commandVM.Command.Execute(null);
            Assert.AreEqual(1, workspaces.Count, "Did not create viewmodel.");

            // Ensure the correct type of workspace was created.
            var allCustomersVM = workspaces[0] as AllCustomersViewModel;
            Assert.IsNotNull(allCustomersVM, "Wrong viewmodel type created.");

            // Tell the "All Customers" workspace to close.
            allCustomersVM.CloseCommand.Execute(null);
            Assert.AreEqual(0, workspaces.Count, "Did not close viewmodel.");
        }
    }
}