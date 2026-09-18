using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Naninovel
{
    /// <summary>
    /// When <see cref="ScriptsConfiguration.StableIdentification"/> enabled, stores
    /// largest text ID (revision) ever generated for each script to prevent collisions.
    /// </summary>
    [Serializable]
    public class ScriptRevisions : ScriptableObject
    {
        [Serializable]
        private class RevisionsMap : SerializableMap<string, int> { }

        [SerializeField] private RevisionsMap map = new();

        /// <summary>
        /// Loads an existing asset from package data folder or creates a new default instance.
        /// </summary>
        public static ScriptRevisions LoadOrDefault ()
        {
            var assetPath = PathUtils.Combine(PackagePath.GeneratedDataPath, $"{nameof(ScriptRevisions)}.asset");
            var obj = AssetDatabase.LoadAssetAtPath<ScriptRevisions>(assetPath);
            if (!obj)
            {
                if (File.Exists(assetPath)) throw new UnityException("Unity failed to load an existing asset. Try restarting the editor.");
                obj = CreateInstance<ScriptRevisions>();
                AssetDatabase.CreateAsset(obj, assetPath);
                AssetDatabase.Refresh();
                AssetDatabase.SaveAssets();
            }
            return obj;
        }

        /// <summary>
        /// Returns last set revision for script asset with specified GUID or null when not not found.
        /// </summary>
        public int GetRevision (string assetGuid)
        {
            return map.GetValueOrDefault(assetGuid, 0);
        }

        /// <summary>
        /// Sets revision for script with specified asset GUID.
        /// </summary>
        public void SetRevision (string assetGuid, int revision)
        {
            map[assetGuid] = revision;
        }

        /// <summary>
        /// Serializes the asset.
        /// </summary>
        public void SaveAsset ()
        {
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
    }
}
