//===============================================================================
// Microsoft patterns & practices
// Unity Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
    /// <summary>
    ///   An implementation of <see cref = "IDependencyResolverPolicy" /> that resolves to
    ///   to an array populated with the values that result from resolving other instances
    ///   of <see cref = "IDependencyResolverPolicy" />.
    /// </summary>
    public class ResolvedArrayWithElementsResolverPolicy : IDependencyResolverPolicy
    {
        private readonly IDependencyResolverPolicy[] elementPolicies;
        private readonly Resolver resolver;

        /// <summary>
        ///   Create an instance of <see cref = "ResolvedArrayWithElementsResolverPolicy" />
        ///   with the given type and a collection of <see cref = "IDependencyResolverPolicy" />
        ///   instances to use when populating the result.
        /// </summary>
        /// <param name = "elementType">The type.</param>
        /// <param name = "elementPolicies">The resolver policies to use when populating an array.</param>
        public ResolvedArrayWithElementsResolverPolicy(Type elementType,
                                                       params IDependencyResolverPolicy[] elementPolicies)
        {
            Guard.ArgumentNotNull(elementType, "elementType");

            MethodInfo resolverMethodInfo
                = typeof (ResolvedArrayWithElementsResolverPolicy)
                    .GetMethod("DoResolve", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly)
                    .MakeGenericMethod(elementType);

            resolver = (Resolver) Delegate.CreateDelegate(typeof (Resolver), resolverMethodInfo);

            this.elementPolicies = elementPolicies;
        }

        #region IDependencyResolverPolicy Members

        /// <summary>
        ///   Resolve the value for a dependency.
        /// </summary>
        /// <param name = "context">Current build context.</param>
        /// <returns>An array pupulated with the results of resolving the resolver policies.</returns>
        // FxCop suppression: Validation is done by Guard class
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public object Resolve(IBuilderContext context)
        {
            Guard.ArgumentNotNull(context, "context");

            return resolver(context, elementPolicies);
        }

        #endregion

        private static object DoResolve<T>(IBuilderContext context, IDependencyResolverPolicy[] elementPolicies)
        {
            var result = new T[elementPolicies.Length];

            for (int i = 0; i < elementPolicies.Length; i++)
            {
                result[i] = (T) elementPolicies[i].Resolve(context);
            }

            return result;
        }

        #region Nested type: Resolver

        private delegate object Resolver(IBuilderContext context, IDependencyResolverPolicy[] elementPolicies);

        #endregion
    }
}