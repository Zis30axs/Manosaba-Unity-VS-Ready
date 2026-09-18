using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Represents serializable data used for <see cref="ResourceLoader{TResource}"/> construction.
    /// </summary>
    [System.Serializable]
    public class ResourceLoaderConfiguration
    {
        [Tooltip("Path prefix to add for each requested resource.")]
        public string PathPrefix = string.Empty;
        [Tooltip("Provider types to use, in order." +
                 "\n\nBuilt-in options:" +
                 "\n • Addressable — For assets managed via the Addressable Asset System." +
                 "\n • Project — For assets stored in project's `Resources` folders." +
                 "\n • Local — For assets stored on a local file system." +
                 "\n • GoogleDrive — For assets stored remotely on a Google Drive account.")]
        public List<string> ProviderTypes = new() { ResourceProviderConfiguration.AddressableTypeName, ResourceProviderConfiguration.ProjectTypeName };

        public ResourceLoader<TResource> CreateFor<TResource> (IResourceProviderManager resources) where TResource : Object
        {
            var providers = resources.GetProviders(ProviderTypes);
            return new(providers, resources, PathPrefix);
        }

        public LocalizableResourceLoader<TResource> CreateLocalizableFor<TResource> (IResourceProviderManager resources,
            ILocalizationManager l10n, bool fallbackToSource = true) where TResource : Object
        {
            var providers = resources.GetProviders(ProviderTypes);
            return new(providers, resources, l10n, PathPrefix, fallbackToSource);
        }

        public override string ToString () => $"{PathPrefix}- ({string.Join(", ", ProviderTypes.Select(t => t.GetBetween(".", "Resource") ?? t.GetBefore(",")))})";
    }
}
