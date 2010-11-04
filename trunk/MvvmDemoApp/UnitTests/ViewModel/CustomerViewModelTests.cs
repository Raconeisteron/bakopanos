using System;
using System.ComponentModel;
using DemoApp.DataAccess.Fake;
using DemoApp.Model;
using DemoApp.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoApp.ViewModel
{
    [TestClass]
    public class CustomerViewModelTests
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
        public void TestCustomerType()
        {
            Customer cust = Customer.CreateNewCustomer();
            var repos = new CustomerRepository(Constants.CUSTOMER_DATA_FILE);
            var target = new CustomerViewModel(cust, repos);

            target.CustomerType = Strings.CustomerViewModel_CustomerTypeOption_Company;
            Assert.IsTrue(cust.IsCompany, "Should be a company");

            target.CustomerType = Strings.CustomerViewModel_CustomerTypeOption_Person;
            Assert.IsFalse(cust.IsCompany, "Should be a person");

            target.CustomerType = Strings.CustomerViewModel_CustomerTypeOption_NotSpecified;
            string error = (target as IDataErrorInfo)["CustomerType"];
            Assert.IsFalse(String.IsNullOrEmpty(error), "Error message should be returned");
        }
    }
}