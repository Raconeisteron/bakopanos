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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;

namespace Microsoft.Practices.Unity
{
    /// <summary>
    ///   A <see cref = "ResolverOverride" /> that composites other
    ///   ResolverOverride objects. The GetResolver operation then
    ///   returns the resolver from the first child override that
    ///   matches the current context and request.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix",
        Justification = "Not really a collection, only implement IEnumerable to get convenient initialization syntax.")]
    public class CompositeResolverOverride : ResolverOverride, IEnumerable<ResolverOverride>
    {
        private readonly List<ResolverOverride> overrides = new List<ResolverOverride>();

        #region IEnumerable<ResolverOverride> Members

        /// <summary>
        ///   Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///   An <see cref = "T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///   Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///   A <see cref = "T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public IEnumerator<ResolverOverride> GetEnumerator()
        {
            return overrides.GetEnumerator();
        }

        #endregion

        /// <summary>
        ///   Add a new <see cref = "ResolverOverride" /> to the collection
        ///   that is checked.
        /// </summary>
        /// <param name = "newOverride">item to add.</param>
        public void Add(ResolverOverride newOverride)
        {
            overrides.Add(newOverride);
        }

        /// <summary>
        ///   Add a setof <see cref = "ResolverOverride" />s to the collection.
        /// </summary>
        /// <param name = "newOverrides">items to add.</param>
        public void AddRange(IEnumerable<ResolverOverride> newOverrides)
        {
            overrides.AddRange(newOverrides);
        }

        /// <summary>
        ///   Return a <see cref = "IDependencyResolverPolicy" /> that can be used to give a value
        ///   for the given desired dependency.
        /// </summary>
        /// <param name = "context">Current build context.</param>
        /// <param name = "dependencyType">Type of dependency desired.</param>
        /// <returns>a <see cref = "IDependencyResolverPolicy" /> object if this override applies, null if not.</returns>
        public override IDependencyResolverPolicy GetResolver(IBuilderContext context, Type dependencyType)
        {
            // Walk backwards over the resolvers, this way newer resolvers can replace
            // older ones.
            for (int index = overrides.Count() - 1; index >= 0; --index)
            {
                IDependencyResolverPolicy resolver = overrides[index].GetResolver(context, dependencyType);
                if (resolver != null)
                {
                    return resolver;
                }
            }
            return null;
        }
    }
}