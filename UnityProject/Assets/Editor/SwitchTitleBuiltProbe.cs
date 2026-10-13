// Isolated built-content check: never boot the game or execute a scenario.
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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
    public static class SwitchTitleBuiltProbe
    {
        private const string Key = "Manosaba.SwitchTitleBuiltProbe.";
        private const string EffectType = "WitchTrials.Views.TitleScreenEffect";
        private static readonly string[] Clips = { "EmaLight", "HiroLight", "ED3" };
        // Counts from the three source prefab object graphs in the Switch title
        // import plan: stage 2 uses SpriteRenderer light layers, not particles.
        private static readonly int[] ExpectedParticleCounts = { 1, 0, 2 };

        [MenuItem("Manosaba/Verify Switch Title Built Content (Empty Play Mode)")]
        public static void Run()
        {
            // Do not overwrite a running probe's report or restore its builder early.
            if (SessionState.GetBool(Key + "active", false))
                throw new InvalidOperationException("A title probe is already running.");
            try
            {
                string repo = Path.GetFullPath(Path.Combine(Application.dataPath, "../.."));
                string directory = Path.Combine(repo, "tools/unity-rebuild/out/unity/switch-title-built",
                    DateTime.UtcNow.ToString("yyyyMMdd-HHmmss-fff"));
                Directory.CreateDirectory(directory);
                SessionState.SetString(Key + "report", Path.Combine(directory, "report.json"));
                SessionState.SetInt(Key + "builder", -1);
                Require(!EditorApplication.isPlayingOrWillChangePlaymode, "Stop Play Mode before running this probe.");
                Require(!EditorApplication.isCompiling, "Wait for compilation before running this probe.");
                Require(EditorSceneManager.playModeStartScene == null, "A Play Mode start scene could boot the game; clear it first.");
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    Scene scene = SceneManager.GetSceneAt(i);
                    Require(scene.isLoaded && !scene.isDirty && scene.rootCount == 0,
                        "Only clean, empty scenes are accepted; this probe does not close user scenes.");
                }
                RequireIsolatedEngine();
                var settings = AddressableAssetSettingsDefaultObject.GetSettings(false);
                Require(settings != null, "Addressables settings are missing.");
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
                SessionState.SetFloat(Key + "deadline", (float)EditorApplication.timeSinceStartup + 180f);
                Hook();
                EditorApplication.EnterPlaymode();
            }
            catch (Exception error)
            {
                Write(Failure(error));
                RestoreBuilder();
                SessionState.SetBool(Key + "active", false);
                Debug.LogError("[SwitchTitleBuiltProbe] " + error);
                if (Application.isBatchMode) EditorApplication.Exit(1);
            }
        }

        [InitializeOnLoadMethod]
        private static void Resume()
        {
            if (!SessionState.GetBool(Key + "active", false)) return;
            if (!SessionState.GetBool(Key + "done", false)) SessionState.SetBool(Key + "started", false);
            Hook();
        }

        private static void Hook()
        {
            EditorApplication.update -= Tick;
            EditorApplication.update += Tick;
        }

        private static void Tick()
        {
            if (!SessionState.GetBool(Key + "active", false)) { EditorApplication.update -= Tick; return; }
            if (SessionState.GetBool(Key + "done", false))
            {
                if (EditorApplication.isPlayingOrWillChangePlaymode) return;
                try
                {
                    RestoreBuilder();
                    JObject report = JObject.Parse(File.ReadAllText(SessionState.GetString(Key + "report", "")));
                    report["builder_restored"] = true;
                    Write(report);
                    SessionState.SetBool(Key + "active", false);
                    Debug.Log("[SwitchTitleBuiltProbe] " + report["status"] + " " + SessionState.GetString(Key + "report", ""));
                    if (Application.isBatchMode) EditorApplication.Exit((string)report["status"] == "PASS" ? 0 : 1);
                }
                catch (Exception error)
                {
                    Write(Failure(error));
                    SessionState.SetBool(Key + "active", false);
                    Debug.LogError("[SwitchTitleBuiltProbe] restoration failed: " + error);
                    if (Application.isBatchMode) EditorApplication.Exit(1);
                }
                return;
            }
            if (EditorApplication.timeSinceStartup > SessionState.GetFloat(Key + "deadline", 0))
            { Finish(Failure(new TimeoutException("Built-title probe exceeded 180 seconds."))); return; }
            if (!EditorApplication.isPlaying || EditorApplication.isCompiling || SessionState.GetBool(Key + "started", false)) return;
            SessionState.SetBool(Key + "started", true);
            CheckRuntime();
        }

        private static async void CheckRuntime()
        {
            var result = new JObject
            {
                ["status"] = "RUNNING", ["generated_utc"] = DateTime.UtcNow.ToString("o"),
                ["play_mode"] = true, ["boot_executed"] = false, ["scenario_executed"] = false,
                ["visual_render_verified"] = false, ["builder_restored"] = false, ["cases"] = new JArray()
            };
            try
            {
                RequireIsolatedEngine();
                var initialized = Addressables.InitializeAsync(false);
                try
                {
                    await initialized.Task;
                    Require(initialized.Status == AsyncOperationStatus.Succeeded, "Addressables initialization failed.");
                }
                finally { Addressables.Release(initialized); }
                var cases = (JArray)result["cases"];
                for (int stage = 1; stage <= 3; stage++)
                {
                    int selected = stage;
                    cases.Add(await Load<GameObject>("Naninovel/Spawn/TitleScreenEffect_" + stage,
                        asset => CheckEffect(asset, selected)));
                }
                cases.Add(await Load<Texture2D>("Naninovel/Backgrounds/Stills/3_1", texture =>
                {
                    Require(texture.width == 2048 && texture.height == 1024, "Stills/3_1 dimensions changed.");
                    return Task.FromResult(new JObject { ["name"] = texture.name, ["width"] = texture.width, ["height"] = texture.height });
                }));
                cases.Add(await Load<Script>("Naninovel/Scripts/System/System_Title", script => Task.FromResult(CheckScenario(script))));
                RequireIsolatedEngine();
                result["engine_initialized"] = Engine.Initialized;
                result["status"] = "PASS";
            }
            catch (Exception error)
            {
                result["status"] = "FAIL";
                result["error"] = error.ToString();
                Debug.LogError("[SwitchTitleBuiltProbe] " + error);
            }
            Finish(result);
        }

        private static async Task<JObject> Load<T>(string key, Func<T, Task<JObject>> check) where T : Object
        {
            var locations = Addressables.LoadResourceLocationsAsync(key, typeof(T));
            try
            {
                await locations.Task;
                Require(locations.Status == AsyncOperationStatus.Succeeded && locations.Result.Count == 1,
                    "Expected one built location for " + key);
                var location = locations.Result[0];
                Require(location.ProviderId.IndexOf("BundledAssetProvider", StringComparison.Ordinal) >= 0,
                    "Built bundles are required; provider was " + location.ProviderId);
                var handle = Addressables.LoadAssetAsync<T>(location);
                try
                {
                    await handle.Task;
                    Require(handle.Status == AsyncOperationStatus.Succeeded && handle.Result, "Could not load " + key);
                    JObject row = await check(handle.Result);
                    row["address"] = key;
                    row["provider"] = location.ProviderId;
                    row["internal_id"] = location.InternalId;
                    row["status"] = "PASS";
                    return row;
                }
                finally { if (handle.IsValid()) Addressables.Release(handle); }
            }
            finally { if (locations.IsValid()) Addressables.Release(locations); }
        }

        private static async Task<JObject> CheckEffect(GameObject prefab, int stage)
        {
            var transforms = prefab.GetComponentsInChildren<Transform>(true);
            Require(transforms.Sum(t => GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(t.gameObject)) == 0,
                prefab.name + " has missing scripts.");
            var behaviours = prefab.GetComponentsInChildren<MonoBehaviour>(true);
            Require(behaviours.Length == 1 && behaviours[0].GetType().FullName == EffectType && behaviours[0] is Spawn.IAwaitable,
                "Only TitleScreenEffect may run in this isolated probe.");
            var effect = behaviours[0];
            var root = new SerializedObject(effect).FindProperty("_rootTransform")?.objectReferenceValue as Transform;
            Require(root && root.IsChildOf(prefab.transform), "Title effect root reference is missing.");
            var animators = prefab.GetComponentsInChildren<Animator>(true);
            Require(animators.Length == 1 && animators[0].runtimeAnimatorController, "Title controller is missing.");
            var clips = animators[0].runtimeAnimatorController.animationClips;
            Require(clips.Any(clip => clip && clip.name == Clips[stage - 1] && clip.length > 5f), "Expected title clip is missing.");
            var particles = prefab.GetComponentsInChildren<ParticleSystem>(true);
            int expectedParticles = ExpectedParticleCounts[stage - 1];
            Require(particles.Length == expectedParticles,
                prefab.name + " expected " + expectedParticles + " particles, found " + particles.Length + ".");
            var bindings = new JArray();
            foreach (var particle in particles)
            {
                var sheet = particle.textureSheetAnimation;
                var renderer = particle.GetComponent<ParticleSystemRenderer>();
                Require(sheet.enabled && sheet.mode == ParticleSystemAnimationMode.Sprites && sheet.spriteCount == 1,
                    particle.name + " has no single Sprite animation binding.");
                Sprite sprite = sheet.GetSprite(0);
                Require(sprite && sprite.texture && renderer && renderer.sharedMaterials.Length == 1,
                    particle.name + " has missing Sprite/material references.");
                Material material = renderer.sharedMaterial;
                Require(material && material.shader && material.HasProperty("_MainTex") &&
                    material.GetTexture("_MainTex") == sprite.texture, particle.name + " has the wrong particle texture.");
                bindings.Add(new JObject { ["particle"] = particle.name, ["sprite"] = sprite.name,
                    ["texture"] = sprite.texture.name, ["material"] = material.name, ["shader"] = material.shader.name });
            }
            foreach (var renderer in prefab.GetComponentsInChildren<SpriteRenderer>(true))
                Require(renderer.sprite && renderer.sharedMaterial && renderer.sharedMaterial.shader,
                    renderer.name + " has missing light/fog references.");
            var row = new JObject { ["name"] = prefab.name, ["controller"] = animators[0].runtimeAnimatorController.name,
                ["clips"] = new JArray(clips.Select(clip => new JObject { ["name"] = clip.name, ["length"] = clip.length })),
                ["particle_bindings"] = bindings, ["particle_count"] = particles.Length,
                ["expected_particle_count"] = expectedParticles, ["await_spawn_verified"] = false };
            if (stage == 1) row["entrance"] = await CheckEntrance(prefab);
            else row["await_spawn_scope"] = "Shared component behavior tested on stage 1 only.";
            row["await_spawn_verified"] = stage == 1;
            return row;
        }

        private static async Task<JObject> CheckEntrance(GameObject prefab)
        {
            Require(Engine.Time is UnityTime && Mathf.Approximately(UnityEngine.Time.timeScale, 1f),
                "Entrance check requires the default UnityTime at time scale 1.");
            GameObject instance = Object.Instantiate(prefab);
            try
            {
                var effect = instance.GetComponentsInChildren<MonoBehaviour>(true).Single(behaviour => behaviour.GetType().FullName == EffectType);
                Transform root = (Transform)new SerializedObject(effect).FindProperty("_rootTransform").objectReferenceValue;
                Vector3 baseline = root.localScale;
                async Task AwaitEntrance() { await ((Spawn.IAwaitable)effect).AwaitSpawn(); }
                float started = UnityEngine.Time.time;
                float initialDelta = Engine.Time.DeltaTime;
                Task first = AwaitEntrance();
                float firstRatio = root.localScale.x / baseline.x;
                // The tween performs one real deltaTime step before its first yield.
                Require(firstRatio > 1.03f && firstRatio <= 1.05001f, "Entrance did not start near 1.05 scale.");
                await Within(first, 8000);
                Require(Vector3.Distance(root.localScale, baseline) < 0.0001f, "Entrance did not restore default scale.");
                float duration = UnityEngine.Time.time - started;
                Require(duration + initialDelta >= 2.69f && duration + initialDelta < 3.2f,
                    "Entrance duration is not approximately 2.7 seconds including its immediate first step.");
                Task replaced = AwaitEntrance();
                await Task.Delay(100);
                Task replacement = AwaitEntrance();
                await Within(replaced, 2000);
                Require(root.localScale.x > baseline.x, "Old spawn completion reset its replacement.");
                await Within(replacement, 8000);
                Require(Vector3.Distance(root.localScale, baseline) < 0.0001f, "Re-entry did not restore default scale.");
                Task disabled = AwaitEntrance();
                instance.SetActive(false);
                Require(Vector3.Distance(root.localScale, baseline) < 0.0001f, "Disabling did not restore default scale.");
                await Within(disabled, 2000);
                RequireIsolatedEngine();
                return new JObject { ["status"] = "PASS", ["first_observed_scale_ratio"] = firstRatio,
                    ["elapsed_game_seconds"] = duration, ["initial_delta_seconds"] = initialDelta,
                    ["completed"] = true, ["reentry"] = true, ["disable_restored"] = true,
                    ["time_source"] = "Default Engine.Time (UnityTime); no engine initialization or frame-loop substitution." };
            }
            finally { if (instance) Object.DestroyImmediate(instance); }
        }

        private static JObject CheckScenario(Script script)
        {
            Require(script.Path == "System/System_Title" && script.Playlist?.Count == 36, "Built title script is stale or incomplete.");
            Require(script.Playlist.All(command => command != null), "Title playlist contains null commands.");
            var commands = script.Playlist.ToArray();
            int start = Array.FindIndex(commands, command => command is SetCustomVariable set &&
                set.Expression?.ToString().Contains("titleStillId") == true);
            Require(start >= 0 && start + 8 <= commands.Length, "Title selection commands are missing.");
            var selection = commands.Skip(start).Take(8).ToArray();
            Require(selection.Select(command => command.Indent).SequenceEqual(new[] { 0, 0, 0, 1, 1, 0, 1, 1 }),
                "Title branch indentation is invalid.");
            var rows = new JArray();
            foreach (int progress in new[] { 0, 5, 6, 11, 12 })
            {
                string still = null, effect = null;
                bool include = true;
                foreach (Command command in selection)
                {
                    if (command is BeginIf branch)
                    {
                        var match = Regex.Match(branch.Expression.Value, @"^\s*g_gameProgress\s*>\s*(5|11)\s*$");
                        Require(match.Success, "Unexpected title progress condition.");
                        include = progress > int.Parse(match.Groups[1].Value);
                    }
                    else
                    {
                        Require(command is SetCustomVariable, "Unexpected command inside title selection.");
                        var set = (SetCustomVariable)command;
                        var match = Regex.Match(set.Expression.Value, "^\\s*(titleStillId|titleScreenEffectId)\\s*=\\s*\"([^\"]+)\"\\s*$");
                        Require(match.Success, "Unexpected title assignment.");
                        if (command.Indent > 0 && !include) continue;
                        if (match.Groups[1].Value == "titleStillId") still = match.Groups[2].Value;
                        else effect = match.Groups[2].Value;
                    }
                }
                int expected = progress > 11 ? 3 : progress > 5 ? 2 : 1;
                Require(still == expected + "_1" && effect == "TitleScreenEffect_" + expected, "Title selection is incorrect.");
                rows.Add(new JObject { ["progress"] = progress, ["still"] = still, ["effect"] = effect });
            }
            Require(commands.Count(command => command.GetType().Name == "SpawnExtended") == 1 &&
                commands.Count(command => command.GetType().Name == "DestroySpawnedExtended") == 1, "Effect spawn/cleanup commands are missing.");
            return new JObject { ["path"] = script.Path, ["commands"] = script.Playlist.Count,
                ["branch_checks"] = rows, ["branch_check_mode"] = "Read built runtime commands; inspect selection without executing scenario or accessing saves." };
        }

        private static async Task Within(Task task, int milliseconds)
        {
            if (await Task.WhenAny(task, Task.Delay(milliseconds)) != task)
                throw new TimeoutException("Real Play Mode animation did not complete; no substitute frame loop was used.");
            await task;
        }

        private static void RequireIsolatedEngine()
        {
            Require(!Configuration.GetOrDefault<EngineConfiguration>().InitializeOnApplicationLoad,
                "Disable engine automatic initialization before this probe; configuration is not changed by the probe.");
            Require(!Engine.Initialized && !Engine.Initializing && Engine.Behaviour == null,
                "The game engine must remain uninitialized during this probe.");
        }

        private static void RestoreBuilder()
        {
            int index = SessionState.GetInt(Key + "builder", -1);
            if (index < 0) return;
            var settings = AddressableAssetSettingsDefaultObject.GetSettings(false);
            Require(settings && index < settings.DataBuilders.Count, "Could not restore previous Play Mode builder.");
            settings.ActivePlayModeDataBuilderIndex = index;
            EditorUtility.SetDirty(settings);
            AssetDatabase.SaveAssetIfDirty(settings);
            SessionState.SetInt(Key + "builder", -1);
        }

        private static void Finish(JObject result)
        {
            if (SessionState.GetBool(Key + "done", false)) return;
            Write(result);
            SessionState.SetBool(Key + "done", true);
            EditorApplication.ExitPlaymode();
        }

        private static JObject Failure(Exception error) => new JObject
        { ["status"] = "FAIL", ["generated_utc"] = DateTime.UtcNow.ToString("o"), ["error"] = error.ToString(), ["builder_restored"] = false };

        private static void Write(JObject result)
        {
            string path = SessionState.GetString(Key + "report", "");
            if (!string.IsNullOrEmpty(path)) File.WriteAllText(path, result.ToString());
        }

        private static void Require(bool condition, string message)
        { if (!condition) throw new InvalidOperationException(message); }
    }
}
