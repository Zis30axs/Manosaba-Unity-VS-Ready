// Builds the project's own Addressables content (G3 step 2).
//
//   Unity -batchmode -quit -projectPath UnityProject \
//         -executeMethod Manosaba.EditorTools.AddressablesBuild.Run -logFile -
//
// Runs AddressableAssetSettings.BuildPlayerContent and records what came out: the result code, the
// output path, the generated catalog(s) and bundle count with sizes, so the catalog can be compared
// with the shipped one (tools/unity-rebuild/out/unity/catalog_dump.json).
//
// Result: tools/unity-rebuild/out/unity/addressables_build_result.json
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Manosaba.EditorTools
{
    public static class AddressablesBuild
    {
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/addressables_build_result.json";

        [MenuItem("Manosaba/Build Addressables Content")]
        public static void Run()
        {
            int code = Execute();
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        public static int Execute()
        {
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));
            var result = new BuildResult { generated = DateTime.UtcNow.ToString("o") };

            try
            {
                // GameContent is generated and ignored by Git. Reapply the tracked curtain
                // recipe before reading Addressables entries so a fresh collaborator clone
                // cannot build with an empty SpriteSkin.
                RestoreOriginalCurtainSkin.EnsureApplied();
                AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;
                if (settings == null)
                {
                    result.status = "BLOCKED_NO_SETTINGS";
                    result.error = "no AddressableAssetSettings; run AddressablesSetup first";
                    Write(outPath, result);
                    Debug.LogError("[AddressablesBuild] " + result.error);
                    return 2;
                }

                result.settings_path = AssetDatabase.GetAssetPath(settings);
                result.groups = settings.groups.Where(g => g != null)
                    .OrderBy(g => g.Name, StringComparer.Ordinal)
                    .Select(g => g.Name + "=" + g.entries.Count).ToList();
                result.entry_count = settings.groups.Where(g => g != null).Sum(g => g.entries.Count);

                var started = DateTime.UtcNow;
                AddressableAssetSettings.BuildPlayerContent(out AddressablesPlayerBuildResult build);
                result.duration_seconds = Math.Round((DateTime.UtcNow - started).TotalSeconds, 1);
                if (build != null)
                {
                    result.error = build.Error;
                    result.output_path = build.OutputPath;
                    result.location_count = build.LocationCount;
                }
                result.status = string.IsNullOrEmpty(result.error) ? "OK" : "FAILED";
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.error = ex.ToString();
                Debug.LogError("[AddressablesBuild] " + ex);
            }

            // ---- inspect what was produced -------------------------------------------------
            try
            {
                string buildRoot = Addressables.BuildPath;   // Library/com.unity.addressables/aa/<platform>
                result.build_root = buildRoot;
                if (Directory.Exists(buildRoot))
                {
                    string[] catalogs = Directory.GetFiles(buildRoot, "catalog*.json", SearchOption.AllDirectories)
                        .Concat(Directory.GetFiles(buildRoot, "catalog*.bin", SearchOption.AllDirectories))
                        .ToArray();
                    foreach (string catalog in catalogs)
                    {
                        result.catalogs.Add(new FileRecord
                        {
                            path = catalog.Replace('\\', '/'),
                            bytes = new FileInfo(catalog).Length,
                            relative = Path.GetRelativePath(buildRoot, catalog).Replace('\\', '/'),
                        });
                    }
                    string[] bundles = Directory.GetFiles(buildRoot, "*.bundle", SearchOption.AllDirectories);
                    result.bundle_count = bundles.Length;
                    result.bundle_bytes = bundles.Sum(b => new FileInfo(b).Length);
                }

                // Where the built content must live for a player to ship it:
                // com.unity.addressables' AddressablesPlayerBuildProcessor.PrepareForPlayerbuild() calls
                // GetStreamingAssetPaths(), which adds Addressables.BuildPath -> "aa" as an *additional
                // path to StreamingAssets* for every player build, regardless of the
                // BuildAddressablesWithPlayerBuild option (that option only controls whether the content
                // is rebuilt).  So the player picks the content up straight from Library -- no copy into
                // Assets/StreamingAssets is needed, and doing that copy only duplicates ~1 GB on disk and
                // risks two providers claiming the same "aa" folder.
                //
                // Consequence worth remembering: ScriptableBuildPipeline *cleans* BuildPath when a content
                // build starts, so a failed content build leaves BuildPath empty and the next player build
                // ships a player with no Addressables content at all (observed 2026-09-17: player smoke
                // content probe failed with InvalidKeyException for all four addresses).  Set
                // MANOSABA_STAGE_STREAMINGASSETS=1 to force the legacy Assets/StreamingAssets/aa copy.
                string stageEnv = Environment.GetEnvironmentVariable("MANOSABA_STAGE_STREAMINGASSETS");
                result.player_streaming_source = buildRoot.Replace('\\', '/');
                result.player_streaming_source_ready =
                    Directory.Exists(buildRoot) &&
                    Directory.GetFiles(buildRoot, "catalog*", SearchOption.AllDirectories).Length > 0 &&
                    Directory.GetFiles(buildRoot, "*.bundle", SearchOption.AllDirectories).Length > 0;
                if (stageEnv == "1" && Directory.Exists(buildRoot) && result.status == "OK")
                {
                    string stageRoot = Path.GetFullPath(Path.Combine(projectRoot, "Assets", "StreamingAssets", "aa"));
                    if (Directory.Exists(stageRoot)) Directory.Delete(stageRoot, true);
                    Directory.CreateDirectory(stageRoot);
                    foreach (string file in Directory.GetFiles(buildRoot, "*", SearchOption.AllDirectories))
                    {
                        string relative = Path.GetRelativePath(buildRoot, file);
                        string target = Path.Combine(stageRoot, relative);
                        Directory.CreateDirectory(Path.GetDirectoryName(target));
                        File.Copy(file, target, true);
                        result.staged_files++;
                        result.staged_bytes += new FileInfo(target).Length;
                    }
                    AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
                    result.staged_root = stageRoot.Replace('\\', '/');
                }
            }
            catch (Exception ex)
            {
                result.inspection_error = ex.GetType().Name + ": " + ex.Message;
            }

            Write(outPath, result);
            Debug.Log($"[AddressablesBuild] {result.status}: entries={result.entry_count} bundles={result.bundle_count} " +
                      $"({result.bundle_bytes / 1048576.0:F1} MB) catalogs={result.catalogs.Count} " +
                      $"in {result.duration_seconds}s; wrote {outPath}");
            return result.status == "OK" ? 0 : 1;
        }

        private static void Write(string path, BuildResult result)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, JsonUtility.ToJson(result, true), new UTF8Encoding(false));
        }

        [Serializable]
        public class FileRecord
        {
            public string path;
            public string relative;
            public long bytes;
        }

        [Serializable]
        public class BuildResult
        {
            public string status = "NOT_RUN";
            public string generated;
            public string settings_path;
            public string error;
            public string inspection_error;
            public string output_path;
            public string build_root;
            public int entry_count;
            public int location_count;
            public int bundle_count;
            public long bundle_bytes;
            public double duration_seconds;
            public string staged_root;
            public int staged_files;
            public long staged_bytes;
            public string player_streaming_source;
            public bool player_streaming_source_ready;
            public List<string> groups = new List<string>();
            public List<FileRecord> catalogs = new List<FileRecord>();
        }
    }
}

