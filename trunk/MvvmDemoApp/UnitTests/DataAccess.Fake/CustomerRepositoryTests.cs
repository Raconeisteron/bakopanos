using DemoApp.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoApp.DataAccess.Fake
{
    [TestClass]
    public class CustomerRepositoryTests
    {
        #region Boilerplate Code

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #endregion // Boilerplate Code

        [TestMethod]
        public void TestAllCustomersAreLoaded()
        {
            var target = new CustomerRepository(Constants.CUSTOMER_DATA_FILE);
            Assert.AreEqual(3, target.Get().Count, "Invalid number of customers in repository.");
        }

        [TestMethod]
        public void TestNewCustomerIsAddedProperly()
        {
            var target = new CustomerRepository(Constants.CUSTOMER_DATA_FILE);
            Customer cust = Customer.CreateNewCustomer();

            bool eventArgIsValid = false;
            target.ItemAdded += (sender, e) => eventArgIsValid = (e.NewItem == cust);
            target.Add(cust);

            Assert.IsTrue(eventArgIsValid, "Invalid NewCustomer property");
            Assert.IsTrue(target.Contains(cust), "New customer was not added");
        }
    }
}