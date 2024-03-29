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
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Practices.Unity.Properties;

namespace Microsoft.Practices.ObjectBuilder2
{
    /// <summary>
    ///   A custom collection wrapper over <see cref = "IBuilderPolicy" /> objects.
    /// </summary>
    public class PolicyList : IPolicyList
    {
        private readonly IPolicyList innerPolicyList;
        private readonly object lockObject = new object();
        private Dictionary<PolicyKey, IBuilderPolicy> policies = new Dictionary<PolicyKey, IBuilderPolicy>();

        /// <summary>
        ///   Initialize a new instance of a <see cref = "PolicyList" /> class.
        /// </summary>
        public PolicyList()
            : this(null)
        {
        }

        /// <summary>
        ///   Initialize a new instance of a <see cref = "PolicyList" /> class with another policy list.
        /// </summary>
        /// <param name = "innerPolicyList">An inner policy list to search.</param>
        public PolicyList(IPolicyList innerPolicyList)
        {
            this.innerPolicyList = innerPolicyList ?? new NullPolicyList();
        }

        /// <summary>
        ///   Gets the number of items in the locator.
        /// </summary>
        /// <value>
        ///   The number of items in the locator.
        /// </value>
        public int Count
        {
            get { return policies.Count; }
        }

        #region IPolicyList Members

        /// <summary>
        ///   Removes an individual policy type for a build key.
        /// </summary>
        /// <param name = "policyInterface">The type of policy to remove.</param>
        /// <param name = "buildKey">The key the policy applies.</param>
        public void Clear(Type policyInterface,
                          object buildKey)
        {
            lock (lockObject)
            {
                Dictionary<PolicyKey, IBuilderPolicy> newPolicies = ClonePolicies();
                newPolicies.Remove(new PolicyKey(policyInterface, buildKey));
                SwapPolicies(newPolicies);
            }
        }

        /// <summary>
        ///   Removes all policies from the list.
        /// </summary>
        public void ClearAll()
        {
            lock (lockObject)
            {
                SwapPolicies(new Dictionary<PolicyKey, IBuilderPolicy>());
            }
        }

        /// <summary>
        ///   Removes a default policy.
        /// </summary>
        /// <param name = "policyInterface">The type the policy was registered as.</param>
        public void ClearDefault(Type policyInterface)
        {
            Clear(policyInterface, null);
        }

        /// <summary>
        ///   Gets an individual policy.
        /// </summary>
        /// <param name = "policyInterface">The interface the policy is registered under.</param>
        /// <param name = "buildKey">The key the policy applies.</param>
        /// <param name = "localOnly">true if the policy searches local only; otherwise false to seach up the parent chain.</param>
        /// <param name = "containingPolicyList">The policy list in the chain that the searched for policy was found in, null if the policy was
        ///   not found.</param>
        /// <returns>The policy in the list, if present; returns null otherwise.</returns>
        public IBuilderPolicy Get(Type policyInterface, object buildKey, bool localOnly,
                                  out IPolicyList containingPolicyList)
        {
            Type buildType;
            TryGetType(buildKey, out buildType);

            return GetPolicyForKey(policyInterface, buildKey, localOnly, out containingPolicyList) ??
                   GetPolicyForOpenGenericKey(policyInterface, buildKey, buildType, localOnly, out containingPolicyList) ??
                   GetPolicyForType(policyInterface, buildType, localOnly, out containingPolicyList) ??
                   GetPolicyForOpenGenericType(policyInterface, buildType, localOnly, out containingPolicyList) ??
                   GetDefaultForPolicy(policyInterface, localOnly, out containingPolicyList);
        }

        /// <summary>
        ///   Get the non default policy.
        /// </summary>
        /// <param name = "policyInterface">The interface the policy is registered under.</param>
        /// <param name = "buildKey">The key the policy applies to.</param>
        /// <param name = "localOnly">True if the search should be in the local policy list only; otherwise false to search up the parent chain.</param>
        /// <param name = "containingPolicyList">The policy list in the chain that the searched for policy was found in, null if the policy was
        ///   not found.</param>
        /// <returns>The policy in the list if present; returns null otherwise.</returns>
        public IBuilderPolicy GetNoDefault(Type policyInterface, object buildKey, bool localOnly,
                                           out IPolicyList containingPolicyList)
        {
            containingPolicyList = null;

            IBuilderPolicy policy;
            if (policies.TryGetValue(new PolicyKey(policyInterface, buildKey), out policy))
            {
                containingPolicyList = this;
                return policy;
            }

            if (localOnly)
                return null;

            return innerPolicyList.GetNoDefault(policyInterface, buildKey, false, out containingPolicyList);
        }

        /// <summary>
        ///   Sets an individual policy.
        /// </summary>
        /// <param name = "policyInterface">The <see cref = "Type" /> of the policy.</param>
        /// <param name = "policy">The policy to be registered.</param>
        /// <param name = "buildKey">The key the policy applies.</param>
        public void Set(Type policyInterface,
                        IBuilderPolicy policy,
                        object buildKey)
        {
            lock (lockObject)
            {
                Dictionary<PolicyKey, IBuilderPolicy> newPolicies = ClonePolicies();
                newPolicies[new PolicyKey(policyInterface, buildKey)] = policy;
                SwapPolicies(newPolicies);
            }
        }

        /// <summary>
        ///   Sets a default policy. When checking for a policy, if no specific individual policy
        ///   is available, the default will be used.
        /// </summary>
        /// <param name = "policyInterface">The interface to register the policy under.</param>
        /// <param name = "policy">The default policy to be registered.</param>
        public void SetDefault(Type policyInterface,
                               IBuilderPolicy policy)
        {
            Set(policyInterface, policy, null);
        }

        #endregion

        private IBuilderPolicy GetPolicyForKey(Type policyInterface, object buildKey, bool localOnly,
                                               out IPolicyList containingPolicyList)
        {
            if (buildKey != null)
            {
                return GetNoDefault(policyInterface, buildKey, localOnly, out containingPolicyList);
            }
            containingPolicyList = null;
            return null;
        }

        private IBuilderPolicy GetPolicyForOpenGenericKey(Type policyInterface, object buildKey, Type buildType,
                                                          bool localOnly, out IPolicyList containingPolicyList)
        {
            if (buildType != null && buildType.IsGenericType)
            {
                return GetNoDefault(policyInterface, ReplaceType(buildKey, buildType.GetGenericTypeDefinition()),
                                    localOnly, out containingPolicyList);
            }
            containingPolicyList = null;
            return null;
        }

        private IBuilderPolicy GetPolicyForType(Type policyInterface, Type buildType, bool localOnly,
                                                out IPolicyList containingPolicyList)
        {
            if (buildType != null)
            {
                return GetNoDefault(policyInterface, buildType, localOnly, out containingPolicyList);
            }
            containingPolicyList = null;
            return null;
        }

        private IBuilderPolicy GetPolicyForOpenGenericType(Type policyInterface, Type buildType, bool localOnly,
                                                           out IPolicyList containingPolicyList)
        {
            if (buildType != null && buildType.IsGenericType)
            {
                return GetNoDefault(policyInterface, buildType.GetGenericTypeDefinition(), localOnly,
                                    out containingPolicyList);
            }
            containingPolicyList = null;
            return null;
        }

        private IBuilderPolicy GetDefaultForPolicy(Type policyInterface, bool localOnly,
                                                   out IPolicyList containingPolicyList)
        {
            return GetNoDefault(policyInterface, null, localOnly, out containingPolicyList);
        }

        private Dictionary<PolicyKey, IBuilderPolicy> ClonePolicies()
        {
            return new Dictionary<PolicyKey, IBuilderPolicy>(policies);
        }

        private void SwapPolicies(Dictionary<PolicyKey, IBuilderPolicy> newPolicies)
        {
            policies = newPolicies;
            Thread.MemoryBarrier();
        }

        private static bool TryGetType(object buildKey, out Type type)
        {
            type = buildKey as Type;

            if (type == null)
            {
                var basedBuildKey = buildKey as NamedTypeBuildKey;
                if (basedBuildKey != null)
                    type = basedBuildKey.Type;
            }

            return type != null;
        }

        private static object ReplaceType(object buildKey, Type newType)
        {
            var typeKey = buildKey as Type;
            if (typeKey != null)
            {
                return newType;
            }

            var originalKey = buildKey as NamedTypeBuildKey;
            if (originalKey != null)
            {
                return new NamedTypeBuildKey(newType, originalKey.Name);
            }

            throw new ArgumentException(
                string.Format(
                    CultureInfo.CurrentCulture,
                    Resources.CannotExtractTypeFromBuildKey,
                    buildKey),
                "buildKey");
        }

        #region Nested type: NullPolicyList

        private class NullPolicyList : IPolicyList
        {
            #region IPolicyList Members

            public void Clear(Type policyInterface,
                              object buildKey)
            {
                throw new NotImplementedException();
            }

            public void ClearAll()
            {
                throw new NotImplementedException();
            }

            public void ClearDefault(Type policyInterface)
            {
                throw new NotImplementedException();
            }

            public IBuilderPolicy Get(Type policyInterface, object buildKey, bool localOnly,
                                      out IPolicyList containingPolicyList)
            {
                containingPolicyList = null;
                return null;
            }

            public IBuilderPolicy GetNoDefault(Type policyInterface, object buildKey, bool localOnly,
                                               out IPolicyList containingPolicyList)
            {
                containingPolicyList = null;
                return null;
            }

            public void Set(Type policyInterface,
                            IBuilderPolicy policy,
                            object buildKey)
            {
                throw new NotImplementedException();
            }

            public void SetDefault(Type policyInterface,
                                   IBuilderPolicy policy)
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        #endregion

        #region Nested type: PolicyKey

        private struct PolicyKey
        {
#pragma warning disable 219
            public readonly object BuildKey;
            public readonly Type PolicyType;
#pragma warning restore 219

            public PolicyKey(Type policyType,
                             object buildKey)
            {
                PolicyType = policyType;
                BuildKey = buildKey;
            }


            public override bool Equals(object obj)
            {
                if (obj != null && obj.GetType() == typeof (PolicyKey))
                {
                    return this == (PolicyKey) obj;
                }
                return false;
            }

            public override int GetHashCode()
            {
                return (SafeGetHashCode(PolicyType))*37 +
                       SafeGetHashCode(BuildKey);
            }

            public static bool operator ==(PolicyKey left, PolicyKey right)
            {
                return left.PolicyType == right.PolicyType &&
                       Equals(left.BuildKey, right.BuildKey);
            }

            public static bool operator !=(PolicyKey left, PolicyKey right)
            {
                return !(left == right);
            }

            private static int SafeGetHashCode(object obj)
            {
                return obj != null ? obj.GetHashCode() : 0;
            }
        }

        #endregion
    }
}