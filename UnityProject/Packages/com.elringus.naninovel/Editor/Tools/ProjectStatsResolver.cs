using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Naninovel
{
    public static class ProjectStatsResolver
    {
        public static ProjectStats Resolve ()
        {
            try
            {
                var asset = GetOrCreateAsset();
                var scripts = LoadScripts();
                asset.TotalScriptsCount = scripts.Count;
                asset.TotalCommandCount = CountCommands(scripts);
                asset.TotalWordsCount = CountWords(scripts);
                EditorUtility.SetDirty(asset);
                AssetDatabase.SaveAssets();
                return asset;
            }
            finally { EditorUtility.ClearProgressBar(); }
        }

        [MenuItem("Naninovel/Show Project Stats", priority = 5)]
        private static void ResolveAndDisplay ()
        {
            var stats = Resolve();
            EditorUtility.DisplayDialog("Naninovel Project Stats",
                $"Total word count across {stats.TotalScriptsCount} scenario scripts stored under \"{PackagePath.ScriptsRoot}\" is {stats.TotalWordsCount}.\n\n" +
                $"This only includes the words in the \"natural\" language, ie text directly shown to the player, such as printed messages and choices.", "Close");
        }

        private static IReadOnlyCollection<Script> LoadScripts ()
        {
            EditorUtility.DisplayProgressBar("Resolving Project Stats", "Loading scenario script assets...", 0);
            return AssetDatabase.FindAssets("t:Naninovel.Script", new[] { PackagePath.ScriptsRoot })
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<Script>)
                .ToArray();
        }

        private static int CountCommands (IEnumerable<Script> scripts)
        {
            var result = 0;
            foreach (var script in scripts)
                result += script.ExtractCommands().Count;
            return result;
        }

        private static int CountWords (IEnumerable<Script> scripts)
        {
            EditorUtility.DisplayProgressBar("Resolving Project Stats", "Counting words in the scripts...", .5f);
            return scripts.Sum(CountInScript);

            static int CountInScript (Script script) => script.TextMap.Map.Values.Sum(CountInText);
            static int CountInText (string text) => text.Split(default(char[]), StringSplitOptions.RemoveEmptyEntries).Length;
        }

        private static ProjectStats GetOrCreateAsset ()
        {
            var asset = Resources.Load<ProjectStats>(ProjectStats.ResourcesPath);
            if (asset) return asset;

            asset = ScriptableObject.CreateInstance<ProjectStats>();
            var path = PathUtils.Combine(PackagePath.TransientAssetPath, "Resources", $"{ProjectStats.ResourcesPath}.asset");
            if (File.Exists(path)) throw new Error($"Unity failed to load an existing '{path}' asset. Try restarting the editor.");
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            AssetDatabase.CreateAsset(asset, path);
            return asset;
        }
    }
}
