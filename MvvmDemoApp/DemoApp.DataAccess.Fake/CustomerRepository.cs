using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Resources;
using System.Xml;
using System.Xml.Linq;
using DemoApp.Model;

namespace DemoApp.DataAccess.Fake
{
    /// <summary>
    /// Represents a source of customers in the application.
    /// </summary>
    internal class CustomerRepository : ICustomerRepository
    {
        #region Fields

        private readonly List<Customer> _customers;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Creates a new repository of customers.
        /// </summary>
        /// <param name="customerDataFile">The relative path to an XML resource file that contains customer data.</param>
        public CustomerRepository(string customerDataFile)
        {
            _customers = LoadCustomers(customerDataFile);
        }

        #endregion // Constructor

        #region Public Interface

        /// <summary>
        /// Raised when a customer is placed into the repository.
        /// </summary>
        public event EventHandler<ItemAddedEventArgs<Customer>> ItemAdded;

        /// <summary>
        /// Places the specified customer into the repository.
        /// If the customer is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        public void Add(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            if (!_customers.Contains(customer))
            {
                _customers.Add(customer);

                if (ItemAdded != null)
                    ItemAdded(this, new ItemAddedEventArgs<Customer>(customer));
            }
        }

        /// <summary>
        /// Returns true if the specified customer exists in the
        /// repository, or false if it is not.
        /// </summary>
        public bool Contains(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            return _customers.Contains(customer);
        }

        /// <summary>
        /// Returns a shallow-copied list of all customers in the repository.
        /// </summary>
        public List<Customer> Get()
        {
            return new List<Customer>(_customers);
        }

        #endregion // Public Interface

        #region Private Helpers

        private static List<Customer> LoadCustomers(string customerDataFile)
        {
            // In a real application, the data would come from an external source,
            // but for this demo let's keep things simple and use a resource file.
            using (Stream stream = GetResourceStream(customerDataFile))
            using (XmlReader xmlRdr = new XmlTextReader(stream))
                return
                    (from customerElem in XDocument.Load(xmlRdr).Element("customers").Elements("customer")
                     select Customer.CreateCustomer(
                         (double) customerElem.Attribute("totalSales"),
                         (string) customerElem.Attribute("firstName"),
                         (string) customerElem.Attribute("lastName"),
                         (bool) customerElem.Attribute("isCompany"),
                         (string) customerElem.Attribute("email")
                         )).ToList();
        }

        private static Stream GetResourceStream(string resourceFile)
        {
            var uri = new Uri(resourceFile, UriKind.RelativeOrAbsolute);

            StreamResourceInfo info = Application.GetResourceStream(uri);
            if (info == null || info.Stream == null)
                throw new ApplicationException("Missing resource file: " + resourceFile);

            return info.Stream;
        }

        #endregion // Private Helpers
    }
}