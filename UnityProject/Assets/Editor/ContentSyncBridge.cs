// Local content sync shares the reconstruction converters; it never reads a game installation.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

namespace Manosaba.EditorTools
{
    [InitializeOnLoad]
    public static class ContentSyncBridge
    {
        private static readonly string Repo = Path.GetFullPath(Path.Combine(Application.dataPath, "../.."));
        private static readonly string Root = Path.GetFullPath(Path.Combine(Application.dataPath, "../Library/ManosabaContent"));
        private static bool busy;
        private static double nextPoll;
        private static string waitingId;
        private static string refreshedId;
        private static double refreshedAt;
        // Build time of the editor assembly loaded into this domain.  A converter edited
        // after it (a pull while the Editor is open) is not running yet.
        private static readonly DateTime LoadedAssemblyUtc =
            File.GetLastWriteTimeUtc(typeof(ContentSyncBridge).Assembly.Location);

        static ContentSyncBridge() { EditorApplication.update += Poll; }

        private static void Poll()
        {
            if (Application.isBatchMode || busy || EditorApplication.timeSinceStartup < nextPoll ||
                EditorApplication.isCompiling || EditorApplication.isUpdating) return;
            nextPoll = EditorApplication.timeSinceStartup + 0.5;
            if (!File.Exists(Path.Combine(Root, "request.json"))) return;
            try
            {
                JObject request = ReadRequest();
                string id = (string)request["id"];
                if (EditorApplication.isPlayingOrWillChangePlaymode)
                {
                    if (waitingId != id)
                    {
                        Write(new JObject { ["id"] = id, ["status"] = "WAITING_FOR_EDIT_MODE",
                            ["message"] = "Stop Play Mode; edited content will import automatically." });
                        waitingId = id;
                    }
                    return;
                }
                // Execute() refreshes and converts in one call, so an edited PrefabBuilder would
                // only be queued for compilation while the old one writes the prefabs, and sync
                // would then record the new recipe as applied.  Compile first; the domain reload
                // re-enters Poll with the current converters.
                if (ConvertersNewerThanLoadedAssembly())
                {
                    if (refreshedId != id)
                    {
                        refreshedId = id;
                        refreshedAt = EditorApplication.timeSinceStartup;
                        Write(new JObject { ["id"] = id, ["status"] = "WAITING_FOR_COMPILE",
                            ["message"] = "Compiling changed editor converters before content sync." });
                        AssetDatabase.Refresh();
                        return;
                    }
                    if (EditorUtility.scriptCompilationFailed)
                    {
                        const string failure = "editor scripts have compile errors; content converters were not run";
                        File.Delete(Path.Combine(Root, "request.json"));
                        Write(new JObject { ["id"] = id, ["status"] = "ERROR", ["error"] = failure, ["message"] = failure });
                        Debug.LogError("[ContentSync] " + failure);
                        return;
                    }
                    // Touched but unchanged sources do not recompile; then the loaded code is current.
                    if (EditorApplication.timeSinceStartup - refreshedAt < 5) return;
                }
                Execute();
            }
            catch (Exception error)
            {
                try { Write(new JObject { ["status"] = "ERROR", ["error"] = error.Message, ["message"] = error.Message }); }
                catch (Exception publishError) { Debug.LogError("[ContentSync] Could not publish failure result: " + publishError); }
                File.Delete(Path.Combine(Root, "request.json"));
                Debug.LogException(error);
            }
        }

        private static bool ConvertersNewerThanLoadedAssembly() =>
            Directory.EnumerateFiles(Path.Combine(Application.dataPath, "Editor"), "*.cs", SearchOption.AllDirectories)
                .Any(path => File.GetLastWriteTimeUtc(path) > LoadedAssemblyUtc);

        /// <summary>Batch entry point: -executeMethod Manosaba.EditorTools.ContentSyncBridge.Apply.</summary>
        public static void Apply()
        {
            int code = Execute();
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        private static JObject ReadRequest()
        {
            JObject request = JObject.Parse(File.ReadAllText(Path.Combine(Root, "request.json")));
            if (!Guid.TryParseExact((string)request["id"], "N", out _) ||
                !DateTime.TryParse((string)request["expiresUtc"], CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind, out DateTime expiry) || expiry.ToUniversalTime() < DateTime.UtcNow)
                throw new InvalidOperationException("invalid or expired content request");
            return request;
        }

        public static int Execute()
        {
            var result = new JObject { ["status"] = "ERROR", ["generated"] = DateTime.UtcNow.ToString("o"),
                ["jobs"] = new JArray(), ["imports"] = new JArray(), ["verification"] = new JArray() };
            string previousAddressablesPlan = null;
            bool addressablesPlanChanged = false;
            string sceneToRestore = null;
            bool rebuiltPrefabs = false;
            try
            {
                if (busy || EditorApplication.isPlayingOrWillChangePlaymode)
                    throw new InvalidOperationException("content sync requires idle Edit Mode");
                JObject request = ReadRequest();
                result["id"] = request["id"];
                busy = true;
                File.Delete(Path.Combine(Root, "request.json"));
                Write(new JObject { ["id"] = request["id"], ["status"] = "IMPORTING" });
                if (request.Value<bool?>("export_master_data") == true)
                    result["master_data_source"] = MasterDataImporter.ExportEditableSource();
                AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
                foreach (JToken path in request["import_paths"] as JArray ?? new JArray())
                    ((JArray)result["imports"]).Add(ImportLeaf(Target((string)path)));
                var requestJobs = (request["jobs"] as JArray ?? new JArray()).OfType<JObject>().ToList();
                var prefabGroups = requestJobs.Where(j => (string)j["category"] == "PREFAB")
                    .GroupBy(j => Target((string)j["target"]))
                    .ToDictionary(g => g.Key, g => g.ToArray(), StringComparer.Ordinal);
                var planCache = new Dictionary<string, JObject>(StringComparer.OrdinalIgnoreCase);
                var appliedPrefabs = new HashSet<string>(StringComparer.Ordinal);
                foreach (JObject job in requestJobs)
                {
                    string category = (string)job["category"];
                    string source = Source((string)job["source"]);
                    string target = Target((string)job["target"]);
                    JObject record;
                    if (category == "DATA" || category == "SCENARIO")
                    {
                        var converted = ScriptableObjectConverter.ApplyEditable(source, target,
                            (string)job["type"], (string)job["assembly"], category == "SCENARIO");
                        record = JObject.FromObject(converted);
                    }
                    else if (category == "PREFAB")
                    {
                        if (!appliedPrefabs.Add(target)) continue;
                        var selectedJobs = prefabGroups[target];
                        string planPath = Source((string)job["plan"]);
                        if (selectedJobs.Any(j => Source((string)j["plan"]) != planPath || (string)j["asset_path"] != (string)job["asset_path"]))
                            throw new InvalidOperationException("conflicting prefab plans for " + target);
                        if (!planCache.TryGetValue(planPath, out JObject plan))
                            planCache[planPath] = plan = JObject.Parse(File.ReadAllText(planPath));
                        JObject match = (plan["jobs"] as JArray ?? new JArray()).OfType<JObject>()
                            .SingleOrDefault(p => (string)p["asset_path"] == (string)job["asset_path"]);
                        if (match == null) throw new InvalidOperationException("prefab asset is absent from cached reconstruction plan");
                        // No original absolute dump paths or targets may escape the editable workspace.
                        foreach (JToken obj in match["objects"] as JArray ?? new JArray()) Source((string)obj["dump"]);
                        match["target"] = target;
                        // A changed converter must reach existing copies even when res is unchanged.
                        // Field patches cannot regenerate Timeline/AnimationClip sub-assets, so those
                        // prefabs are rebuilt by the full builder instead.
                        bool rebuild = selectedJobs.Any(j => j.Value<bool?>("rebuild") == true) &&
                                       PrefabBuilder.RebuildsOnRecipeChange(match);
                        if (rebuild && sceneToRestore == null) sceneToRestore = LeaveSceneForRebuild();
                        rebuiltPrefabs |= rebuild;
                        var converted = rebuild
                            ? PrefabBuilder.RebuildEditable(Repo, match, selectedJobs.Select(j => Source((string)j["source"])))
                            : PrefabBuilder.ApplyEditable(Repo, match, selectedJobs.Select(j => Source((string)j["source"])));
                        foreach (JObject selected in selectedJobs)
                        {
                            string selectedSource = Source((string)selected["source"]);
                            var detail = converted.source_results.FirstOrDefault(r => r.source == selectedSource);
                            JObject perSource = detail == null ? new JObject() : JObject.FromObject(detail);
                            if (converted.status == "ERROR")
                            {
                                perSource["status"] = "ERROR";
                                perSource["error"] = converted.error;
                            }
                            perSource["target"] = target;
                            perSource["category"] = category;
                            perSource["source"] = selected["source"];
                            perSource["guid_before"] = converted.guid_before;
                            perSource["guid_after"] = converted.guid_after;
                            ((JArray)result["jobs"]).Add(perSource);
                        }
                        continue;
                    }
                    else throw new InvalidOperationException("unsupported converter category: " + category);
                    record["category"] = category;
                    record["source"] = job["source"];
                    ((JArray)result["jobs"]).Add(record);
                }
                AssetDatabase.SaveAssets();
                if (((JArray)result["imports"]).Any(j => (string)j["status"] == "ERROR"))
                    throw new InvalidOperationException("one or more copied assets failed to import; see per-target import results");
                if (((JArray)result["jobs"]).Any(j => (string)j["status"] == "ERROR"))
                    throw new InvalidOperationException("one or more content converters failed; see per-asset results");
                // Run after leaf imports and prefab conversion: a copied curtain PNG must not
                // be allowed to erase the generated SpriteSkin before the request completes.
                if (request.Value<bool?>("ensure_curtain_skin") == true)
                    result["curtain_skin"] = RestoreOriginalCurtainSkin.EnsureApplied();
                // A rebuild deletes and re-saves the prefab, which drops its Addressables entry.
                if (request.Value<bool?>("setup_addressables") == true || rebuiltPrefabs)
                {
                    string plan = (string)request["addressables_plan"];
                    if (string.IsNullOrEmpty(plan)) plan = "addressables_plan_all.json";
                    if (!string.Equals(Path.GetFileName(plan), plan, StringComparison.Ordinal) ||
                        !plan.EndsWith(".json", StringComparison.OrdinalIgnoreCase))
                        throw new InvalidOperationException("Addressables plan must be a file name ending in .json.");
                    previousAddressablesPlan = Environment.GetEnvironmentVariable("MANOSABA_ADDRESSABLES_PLAN");
                    Environment.SetEnvironmentVariable("MANOSABA_ADDRESSABLES_PLAN", plan);
                    addressablesPlanChanged = true;
                    if (AddressablesSetup.Execute() != 0)
                        throw new InvalidOperationException("Addressables setup failed");
                }
                if (request.Value<bool?>("build_addressables") == true)
                {
                    Write(new JObject { ["id"] = request["id"], ["status"] = "BUILDING" });
                    result["build_exit_code"] = AddressablesBuild.Execute();
                    if (result.Value<int>("build_exit_code") != 0) throw new InvalidOperationException("Addressables build failed");
                }
                foreach (JObject expectation in request["verify"] as JArray ?? new JArray())
                {
                    JObject verified = Verify(expectation);
                    ((JArray)result["verification"]).Add(verified);
                    if (verified.Value<bool>("matches") != true) throw new InvalidOperationException("content runtime accessor verification failed");
                }
                result["status"] = "OK";
                result["message"] = "Imported edited content; field limitations are recorded per asset.";
                Write(result);
                return 0;
            }
            catch (Exception error)
            {
                result["status"] = "ERROR";
                result["error"] = error.Message;
                result["message"] = error.Message;
                try { Write(result); }
                catch (Exception publishError) { Debug.LogError("[ContentSync] Could not publish failure result: " + publishError); }
                Debug.LogError("[ContentSync] " + error);
                return 1;
            }
            finally
            {
                if (addressablesPlanChanged)
                    Environment.SetEnvironmentVariable("MANOSABA_ADDRESSABLES_PLAN", previousAddressablesPlan);
                if (!string.IsNullOrEmpty(sceneToRestore))
                    UnityEditor.SceneManagement.EditorSceneManager.OpenScene(sceneToRestore);
                busy = false;
            }
        }

        // PrefabBuilder assembles in the active scene.  Build in an empty one so the user's open
        // scene is neither modified nor left dirty, and refuse rather than discard unsaved edits.
        private static string LeaveSceneForRebuild()
        {
            var active = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            if (active.isDirty)
                throw new InvalidOperationException("save or revert " + (active.path.Length > 0 ? active.path : "the open scene") +
                                                    " before syncing; generated prefabs are rebuilt outside it");
            UnityEditor.SceneManagement.EditorSceneManager.NewScene(
                UnityEditor.SceneManagement.NewSceneSetup.EmptyScene, UnityEditor.SceneManagement.NewSceneMode.Single);
            return active.path;
        }

        private static JObject Verify(JObject expectation)
        {
            string target = Target((string)expectation["target"]);
            UnityEngine.Object asset = AssetDatabase.LoadMainAssetAtPath(target);
            if (asset == null) throw new InvalidOperationException("verification target does not load: " + target);
            if (expectation["resources_key"] != null)
            {
                asset = Resources.Load((string)expectation["resources_key"]);
                if (asset == null || AssetDatabase.GetAssetPath(asset) != target)
                    throw new InvalidOperationException("Resources runtime key did not resolve the expected asset");
            }
            JToken actual;
            if (expectation["text_id"] != null)
            {
                if (!(asset is Naninovel.Script script)) throw new InvalidOperationException("scenario asset is not a Naninovel Script");
                actual = new JValue(script.TextMap.GetTextOrNull((string)expectation["text_id"]));
                if (script.Playlist.Count == 0) throw new InvalidOperationException("runtime scenario playlist is empty");
            }
            else if (expectation["runtime_property"] != null)
            {
                string name = (string)expectation["runtime_property"];
                var property = asset.GetType().GetProperty(name);
                if (property == null || property.GetIndexParameters().Length != 0)
                    throw new InvalidOperationException("runtime getter not found: " + name);
                actual = JToken.FromObject(property.GetValue(asset));
            }
            else actual = JsonToSerializedApplier.ReadValue(new SerializedObject(asset).FindProperty((string)expectation["property"]));
            JToken expected = expectation["expected"];
            bool matches = JToken.DeepEquals(actual, expected);
            if (actual is JValue value && expected is JValue wanted &&
                (value.Type == JTokenType.Float || value.Type == JTokenType.Integer) &&
                (wanted.Type == JTokenType.Float || wanted.Type == JTokenType.Integer))
                matches = Math.Abs(value.Value<double>() - wanted.Value<double>()) <= 0.00001;
            return new JObject { ["target"] = target, ["property"] = expectation["property"],
                ["text_id"] = expectation["text_id"], ["actual"] = actual, ["expected"] = expected, ["matches"] = matches,
                ["runtime_accessor"] = expectation["runtime_property"] ?? expectation["text_id"] ?? expectation["property"] };
        }

        private static JObject ImportLeaf(string target)
        {
            string project = Directory.GetParent(Application.dataPath).FullName;
            string meta = Path.Combine(project, target + ".meta");
            string before = AssetDatabase.AssetPathToGUID(target);
            var report = new JObject { ["target"] = target, ["status"] = "ERROR", ["guid_before"] = before,
                ["meta_sha256_before"] = FileHash(meta) };
            try
            {
                AssetDatabase.ImportAsset(target, ImportAssetOptions.ForceSynchronousImport | ImportAssetOptions.ForceUpdate);
                UnityEngine.Object asset = AssetDatabase.LoadMainAssetAtPath(target);
                string after = AssetDatabase.AssetPathToGUID(target);
                report["guid_after"] = after;
                report["meta_sha256_after"] = FileHash(meta);
                report["meta_bytes_preserved"] = JToken.DeepEquals(report["meta_sha256_before"], report["meta_sha256_after"]);
                if (asset == null) throw new InvalidOperationException("imported file does not load as a Unity asset (invalid/corrupt content)");
                if (string.IsNullOrEmpty(after)) throw new InvalidOperationException("imported asset has no GUID");
                if (!string.IsNullOrEmpty(before) && before != after)
                    throw new InvalidOperationException("existing asset GUID changed during import");
                report["type"] = asset.GetType().FullName;
                report["status"] = "IMPORTED";
            }
            catch (Exception error) { report["error"] = error.Message; }
            return report;
        }

        private static string FileHash(string path)
        {
            if (!File.Exists(path)) return null;
            using (var hash = SHA256.Create())
            using (var stream = File.OpenRead(path))
                return BitConverter.ToString(hash.ComputeHash(stream)).Replace("-", "").ToLowerInvariant();
        }

        private static string Source(string value)
        {
            if (string.IsNullOrEmpty(value)) throw new InvalidOperationException("content source is missing");
            string path = Path.GetFullPath(Path.Combine(Repo, value));
            string res = Path.Combine(Repo, "res") + Path.DirectorySeparatorChar;
            if (!path.StartsWith(res, StringComparison.OrdinalIgnoreCase) || !File.Exists(path))
                throw new InvalidOperationException("content source must be an existing file under res: " + value);
            for (string cursor = path; cursor.Length >= res.TrimEnd(Path.DirectorySeparatorChar).Length; cursor = Path.GetDirectoryName(cursor))
                if ((File.GetAttributes(cursor) & FileAttributes.ReparsePoint) != 0)
                    throw new InvalidOperationException("content source cannot follow a symlink/junction: " + value);
            return path;
        }

        private static string Target(string value)
        {
            string project = Directory.GetParent(Application.dataPath).FullName;
            if (string.IsNullOrEmpty(value)) throw new InvalidOperationException("content target is missing");
            string absolute = Path.GetFullPath(Path.Combine(project, value));
            string target = Path.GetRelativePath(project, absolute).Replace('\\', '/');
            if (!(target.StartsWith("Assets/GameContent/", StringComparison.Ordinal) ||
                  target == "Assets/NaninovelData/Resources/MasterData.asset") || target.EndsWith(".meta", StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException("content target must be generated content: " + value);
            for (string cursor = absolute; cursor.Length > project.Length; cursor = Path.GetDirectoryName(cursor))
                if ((File.Exists(cursor) || Directory.Exists(cursor)) && (File.GetAttributes(cursor) & FileAttributes.ReparsePoint) != 0)
                    throw new InvalidOperationException("content target cannot follow a symlink/junction: " + value);
            return target;
        }

        private static void Write(JObject result)
        {
            Directory.CreateDirectory(Root);
            string path = Path.Combine(Root, "result.json");
            string temporary = path + ".tmp";
            File.WriteAllText(temporary, result.ToString(), new UTF8Encoding(false));
            DateTime deadline = DateTime.UtcNow.AddSeconds(10);
            while (true)
            {
                try
                {
                    if (File.Exists(path)) File.Replace(temporary, path, null); else File.Move(temporary, path);
                    return;
                }
                catch (IOException) when (DateTime.UtcNow < deadline)
                {
                    // Windows readers or antivirus can briefly deny replacement; keep the existing result atomic.
                    System.Threading.Thread.Sleep(100);
                }
            }
        }
    }
}
