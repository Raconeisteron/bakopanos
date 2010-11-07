﻿using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using DemoApp.DataAccess.Fake;
using DemoApp.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoApp.ViewModel
{
    [TestClass]
    public class MainWindowViewModelTests
    {
        #region Boilerplate Code

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes

        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //

        #endregion

        #endregion // Boilerplate Code

        //
        // NOTE: In order to access the auto-generated Strings class in the main assembly, I had to change the
        // Custom Tool associated with Strings.resx from ResXFileCodeGenerator to PublicResXFileCodeGenerator.
        // You can specify the custom tool by selecting that resx file in Solution Explorer, and then view its 
        // properties by opening the Properties Window for that file.
        //

        [TestMethod]
        public void TestViewAllCustomers()
        {
            var target = new MainWindowViewModel(new CustomerRepository(Constants.CUSTOMER_DATA_FILE), new ProjectRepository("",""));
            CommandViewModel commandVM =
                target.Commands.First(cvm => cvm.DisplayName == Strings.MainWindowViewModel_Command_ViewAllCustomers);
            commandVM.Command.Execute(null);

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(target.Workspaces);
            Assert.IsTrue(collectionView.CurrentItem is AllCustomersViewModel, "Invalid current item.");
        }

        [TestMethod]
        public void TestCreateNewCustomer()
        {
            var target = new MainWindowViewModel(new CustomerRepository(Constants.CUSTOMER_DATA_FILE), new ProjectRepository("",""));
            CommandViewModel commandVM =
                target.Commands.First(cvm => cvm.DisplayName == Strings.MainWindowViewModel_Command_CreateNewCustomer);
            commandVM.Command.Execute(null);

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(target.Workspaces);
            Assert.IsTrue(collectionView.CurrentItem is CustomerViewModel, "Invalid current item.");
        }

        [TestMethod]
        public void TestCannotViewAllCustomersTwice()
        {
            var target = new MainWindowViewModel(new CustomerRepository(Constants.CUSTOMER_DATA_FILE), new ProjectRepository("",""));
            CommandViewModel commandVM =
                target.Commands.First(cvm => cvm.DisplayName == Strings.MainWindowViewModel_Command_ViewAllCustomers);
            // Tell the ViewModel to show all customers twice.
            commandVM.Command.Execute(null);
            commandVM.Command.Execute(null);

            ICollectionView collectionView = CollectionViewSource.GetDefaultView(target.Workspaces);
            Assert.IsTrue(collectionView.CurrentItem is AllCustomersViewModel, "Invalid current item.");
            Assert.IsTrue(target.Workspaces.Count == 1, "Invalid number of view models.");
        }

        [TestMethod]
        public void TestCloseAllCustomersWorkspace()
        {
            // Create the MainWindowViewModel, but not the MainWindow.
            var target = new MainWindowViewModel(new CustomerRepository(Constants.CUSTOMER_DATA_FILE), new ProjectRepository("",""));

            Assert.AreEqual(0, target.Workspaces.Count, "Workspaces isn't empty.");

            // Find the command that opens the "All Customers" workspace.
            CommandViewModel commandVM =
                target.Commands.First(cvm => cvm.DisplayName == Strings.MainWindowViewModel_Command_ViewAllCustomers);

            // Open the "All Customers" workspace.
            commandVM.Command.Execute(null);
            Assert.AreEqual(1, target.Workspaces.Count, "Did not create viewmodel.");

            // Ensure the correct type of workspace was created.
            var allCustomersVM = target.Workspaces[0] as AllCustomersViewModel;
            Assert.IsNotNull(allCustomersVM, "Wrong viewmodel type created.");

            // Tell the "All Customers" workspace to close.
            allCustomersVM.CloseCommand.Execute(null);
            Assert.AreEqual(0, target.Workspaces.Count, "Did not close viewmodel.");
        }
    }
}