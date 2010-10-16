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
using System.Linq;

namespace Microsoft.Practices.Unity
{
    // A helper class to manage the names that get
    // registered in the container
    internal class NamedTypesRegistry
    {
        private readonly NamedTypesRegistry parent;
        private readonly Dictionary<Type, List<string>> registeredKeys;

        public NamedTypesRegistry()
            : this(null)
        {
        }

        public NamedTypesRegistry(NamedTypesRegistry parent)
        {
            this.parent = parent;
            registeredKeys = new Dictionary<Type, List<string>>();
        }

        public IEnumerable<Type> RegisteredTypes
        {
            get { return registeredKeys.Keys; }
        }

        public void RegisterType(Type t, string name)
        {
            if (!registeredKeys.ContainsKey(t))
            {
                registeredKeys[t] = new List<string>();
            }

            RemoveMatchingKeys(t, name);
            registeredKeys[t].Add(name);
        }

        public IEnumerable<string> GetKeys(Type t)
        {
            IEnumerable<string> keys = Enumerable.Empty<string>();

            if (parent != null)
            {
                keys = keys.Concat(parent.GetKeys(t));
            }

            if (registeredKeys.ContainsKey(t))
            {
                keys = keys.Concat(registeredKeys[t]);
            }

            return keys;
        }

        public void Clear()
        {
            registeredKeys.Clear();
        }

        // We need to do this the long way - Silverlight doesn't support List<T>.RemoveAll(Predicate)
        private void RemoveMatchingKeys(Type t, string name)
        {
            IEnumerable<string> uniqueNames = from registeredName in registeredKeys[t]
                                              where registeredName != name
                                              select registeredName;

            registeredKeys[t] = uniqueNames.ToList();
        }
    }
}