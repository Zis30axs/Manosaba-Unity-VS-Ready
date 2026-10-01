#if UNITY_EDITOR
using System;
using Naninovel;
using UnityEditor;
using UnityEngine;

namespace Manosaba.EditorTools
{
    /// <summary>Serves a private opening script in memory while retaining normal editor resource loading.</summary>
    public sealed class LocalOpeningResourceProvider : EditorResourceProvider
    {
        private readonly string openingFullPath;
        private Script openingScript;
        private bool disposed;

        public LocalOpeningResourceProvider(string fullPath, Script script)
        {
            if (string.IsNullOrWhiteSpace(fullPath)) throw new ArgumentException("An opening resource path is required.", nameof(fullPath));
            if (!script) throw new ArgumentNullException(nameof(script));
            if (AssetDatabase.Contains(script)) throw new ArgumentException("The local opening provider only owns transient scripts.", nameof(script));

            openingFullPath = fullPath;
            openingScript = script;
            openingScript.hideFlags = HideFlags.HideAndDontSave;
            LoadedResources[fullPath] = new Resource<Script>(fullPath, script);
            LocationsCache.Add(new CachedResourceLocation(fullPath, typeof(Script)));
        }

        public override void UnloadResourceBlocking(string path)
        {
            // Keep the private script available when starting another new game in this Play session.
            if (!disposed && path == openingFullPath) return;
            base.UnloadResourceBlocking(path);
        }

        protected override bool AreTypesCompatible(Type sourceType, Type targetType)
        {
            return sourceType == typeof(Script) ? targetType == typeof(Script)
                : base.AreTypesCompatible(sourceType, targetType);
        }

        /// <summary>Call after Play has stopped and all script users have been destroyed.</summary>
        public void DisposeOpening()
        {
            if (disposed) return;
            disposed = true;
            LoadedResources.Remove(openingFullPath);
            LocationsCache.RemoveAll(location => location.Path == openingFullPath);
            if (openingScript) UnityEngine.Object.DestroyImmediate(openingScript);
            openingScript = null;
        }
    }
}
#endif
