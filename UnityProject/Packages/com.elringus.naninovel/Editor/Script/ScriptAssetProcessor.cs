using System;
using System.Collections.Generic;
using System.IO;
using Naninovel.Metadata;
using UnityEditor;

namespace Naninovel
{
    public class ScriptAssetPostprocessor : AssetPostprocessor
    {
        private static ScriptsConfiguration cfg;
        private static EditorResources res;

        private static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
        {
            if (BuildProcessor.Building) return;
            // Delayed call is required to prevent running when re-importing all assets,
            // at which point editor resources asset is not available.
            EditorApplication.delayCall += () => PostprocessDelayed(importedAssets, movedAssets);
        }

        private static void PostprocessDelayed (string[] importedAssets, string[] movedAssets)
        {
            cfg ??= Configuration.GetOrDefault<ScriptsConfiguration>();
            res ??= EditorResources.LoadOrDefault();

            var modifiedResource = false;
            var importedDirs = new HashSet<string>();

            foreach (string assetPath in importedAssets)
            {
                if (!assetPath.EndsWithFast(".nani")) continue;
                HandleAutoAdd(assetPath, ref modifiedResource);
                UpdateScriptPath(assetPath, ref modifiedResource);
                importedDirs.Add(Path.GetDirectoryName(Path.GetFullPath(assetPath)));
            }

            foreach (string assetPath in movedAssets)
            {
                if (!assetPath.EndsWithFast(".nani")) continue;
                UpdateScriptPath(assetPath, ref modifiedResource);
                AssetDatabase.ImportAsset(assetPath); // re-import required to actualize script path in serialized lines
            }

            if (modifiedResource)
            {
                EditorUtility.SetDirty(res);
                AssetDatabase.SaveAssets();
            }

            if (importedDirs.Count > 0)
                ScriptFileWatcher.AddWatchedDirectories(importedDirs);
        }

        private static void UpdateScriptPath (string assetPath, ref bool modifiedResource)
        {
            if (!cfg.AutoResolvePath) return;

            var guid = AssetDatabase.AssetPathToGUID(assetPath);
            if (guid is null) return;

            var newPath = ResolveScriptPath(assetPath);
            var newFullPath = $"{cfg.Loader.PathPrefix}/{newPath}";
            var oldFullPath = res.GetPathByGuid(guid);
            if (newFullPath == oldFullPath) return;

            res.RemoveAllRecordsWithGuid(guid);
            res.AddRecord(cfg.Loader.PathPrefix, cfg.Loader.PathPrefix, newPath, guid);
            modifiedResource = true;
        }

        private static void HandleAutoAdd (string assetPath, ref bool modifiedResource)
        {
            if (!cfg.AutoAddScripts) return;

            var guid = AssetDatabase.AssetPathToGUID(assetPath);
            var path = ResolveScriptPath(assetPath);

            // Don't add the script if it's already added.
            if (guid is null || res.GetPathByGuid(guid) != null) return;

            // Add only new scripts created via context menu (will always have a @stop at second line).
            var linesEnum = File.ReadLines(assetPath).GetEnumerator();
            var secondLine = (linesEnum.MoveNext() && linesEnum.MoveNext()) ? linesEnum.Current : null;
            linesEnum.Dispose(); // Release the file.
            if (!secondLine?.EqualsFast(AssetMenuItems.DefaultScriptContent.GetAfterFirst(Environment.NewLine)) ?? true) return;

            // Don't add if another with the same path is already added.
            if (res.Exists(path, cfg.Loader.PathPrefix, cfg.Loader.PathPrefix))
            {
                Engine.Err($"Failed to add '{path}' script: another script with the same path is already added. " +
                           $"Either delete the existing script or use another path.");
                AssetDatabase.MoveAssetToTrash(assetPath);
                return;
            }

            res.AddRecord(cfg.Loader.PathPrefix, cfg.Loader.PathPrefix, path, guid);
            modifiedResource = true;
        }

        private static string ResolveScriptPath (string assetPath)
        {
            var resolver = new ScriptPathResolver { RootUri = PackagePath.ScriptsRoot };
            return resolver.Resolve(assetPath);
        }
    }

    public class ScriptAssetProcessor : AssetModificationProcessor
    {
        private static EditorResources editorResources;

        private static AssetDeleteResult OnWillDeleteAsset (string assetPath, RemoveAssetOptions options)
        {
            if (AssetDatabase.GetMainAssetTypeAtPath(assetPath) != typeof(Script))
                return AssetDeleteResult.DidNotDelete;

            var guid = AssetDatabase.AssetPathToGUID(assetPath);
            if (guid is null) return AssetDeleteResult.DidNotDelete;

            editorResources ??= EditorResources.LoadOrDefault();
            editorResources.RemoveAllRecordsWithGuid(guid);
            EditorUtility.SetDirty(editorResources);
            AssetDatabase.SaveAssets();

            return AssetDeleteResult.DidNotDelete;
        }
    }
}
