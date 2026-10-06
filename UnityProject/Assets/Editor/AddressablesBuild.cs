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
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
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

        [MenuItem("Manosaba/Build Android Addressables Content (offline)")]
        public static void RunAndroid()
        {
            int code = ExecuteAndroid();
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        /// <summary>
        /// Build Android content with a temporary local-only Addressables configuration.
        /// The project has a few intentionally remote groups (for example Video); those
        /// paths are unsuitable for the first offline Android player.  This method changes
        /// only the in-memory/serialized settings for the duration of BuildPlayerContent,
        /// then restores every profile/schema value in finally before saving the asset.
        /// </summary>
        public static int ExecuteAndroid()
        {
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));
            var result = new BuildResult
            {
                generated = DateTime.UtcNow.ToString("o"),
                active_build_target = EditorUserBuildSettings.activeBuildTarget.ToString(),
                original_active_build_target = EditorUserBuildSettings.activeBuildTarget.ToString(),
                profile = "Default",
                offline_groups_verified = false,
            };
            var offlineGroups = new List<string>();
            var schemaStates = new List<AndroidSchemaState>();
            AddressableAssetSettings settings = null;
            string originalProfileId = null;
            bool originalBuildRemoteCatalog = false;
            bool originalDisableCatalogUpdateOnStartup = false;
            bool settingsFlagsCaptured = false;
            int code = 1;
            Exception restoreError = null;

            try
            {
                if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
                {
                    result.status = "BLOCKED_WRONG_ACTIVE_BUILD_TARGET";
                    result.error = "Run Unity with -buildTarget Android before invoking RunAndroid.";
                    code = 2;
                }
                else
                {
                    settings = AddressableAssetSettingsDefaultObject.Settings;
                    if (settings == null)
                    {
                        result.status = "BLOCKED_NO_SETTINGS";
                        result.error = "no AddressableAssetSettings; run AddressablesSetup first";
                        code = 2;
                    }
                    else
                    {
                        result.settings_path = AssetDatabase.GetAssetPath(settings);
                        originalProfileId = settings.activeProfileId;
                        originalBuildRemoteCatalog = settings.BuildRemoteCatalog;
                        originalDisableCatalogUpdateOnStartup = settings.DisableCatalogUpdateOnStartup;
                        settingsFlagsCaptured = true;
                        string defaultProfileId = settings.profileSettings.GetProfileId("Default");
                        if (string.IsNullOrEmpty(defaultProfileId))
                        {
                            result.status = "BLOCKED_NO_DEFAULT_PROFILE";
                            result.error = "Addressables profile Default is missing.";
                            code = 2;
                        }
                        else
                        {
                            settings.activeProfileId = defaultProfileId;
                            // The Android first run is self-contained.  Do not emit a remote
                            // catalog or let runtime initialization probe for an update while
                            // the temporary local schema overrides are active.
                            settings.BuildRemoteCatalog = false;
                            settings.DisableCatalogUpdateOnStartup = true;
                            foreach (AddressableAssetGroup group in settings.groups
                                         .Where(g => g != null && g.entries != null && g.entries.Count > 0)
                                         .OrderBy(g => g.Name, StringComparer.Ordinal))
                            {
                                BundledAssetGroupSchema schema = group.GetSchema<BundledAssetGroupSchema>();
                                if (schema == null)
                                    throw new InvalidOperationException("Addressables group has entries but no BundledAssetGroupSchema: " + group.Name);

                                schemaStates.Add(new AndroidSchemaState
                                {
                                    group = group,
                                    schema = schema,
                                    build_path_id = schema.BuildPath.Id,
                                    load_path_id = schema.LoadPath.Id,
                                    include_in_build = schema.IncludeInBuild,
                                });
                                if (!schema.BuildPath.SetVariableByName(settings, AddressableAssetSettings.kLocalBuildPath) ||
                                    !schema.LoadPath.SetVariableByName(settings, AddressableAssetSettings.kLocalLoadPath))
                                    throw new InvalidOperationException("Could not assign Local.BuildPath/Local.LoadPath for group: " + group.Name);
                                schema.IncludeInBuild = true;
                                offlineGroups.Add(group.Name);
                            }

                            result.offline_groups = offlineGroups;
                            result.offline_groups_verified = offlineGroups.Count == schemaStates.Count && offlineGroups.Count > 0;
                            if (!result.offline_groups_verified)
                                throw new InvalidOperationException("No non-empty bundled Addressables groups were verified for offline Android build.");

                            settings.SetDirty(AddressableAssetSettings.ModificationEvent.BatchModification, null, true, true);
                            // BuildPlayerContent reads this in-memory settings object.  Do not
                            // persist the temporary offline overrides before the build; the
                            // finally block saves only the restored original state.
                            code = Execute();
                            result = ReadResult(outPath) ?? result;
                            result.active_build_target = BuildTarget.Android.ToString();
                            result.profile = "Default";
                            result.offline_groups = offlineGroups;
                            result.offline_groups_verified = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.error = ex.ToString();
                Debug.LogError("[AddressablesBuild Android] " + ex);
                code = 1;
            }
            finally
            {
                var restoreErrors = new List<Exception>();
                try
                {
                    foreach (AndroidSchemaState state in schemaStates)
                    {
                        try
                        {
                            if (state.schema == null || state.group == null || state.group.Settings == null)
                                throw new InvalidOperationException("Could not restore Addressables schema state for a temporary Android group.");
                            RestoreProfileReference(state.schema, state.group.Settings, "m_BuildPath", state.build_path_id, state.group.Name);
                            RestoreProfileReference(state.schema, state.group.Settings, "m_LoadPath", state.load_path_id, state.group.Name);
                            state.schema.IncludeInBuild = state.include_in_build;
                        }
                        catch (Exception ex) { restoreErrors.Add(ex); }
                    }
                    try
                    {
                        if (settings != null && originalProfileId != null)
                            settings.activeProfileId = originalProfileId;
                    }
                    catch (Exception ex) { restoreErrors.Add(ex); }
                    if (settings != null && settingsFlagsCaptured)
                    {
                        try
                        {
                            settings.BuildRemoteCatalog = originalBuildRemoteCatalog;
                            settings.DisableCatalogUpdateOnStartup = originalDisableCatalogUpdateOnStartup;
                        }
                        catch (Exception ex) { restoreErrors.Add(ex); }
                    }
                    if (settings != null)
                    {
                        try
                        {
                            settings.SetDirty(AddressableAssetSettings.ModificationEvent.BatchModification, null, true, true);
                            AssetDatabase.SaveAssets();
                        }
                        catch (Exception ex) { restoreErrors.Add(ex); }
                    }
                }
                catch (Exception ex)
                {
                    restoreErrors.Add(ex);
                }
                if (restoreErrors.Count > 0)
                {
                    restoreError = new AggregateException("Failed to restore one or more temporary Android Addressables settings.", restoreErrors);
                    Debug.LogError("[AddressablesBuild Android] " + restoreError);
                }
            }

            if (restoreError != null)
            {
                result.status = "ERROR";
                result.error = "Temporary Android Addressables settings could not be restored: " + restoreError;
                code = 1;
            }
            result.active_build_target = EditorUserBuildSettings.activeBuildTarget.ToString();
            result.profile = "Default";
            result.offline_groups = offlineGroups;
            result.offline_groups_verified = result.offline_groups_verified && restoreError == null;
            Write(outPath, result);
            Debug.Log("[AddressablesBuild Android] " + result.status + ": target=" + result.active_build_target +
                      " profile=" + result.profile + " offlineGroups=" + result.offline_groups.Count +
                      " verified=" + result.offline_groups_verified + "; wrote " + outPath);
            return result.status == "OK" && code == 0 && result.offline_groups_verified ? 0 : code == 2 ? 2 : 1;
        }

        private static BuildResult ReadResult(string path)
        {
            if (!File.Exists(path)) return null;
            try { return JsonUtility.FromJson<BuildResult>(File.ReadAllText(path)); }
            catch (Exception ex)
            {
                Debug.LogError("[AddressablesBuild Android] Could not read build receipt: " + ex);
                return null;
            }
        }

        private static void RestoreProfileReference(BundledAssetGroupSchema schema,
            AddressableAssetSettings settings, string serializedField, string id, string groupName)
        {
            if (!string.IsNullOrEmpty(id))
            {
                ProfileValueReference reference = serializedField == "m_BuildPath" ? schema.BuildPath : schema.LoadPath;
                if (!reference.SetVariableById(settings, id))
                    throw new InvalidOperationException("Could not restore " + serializedField + " for group: " + groupName);
                return;
            }

            // An empty original reference is valid serialized state for an incomplete group.
            // SetVariableById intentionally rejects empty IDs, so preserve that exact state
            // through the schema's serialized field instead of leaving the temporary Local ID.
            var serialized = new SerializedObject(schema);
            SerializedProperty property = serialized.FindProperty(serializedField + ".m_Id");
            if (property == null)
                throw new InvalidOperationException("Could not locate " + serializedField + ".m_Id for group: " + groupName);
            property.stringValue = string.Empty;
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }

        private sealed class AndroidSchemaState
        {
            public AddressableAssetGroup group;
            public BundledAssetGroupSchema schema;
            public string build_path_id;
            public string load_path_id;
            public bool include_in_build;
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
            public string active_build_target;
            public string original_active_build_target;
            public string profile;
            public bool offline_groups_verified;
            public List<string> offline_groups = new List<string>();
            public List<string> groups = new List<string>();
            public List<FileRecord> catalogs = new List<FileRecord>();
        }
    }
}

