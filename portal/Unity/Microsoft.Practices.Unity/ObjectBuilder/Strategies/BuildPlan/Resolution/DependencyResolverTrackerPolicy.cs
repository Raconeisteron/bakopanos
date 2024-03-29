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

using System.Collections.Generic;

namespace Microsoft.Practices.ObjectBuilder2
{
    /// <summary>
    ///   Implementation of <see cref = "IDependencyResolverTrackerPolicy" />.
    /// </summary>
    public class DependencyResolverTrackerPolicy : IDependencyResolverTrackerPolicy
    {
        private readonly List<object> keys = new List<object>();

        #region IDependencyResolverTrackerPolicy Members

        /// <summary>
        ///   Add a new resolver to track by key.
        /// </summary>
        /// <param name = "key">Key that was used to add the resolver to the policy set.</param>
        public void AddResolverKey(object key)
        {
            keys.Add(key);
        }

        /// <summary>
        ///   Remove the currently tracked resolvers from the given policy list.
        /// </summary>
        /// <param name = "policies">Policy list to remove the resolvers from.</param>
        public void RemoveResolvers(IPolicyList policies)
        {
            foreach (object key in keys)
            {
                policies.Clear<IDependencyResolverPolicy>(key);
            }
            keys.Clear();
        }

        #endregion

        // Helper methods for adding and removing the tracker policy.

        /// <summary>
        ///   Get an instance that implements <see cref = "IDependencyResolverTrackerPolicy" />,
        ///   either the current one in the policy set or creating a new one if it doesn't
        ///   exist.
        /// </summary>
        /// <param name = "policies">Policy list to look up from.</param>
        /// <param name = "buildKey">Build key to track.</param>
        /// <returns>The resolver tracker.</returns>
        public static IDependencyResolverTrackerPolicy GetTracker(IPolicyList policies, object buildKey)
        {
            var tracker =
                policies.Get<IDependencyResolverTrackerPolicy>(buildKey);
            if (tracker == null)
            {
                tracker = new DependencyResolverTrackerPolicy();
                policies.Set(tracker, buildKey);
            }
            return tracker;
        }

        /// <summary>
        ///   Add a key to be tracked to the current tracker.
        /// </summary>
        /// <param name = "policies">Policy list containing the resolvers and trackers.</param>
        /// <param name = "buildKey">Build key for the resolvers being tracked.</param>
        /// <param name = "resolverKey">Key for the resolver.</param>
        public static void TrackKey(IPolicyList policies, object buildKey, object resolverKey)
        {
            IDependencyResolverTrackerPolicy tracker = GetTracker(policies, buildKey);
            tracker.AddResolverKey(resolverKey);
        }

        /// <summary>
        ///   Remove the resolvers for the given build key.
        /// </summary>
        /// <param name = "policies">Policy list containing the build key.</param>
        /// <param name = "buildKey">Build key.</param>
        public static void RemoveResolvers(IPolicyList policies, object buildKey)
        {
            IDependencyResolverTrackerPolicy tracker = GetTracker(policies, buildKey);
            tracker.RemoveResolvers(policies);
        }
    }
}