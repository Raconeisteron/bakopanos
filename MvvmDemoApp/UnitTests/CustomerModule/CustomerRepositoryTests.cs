using DemoApp.DataAccess;
using DemoApp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoApp
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        [TestMethod]
        public void TestAllCustomersAreLoaded()
        {
            var target = new CustomerRepository(new FakeCustomerModule());
            Assert.AreEqual(3, target.GetCustomers().Count, "Invalid number of customers in repository.");
        }

        [TestMethod]
        public void TestNewCustomerIsAddedProperly()
        {
            var target = new CustomerRepository(new FakeCustomerModule());
            Customer cust = Customer.CreateNewCustomer();

            bool eventArgIsValid = false;
            target.CustomerAdded += (sender, e) => eventArgIsValid = (e.NewItem == cust);
            target.AddCustomer(cust);

            Assert.IsTrue(eventArgIsValid, "Invalid NewCustomer property");
            Assert.IsTrue(target.ContainsCustomer(cust), "New customer was not added");
        }
    }
}