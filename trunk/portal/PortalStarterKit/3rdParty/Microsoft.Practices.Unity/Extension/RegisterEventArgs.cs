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

namespace Microsoft.Practices.Unity
{
    /// <summary>
    ///   Event argument class for the <see cref = "ExtensionContext.Registering" /> event.
    /// </summary>
    public class RegisterEventArgs : NamedEventArgs
    {
        /// <summary>
        ///   Create a new instance of <see cref = "RegisterEventArgs" />.
        /// </summary>
        /// <param name = "typeFrom">Type to map from.</param>
        /// <param name = "typeTo">Type to map to.</param>
        /// <param name = "name">Name for the registration.</param>
        /// <param name = "lifetimeManager"><see cref = "LifetimeManager" /> to manage instances.</param>
        public RegisterEventArgs(Type typeFrom, Type typeTo, string name, LifetimeManager lifetimeManager) : base(name)
        {
            this.TypeFrom = typeFrom;
            this.TypeTo = typeTo;
            this.LifetimeManager = lifetimeManager;
        }

        /// <summary>
        ///   Type to map from.
        /// </summary>
        public Type TypeFrom { get; set; }

        /// <summary>
        ///   Type to map to.
        /// </summary>
        public Type TypeTo { get; set; }

        /// <summary>
        ///   <see cref = "LifetimeManager" /> to manage instances.
        /// </summary>
        public LifetimeManager LifetimeManager { get; set; }
    }
}