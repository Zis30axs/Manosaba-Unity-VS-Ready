#if ADDRESSABLES_AVAILABLE
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

namespace Naninovel
{
    public static class AddressableLocator
    {
        /// <summary>
        /// Retrieves all the addressable entries with "Naninovel" label.
        /// </summary>
        public static void LocateResources (ICollection<Metadata.Resource> resources)
        {
            const string label = "Naninovel";
            var settings = AddressableAssetSettingsDefaultObject.GetSettings(false);
            if (!settings) return;
            foreach (var group in settings.groups)
            {
                if (!group) continue;
                foreach (var entry in group.entries)
                    if (entry.labels.Contains(label) && ResolveResource(entry) is { } resource)
                        resources.Add(resource);
            }
        }

        [CanBeNull]
        private static Metadata.Resource ResolveResource (AddressableAssetEntry entry)
        {
            var type = entry.address.GetBetween("Naninovel/", "/");
            if (string.IsNullOrEmpty(type) || type == LocalizationConfiguration.DefaultLocalizationPathPrefix) return null;
            var path = entry.address.GetAfterFirst($"{type}/");
            if (string.IsNullOrEmpty(path)) return null;
            return new() { Type = type, Path = path, AssetId = entry.guid };
        }
    }
}

#else
namespace Naninovel
{
    public static class AddressableLocator
    {
        public static void LocateResources (System.Collections.Generic.ICollection<Metadata.Resource> _) { }
    }
}
#endif
