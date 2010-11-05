using System;
using System.Collections.Generic;

namespace DemoApp.DataAccess
{
    /// <summary>
    /// Represents a source of items in the application.
    /// </summary>
    public interface IRepository<T>
        where T : class
    {
        /// <summary>
        /// Raised when an item is placed into the repository.
        /// </summary>
        event EventHandler<ItemAddedEventArgs<T>> ItemAdded;

        /// <summary>
        /// Places the specified item into the repository.
        /// If the item is already in the repository, an
        /// exception is not thrown.
        /// </summary>
        void Add(T item);

        /// <summary>
        /// Returns true if the specified item exists in the
        /// repository, or false if it is not.
        /// </summary>
        bool Contains(T item);

        /// <summary>
        /// Returns a shallow-copied list of all item in the repository.
        /// </summary>
        List<T> Get();
    }
}