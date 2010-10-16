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
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity.Utility;

namespace Microsoft.Practices.Unity
{
    /// <summary>
    ///   This strategy implements the logic that will call container.ResolveAll
    ///   when an array parameter is detected.
    /// </summary>
    public class ArrayResolutionStrategy : BuilderStrategy
    {
        private static readonly MethodInfo genericResolveArrayMethod = typeof (ArrayResolutionStrategy)
            .GetMethod("ResolveArray", BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.DeclaredOnly);

        /// <summary>
        ///   Do the PreBuildUp stage of construction. This is where the actual work is performed.
        /// </summary>
        /// <param name = "context">Current build context.</param>
        [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods",
            Justification = "Validation done by Guard class")]
        public override void PreBuildUp(IBuilderContext context)
        {
            Guard.ArgumentNotNull(context, "context");
            Type typeToBuild = context.BuildKey.Type;
            if (typeToBuild.IsArray && typeToBuild.GetArrayRank() == 1)
            {
                Type elementType = typeToBuild.GetElementType();

                MethodInfo resolverMethod = genericResolveArrayMethod.MakeGenericMethod(elementType);

                var resolver = (ArrayResolver) Delegate.CreateDelegate(typeof (ArrayResolver), resolverMethod);

                context.Existing = resolver(context);
                context.BuildComplete = true;
            }
        }

        private static object ResolveArray<T>(IBuilderContext context)
        {
            var container = context.NewBuildUp<IUnityContainer>();
            var results = new List<T>(container.ResolveAll<T>());
            return results.ToArray();
        }

        #region Nested type: ArrayResolver

        private delegate object ArrayResolver(IBuilderContext context);

        #endregion
    }
}