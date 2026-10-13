using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Naninovel;
using Naninovel.Commands;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Manosaba.EditorTools
{
    /// <summary>Loads the reviewed story delta from the built catalog without running Boot or a scenario.</summary>
    public static class SwitchStoryBuiltProbe
    {
        private const string Key = "Manosaba.SwitchStoryBuiltProbe.";

        [MenuItem("Manosaba/Verify Switch Story Built Content (Empty Play Mode)")]
        public static void Run()
        {
            if (SessionState.GetBool(Key + "active", false)) throw new InvalidOperationException("A story probe is already running.");
            string output = Output("switch-story-built");
            SessionState.SetString(Key + "report", Path.Combine(output, "report.json"));
            SessionState.SetInt(Key + "builder", -1);
            try
            {
                SwitchStoryContentBuilder.ReadValidatedPlan();
                Require(!EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isCompiling, "Wait for Edit Mode and compilation.");
                Require(EditorSceneManager.playModeStartScene == null, "Clear the Play Mode start scene to prevent game boot.");
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    Require(scene.isLoaded && !scene.isDirty && scene.rootCount == 0, "This probe requires clean empty scenes and never closes user scenes.");
                }
                Isolated();
                var settings = AddressableAssetSettingsDefaultObject.GetSettings(false);
                Require(settings, "Addressables settings are missing.");
                int existing = settings.DataBuilders.FindIndex(asset => asset is IDataBuilder builder &&
                    builder.Name.IndexOf("Existing Build", StringComparison.OrdinalIgnoreCase) >= 0);
                Require(existing >= 0, "Use Existing Build is unavailable.");
                SessionState.SetInt(Key + "builder", settings.ActivePlayModeDataBuilderIndex);
                settings.ActivePlayModeDataBuilderIndex = existing;
                EditorUtility.SetDirty(settings);
                AssetDatabase.SaveAssetIfDirty(settings);
                SessionState.SetBool(Key + "active", true);
                SessionState.SetBool(Key + "started", false);
                SessionState.SetBool(Key + "done", false);
                SessionState.SetFloat(Key + "deadline", (float)EditorApplication.timeSinceStartup + 360f);
                Hook();
                EditorApplication.EnterPlaymode();
            }
            catch (Exception error)
            {
                Write(Fail(error));
                Restore();
                SessionState.SetBool(Key + "active", false);
                Debug.LogException(error);
                if (Application.isBatchMode) EditorApplication.Exit(1);
            }
        }

        [MenuItem("Manosaba/Render Switch Story Samples (Asset Database)")]
        public static void RunVisuals() => SwitchStoryVisualProbe.Run();

        internal static string Output(string name)
        {
            string output = Path.GetFullPath(Path.Combine(Application.dataPath, "../../tools/unity-rebuild/out/unity", name,
                DateTime.UtcNow.ToString("yyyyMMdd-HHmmss-fff")));
            Directory.CreateDirectory(output);
            return output;
        }

        [InitializeOnLoadMethod]
        private static void Resume()
        {
            if (!SessionState.GetBool(Key + "active", false)) return;
            if (!SessionState.GetBool(Key + "done", false)) SessionState.SetBool(Key + "started", false);
            Hook();
        }

        private static void Hook() { EditorApplication.update -= Tick; EditorApplication.update += Tick; }

        private static void Tick()
        {
            if (!SessionState.GetBool(Key + "active", false)) { EditorApplication.update -= Tick; return; }
            if (SessionState.GetBool(Key + "done", false))
            {
                if (EditorApplication.isPlayingOrWillChangePlaymode) return;
                try
                {
                    Restore();
                    var report = JObject.Parse(File.ReadAllText(SessionState.GetString(Key + "report", "")));
                    report["builder_restored"] = true;
                    Write(report);
                    SessionState.SetBool(Key + "active", false);
                    Debug.Log("[SwitchStoryBuiltProbe] " + report["status"] + " " + SessionState.GetString(Key + "report", ""));
                    if (Application.isBatchMode) EditorApplication.Exit((string)report["status"] == "PASS" ? 0 : 1);
                }
                catch (Exception error)
                {
                    Write(Fail(error));
                    SessionState.SetBool(Key + "active", false);
                    Debug.LogException(error);
                    if (Application.isBatchMode) EditorApplication.Exit(1);
                }
                return;
            }
            if (EditorApplication.timeSinceStartup > SessionState.GetFloat(Key + "deadline", 0))
            { Finish(Fail(new TimeoutException("Story built-content probe exceeded 360 seconds."))); return; }
            if (!EditorApplication.isPlaying || EditorApplication.isCompiling || SessionState.GetBool(Key + "started", false)) return;
            SessionState.SetBool(Key + "started", true);
            Check();
        }

        private static async void Check()
        {
            var report = new JObject { ["status"] = "RUNNING", ["generated_utc"] = DateTime.UtcNow.ToString("o"),
                ["play_mode"] = true, ["boot_executed"] = false, ["scenario_executed"] = false,
                ["prefab_instantiation_scope"] = "Only the double-objection parameter check uses an inactive clone; no Awake, Boot or timeline playback.",
                ["save_state_accessed"] = false, ["render_verified"] = false,
                ["localization_documents_executed"] = false,
                ["builder_restored"] = false, ["cases"] = new JArray() };
            try
            {
                Isolated();
                JObject plan = SwitchStoryContentBuilder.ReadValidatedPlan();
                report["signature"] = plan["signature"].DeepClone();
                var initialized = Addressables.InitializeAsync(false);
                try { await initialized.Task; Require(initialized.Status == AsyncOperationStatus.Succeeded, "Addressables initialization failed."); }
                finally { if (initialized.IsValid()) Addressables.Release(initialized); }
                foreach (JObject address in (JArray)plan["addresses"])
                {
                    string target = (string)address["path"];
                    Type type = target.EndsWith(".prefab", StringComparison.Ordinal) ? typeof(GameObject) :
                        target.EndsWith(".asset", StringComparison.Ordinal) ? typeof(Script) :
                        target.EndsWith(".wav", StringComparison.Ordinal) ? typeof(AudioClip) : typeof(Texture2D);
                    ((JArray)report["cases"]).Add(await Load((string)address["address"], target, type, plan));
                }
                var calls = ((JArray)report["cases"]).SelectMany(row => row["dynamic_objection_calls"] as JArray ?? new JArray()).ToArray();
                var objectionAddresses = new JArray();
                report["dynamic_objection_addresses"] = objectionAddresses;
                foreach (string key in calls.Where(call => (bool)call["literal_parameters"])
                    .Select(call => (string)call["address"]).Distinct(StringComparer.Ordinal).OrderBy(key => key, StringComparer.Ordinal))
                {
                    if (key == "Naninovel/Spawn/ObjectionCutIn_w")
                        Require(calls.Where(call => (string)call["address"] == key).All(call => (int)call["index"] == 0),
                            "The reviewed double-objection call must select index 0.");
                    objectionAddresses.Add(await CheckObjectionAddress(key));
                }
                Isolated();
                report["status"] = "PASS";
            }
            catch (Exception error) { report["status"] = "FAIL"; report["error"] = error.ToString(); Debug.LogException(error); }
            Finish(report);
        }

        private static async Task<JObject> Load(string key, string target, Type type, JObject plan)
        {
            var locations = Addressables.LoadResourceLocationsAsync(key, type);
            try
            {
                await locations.Task;
                Require(locations.Status == AsyncOperationStatus.Succeeded && locations.Result.Count == 1, "Expected one built location: " + key);
                var location = locations.Result[0];
                Require(location.ProviderId.IndexOf("BundledAssetProvider", StringComparison.Ordinal) >= 0, "Expected BundledAssetProvider: " + key);
                AsyncOperationHandle handle = type == typeof(GameObject) ? Addressables.LoadAssetAsync<GameObject>(location) :
                    type == typeof(Script) ? Addressables.LoadAssetAsync<Script>(location) :
                    type == typeof(AudioClip) ? Addressables.LoadAssetAsync<AudioClip>(location) : Addressables.LoadAssetAsync<Texture2D>(location);
                try
                {
                    await handle.Task;
                    Require(handle.Status == AsyncOperationStatus.Succeeded && handle.Result is Object asset && asset, "Built asset load failed: " + key);
                    var row = new JObject { ["address"] = key, ["target"] = target, ["provider"] = location.ProviderId, ["internal_id"] = location.InternalId };
                    if (handle.Result is GameObject prefab)
                    {
                        JObject job = ((JArray)plan["jobs"]).OfType<JObject>().Single(item => (string)item["target"] == target);
                        row["prefab"] = SwitchStoryContentBuilder.VerifyPrefab(prefab, job);
                        if (key == "Naninovel/Spawn/ObjectionCutIn_w")
                            row["sprite_geometry"] = CheckBuiltSpriteGeometry(prefab, job, plan);
                    }
                    else if (handle.Result is Script script)
                    {
                        JObject recipe = ((JArray)plan["scenarios"]).OfType<JObject>().Single(item => (string)item["target"] == target);
                        string repo = Path.GetFullPath(Path.Combine(Application.dataPath, "../.."));
                        var expected = JObject.Parse(File.ReadAllText(SwitchTitleContentBuilder.ResolveBelow(repo, (string)recipe["source"])));
                        Require(script.Path == (string)expected["path"] && script.Playlist.Count == ((JArray)expected["playlist"]["commands"]).Count &&
                            script.Lines.Count == ((JArray)expected["lines"]).Count && script.Playlist.All(command => command != null), "Built story graph is stale: " + key);
                        var keys = (JArray)expected["textMap"]["idToText"]["keys"];
                        var values = (JArray)expected["textMap"]["idToText"]["values"];
                        Require(script.TextMap.Map.Count == keys.Count, "Built story text-map count changed: " + key);
                        for (int i = 0; i < keys.Count; i++)
                            Require(script.TextMap.GetTextOrNull((string)keys[i]) == (string)values[i], "Built story text/identifier changed: " + keys[i]);
                        row["commands"] = script.Playlist.Count;
                        row["text_ids_verified"] = keys.Count;
                        row["dynamic_objection_calls"] = SwitchStoryContentBuilder.VerifyDynamicObjectionCalls(script, expected, recipe);
                    }
                    else if (handle.Result is Texture2D texture)
                    {
                        JObject leaf = ((JArray)plan["leaves"]).OfType<JObject>().Single(item => (string)item["target"] == target);
                        Require(texture.width == (int)leaf["size"][0] && texture.height == (int)leaf["size"][1], "Built CG dimensions changed: " + key);
                        row["width"] = texture.width; row["height"] = texture.height;
                    }
                    else if (handle.Result is AudioClip audio)
                    {
                        JObject leaf = ((JArray)plan["leaves"]).OfType<JObject>().Single(item => (string)item["target"] == target);
                        Require(audio.samples == (int)leaf["samples"] && audio.channels == (int)leaf["channels"] &&
                            audio.frequency == (int)leaf["frequency"] && Math.Abs(audio.length - (double)leaf["length"]) < .01,
                            "Built voice format or duration changed: " + key);
                        row["samples"] = audio.samples; row["channels"] = audio.channels;
                        row["frequency"] = audio.frequency; row["seconds"] = audio.length;
                    }
                    row["status"] = "PASS";
                    return row;
                }
                finally { if (handle.IsValid()) Addressables.Release(handle); }
            }
            finally { if (locations.IsValid()) Addressables.Release(locations); }
        }

        private static JObject CheckBuiltSpriteGeometry(GameObject prefab, JObject job, JObject plan)
        {
            // Read the Sprite from the loaded bundle's component, never from the
            // AssetDatabase. The validated recipe supplies both geometry and the
            // source component -> asset references; sprite names are not locators.
            const float tolerance = .00002f;
            var leaves = ((JArray)plan["leaves"]).OfType<JObject>()
                .Where(leaf => leaf["sprite_geometry"] is JObject)
                .ToDictionary(leaf => (string)leaf["target"], StringComparer.Ordinal);
            var references = ((JObject)job["references"]).Properties()
                .Where(reference => leaves.ContainsKey((string)reference.Value)).ToArray();
            Require(leaves.Count == 3 && references.Length == 4 &&
                references.Select(reference => (string)reference.Value).Distinct(StringComparer.Ordinal).Count() == 3,
                "Built W geometry must cover the three reviewed leaves and all four source component references.");
            string repo = Path.GetFullPath(Path.Combine(Application.dataPath, "../.."));
            var rows = ((JArray)job["objects"]).OfType<JObject>().ToDictionary(row => (string)row["pid"]);
            var source = rows.Where(pair => new[] { "GameObject", "Transform", "RectTransform", "SpriteRenderer", "SpriteMask" }
                    .Contains((string)pair.Value["type"]))
                .ToDictionary(pair => pair.Key, pair => JObject.Parse(File.ReadAllText(
                    SwitchTitleContentBuilder.ResolveBelow(repo, (string)pair.Value["dump"]))));
            var transforms = source.Where(pair => pair.Value["m_Father"] != null)
                .ToDictionary(pair => (string)pair.Value["m_GameObject"]["m_PathID"], pair => pair.Value);
            var checkedSprites = new System.Collections.Generic.Dictionary<string, Sprite>(StringComparer.Ordinal);
            var cases = new JArray();
            foreach (var reference in references.OrderBy(item => item.Name, StringComparer.Ordinal))
            {
                string[] pointer = reference.Name.Split(':');
                Require(pointer.Length == 3 && rows.ContainsKey(pointer[0]) && source.ContainsKey(pointer[0]),
                    "Unknown source geometry reference: " + reference.Name);
                string componentType = (string)rows[pointer[0]]["type"];
                Require(componentType == "SpriteRenderer" || componentType == "SpriteMask",
                    "Unexpected geometry component: " + reference.Name + " / " + componentType);
                JObject data = source[pointer[0]];
                JObject leaf = leaves[(string)reference.Value];
                JObject geometry = (JObject)leaf["sprite_geometry"];
                Require((int)geometry["version"] == 1 && (string)geometry["coordinate_space"] == "sprite_local_units" &&
                    (string)data["m_Sprite"]["m_FileID"] == pointer[1] && (string)data["m_Sprite"]["m_PathID"] == pointer[2] &&
                    (string)geometry["source_path_id"] == pointer[2], "Geometry source pointer disagrees with recipe: " + reference.Name);
                var names = new System.Collections.Generic.List<string>();
                var visited = new System.Collections.Generic.HashSet<string>();
                string gameObjectId = (string)data["m_GameObject"]["m_PathID"];
                while (true)
                {
                    Require(visited.Add(gameObjectId) && source.ContainsKey(gameObjectId) && transforms.ContainsKey(gameObjectId),
                        "Invalid source geometry hierarchy: " + reference.Name);
                    string name = (string)source[gameObjectId]["m_Name"];
                    string parent = (string)transforms[gameObjectId]["m_Father"]["m_PathID"];
                    if (parent == "0")
                    {
                        Require(name == prefab.name, "Geometry source root differs from loaded bundle prefab.");
                        break;
                    }
                    names.Add(name);
                    Require(source.ContainsKey(parent), "Missing source geometry parent: " + parent);
                    gameObjectId = (string)source[parent]["m_GameObject"]["m_PathID"];
                }
                names.Reverse();
                Transform node = prefab.transform;
                foreach (string name in names)
                {
                    var children = Enumerable.Range(0, node.childCount).Select(index => node.GetChild(index))
                        .Where(child => child.name == name).ToArray();
                    Require(children.Length == 1, "Missing or ambiguous bundle geometry hierarchy: " + string.Join("/", names));
                    node = children[0];
                }
                Sprite actual;
                if (componentType == "SpriteRenderer")
                {
                    var components = node.GetComponents<SpriteRenderer>();
                    Require(components.Length == 1, "Bundle geometry SpriteRenderer slot changed: " + reference.Name);
                    actual = components[0].sprite;
                }
                else
                {
                    var components = node.GetComponents<SpriteMask>();
                    Require(components.Length == 1, "Bundle geometry SpriteMask slot changed: " + reference.Name);
                    actual = components[0].sprite;
                }
                Require(actual, "Bundle geometry Sprite is missing: " + reference.Name);
                if (checkedSprites.TryGetValue(pointer[2], out Sprite previous))
                    Require(previous == actual, "One source geometry sprite became different bundle sprites: " + pointer[2]);
                else checkedSprites.Add(pointer[2], actual);
                var expectedVertices = (JArray)geometry["vertices"];
                var expectedIndices = (JArray)geometry["indices"];
                Vector2[] vertices = actual.vertices;
                ushort[] indices = actual.triangles;
                Require(vertices.Length == expectedVertices.Count && indices.Length == expectedIndices.Count,
                    "Bundle geometry count changed: " + reference.Name + " expected " + expectedVertices.Count + "/" +
                    expectedIndices.Count + " vertices/indices, got " + vertices.Length + "/" + indices.Length);
                float maxError = 0;
                for (int i = 0; i < vertices.Length; i++)
                {
                    float x = (float)expectedVertices[i][0], y = (float)expectedVertices[i][1];
                    float error = Mathf.Max(Mathf.Abs(vertices[i].x - x), Mathf.Abs(vertices[i].y - y));
                    Require(!float.IsNaN(error) && !float.IsInfinity(error) && error <= tolerance,
                        "Bundle geometry vertex changed: " + reference.Name + " index " + i +
                        " expected (" + x + "," + y + "), got (" + vertices[i].x + "," + vertices[i].y + ")");
                    maxError = Mathf.Max(maxError, error);
                }
                for (int i = 0; i < indices.Length; i++)
                    Require(indices[i] == (int)expectedIndices[i], "Bundle geometry triangle index changed: " +
                        reference.Name + " index " + i + " expected " + expectedIndices[i] + ", got " + indices[i]);
                cases.Add(new JObject { ["reference"] = reference.Name, ["target"] = (string)reference.Value,
                    ["source_path_id"] = pointer[2], ["component_type"] = componentType, ["path"] = string.Join("/", names),
                    ["vertices"] = vertices.Length, ["indices"] = indices.Length, ["triangles"] = indices.Length / 3,
                    ["vertex_max_abs_error"] = maxError, ["vertex_tolerance"] = tolerance, ["indices_exact"] = true,
                    ["status"] = "PASS" });
            }
            Require(checkedSprites.Count == 3 && checkedSprites.Values.Distinct().Count() == 3,
                "Built W must retain three distinct reviewed geometry sprites.");
            return new JObject { ["status"] = "PASS", ["unique_sprites"] = checkedSprites.Count,
                ["component_references"] = cases.Count, ["unique_vertices"] = checkedSprites.Values.Sum(sprite => sprite.vertices.Length),
                ["unique_indices"] = checkedSprites.Values.Sum(sprite => sprite.triangles.Length),
                ["cases"] = cases, ["scope"] = "Actual bundle-loaded Sprite vertices and triangle indices versus validated source geometry; no AssetDatabase sprite substitution." };
        }

        private static async Task<JObject> CheckObjectionAddress(string key)
        {
            var locations = Addressables.LoadResourceLocationsAsync(key, typeof(GameObject));
            try
            {
                await locations.Task;
                Require(locations.Status == AsyncOperationStatus.Succeeded && locations.Result.Count == 1,
                    "Dynamic objection address must resolve exactly once: " + key);
                var location = locations.Result[0];
                Require(location.ProviderId.IndexOf("BundledAssetProvider", StringComparison.Ordinal) >= 0,
                    "Dynamic objection requires a built bundle: " + key);
                var row = new JObject { ["address"] = key, ["provider"] = location.ProviderId, ["internal_id"] = location.InternalId };
                if (key == "Naninovel/Spawn/ObjectionCutIn_w")
                {
                    var loaded = Addressables.LoadAssetAsync<GameObject>(location);
                    try
                    {
                        await loaded.Task;
                        Require(loaded.Status == AsyncOperationStatus.Succeeded && loaded.Result, "Double-objection bundle load failed.");
                        row["spawn_parameters"] = CheckDoubleObjectionParameters(loaded.Result);
                    }
                    finally { if (loaded.IsValid()) Addressables.Release(loaded); }
                }
                row["status"] = "PASS";
                return row;
            }
            finally { if (locations.IsValid()) Addressables.Release(locations); }
        }

        private static JObject CheckDoubleObjectionParameters(GameObject prefab)
        {
            Isolated();
            var inactiveParent = new GameObject("SwitchStoryInactiveParameterProbe") { hideFlags = HideFlags.HideAndDontSave };
            inactiveParent.SetActive(false);
            try
            {
                // SpawnableWithAudio.Awake requires the live audio service. Parenting at
                // instantiation keeps Awake uncalled while exercising the real method.
                GameObject clone = Object.Instantiate(prefab, inactiveParent.transform, false);
                Require(!clone.activeInHierarchy, "Parameter probe must remain inactive.");
                var component = clone.GetComponentsInChildren<MonoBehaviour>(true)
                    .Single(item => item.GetType().FullName == "WitchTrials.Views.ObjectionCutIn");
                var serialized = new SerializedObject(component);
                var library = serialized.FindProperty("_objectLibrary");
                Require(library != null && library.arraySize == 1 &&
                    library.GetArrayElementAtIndex(0).FindPropertyRelative("Objects").arraySize == 0,
                    "Source double-objection library must remain one empty activation entry.");
                var emaRule = component.GetType().GetProperty("IsEmaCutIn", BindingFlags.Instance | BindingFlags.NonPublic);
                var currentIndex = component.GetType().GetField("_currentIndex", BindingFlags.Instance | BindingFlags.NonPublic);
                Require(emaRule != null && currentIndex != null && !(bool)emaRule.GetValue(component),
                    "Double objection must not enter the single-Ema portrait override.");
                ((Spawn.IParameterized)component).SetSpawnParameters(new[] { "0" }, false);
                Require((int)currentIndex.GetValue(component) == 0, "Double-objection parameter selected the wrong activation index.");
                var portraits = new JArray();
                foreach (string path in new[] { "Ema_Container/Ema/RefuteCutIn_Ema_003", "Hiro_Container/Hiro/RefuteCutIn_Hiro_003" })
                {
                    Transform portrait = clone.transform.Find(path);
                    Require(portrait && portrait.GetComponent<SpriteRenderer>()?.sprite, "Double-objection portrait is missing: " + path);
                    for (Transform item = portrait; item != inactiveParent.transform; item = item.parent)
                        Require(item && item.gameObject.activeSelf, "Double-objection source hierarchy deactivated a portrait: " + path);
                    portraits.Add(new JObject { ["path"] = path, ["active_relative_to_prefab_root"] = true });
                }
                Isolated();
                return new JObject { ["input_index"] = 0, ["actual_index"] = 0, ["is_ema_cut_in"] = false,
                    ["activation_library_entries"] = 1, ["selected_entry_objects"] = 0, ["portraits"] = portraits,
                    ["awake_executed"] = false, ["await_spawn_exercised"] = false,
                    ["scope"] = "SetSpawnParameters ran on an inactive bundle clone. Both source portrait hierarchies remain locally active; AwaitSpawn needs the game's audio service and is not exercised." };
            }
            finally { Object.DestroyImmediate(inactiveParent); }
        }

        private static void Isolated()
        {
            Require(!Configuration.GetOrDefault<EngineConfiguration>().InitializeOnApplicationLoad &&
                !Engine.Initialized && !Engine.Initializing && Engine.Behaviour == null, "Naninovel must remain uninitialized; automatic initialization is not allowed.");
        }

        private static void Restore()
        {
            int previous = SessionState.GetInt(Key + "builder", -1);
            if (previous < 0) return;
            var settings = AddressableAssetSettingsDefaultObject.GetSettings(false);
            Require(settings && previous < settings.DataBuilders.Count, "Cannot restore the previous Play Mode builder.");
            settings.ActivePlayModeDataBuilderIndex = previous;
            EditorUtility.SetDirty(settings); AssetDatabase.SaveAssetIfDirty(settings);
            SessionState.SetInt(Key + "builder", -1);
        }

        private static void Finish(JObject report)
        {
            if (SessionState.GetBool(Key + "done", false)) return;
            Write(report); SessionState.SetBool(Key + "done", true); EditorApplication.ExitPlaymode();
        }
        private static JObject Fail(Exception error) => new JObject { ["status"] = "FAIL", ["error"] = error.ToString(), ["builder_restored"] = false };
        private static void Write(JObject report) => File.WriteAllText(SessionState.GetString(Key + "report", ""), report.ToString());
        private static void Require(bool condition, string message) { if (!condition) throw new InvalidOperationException(message); }
    }
}
