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
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.Unity
{
    /// <summary>
    ///   Base class for all <see cref = "UnityContainer" /> extension objects.
    /// </summary>
    public abstract class UnityContainerExtension : IUnityContainerExtensionConfigurator
    {
        private IUnityContainer container;
        private ExtensionContext context;

        /// <summary>
        ///   The <see cref = "ExtensionContext" /> object used to manipulate
        ///   the inner state of the container.
        /// </summary>
        protected ExtensionContext Context
        {
            get { return context; }
        }

        #region IUnityContainerExtensionConfigurator Members

        /// <summary>
        ///   The container this extension has been added to.
        /// </summary>
        /// <value>The <see cref = "UnityContainer" /> that this extension has been added to.</value>
        public IUnityContainer Container
        {
            get { return container; }
        }

        #endregion

        /// <summary>
        ///   The container calls this method when the extension is added.
        /// </summary>
        /// <param name = "context">A <see cref = "ExtensionContext" /> instance that gives the
        ///   extension access to the internals of the container.</param>
        // FxCop suppression: Names are the same deliberately, as the property gets set from the parameter.
        [SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames",
            MessageId = "context")]
        public void InitializeExtension(ExtensionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            container = context.Container;
            this.context = context;
            Initialize();
        }

        /// <summary>
        ///   Initial the container with this extension's functionality.
        /// </summary>
        /// <remarks>
        ///   When overridden in a derived class, this method will modify the given
        ///   <see cref = "ExtensionContext" /> by adding strategies, policies, etc. to
        ///   install it's functions into the container.
        /// </remarks>
        protected abstract void Initialize();

        /// <summary>
        ///   Removes the extension's functions from the container.
        /// </summary>
        /// <remarks>
        ///   <para>
        ///     This method is called when extensions are being removed from the container. It can be
        ///     used to do things like disconnect event handlers or clean up member state. You do not
        ///     need to remove strategies or policies here; the container will do that automatically.
        ///   </para>
        ///   <para>
        ///     The default implementation of this method does nothing.</para>
        /// </remarks>
        public virtual void Remove()
        {
            // Do nothing by default, can be overridden to do whatever you want.
        }
    }
}