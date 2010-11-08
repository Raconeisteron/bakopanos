﻿using DemoApp.DataAccess;
using DemoApp.DataAccess.Fake;
using DemoApp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace DemoApp.ViewModel
{
    [TestClass]
    public class AllCustomersViewModelTests
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

        [TestMethod]
        public void TestTotalSelectedSales()
        {
            var mocks = new MockRepository();
            var repos = mocks.StrictMock<ICustomerRepository>();
            var target = new AllCustomersViewModel(repos);

            int notifications = 0;
            target.PropertyChanged += (sender, e) =>
                                          {
                                              if (e.PropertyName == "TotalSelectedSales")
                                                  ++notifications;
                                          };

            Assert.AreEqual(0.0, target.TotalSelectedSales, "Should be zero when no customers are selected");

            CustomerViewModel firstCust = target.AllCustomers[0];

            firstCust.IsSelected = true;
            Assert.AreEqual(1, notifications, "TotalSelectedSales change notification was not raised");
            Assert.AreEqual(firstCust.TotalSales, target.TotalSelectedSales);

            CustomerViewModel secondCust = target.AllCustomers[1];
            secondCust.IsSelected = true;
            Assert.AreEqual(2, notifications, "TotalSelectedSales change notification was not raised again");
            Assert.AreEqual(firstCust.TotalSales + secondCust.TotalSales, target.TotalSelectedSales);
        }

        [TestMethod]
        public void TestNewCustomerIsAdded()
        {
            var mocks = new MockRepository();
            var repos = mocks.StrictMock<ICustomerRepository>();
            var target = new AllCustomersViewModel(repos);

            Assert.AreEqual(3, target.AllCustomers.Count, "Test data includes three customers");

            repos.Add(Customer.CreateCustomer(123.45, "new", "customer", false, "new.customer@email.com"));

            Assert.AreEqual(4, target.AllCustomers.Count, "Adding a customer to the repository increase the Count");
        }
    }
}