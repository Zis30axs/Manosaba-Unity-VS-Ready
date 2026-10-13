using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manosaba.EditorTools
{
    /// <summary>Optional Switch title recipe, applied by the normal content sync/build pipeline.</summary>
    public static class SwitchTitleContentBuilder
    {
        private const string PlanPath = "res/.manosaba/switch-title/plan.json";
        private const string GroupName = "Manosaba.SwitchTitle";
        private const string PcScenarioSource = "res/Assets/#WitchTrials/Scenarios/System/System_Title.asset.json";
        private const string ScenarioTarget = "Assets/GameContent/#WitchTrials/Scenarios/System/System_Title.asset";

        /// <summary>Only the original PC title input is delegated to this opt-in merged recipe.</summary>
        public static bool HandlesScenario(string source, string target, string typeName, string assembly)
        {
            if (target != ScenarioTarget || typeName != "Naninovel.Script" || assembly != "Elringus.Naninovel.Runtime")
                return false;
            string project = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            string repo = Path.GetFullPath(Path.Combine(project, ".."));
            if (!string.Equals(Path.GetFullPath(source), ResolveBelow(repo, PcScenarioSource), StringComparison.OrdinalIgnoreCase))
                return false;
            string input = ResolveBelow(repo, PlanPath);
            if (!File.Exists(input)) return false;
            ValidatePlan(repo, project, JObject.Parse(File.ReadAllText(input)));
            return true;
        }

        public static JObject EnsureApplied()
        {
            string project = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            string repo = Path.GetFullPath(Path.Combine(project, ".."));
            string input = ResolveBelow(repo, PlanPath);
            if (!File.Exists(input)) return new JObject { ["status"] = "disabled", ["reason"] = "No local Switch title plan" };

            string statePath = Path.Combine(project, "Library/ManosabaContent/switch-title-state.json");
            var report = new JObject
            {
                ["status"] = "ERROR", ["generated_utc"] = DateTime.UtcNow.ToString("o"),
                ["plan"] = PlanPath, ["render_verified"] = false
            };
            try
            {
                if (EditorApplication.isPlayingOrWillChangePlaymode)
                    throw new InvalidOperationException("Switch title content must be synchronized outside Play mode.");
                var plan = JObject.Parse(File.ReadAllText(input));
                ValidatePlan(repo, project, plan);
                string signature = (string)plan["signature"];
                report["signature"] = signature;
                report["plan_sha256"] = Hash(input);
                report["pc_source_sha256"] = plan["scenario_merge"]["pc_sha256"].DeepClone();
                var leaves = (JArray)plan["leaves"];
                var jobs = (JArray)plan["jobs"];
                var scenario = (JObject)plan["scenario"];
                JObject previous = ReadState(statePath);
                bool cached = (string)previous?["status"] == "OK" && (string)previous?["signature"] == signature
                    && previous["output_sha256"] is JObject previousHashes
                    && previousHashes.Properties().All(row => File.Exists(ResolveBelow(project, row.Name))
                        && (string)row.Value == Hash(ResolveBelow(project, row.Name)))
                    && Targets(plan).All(path => File.Exists(ResolveBelow(project, path))
                        && !string.IsNullOrEmpty(AssetDatabase.AssetPathToGUID(path))
                        && (string)previous?["output_sha256"]?[path] == Hash(ResolveBelow(project, path)));

                // Refuse before copying anything if rebuilding would require leaving a dirty scene.
                if (!cached) RequireCleanScenes();
                var leafResults = new JArray();
                bool copied = false;
                foreach (JObject leaf in leaves)
                {
                    string source = ResolveBelow(repo, (string)leaf["source"]);
                    string target = (string)leaf["target"];
                    string destination = ResolveBelow(project, target);
                    string hash = Hash(source);
                    string guid = AssetDatabase.AssetPathToGUID(target);
                    bool changed = !File.Exists(destination) || Hash(destination) != hash;
                    if (changed)
                    {
                        RequireCleanScenes();
                        Directory.CreateDirectory(Path.GetDirectoryName(destination));
                        File.Copy(source, destination, true); // Keep the existing .meta and all its external references.
                        copied = true;
                    }
                    leafResults.Add(new JObject { ["target"] = target, ["sha256"] = hash, ["copied"] = changed, ["guid_before"] = guid });
                }
                if (copied) AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
                bool importerChanged = false;
                for (int i = 0; i < leaves.Count; i++)
                    importerChanged |= EnsureTexture(repo, (JObject)leaves[i], (JObject)leafResults[i]);
                report["leaves"] = leafResults;

                bool rebuild = !cached || copied || importerChanged;
                report["rebuilt"] = rebuild;
                if (rebuild)
                {
                    RequireCleanScenes();
                    Scene previousScene = SceneManager.GetActiveScene();
                    bool replaceEmptyBatchScene = CanReplaceEmptyBatchScene();
                    report["scene_mode"] = replaceEmptyBatchScene ? "BATCH_EMPTY_SINGLE" : "ADDITIVE";
                    Scene temporary = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene,
                        replaceEmptyBatchScene ? NewSceneMode.Single : NewSceneMode.Additive);
                    try
                    {
                        SceneManager.SetActiveScene(temporary);
                        var jobResults = new JArray();
                        report["jobs"] = jobResults;
                        foreach (JObject job in jobs)
                        {
                            var sources = ((JArray)job["objects"]).OfType<JObject>()
                                .Select(row => ResolveBelow(repo, (string)row["dump"]));
                            var result = PrefabBuilder.RebuildEditable(repo, job, sources);
                            jobResults.Add(JObject.FromObject(result));
                            if (result.status != "REBUILT")
                                throw new InvalidOperationException("Switch title prefab failed: " + result.target + ": " + result.error);
                        }
                        var scriptResult = ScriptableObjectConverter.RebuildScenario(
                            ResolveBelow(repo, (string)scenario["source"]), (string)scenario["target"]);
                        report["scenario"] = JObject.FromObject(scriptResult);
                        if ((scriptResult.status != "CONVERTED" && scriptResult.status != "CONVERTED_PARTIAL")
                            || scriptResult.managed_references_failed != 0
                            || scriptResult.params_with_value_dump != scriptResult.params_with_value_written)
                            throw new InvalidOperationException("Switch title scenario failed: " + scriptResult.error);
                    }
                    finally
                    {
                        if (replaceEmptyBatchScene)
                        {
                            // The only replaced scene was the batch runner's clean, zero-object
                            // startup scene. Recreate that empty state and discard only our work.
                            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
                        }
                        else
                        {
                            if (previousScene.IsValid() && previousScene.isLoaded) SceneManager.SetActiveScene(previousScene);
                            if (temporary.IsValid() && temporary.isLoaded) EditorSceneManager.CloseScene(temporary, true);
                        }
                    }
                }
                else
                {
                    // Retain converter diagnostics on a cache hit; skipping work is not a fresh conversion test.
                    report["jobs"] = previous?["jobs"]?.DeepClone();
                    report["scenario"] = previous?["scenario"]?.DeepClone();
                }

                var verified = new JArray();
                foreach (JObject job in jobs)
                {
                    string path = (string)job["target"];
                    var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                    if (!prefab) throw new InvalidDataException("Switch title prefab did not load: " + path);
                    int missing = prefab.GetComponentsInChildren<Transform>(true)
                        .Sum(t => GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(t.gameObject));
                    if (missing != 0) throw new InvalidDataException(path + ": " + missing + " missing scripts");
                    verified.Add(new JObject { ["target"] = path, ["loaded"] = true, ["missing_scripts"] = missing });
                }
                var script = AssetDatabase.LoadAssetAtPath<Naninovel.Script>((string)scenario["target"]);
                if (!script || script.Playlist == null || script.Playlist.Count == 0)
                    throw new InvalidDataException("Switch title scenario has no compiled playlist.");
                report["verified_prefabs"] = verified;
                report["scenario_commands"] = script.Playlist.Count;
                // Core address setup can move/remove entries even when the content signature is unchanged.
                report["addresses"] = EnsureAddresses((JArray)plan["addresses"]);
                // Generic sync can replace an existing Script/prefab without changing this
                // plan. Detect that overwrite next time instead of trusting existence alone.
                var outputHashes = new JObject();
                var generatedDependencies = AssetDatabase.GetDependencies(
                    jobs.Select(job => (string)job["target"]).ToArray(), true)
                    .Where(path => path.StartsWith("Assets/GameContent/#WitchTrials/SwitchTitle/", StringComparison.Ordinal));
                foreach (string path in Targets(plan).Concat(generatedDependencies).Distinct(StringComparer.Ordinal))
                    outputHashes[path] = Hash(ResolveBelow(project, path));
                report["output_sha256"] = outputHashes;
                report["status"] = "OK";
                WriteState(statePath, report);
                return report;
            }
            catch (Exception error)
            {
                report["status"] = "ERROR";
                report["error"] = error.ToString();
                try { WriteState(statePath, report); }
                catch (Exception stateError) { Debug.LogError("Could not save Switch title failure report: " + stateError.Message); }
                throw;
            }
        }

        internal static bool EnsureTexture(string repo, JObject leaf, JObject report)
        {
            string path = (string)leaf["target"];
            var importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (!importer) throw new InvalidDataException("Missing texture importer: " + path);
            bool sprite = (bool)leaf["sprite"];
            var pivot = new Vector2((float)leaf["pivot"][0], (float)leaf["pivot"][1]);
            var border = new Vector4((float)leaf["border"][0], (float)leaf["border"][1], (float)leaf["border"][2], (float)leaf["border"][3]);
            var wrap = (TextureWrapMode)(int)leaf["wrap_mode"];
            var textureSettings = new TextureImporterSettings();
            importer.ReadTextureSettings(textureSettings);
            bool changed = importer.textureType != (sprite ? TextureImporterType.Sprite : TextureImporterType.Default)
                || importer.maxTextureSize != 8192 || importer.npotScale != TextureImporterNPOTScale.None
                || importer.textureCompression != TextureImporterCompression.Uncompressed || importer.mipmapEnabled
                || importer.wrapModeU != wrap || importer.wrapModeV != wrap || importer.wrapModeW != wrap
                || (sprite && (importer.spriteImportMode != SpriteImportMode.Single
                    || !Mathf.Approximately(importer.spritePixelsPerUnit, (float)leaf["ppu"])
                    || textureSettings.spriteAlignment != (int)SpriteAlignment.Custom
                    || textureSettings.spritePivot != pivot || textureSettings.spriteBorder != border));
            if (changed)
            {
                importer.textureType = sprite ? TextureImporterType.Sprite : TextureImporterType.Default;
                importer.maxTextureSize = 8192;
                importer.npotScale = TextureImporterNPOTScale.None;
                importer.textureCompression = TextureImporterCompression.Uncompressed;
                importer.mipmapEnabled = false;
                importer.wrapMode = wrap;
                if (sprite)
                {
                    importer.spriteImportMode = SpriteImportMode.Single;
                    importer.spritePixelsPerUnit = (float)leaf["ppu"];
                    // Read again after changing type/PPU so SetTextureSettings cannot restore their old values.
                    importer.ReadTextureSettings(textureSettings);
                    textureSettings.spriteAlignment = (int)SpriteAlignment.Custom;
                    textureSettings.spritePivot = pivot;
                    textureSettings.spriteBorder = border;
                    importer.SetTextureSettings(textureSettings);
                }
                importer.SaveAndReimport();
            }
            Vector2Int size = PngSize(ResolveBelow(repo, (string)leaf["source"]));
            var texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
            if (!texture || texture.width != size.x || texture.height != size.y)
                throw new InvalidDataException(path + ": imported texture was resized; check platform overrides.");
            if (sprite)
            {
                var asset = AssetDatabase.LoadAssetAtPath<Sprite>(path);
                if (!asset || asset.rect.size != new Vector2(size.x, size.y)
                    || !Mathf.Approximately(asset.pixelsPerUnit, (float)leaf["ppu"])
                    || Vector2.Distance(asset.pivot, Vector2.Scale(pivot, asset.rect.size)) > 0.01f || asset.border != border)
                    throw new InvalidDataException(path + ": imported sprite geometry differs from the recipe.");
            }
            string guid = AssetDatabase.AssetPathToGUID(path);
            if (!string.IsNullOrEmpty((string)report["guid_before"]) && (string)report["guid_before"] != guid)
                throw new InvalidDataException(path + ": texture GUID changed.");
            report["guid"] = guid;
            report["importer_changed"] = changed;
            report["width"] = size.x;
            report["height"] = size.y;
            report["verified"] = true;
            return changed;
        }

        internal static JObject EnsureAddresses(JArray rows, string groupName = GroupName)
        {
            var settings = AddressableAssetSettingsDefaultObject.GetSettings(false);
            if (!settings) throw new InvalidOperationException("Addressables settings are missing; run the core content setup first.");
            var group = settings.FindGroup(groupName);
            bool changed = !group;
            if (!group)
                group = settings.CreateGroup(groupName, false, false, false, GetDefaultSchemas(settings));
            var schema = group.GetSchema<BundledAssetGroupSchema>();
            if (!schema) throw new InvalidOperationException("Switch title group has no bundled asset schema.");
            // Only this extension's group is changed; the template/default group remains untouched.
            if (schema.BuildPath.GetName(settings) != "Local.BuildPath")
            { schema.BuildPath.SetVariableByName(settings, "Local.BuildPath"); changed = true; }
            if (schema.LoadPath.GetName(settings) != "Local.LoadPath")
            { schema.LoadPath.SetVariableByName(settings, "Local.LoadPath"); changed = true; }
            if (!schema.IncludeInBuild) { schema.IncludeInBuild = true; changed = true; }
            var entries = new JArray();
            foreach (JObject row in rows)
            {
                string path = (string)row["path"], address = (string)row["address"];
                string guid = AssetDatabase.AssetPathToGUID(path);
                if (string.IsNullOrEmpty(guid) || !AssetDatabase.LoadMainAssetAtPath(path))
                    throw new InvalidDataException("Addressable asset is missing: " + path);
                if (settings.groups.Where(g => g).SelectMany(g => g.entries)
                    .Any(entry => entry != null && entry.address == address && entry.guid != guid))
                    throw new InvalidDataException("A different asset already uses address " + address);
                var entry = settings.FindAssetEntry(guid);
                if (entry == null || entry.parentGroup != group)
                { entry = settings.CreateOrMoveEntry(guid, group, false, false); changed = true; }
                if (entry.address != address) { entry.SetAddress(address, false); changed = true; }
                foreach (string label in ((JArray)row["labels"]).Values<string>())
                {
                    if (!settings.GetLabels().Contains(label)) { settings.AddLabel(label, false); changed = true; }
                    if (entry.SetLabel(label, true, true, false)) changed = true;
                }
                entries.Add(new JObject { ["path"] = path, ["guid"] = guid, ["address"] = entry.address });
            }
            if (changed)
            {
                EditorUtility.SetDirty(schema);
                EditorUtility.SetDirty(group);
                settings.SetDirty(AddressableAssetSettings.ModificationEvent.BatchModification, null, true, true);
                AssetDatabase.SaveAssets();
            }
            return new JObject { ["group"] = groupName, ["changed"] = changed, ["entries"] = entries };
        }

        private static List<AddressableAssetGroupSchema> GetDefaultSchemas(AddressableAssetSettings settings)
        {
            var template = settings.DefaultGroup;
            if (!template || !template.GetSchema<BundledAssetGroupSchema>())
                throw new InvalidOperationException("The default local Addressables group has no bundled asset schema.");
            // CreateGroup clones these schemas; never edit the shared template instances.
            return template.Schemas;
        }

        private static void ValidatePlan(string repo, string project, JObject plan)
        {
            if ((int?)plan["version"] != 1 || string.IsNullOrWhiteSpace((string)plan["signature"]))
                throw new InvalidDataException("Unsupported or unsigned Switch title plan.");
            if (!(plan["leaves"] is JArray leaves) || !(plan["jobs"] is JArray jobs)
                || !(plan["scenario"] is JObject scenario) || !(plan["addresses"] is JArray addresses))
                throw new InvalidDataException("Switch title plan is incomplete.");
            foreach (JObject leaf in leaves)
                ValidateTextureRecipe(repo, project, leaf);
            foreach (JObject job in jobs)
            {
                RequireTarget(project, (string)job["target"], ".prefab");
                if (!(job["objects"] is JArray objects) || objects.Count == 0)
                    throw new InvalidDataException("Switch title prefab has no object graph: " + job["target"]);
                foreach (JObject row in objects) RequireSource(repo, (string)row["dump"]);
                if (job["references"] is JObject references)
                    foreach (JProperty reference in references.Properties())
                    {
                        string referencePath = (string)reference.Value;
                        ResolveBelow(project, referencePath);
                        if (!referencePath.StartsWith("Assets/", StringComparison.Ordinal)
                            && !referencePath.StartsWith("Packages/", StringComparison.Ordinal))
                            throw new InvalidDataException("Unexpected prefab reference: " + referencePath);
                    }
            }
            RequireSource(repo, (string)scenario["source"]);
            RequireTarget(project, (string)scenario["target"], ".asset");
            if ((string)scenario["target"] != ScenarioTarget || (string)scenario["type"] != "Naninovel.Script"
                || (string)scenario["assembly"] != "Elringus.Naninovel.Runtime")
                throw new InvalidDataException("Switch title scenario must use the existing Naninovel Script converter.");
            RequireSource(repo, PcScenarioSource);
            string pcHash = (string)plan["scenario_merge"]?["pc_sha256"];
            if (string.IsNullOrEmpty(pcHash) || !string.Equals(pcHash, Hash(ResolveBelow(repo, PcScenarioSource)), StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("Switch title plan is stale for the PC scenario source; run normal content sync to refresh the merged recipe before applying it.");
            var seenAddresses = new HashSet<string>(StringComparer.Ordinal);
            foreach (JObject row in addresses)
            {
                RequireTarget(project, (string)row["path"], null);
                if (string.IsNullOrWhiteSpace((string)row["address"]) || !seenAddresses.Add((string)row["address"])
                    || !(row["labels"] is JArray labels) || labels.Any(label => string.IsNullOrWhiteSpace((string)label)))
                    throw new InvalidDataException("Invalid or duplicate Switch title address.");
            }
        }

        private static IEnumerable<string> Targets(JObject plan) =>
            ((JArray)plan["leaves"]).Select(row => (string)row["target"])
                .Concat(((JArray)plan["jobs"]).Select(row => (string)row["target"]))
                .Concat(new[] { (string)plan["scenario"]["target"] })
                .Concat(((JArray)plan["addresses"]).Select(row => (string)row["path"]));

        internal static void ValidateTextureRecipe(string repo, string project, JObject leaf)
        {
            RequireSource(repo, (string)leaf["source"]);
            RequireTarget(project, (string)leaf["target"], ".png");
            Vector2Int size = PngSize(ResolveBelow(repo, (string)leaf["source"]));
            if (size.x < 1 || size.y < 1 || size.x > 8192 || size.y > 8192
                || !(leaf["sprite"]?.Type == JTokenType.Boolean) || (float?)leaf["ppu"] <= 0
                || !(leaf["pivot"] is JArray pivot) || pivot.Count != 2
                || !(leaf["border"] is JArray border) || border.Count != 4
                || !Enum.IsDefined(typeof(TextureWrapMode), (int?)leaf["wrap_mode"] ?? -1))
                throw new InvalidDataException("Invalid Switch texture recipe: " + leaf["target"]);
            if (leaf["ppu"] == null || !Finite((float)leaf["ppu"]) || pivot.Any(v => !Finite((float)v))
                || border.Any(v => !Finite((float)v) || (float)v < 0))
                throw new InvalidDataException("Invalid Switch sprite geometry: " + leaf["target"]);
        }

        internal static void RequireCleanScenes()
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
                if (SceneManager.GetSceneAt(i).isDirty)
                    throw new InvalidOperationException("Save the open scenes before synchronizing Switch title content; no scene changes were discarded.");
        }

        internal static bool CanReplaceEmptyBatchScene()
        {
            bool hasUntitledScene = false;
            for (int i = 0; i < SceneManager.sceneCount; i++)
                hasUntitledScene |= string.IsNullOrEmpty(SceneManager.GetSceneAt(i).path);
            if (!hasUntitledScene) return false;
            if (Application.isBatchMode && SceneManager.sceneCount == 1)
            {
                Scene only = SceneManager.GetSceneAt(0);
                if (!only.isDirty && only.isLoaded && only.rootCount == 0) return true;
            }
            // Unity refuses additive creation beside an unsaved untitled scene. Do not
            // work around it by replacing a user's scene, even if its dirty flag is false.
            string details = string.Join(", ", Enumerable.Range(0, SceneManager.sceneCount).Select(i =>
            {
                Scene scene = SceneManager.GetSceneAt(i);
                return scene.name + " (roots=" + scene.rootCount + ", dirty=" + scene.isDirty + ")";
            }));
            throw new InvalidOperationException("Save the untitled scene before synchronizing Switch title content; retained " + details);
        }

        internal static string ResolveBelow(string root, string relative)
        {
            if (string.IsNullOrWhiteSpace(relative) || Path.IsPathRooted(relative)
                || relative.Split('/', '\\').Any(part => part == ".." || part == "."))
                throw new InvalidDataException("Expected a contained relative path: " + relative);
            string prefix = Path.GetFullPath(root).TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar) + Path.DirectorySeparatorChar;
            string full = Path.GetFullPath(Path.Combine(prefix, relative));
            if (!full.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                throw new InvalidDataException("Path leaves its content root: " + relative);
            return full;
        }

        private static void RequireTarget(string project, string path, string extension)
        {
            ResolveBelow(project, path);
            if (!path.StartsWith("Assets/GameContent/", StringComparison.Ordinal)
                || (extension != null && !path.EndsWith(extension, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidDataException("Unexpected generated Switch title target: " + path);
        }

        private static void RequireSource(string repo, string path)
        {
            string full = ResolveBelow(repo, path);
            if (!File.Exists(full)) throw new FileNotFoundException("Switch title recipe source is missing", full);
        }

        private static bool Finite(float value) => !float.IsNaN(value) && !float.IsInfinity(value);

        private static Vector2Int PngSize(string path)
        {
            var header = new byte[24];
            using (var stream = File.OpenRead(path))
                if (stream.Read(header, 0, header.Length) != header.Length
                    || !header.Take(8).SequenceEqual(new byte[] { 137, 80, 78, 71, 13, 10, 26, 10 })
                    || header[12] != 73 || header[13] != 72 || header[14] != 68 || header[15] != 82)
                    throw new InvalidDataException("Invalid PNG header: " + path);
            return new Vector2Int(ReadBigEndian(header, 16), ReadBigEndian(header, 20));
        }

        private static int ReadBigEndian(byte[] data, int offset) =>
            (data[offset] << 24) | (data[offset + 1] << 16) | (data[offset + 2] << 8) | data[offset + 3];

        internal static string Hash(string path)
        {
            using (var sha = SHA256.Create())
            using (var stream = File.OpenRead(path))
                return BitConverter.ToString(sha.ComputeHash(stream)).Replace("-", "").ToLowerInvariant();
        }

        internal static JObject ReadState(string path)
        {
            if (!File.Exists(path)) return null;
            try { return JObject.Parse(File.ReadAllText(path)); }
            catch (JsonException) { return null; } // A damaged cache requires a real rebuild, never a skipped one.
        }

        internal static void WriteState(string path, JObject report)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            string temporary = path + ".tmp";
            File.WriteAllText(temporary, report.ToString(Formatting.Indented));
            DateTime deadline = DateTime.UtcNow.AddSeconds(10);
            while (true)
            {
                try
                {
                    if (File.Exists(path)) File.Replace(temporary, path, null);
                    else File.Move(temporary, path);
                    return;
                }
                catch (IOException) when (DateTime.UtcNow < deadline)
                {
                    // Match ContentSyncBridge: tolerate transient Windows readers without
                    // deleting the previous report or exposing a partially written replacement.
                    System.Threading.Thread.Sleep(100);
                }
            }
        }
    }
}
