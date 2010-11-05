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
        public void TestAllCustomersAreLoaded()
        {
            var target = new CustomerRepository(Constants.CUSTOMER_DATA_FILE);
            Assert.AreEqual(3, target.GetCustomers().Count, "Invalid number of customers in repository.");
        }

        [TestMethod]
        public void TestNewCustomerIsAddedProperly()
        {
            var target = new CustomerRepository(Constants.CUSTOMER_DATA_FILE);
            Customer cust = Customer.CreateNewCustomer();

            bool eventArgIsValid = false;
            target.ItemAdded += (sender, e) => eventArgIsValid = (e.NewItem == cust);
            target.AddCustomer(cust);

            Assert.IsTrue(eventArgIsValid, "Invalid NewCustomer property");
            Assert.IsTrue(target.ContainsCustomer(cust), "New customer was not added");
        }
    }
}