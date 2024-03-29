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

namespace Microsoft.Practices.ObjectBuilder2
{
    /// <summary>
    ///   Represents a builder policy for mapping build keys.
    /// </summary>
    public class BuildKeyMappingPolicy : IBuildKeyMappingPolicy
    {
        private readonly NamedTypeBuildKey newBuildKey;

        /// <summary>
        ///   Initialize a new instance of the <see cref = "BuildKeyMappingPolicy" /> with the new build key.
        /// </summary>
        /// <param name = "newBuildKey">The new build key.</param>
        public BuildKeyMappingPolicy(NamedTypeBuildKey newBuildKey)
        {
            this.newBuildKey = newBuildKey;
        }

        #region IBuildKeyMappingPolicy Members

        /// <summary>
        ///   Maps the build key.
        /// </summary>
        /// <param name = "buildKey">The build key to map.</param>
        /// <param name = "context">Current build context. Used for contextual information
        ///   if writing a more sophisticated mapping, unused in this implementation.</param>
        /// <returns>The new build key.</returns>
        public NamedTypeBuildKey Map(NamedTypeBuildKey buildKey, IBuilderContext context)
        {
            return newBuildKey;
        }

        #endregion
    }
}