using System;
using System.Collections.Generic;
using DemoApp.Model;

namespace DemoApp.DataAccess
{
    public interface ICustomerRepository
    {
        /// <summary>
        /// Raised when a customer is placed into the repository.
        /// </summary>
        event EventHandler<CustomerAddedEventArgs> CustomerAdded;

        /// <summary>
        /// Places the specified customer into the repository.
        /// If the customer is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        void AddCustomer(Customer customer);

        /// <summary>
        /// Returns true if the specified customer exists in the
        /// repository, or false if it is not.
        /// </summary>
        bool ContainsCustomer(Customer customer);

        /// <summary>
        /// Returns a shallow-copied list of all customers in the repository.
        /// </summary>
        List<Customer> GetCustomers();
    }
}