using System;
using System.Collections.Generic;

namespace Naninovel
{
    /// <summary>
    /// Implementation is able to manage <see cref="IResourceProvider"/> objects.
    /// </summary>
    public interface IResourceProviderManager : IEngineService<ResourceProviderConfiguration>, IHoldersTracker
    {
        /// <summary>
        /// Event invoked when a message is logged by a managed provider.
        /// </summary>
        event Action<string> OnProviderMessage;

        /// <summary>
        /// Checks whether a resource provider of specified type (assembly-qualified name) is available.
        /// </summary>
        bool IsProviderInitialized (string providerType);
        /// <summary>
        /// Returns a resource provider of the requested type (assembly-qualified name).
        /// </summary>
        IResourceProvider GetProvider (string providerType);
        /// <summary>
        /// Collects resource providers of the requested types (assembly-qualified names), in the requested order.
        /// </summary>
        void GetProviders (IList<IResourceProvider> providers, IReadOnlyList<string> types);

        /// <summary>
        /// Creates a list of resource providers of the requested types (assembly-qualified names), in the requested order.
        /// </summary>
        public List<IResourceProvider> GetProviders (IReadOnlyList<string> types)
        {
            var list = new List<IResourceProvider>();
            GetProviders(list, types);
            return list;
        }
    }
}
