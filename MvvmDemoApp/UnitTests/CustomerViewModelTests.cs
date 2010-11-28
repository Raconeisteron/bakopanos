using System;
using System.ComponentModel;
using DemoApp.DataAccess;
using DemoApp.Model;
using DemoApp.Properties;
using DemoApp.ViewModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoApp
{
    [TestClass]
    public class CustomerViewModelTests
    {
        [TestMethod]
        public void TestCustomerType()
        {
            Customer cust = Customer.CreateNewCustomer();
            var repos = new CustomerRepository(new FakeCustomerModule());
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