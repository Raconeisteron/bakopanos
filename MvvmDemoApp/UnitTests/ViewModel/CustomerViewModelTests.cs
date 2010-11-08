using System;
using System.ComponentModel;
using DemoApp.DataAccess;
using DemoApp.DataAccess.Fake;
using DemoApp.Model;
using DemoApp.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

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


        #endregion // Boilerplate Code

        [TestMethod]
        public void TestCustomerType()
        {
            var mocks = new MockRepository();
            Customer cust = Customer.CreateNewCustomer();
            var repos = mocks.DynamicMock<ICustomerRepository>();
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