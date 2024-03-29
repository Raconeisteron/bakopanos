//===============================================================================
// Microsoft patterns & practices
// Unity Application Block
//===============================================================================
// Copyright � Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.ObjectBuilder2
{
    /// <summary>
    ///   A <see cref = "IBuilderPolicy" /> that controls how instances are
    ///   persisted and recovered from an external store. Used to implement
    ///   things like singletons and per-http-request lifetime.
    /// </summary>
    public interface ILifetimePolicy : IBuilderPolicy
    {
        /// <summary>
        ///   Retrieve a value from the backing store associated with this Lifetime policy.
        /// </summary>
        /// <returns>the object desired, or null if no such object is currently stored.</returns>
        // FxCop Suppression: This operation could be slow, we want a method
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        object GetValue();

        /// <summary>
        ///   Stores the given value into backing store for retrieval later.
        /// </summary>
        /// <param name = "newValue">The object to store.</param>
        void SetValue(object newValue);

        /// <summary>
        ///   Remove the value this lifetime policy is managing from backing store.
        /// </summary>
        void RemoveValue();
    }
}