using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// Provides access to cached <see cref="EngineTypes"/>.
    /// </summary>
    public static class TypeCache
    {
        /// <summary>
        /// Default resource path of the serialized type cache asset.
        /// </summary>
        public const string ResourcePath = "Naninovel/TypeCache";

        // Assigned by TypeResolver on editor start and after recompiling scripts.
        // This is required while in editor because an existing asset may fail to load when invoked
        // under [InitializeOnLoadMethod], at which point asset database may not be initialized.
        // One example of this is when 'Library' is removed and editor restarted. This behaviour is mentioned
        // in the Unity docs: https://docs.unity3d.com/ScriptReference/InitializeOnLoadAttribute.html.
        private static EngineTypes PRELOADED_BY_EDITOR;

        /// <summary>
        /// Loads the cached types from resources; throws when cache asset is missing.
        /// </summary>
        public static EngineTypes Load ()
        {
            if (Application.isEditor) return PRELOADED_BY_EDITOR;
            var json = Resources.Load<TextAsset>(ResourcePath);
            if (!json) throw new Error("Failed to load type cache: missing cache asset.");
            if (string.IsNullOrWhiteSpace(json.text)) throw new Error("Failed to load type cache: cache asset is empty.");
            return JsonUtility.FromJson<EngineTypes>(json.text) ?? throw new Error("Failed to load type cache: invalid JSON.");
        }
    }
}
