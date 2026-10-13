// Built-content enhancement settings probe; all save handlers are in memory.
// Shared Boot/presenter setup follows the independently verified epilogue probe.
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Naninovel;
using Naninovel.Commands;
using Naninovel.UI;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using AudioSettings = UnityEngine.AudioSettings;
using Object = UnityEngine.Object;

using UnityEngine.Playables;
namespace Manosaba.EditorTools
{
    public static class EnhancementSettingsProbe
    {
        private const string Key = "Manosaba.EnhancementSettingsProbe.";
        private const string GamePrefab = "Assets/GameContent/#WitchTrials/Prefabs/General/Core/Game.prefab";
        private const string EpiloguePath = "System/System_FireworksEpilogue";
        private const string SettingsType = "WitchTrials.Models.EnhancementSettings";
        private static string Repo => Path.GetFullPath(Path.Combine(Application.dataPath, "../.."));
        private static string RequestPath => Path.Combine(Repo, "tools/unity-rebuild/out/enhancement-settings-probe-request.json");
        private static string RequestResultPath => Path.Combine(Repo, "tools/unity-rebuild/out/enhancement-settings-probe-request-result.json");
        private static double nextRequestPoll;
        [Serializable] private sealed class SavedScene
        {
            public string path;
            public bool isLoaded, isActive;
        }
        [Serializable] private sealed class SavedEditorScenes
        {
            public SavedScene[] scenes;
            public string playModeStartScene;
        }
        private static JObject report;
        private static readonly List<AsyncOperationHandle> handles = new();
        private static IScriptPlayer player;
        private static IStateManager state;
        private static MonoBehaviour entrySequence;
        private static object preferences;
        private static FireworksEpilogueProbe.MemoryGame memory;
        private static MemorySettings settingsMemory;
        private static string callbackFailure, activeCase;
        [MenuItem("Manosaba/Verify Enhancement Settings (Isolated Saves, Built Content)")]
        public static void Run() => RunCore(null);

        [MenuItem("Manosaba/Verify Options Response Audio (Isolated Saves, Built Content)")]
        public static void RunOptionsAudio() => RunCore(null, true);

        private static void RunCore(string requestId, bool audioOnly = false)
        {
            if (SessionState.GetBool(Key + "active", false)) throw new InvalidOperationException("Enhancement settings probe is already active.");
            string output = Path.Combine(Repo, "tools/unity-rebuild/out/unity/enhancement-settings", DateTime.UtcNow.ToString("yyyyMMdd-HHmmss-fff"));
            Directory.CreateDirectory(output);
            SessionState.SetString(Key + "output", output);
            SessionState.SetString(Key + "request_id", requestId ?? "");
            SessionState.SetBool(Key + "audio_only", audioOnly);
            SessionState.SetInt(Key + "builder", -1);
            SessionState.SetBool(Key + "scene_opened", false);
            SessionState.SetBool(Key + "scene_state_captured", false);
            handles.Clear(); callbackFailure = null; activeCase = "starting"; report = null;
            try
            {
                var before = EditorSnapshot();
                SessionState.SetString(Key + "editor_before", before.ToString());
                Write(new JObject { ["status"] = "STARTING", ["editor_before"] = before });
                Require(!EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isCompiling && !EditorApplication.isUpdating,
                    "Stop Play Mode and finish compilation/importing first.");
                Require(SystemInfo.graphicsDeviceType != GraphicsDeviceType.Null, "This probe requires graphics; omit -nographics.");
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    var scene = SceneManager.GetSceneAt(i);
                    Require(!scene.isDirty, "Save user scenes before the probe; dirty scenes are never closed: " + scene.path);
                    Require(!string.IsNullOrEmpty(scene.path) || (SceneManager.sceneCount == 1 && scene.isLoaded && scene.rootCount == 0),
                        "An unsaved scene must be the sole clean empty scene; otherwise save it first.");
                    if (!string.IsNullOrEmpty(scene.path)) Require(File.Exists(Path.Combine(Path.GetFullPath(Path.Combine(Application.dataPath, "..")), scene.path)),
                        "A user scene cannot be restored from disk: " + scene.path);
                }
                Require(!Engine.Initialized && !Engine.Initializing && !Configuration.GetOrDefault<EngineConfiguration>().InitializeOnApplicationLoad,
                    "Automatic Naninovel initialization would bypass save isolation.");
                JObject plan = FireworksEpilogueContentBuilder.ReadValidatedPlan();
                SessionState.SetString(Key + "signature", (string)plan["signature"]);
                SessionState.SetString(Key + "game_guid", AssetDatabase.AssetPathToGUID(GamePrefab));
                var settings = AddressableAssetSettingsDefaultObject.GetSettings(false);
                Require(settings, "Addressables settings are missing.");
                int existing = settings.DataBuilders.FindIndex(x => x is IDataBuilder b && b.Name.IndexOf("Existing Build", StringComparison.OrdinalIgnoreCase) >= 0);
                Require(existing >= 0, "Existing Build mode is missing.");
                CaptureEditorScenes();
                SessionState.SetInt(Key + "builder", settings.ActivePlayModeDataBuilderIndex);
                settings.ActivePlayModeDataBuilderIndex = existing;
                EditorUtility.SetDirty(settings); AssetDatabase.SaveAssetIfDirty(settings);

                // Opening a scene in Edit Mode does not execute ordinary Awake/Start.
                // Do not save these temporary overrides; all original scene bytes stay intact.
                EditorSceneManager.OpenScene("Assets/Scenes/Boot.unity", OpenSceneMode.Single);
                SessionState.SetBool(Key + "scene_opened", true);
                var components = SceneManager.GetActiveScene().GetRootGameObjects().SelectMany(x => x.GetComponentsInChildren<MonoBehaviour>(true)).Where(x => x).ToArray();
                int initializers = 0;
                foreach (var component in components)
                {
                    string type = component.GetType().FullName;
                    if (type == "WitchTrials.Views.GameInitializer") { component.enabled = false; initializers++; }
                    if (type == "Manosaba.Integration.ManosabaBoot") component.enabled = false;
                    // SplashScreen.Awake sets alpha=1 even when disabled. Its normal completed
                    // sequence deactivates this GO; apply that state only to the unsaved probe scene.
                    if (type == "WitchTrials.Views.SplashScreen") component.gameObject.SetActive(false);
                    // enabled=false cannot suppress Awake; remove only this temporary scene component.
                    if (component is RuntimeInitializer) Object.DestroyImmediate(component);
                }
                Require(initializers == 1, "Expected the reviewed single Boot GameInitializer.");
                SessionState.SetBool(Key + "active", true);
                SessionState.SetBool(Key + "started", false);
                SessionState.SetBool(Key + "done", false);
                SessionState.SetFloat(Key + "deadline", (float)EditorApplication.timeSinceStartup + 720f);
                PublishRequestResult("STARTING");
                Hook(); EditorApplication.EnterPlaymode();
            }
            catch (Exception error)
            {
                var failure = Failure(error);
                try { Restore(); failure["builder_and_scene_restored"] = true; failure["editor_after"] = EditorSnapshot(); }
                catch (Exception restoreError) { failure["restore_error"] = restoreError.ToString(); }
                Write(failure); PublishRequestResult("FAIL", failure);
                if (Application.isBatchMode) EditorApplication.Exit(1);
            }
        }

        [InitializeOnLoadMethod]
        private static void Resume()
        {
            EditorApplication.update -= PollRequest;
            EditorApplication.update += PollRequest;
            if (!SessionState.GetBool(Key + "active", false)) return;
            if (!SessionState.GetBool(Key + "done", false)) SessionState.SetBool(Key + "started", false);
            Hook();
        }

        // A fixed local request runs this one probe only; it never accepts paths or method names.
        private static void PollRequest()
        {
            if (Application.isBatchMode || SessionState.GetBool(Key + "active", false) || EditorApplication.timeSinceStartup < nextRequestPoll) return;
            nextRequestPoll = EditorApplication.timeSinceStartup + .5;
            if (!File.Exists(RequestPath)) return;
            string id = null;
            try
            {
                Require(new FileInfo(RequestPath).Length <= 4096, "Probe request is too large.");
                var request = JObject.Parse(File.ReadAllText(RequestPath));
                id = (string)request["id"];
                Require(request.Properties().All(p => p.Name == "id" || p.Name == "expiresUtc") && Guid.TryParseExact(id, "N", out _) &&
                    DateTime.TryParse((string)request["expiresUtc"], CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var expiry) &&
                    expiry.ToUniversalTime() > DateTime.UtcNow, "Probe request is invalid or expired.");
                if (EditorApplication.isPlayingOrWillChangePlaymode || EditorApplication.isCompiling || EditorApplication.isUpdating)
                {
                    File.WriteAllText(RequestResultPath, new JObject { ["id"] = id, ["status"] = "WAITING_FOR_IDLE_EDIT_MODE",
                        ["editor_before"] = EditorSnapshot() }.ToString());
                    return;
                }
                File.Delete(RequestPath);
                RunCore(id);
            }
            catch (Exception error)
            {
                if (File.Exists(RequestPath)) File.Delete(RequestPath);
                File.WriteAllText(RequestResultPath, new JObject { ["id"] = id, ["status"] = "FAIL", ["error"] = error.ToString(),
                    ["editor_before"] = EditorSnapshot() }.ToString());
                Debug.LogWarning("[EnhancementSettingsProbe] Request rejected: " + error.Message);
            }
        }
        private static void Hook() { EditorApplication.update -= Tick; EditorApplication.update += Tick; }
        private static void Tick()
        {
            if (!SessionState.GetBool(Key + "active", false)) { EditorApplication.update -= Tick; return; }
            if (SessionState.GetBool(Key + "done", false))
            {
                if (EditorApplication.isPlayingOrWillChangePlaymode) return;
                var saved = JObject.Parse(File.ReadAllText(Path.Combine(Output, "report.json")));
                try { Restore(); saved["builder_and_scene_restored"] = true; saved["editor_after"] = EditorSnapshot(); }
                catch (Exception error) { saved["status"] = "FAIL"; saved["restore_error"] = error.ToString(); }
                Write(saved);
                PublishRequestResult((string)saved["status"], saved);
                Debug.Log("[EnhancementSettingsProbe] " + saved["status"] + " " + Output);
                if (Application.isBatchMode) EditorApplication.Exit((string)saved["status"] == "PASS" ? 0 : 1);
                return;
            }
            if (EditorApplication.timeSinceStartup > SessionState.GetFloat(Key + "deadline", 0)) { Finish(Failure(new TimeoutException("Enhancement settings probe exceeded 720 seconds."))); return; }
            if (!EditorApplication.isPlaying || EditorApplication.isCompiling || SessionState.GetBool(Key + "started", false)) return;
            SessionState.SetBool(Key + "started", true); Execute();
        }


        private static async void Execute()
        {
            report = new JObject { ["status"] = "RUNNING", ["signature"] = SessionState.GetString(Key + "signature", ""),
                ["editor_before"] = JObject.Parse(SessionState.GetString(Key + "editor_before", "{}")),
                ["built_assets"] = new JArray(), ["screenshots"] = new JArray(), ["errors"] = new JArray(),
                ["portraits"] = new JArray(), ["ui_clicks"] = new JArray(), ["ending_routes"] = new JArray(),
                ["save_isolation"] = "Actual Boot and built Game; clone-on-read/write memory save handlers. Existing legacy PlayerPrefs can be read by OptionsUI, but this probe never sets them or clicks the legacy reset/portrait controls.",
                ["builder_and_scene_restored"] = false };
            Application.logMessageReceived += Log;
            object locale = null; GameObject game = null;
            try
            {
                Require(!Engine.Initialized && !Engine.Initializing, "Automatic startup bypassed memory save isolation.");
                var plan = FireworksEpilogueContentBuilder.ReadValidatedPlan();
                Require((string)plan["signature"] == (string)report["signature"], "Recipe changed during probe.");
                var init = Addressables.InitializeAsync(false); await init.Task; Addressables.Release(init);
                var ending = await Load<Script>("Naninovel/Scripts/" + FireworksEpilogueContentBuilder.EndingPath);
                await Load<Script>("Naninovel/Scripts/" + EpiloguePath);
                await Load<Texture2D>("Naninovel/Backgrounds/Stills/3_1");
                var prefab = await Load<GameObject>(SessionState.GetString(Key + "game_guid", ""));
                locale = Activator.CreateInstance(RuntimeType("WitchTrials.Presenters.DefaultLocalePresenter"));
                await AwaitForeign(Call(locale, "LoadAsync"));
                Require(!prefab.GetComponentsInChildren<RuntimeInitializer>(true).Any(), "Game prefab contains an automatic initializer.");
                game = Object.Instantiate(prefab);
                var provider = new ProjectConfigurationProvider();
                var config = provider.GetConfiguration<StateConfiguration>();
                config.GameStateHandler = typeof(FireworksEpilogueProbe.MemoryGame).AssemblyQualifiedName;
                config.GlobalStateHandler = typeof(FireworksEpilogueProbe.MemoryGlobal).AssemblyQualifiedName;
                config.SettingsStateHandler = typeof(MemorySettings).AssemblyQualifiedName;
                provider.GetConfiguration<EngineConfiguration>().ShowInitializationUI = false;
                provider.GetConfiguration<EngineConfiguration>().EnableBridging = false;
                provider.GetConfiguration<ScriptsConfiguration>().InitializationScript = null;
                provider.GetConfiguration<MoviesConfiguration>().PlayIntroMovie = false;
                await RuntimeInitializer.Initialize(provider);
                state = Engine.GetServiceOrErr<IStateManager>(); player = Engine.GetServiceOrErr<IScriptPlayer>();
                memory = state.GameSlotManager as FireworksEpilogueProbe.MemoryGame;
                settingsMemory = state.SettingsSlotManager as MemorySettings;
                Require(memory != null && settingsMemory != null && state.GlobalSlotManager is FireworksEpilogueProbe.MemoryGlobal,
                    "Memory save isolation failed.");
                preferences = Engine.Services.Single(x => x.GetType().FullName == SettingsType);
                Require(!Corrected && EpilogueEnabled, "Legacy settings without the new record did not load the default original/on values.");
                report["old_settings_defaults"] = SettingsSnapshot();
                entrySequence = game.GetComponentsInChildren<MonoBehaviour>(true).Single(x => x && x.GetType().FullName == "WitchTrials.Views.EntrySequence");
                await AwaitForeign(Call(entrySequence, "PlayInitialSequenceAsync"));
                await WaitTitle();
                await state.SaveGame("EnhancementProbeExisting");
                var slotHashes = memory.Hashes(); int writes = memory.Writes;
                report["memory_slots_before"] = slotHashes.DeepClone();
                if (SessionState.GetBool(Key + "audio_only", false)) await VerifyOptionsAudio();
                else
                {
                    VerifyBuilderFailurePreservesConfirmation();
                    await VerifyOptionsAndPersistence();
                    await VerifySpawnAndPendingRelease();
                    await VerifyPortraits();
                    await ReturnTitle();
                    await VerifyEnding(ending, false);
                    await VerifyEnding(ending, true);
                }
                Require(memory.Writes == writes && JToken.DeepEquals(slotHashes, memory.Hashes()), "Setting tests modified the pre-existing in-memory game slot.");
                report["memory_slots_after"] = memory.Hashes();
                report["settings_writes"] = settingsMemory.Writes;
                Require(((JArray)report["errors"]).Count == 0, "Runtime errors were logged.");
                report["scope"] = "Built content and real service/UI. Ending tests execute the reviewed final four commands and first stable epilogue page, not the full ending movie or all 42 pages. One real ObjectionCutIn.AwaitSpawn is executed; cancellation/disable checks report whether a pending load was observed, without preloading their corrected sprites. Other portrait comparisons use original built Timeline poses in isolated instances; screenshots require visual review. Clean saved editor scenes/start-scene are temporarily switched and then read back after restoration.";
                if (SessionState.GetBool(Key + "audio_only", false))
                    report["scope"] = "Built Game and real OptionsUi with production presentation, pointer raycasts, UI submit events and response sound routing. AudioSource clip identity, playback progression, RMS and mixer routing are observed; physical speakers and hardware controller input are not inferred. Game/global/settings saves stay in memory; editor scenes and builder are restored.";
                report["status"] = "PASS";
            }
            catch (Exception error) { report["status"] = "FAIL"; report["error"] = error.ToString(); report["failure_runtime"] = RuntimeSnapshot(); }
            finally
            {
                try
                {
                    if (Engine.Initialized || Engine.Initializing) Engine.Destroy();
                    if (game) Object.DestroyImmediate(game);
                    (locale as IDisposable)?.Dispose();
                }
                catch (Exception error) { report["status"] = "FAIL"; report["cleanup_error"] = error.ToString(); }
                foreach (var handle in handles) if (handle.IsValid()) Addressables.Release(handle);
                handles.Clear(); Application.logMessageReceived -= Log;
                Finish(report);
            }
        }

        [Serializable] public sealed class ExistingSetting { public string Marker = "preserve-unrelated-settings"; }
        public sealed class MemorySettings : FireworksEpilogueProbe.CountedMemory<SettingsStateMap>
        {
            public MemorySettings(StateConfiguration config, string directory)
            {
                // An old settings save deliberately has no EnhancementSettings record.
                SaveBlocking(config.DefaultSettingsSlotId, SettingsStateMap.With(new ExistingSetting()));
            }
        }
        private static bool Corrected => Get<bool>(preferences, "UseCorrectedCutInPortraits");
        private static bool EpilogueEnabled => Get<bool>(preferences, "FireworksEpilogueEnabled");
        private static bool SessionActive => (bool)RuntimeType("WitchTrials.Views.FireworksEpilogueSession").GetProperty("Active").GetValue(null);
        private static JObject SettingsSnapshot() => new JObject { ["corrected"] = Corrected, ["epilogue"] = EpilogueEnabled };
        private static void Stage(string name) { activeCase = name; report["stage"] = name; Write(report); }

        private static async Task VerifyOptionsAudio()
        {
            Stage("options_response_audio");
            var audio = Engine.GetServiceOrErr<IAudioManager>();
            var input = Options().GetType().GetProperty("InputManager", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(Options());
            var responseMethod = input.GetType().GetMethod("PlayResponseSfx");
            var responseKind = responseMethod.GetParameters()[0].ParameterType;
            var configured = (System.Collections.IDictionary)audio.GetType().GetField("_defaultSfxPaths", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(audio);
            var clips = new Dictionary<string, AudioClip>();
            foreach (string kind in new[] { "Cancel", "Submit", "Move", "Tab", "Alert" })
            {
                string path = (string)configured[Enum.Parse(responseKind, kind)];
                Require(!string.IsNullOrWhiteSpace(path), "Response sound mapping is missing: " + kind);
                clips[path] = await Load<AudioClip>("Naninovel/Audio/" + path);
            }
            string submitPath = (string)configured[Enum.Parse(responseKind, "Submit")];
            var rows = new JArray(); report["response_audio"] = rows;
            report["audio_environment"] = new JObject { ["manager_type"] = audio.GetType().FullName,
                ["input_type"] = input.GetType().FullName, ["master_volume"] = audio.MasterVolume,
                ["sfx_volume"] = audio.SfxVolume, ["listener_volume"] = AudioListener.volume,
                ["listener_paused"] = AudioListener.pause, ["active_listeners"] = Object.FindObjectsByType<AudioListener>(FindObjectsSortMode.None).Count(x => x.enabled) };
            Require(audio.MasterVolume > 0 && audio.SfxVolume > 0 && AudioListener.volume > 0 && !AudioListener.pause,
                "Audio fixture is muted; cannot diagnose response routing.");

            // The same imported clip played directly separates a silent asset/output from a missing UI route.
            await Sample("direct_submit_control", submitPath, async () => await audio.PlaySfx(submitPath));
            await OpenGraphics();
            foreach (string tabName in new[] { "Tab_Message", "Tab_Graphics", "Tab_Audio", "Tab_Message", "Tab_Graphics", "Tab_Audio" })
            {
                var tab = Options().GetComponentsInChildren<Toggle>(true).Single(x => x.name == tabName);
                bool before = tab.isOn; int activations = 0;
                void Changed(bool value) { if (value) activations++; }
                tab.onValueChanged.AddListener(Changed);
                try
                {
                    var row = await Sample("pointer_" + tabName, submitPath, () => Click(tab));
                    row["selected_before"] = before; row["selected_after"] = tab.isOn;
                    row["activation_events"] = activations;
                    Require(!before && tab.isOn && activations == 1, "Pointer did not actually switch one options category: " + tabName);
                }
                finally { tab.onValueChanged.RemoveListener(Changed); }
            }
            await CloseOptions(); await OpenGraphics();
            var audioTab = Options().GetComponentsInChildren<Toggle>(true).Single(x => x.name == "Tab_Audio");
            await Sample("submit_after_reopen", submitPath, async () =>
            {
                Require(!audioTab.isOn && audioTab.IsActive() && audioTab.IsInteractable(), "Submit target is not a different interactive category.");
                EventSystem.current.SetSelectedGameObject(audioTab.gameObject);
                ExecuteEvents.Execute(audioTab.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
                await AsyncUtils.WaitEndOfFrame(); Require(audioTab.isOn, "UI submit did not select the audio category.");
            });
            foreach (string kind in new[] { "Cancel", "Submit", "Move", "Tab", "Alert" })
            {
                object value = Enum.Parse(responseKind, kind);
                string path = (string)configured[value];
                await Sample("input_response_" + kind, path, () => { responseMethod.Invoke(input, new[] { value }); return Task.CompletedTask; });
            }
            await CloseOptions();
            var failures = rows.OfType<JObject>().Where(x => !(bool)x["playing_observed"] || !(bool)x["samples_advanced"] || (double)x["peak_rms"] <= .000001).ToArray();
            report["response_audio_failures"] = new JArray(failures.Select(x => (string)x["case"])); Write(report);
            Require(failures.Length == 0, "Response audio was not observed: " + string.Join(", ", failures.Select(x => (string)x["case"])));

            async Task<JObject> Sample(string name, string path, Func<Task> trigger)
            {
                foreach (string sound in clips.Keys) await audio.StopSfx(sound);
                await Task.Delay(100); await AsyncUtils.WaitEndOfFrame();
                var row = new JObject { ["case"] = name, ["path"] = path, ["clip"] = clips[path].name,
                    ["playing_observed"] = false, ["samples_advanced"] = false, ["peak_rms"] = 0d,
                    ["peak_sources"] = 0, ["source_ids"] = new JArray(), ["mixer_groups"] = new JArray() };
                rows.Add(row); Write(report);
                await trigger();
                var times = new Dictionary<int, int>(); var groups = new HashSet<string>();
                double end = EditorApplication.timeSinceStartup + Math.Min(2.0, Math.Max(.6, clips[path].length + .15));
                var values = new float[512];
                while (EditorApplication.timeSinceStartup < end)
                {
                    var sources = Object.FindObjectsByType<AudioSource>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                        .Where(x => x && x.clip == clips[path] && x.isPlaying).ToArray();
                    row["peak_sources"] = Math.Max((int)row["peak_sources"], sources.Length);
                    foreach (var source in sources)
                    {
                        row["playing_observed"] = true;
                        int id = source.GetInstanceID(), time = source.timeSamples;
                        if (times.TryGetValue(id, out int prior) && time != prior) row["samples_advanced"] = true;
                        times[id] = time;
                        source.GetOutputData(values, 0); double squares = 0;
                        foreach (float value in values) squares += value * value;
                        row["peak_rms"] = Math.Max((double)row["peak_rms"], Math.Sqrt(squares / values.Length));
                        groups.Add(source.outputAudioMixerGroup ? source.outputAudioMixerGroup.name : "<none>");
                        Require(!source.mute && source.volume > 0 && source.outputAudioMixerGroup && source.outputAudioMixerGroup.name == "SFX",
                            "Response audio is muted or bypassed the SFX mixer: " + name);
                    }
                    await Task.Delay(10);
                }
                row["source_ids"] = new JArray(times.Keys); row["mixer_groups"] = new JArray(groups); Write(report);
                return row;
            }
        }

        private static async Task VerifyOptionsAndPersistence()
        {
            Stage("settings_ui_and_persistence");
            await OpenGraphics();
            Require(!Corrected && EpilogueEnabled, "Opening settings altered the legacy defaults.");
            await Capture("settings-defaults");
            await ClickSetting("CorrectedCutInPortraits", true);
            await ClickSetting("FireworksEpilogue", false);
            Require(Corrected && !EpilogueEnabled, "The two new UI controls did not change the real settings service.");
            var options = Options();
            int writes = settingsMemory.Writes;
            Call(options, "Save");
            await Until(() => settingsMemory.Writes > writes, 15, "Options.Save did not persist through the memory settings handler.");
            var saved = await settingsMemory.Load(state.Configuration.DefaultSettingsSlotId);
            Require(saved.GetState<ExistingSetting>()?.Marker == "preserve-unrelated-settings", "Saving options removed an unrelated settings record.");
            string serialized = JsonUtility.ToJson(saved);
            Require(serialized.Contains("EnhancementSettings"), "Settings payload omits the new typed settings record.");
            await Capture("settings-corrected-off");
            await ClickSetting("CorrectedCutInPortraits", false);
            await ClickSetting("FireworksEpilogue", true);
            var service = preferences as IStatefulService<SettingsStateMap>;
            Require(service != null, "Enhancement service is not stateful settings.");
            await service.LoadServiceState(JsonUtility.FromJson<SettingsStateMap>(serialized));
            Require(Corrected && !EpilogueEnabled, "Serialized settings failed to restore the selected choices.");
            await state.ResetState(); RestoreEntryVariables();
            Require(Corrected && !EpilogueEnabled, "A game-state reset reset user presentation settings.");
            await service.LoadServiceState(new SettingsStateMap());
            Require(!Corrected && EpilogueEnabled, "A missing settings record failed to recover documented defaults.");
            report["settings_roundtrip"] = new JObject { ["stored"] = new JObject { ["corrected"] = true, ["epilogue"] = false },
                ["unrelated_record_preserved"] = true, ["reset_preserved_preferences"] = true, ["missing_record_defaults"] = SettingsSnapshot() };
            await CloseOptions();
        }

        private static Component Options() => Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None)
            .Single(x => x && x.GetType().FullName == "WitchTrials.Views.OptionsUi");
        private static string ObjectPath(Component component)
        {
            if (!component) return null;
            var names = new List<string>();
            for (var node = component.transform; node; node = node.parent) names.Add(node.name);
            names.Reverse(); return string.Join("/", names);
        }
        private static JObject OptionsUiDetails(IManagedUI ui)
        {
            if (ui == null) return new JObject { ["available"] = false };
            var component = ui as Component;
            var children = component ? component.GetComponentsInChildren<Transform>(true) : Array.Empty<Transform>();
            return new JObject { ["available"] = true, ["type"] = ui.GetType().FullName,
                ["path"] = ObjectPath(component), ["visible"] = ui.Visible,
                ["active_in_hierarchy"] = component && component.gameObject.activeInHierarchy,
                ["rows"] = new JArray(new[] { "CorrectedCutInPortraits", "FireworksEpilogue" }.Select(name =>
                {
                    var rows = children.Where(x => x.name == name).ToArray();
                    return new JObject { ["name"] = name, ["count"] = rows.Length,
                        ["active_count"] = rows.Count(x => x.gameObject.activeInHierarchy),
                        ["canvas_enabled"] = rows.Length == 1 && rows[0].GetComponentInParent<Canvas>() && rows[0].GetComponentInParent<Canvas>().enabled };
                })) };
        }
        private static void RecordOptionsUi(ISettingsUI selected, string phase)
        {
            var manager = Engine.GetServiceOrErr<IUIManager>();
            var managed = new List<IManagedUI>(); manager.GetManagedUIs(managed);
            if (report["options_ui_diagnostics"] == null) report["options_ui_diagnostics"] = new JArray();
            ((JArray)report["options_ui_diagnostics"]).Add(new JObject { ["phase"] = phase,
                ["frame_count"] = Time.frameCount, ["selected_ui"] = OptionsUiDetails(selected),
                ["named_options_ui"] = OptionsUiDetails(manager.GetUI("OptionsUI")),
                ["settings_interfaces"] = new JArray(managed.OfType<ISettingsUI>().Select(x => OptionsUiDetails(x))) });
            Write(report);
        }
        private static async Task ChangeOptionsThroughProduction(ISettingsUI ui, bool visible)
        {
            // TitleUi and ControlPanel use this extension; it also presents/dismisses NaninovelScreen.
            var method = RuntimeType("GigaCreation.NaninovelExtender.Ui.ManagedUiExtensions")
                .GetMethods(BindingFlags.Public | BindingFlags.Static).Single(x =>
                    x.Name == (visible ? "PresentAsync" : "DismissAsync") && x.GetParameters().Length == 3 &&
                    x.GetParameters()[2].ParameterType == typeof(CancellationToken));
            await AwaitForeign(method.Invoke(null, new object[] { ui, null, CancellationToken.None }));
        }
        private static async Task OpenGraphics()
        {
            var ui = Engine.GetServiceOrErr<IUIManager>().GetUI<ISettingsUI>();
            RecordOptionsUi(ui, "before_production_present");
            Require(ui != null, "Real options UI is unavailable.");
            Require(ReferenceEquals(ui, Options()), "Production GetUI<ISettingsUI>() selected " + ui.GetType().FullName +
                " instead of " + Options().GetType().FullName + "; refusing to bypass the production UI selection.");
            var settingsUis = new List<IManagedUI>(); Engine.GetServiceOrErr<IUIManager>().GetManagedUIs(settingsUis);
            Require(settingsUis.OfType<ISettingsUI>().Count() == 1, "Production must register exactly one ISettingsUI; see options_ui_diagnostics.");
            // A user keeps the modal open while changing multiple controls. Re-presenting
            // an already visible NaninovelScreen re-enables every child page canvas.
            if (!ui.Visible) await ChangeOptionsThroughProduction(ui, true);
            await AsyncUtils.WaitEndOfFrame();
            RecordOptionsUi(ui, "after_production_present");
            var options = Options();
            var rows = options.GetComponentsInChildren<Transform>(true).Where(x => x.name == "CorrectedCutInPortraits").ToArray();
            Require(rows.Length == 1, "Production OptionsUi must expose exactly one CorrectedCutInPortraits row after presentation; found " + rows.Length + ". See options_ui_diagnostics.");
            var row = rows[0];
            var pages = (Array)options.GetType().GetField("_pages", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(options);
            var page = pages.Cast<object>().Single(x => row.IsChildOf(((Canvas)x.GetType().GetField("_canvas", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(x)).transform));
            var tab = Get<Toggle>(page, "Toggle");
            if (!tab.isOn) await Click(tab);
            Require(row.GetComponentInParent<Canvas>().enabled, "Graphic settings page is not visible after clicking its tab.");
        }
        private static async Task CloseOptions()
        {
            var ui = Engine.GetServiceOrErr<IUIManager>().GetUI<ISettingsUI>();
            RecordOptionsUi(ui, "before_production_dismiss");
            Require(ui != null && ReferenceEquals(ui, Options()), "Production options UI selection changed before dismissal.");
            if (ui.Visible) await ChangeOptionsThroughProduction(ui, false);
            await AsyncUtils.WaitEndOfFrame();
            RecordOptionsUi(ui, "after_production_dismiss");
        }
        private static async Task ClickSetting(string rowName, bool enabled)
        {
            await OpenGraphics();
            var row = Options().GetComponentsInChildren<Transform>(true).Single(x => x.name == rowName);
            string option = "OptionItemToggle_" + (enabled ? "2" : "1");
            var toggle = row.GetComponentsInChildren<Toggle>(true).Single(x => x.name == option || x.transform.parent.name == option);
            if (!toggle.isOn) await Click(toggle);
            Require(toggle.isOn, "Requested setting toggle did not become selected: " + rowName);
            Require((rowName == "CorrectedCutInPortraits" ? Corrected : EpilogueEnabled) == enabled, "UI selection differs from the service: " + rowName);
            ((JArray)report["ui_clicks"]).Add(new JObject { ["row"] = rowName, ["value"] = enabled, ["frame"] = Time.frameCount });
        }
        private static async Task Click(Selectable target)
        {
            await AsyncUtils.WaitEndOfFrame(); Canvas.ForceUpdateCanvases();
            Require(target.IsActive() && target.IsInteractable(), "UI click target is inactive or not interactable: " + target.name);
            var canvas = target.GetComponentInParent<Canvas>();
            Require(canvas && canvas.enabled, "UI click target has a disabled canvas.");
            var rect = (RectTransform)target.transform;
            var position = RectTransformUtility.WorldToScreenPoint(canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera,
                rect.TransformPoint(rect.rect.center));
            var data = new PointerEventData(EventSystem.current) { position = position, button = PointerEventData.InputButton.Left };
            var hits = new List<RaycastResult>(); EventSystem.current.RaycastAll(data, hits);
            if (report["ui_raycast_diagnostics"] == null) report["ui_raycast_diagnostics"] = new JArray();
            ((JArray)report["ui_raycast_diagnostics"]).Add(new JObject { ["target"] = ObjectPath(target),
                ["position"] = position.ToString(), ["hits"] = new JArray(hits.Take(12).Select(hit =>
                {
                    var hitCanvas = hit.gameObject.GetComponentInParent<Canvas>();
                    return new JObject { ["path"] = ObjectPath(hit.gameObject.transform),
                        ["canvas_path"] = ObjectPath(hitCanvas), ["canvas_enabled"] = hitCanvas && hitCanvas.enabled,
                        ["raycaster"] = hit.module ? ObjectPath(hit.module) : null, ["depth"] = hit.depth };
                })) });
            Write(report);
            Require(hits.Count > 0 && (hits[0].gameObject.transform.IsChildOf(target.transform) || target.transform.IsChildOf(hits[0].gameObject.transform)),
                "Actual UI raycast is blocked: " + ObjectPath(target) + " top=" + (hits.Count > 0 ? ObjectPath(hits[0].gameObject.transform) : "none"));
            ExecuteEvents.Execute(target.gameObject, data, ExecuteEvents.pointerClickHandler);
            await AsyncUtils.WaitEndOfFrame();
        }

        private static async Task VerifyEnding(Script ending, bool enabled)
        {
            Stage(enabled ? "ending_on" : "ending_off");
            await ClickSetting("FireworksEpilogue", enabled); await CloseOptions();
            var variables = Engine.GetServiceOrErr<ICustomVariableManager>();
            foreach (int progress in new[] { 0, 11, 12 })
            {
                variables.TrySetVariableValue("g_gameProgress", progress);
                Call(Title(), "OnBeginToPresentEvent");
                if (enabled && progress == 12) await Until(() => Get<bool>(Title(), "CanReplayFireworksEpilogue"), 20, "Enabled replay did not appear at progress 12.");
                await AsyncUtils.WaitEndOfFrame();
                bool expected = enabled && progress >= 12;
                Require(Get<bool>(Title(), "CanReplayFireworksEpilogue") == expected && ReplayButton().gameObject.activeSelf == expected,
                    "Replay gate does not match setting and progress.");
            }
            await Capture(enabled ? "title-enabled" : "title-disabled");
            const string expression = "g_gameProgress = max(12, g_gameProgress)";
            int anchor = ending.Playlist.Select((c, i) => (c, i)).Single(x => x.c is SetCustomVariable s && s.Expression.Value == expression).i;
            Require(anchor == ending.Playlist.Count - 4 && ending.Playlist.Last().GetType().Name == "RouteFireworksEpilogue",
                "Built ending does not contain the reviewed conditional route tail.");
            var route = ending.Playlist.Last();
            var fade = (DecimalParameter)route.GetType().GetField("FadeDuration").GetValue(route);
            var delay = (DecimalParameter)route.GetType().GetField("MinDelayDuration").GetValue(route);
            var wait = (BooleanParameter)route.GetType().GetField("Wait").GetValue(route);
            Require(fade != null && delay != null && wait != null && fade.HasValue && delay.HasValue && wait.HasValue
                && Mathf.Abs(fade.Value - 1.8f) < .0001f && Mathf.Abs(delay.Value - .9f) < .0001f && wait.Value,
                "Built conditional route lost the original ending fade/delay/wait values.");
            report["built_ending_route_parameters"] = new JObject { ["fade"] = fade.Value, ["delay"] = delay.Value, ["wait"] = wait.Value };
            player.Stop(); SetInTitle(false); await HideTitle(); RestoreEntryVariables();
            variables.TrySetVariableValue("g_gameProgress", 11);
            bool entered = false;
            void Record(Command command) { if (command.PlaybackSpot.ScriptPath == EpiloguePath) entered = true; }
            player.OnCommandExecutionStart += Record;
            try
            {
                await player.LoadAndPlay(ending.Path, anchor);
                if (enabled)
                {
                    await Until(() => SessionActive && player.PlayedScript?.Path == EpiloguePath && PrinterStable(), 45, "Enabled ending failed to reach stable epilogue text.");
                    Require(entered, "Enabled ending route never executed the epilogue script.");
                    await Capture("ending-enabled-epilogue");
                    await ReturnTitle();
                }
                else
                {
                    await WaitTitle(); Require(!entered && !SessionActive, "Disabled ending still entered the epilogue.");
                    Require(!ReplayButton().gameObject.activeSelf, "Disabled ending exposed replay after returning.");
                    await Capture("ending-disabled-title");
                }
                Require(variables.TryGetVariableValue<int>("g_gameProgress", out int progress) && progress == 12, "Ending lost original completion progress semantics.");
                ((JArray)report["ending_routes"]).Add(new JObject { ["enabled"] = enabled, ["entered_epilogue"] = entered,
                    ["progress"] = progress, ["returned_title"] = player.PlayedScript?.Path == "System/System_Title" });
            }
            finally { player.OnCommandExecutionStart -= Record; }
            if (enabled)
            {
                Require(ReplayButton().gameObject.activeInHierarchy && Get<bool>(Title(), "CanReplayFireworksEpilogue"), "Enabled replay failed to recover on the title.");
                await Click(ReplayButton());
                await Until(() => SessionActive && player.PlayedScript?.Path == EpiloguePath && PrinterStable(), 45, "Actual replay button did not enter the epilogue.");
                await Capture("replay-enabled-epilogue"); await ReturnTitle();
            }
        }
        private static bool PrinterStable() => player.WaitingForInput && Engine.GetServiceOrErr<ITextPrinterManager>().Actors
            .Any(x => x.Visible && x.RevealProgress >= .999f);
        private static Button ReplayButton() => Title().GetComponentsInChildren<Button>(true).Single(x => x.name == "Button_FireworksEpilogue");
        private static async Task WaitTitle()
        {
            await Until(() => Title() && Get<bool>(Title(), "Visible") && player.PlayedScript?.Path == "System/System_Title", 45, "Real title did not appear.");
            await Task.Delay(3200); await AsyncUtils.WaitEndOfFrame();
        }
        private static void SetInTitle(bool value)
        {
            var title = Title();
            var manager = title.GetType().GetProperty("WitchTrialsNaninovelManager", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(title);
            manager.GetType().GetMethod("SetTitleScreenShown").Invoke(manager, new object[] { value });
        }
        private static async Task HideTitle()
        {
            await Engine.GetServiceOrErr<IUIManager>().GetUI("TitleUI").ChangeVisibility(false, 0);
            Require(!Get<bool>(Title(), "Visible") && Title().GetComponent<CanvasGroup>().alpha <= .001f, "Title canvas still covers the game fixture.");
        }
        private static async Task ReturnTitle()
        {
            SetInTitle(false);
            var command = (Command)Activator.CreateInstance(RuntimeType("WitchTrials.Views.ReturnToTitle"));
            command.GetType().GetField("Wait").SetValue(command, (BooleanParameter)true);
            await command.Execute(); await WaitTitle();
            Require(!SessionActive, "Return to title left epilogue state active.");
        }
        private static void RestoreEntryVariables()
        {
            entrySequence.GetType().GetMethod("ResetDebugVariables", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(entrySequence, null);
            var value = Engine.GetServiceOrErr<ICustomVariableManager>().GetVariableValue("enableDebugSkip");
            Require(value.Type == CustomVariableValueType.Boolean && !value.Boolean, "Fixture did not restore the normal game variables.");
        }

        private static Component PortraitAdapter(GameObject instance) => instance.GetComponents<Component>()
            .SingleOrDefault(x => x && x.GetType().FullName == "WitchTrials.Views.CorrectedCutInPortraitAdapter");
        private static async Task ActualAwaitSpawn(GameObject instance, AsyncToken token)
        {
            var objection = instance.GetComponents<MonoBehaviour>().Single(x => x && x.GetType().FullName == "WitchTrials.Views.ObjectionCutIn");
            ((Spawn.IParameterized)objection).SetSpawnParameters(new[] { "0" }, false);
            await ((Spawn.IAwaitable)objection).AwaitSpawn(token);
        }
        private static async Task ApplyAdapter(Component adapter)
        {
            await (UniTask)adapter.GetType().GetMethod("ApplyCurrentSetting").Invoke(adapter, new object[] { default(AsyncToken) });
        }
        private static async Task AwaitTask(Task task, double timeout, string error)
        {
            await Until(() => task.IsCompleted, timeout, error);
            await task;
        }
        private static void VerifyBuilderFailurePreservesConfirmation()
        {
            Stage("builder_failure_preserves_success");
            string cache = Path.GetFullPath(Path.Combine(Application.dataPath, "../Library/ManosabaContent"));
            string successPath = Path.Combine(cache, "corrected-cutin-portraits-state.json");
            string errorPath = Path.Combine(cache, "corrected-cutin-portraits-error.json");
            Require(File.Exists(successPath), "The normal build did not write corrected portrait confirmation.");
            byte[] successBefore = File.ReadAllBytes(successPath);
            var success = JObject.Parse(Encoding.UTF8.GetString(successBefore));
            Require((string)success["status"] == "OK" && success["output_sha256"] is JObject outputs && outputs.Count == 26,
                "The normal portrait build confirmation is not complete.");
            byte[] errorBefore = File.Exists(errorPath) ? File.ReadAllBytes(errorPath) : null;
            try
            {
                // Exercise a real, deterministic preflight failure without touching
                // any source/texture/prefab: the importer rejects Play Mode first.
                bool rejected = false;
                try { CorrectedCutInPortraitContentBuilder.EnsureApplied(); }
                catch (InvalidDataException error)
                {
                    Require(error.Message == "Import corrected portraits outside Play mode.", "Unexpected builder rejection: " + error.Message);
                    rejected = true;
                }
                Require(rejected, "The portrait builder accepted Play Mode.");
                Require(File.ReadAllBytes(successPath).SequenceEqual(successBefore), "A failed import erased or changed the last successful confirmation.");
                var failure = JObject.Parse(File.ReadAllText(errorPath));
                Require((string)failure["status"] == "ERROR" && (bool?)failure["successful_state_preserved"] == true,
                    "Import failure was not recorded separately from the success state.");
                report["builder_failure_state"] = new JObject { ["expected_preflight_rejection"] = true,
                    ["successful_confirmation_sha256_before"] = Hash(successBefore), ["successful_confirmation_sha256_after"] = Hash(File.ReadAllBytes(successPath)),
                    ["successful_confirmation_unchanged"] = true, ["error_recorded_separately"] = true };
            }
            finally
            {
                // The expected rejection is probe evidence, not a new persistent
                // build failure. Restore only this fixed local diagnostic file.
                if (errorBefore != null) File.WriteAllBytes(errorPath, errorBefore);
                else if (File.Exists(errorPath)) File.Delete(errorPath);
            }
        }
        private static async Task VerifySpawnAndPendingRelease()
        {
            Stage("actual_spawn_and_pending_release");
            await ClickSetting("CorrectedCutInPortraits", true); await CloseOptions();
            player.Stop(); await state.ResetState(); RestoreEntryVariables(); SetInTitle(false); await HideTitle();
            var cases = new JArray(); report["spawn_resource_cases"] = cases;
            report["pending_resource_scope"] = "These checks run before any direct probe Load<Sprite> of corrected art. They report observed pending leases; a cold Addressables bundle/disk cache is not asserted.";
            var racePrefab = await Load<GameObject>("Naninovel/Spawn/CutinSmall_Alisa");
            GameObject instance = null;
            try
            {
                instance = Object.Instantiate(racePrefab);
                Task spawn = ActualAwaitSpawn(instance, default);
                var adapter = PortraitAdapter(instance);
                Require(adapter, "Race fixture did not use the production portrait hook.");
                var pendingField = adapter.GetType().GetField("pending", BindingFlags.Instance | BindingFlags.NonPublic);
                object pendingBefore = pendingField.GetValue(adapter);
                bool pendingObserved = Get<bool>(adapter, "HasPendingLoad") && !Get<bool>(adapter, "UsingCorrected");
                bool epilogueBefore = EpilogueEnabled;
                var epilogueProperty = preferences.GetType().GetProperty("FireworksEpilogueEnabled");
                epilogueProperty.SetValue(preferences, !epilogueBefore);
                if (pendingObserved) Require(ReferenceEquals(pendingBefore, pendingField.GetValue(adapter)),
                    "Changing the epilogue restarted a pending production portrait request.");
                Task duplicate = ApplyAdapter(adapter);
                if (pendingObserved) Require(ReferenceEquals(pendingBefore, pendingField.GetValue(adapter)),
                    "A concurrent portrait caller canceled/replaced the production request.");
                epilogueProperty.SetValue(preferences, epilogueBefore);
                if (pendingObserved) Require(ReferenceEquals(pendingBefore, pendingField.GetValue(adapter)) && !spawn.IsCompleted,
                    "Restoring the epilogue preference released the request or completed AwaitSpawn before corrected art.");
                await Until(() => Get<bool>(adapter, "UsingCorrected") || Get<string>(adapter, "LastFailure") != null, 30,
                    "Concurrent/epilogue-change fixture did not finish loading corrected art.");
                Require(Get<string>(adapter, "LastFailure") == null && Get<bool>(adapter, "UsingCorrected"),
                    "The epilogue-change fixture fell back to original art.");
                await AwaitTask(duplicate, 15, "The concurrent portrait waiter did not settle.");
                await AwaitTask(spawn, 30, "The production spawn did not finish after the epilogue change.");
                instance.SetActive(false);
                Require(!Get<bool>(adapter, "HasResourceHandle"), "The epilogue-change fixture leaked its corrected lease.");
                cases.Add(new JObject { ["case"] = "real_AwaitSpawn_epilogue_change_and_concurrent_waiter", ["character"] = "Alisa",
                    ["corrected_sprite_preloaded_by_probe"] = false, ["pending_observed"] = pendingObserved,
                    ["pending_epilogue_change_verified"] = pendingObserved, ["pending_request_shared_verified"] = pendingObserved,
                    ["production_spawn_completed_with_corrected_art"] = true, ["epilogue_preference_restored"] = EpilogueEnabled == epilogueBefore,
                    ["disabled_resource_handle_released"] = true });
            }
            finally { if (instance) Object.DestroyImmediate(instance); instance = null; }
            var prefab = await Load<GameObject>("Naninovel/Spawn/CutinSmall_Hiro");
            try
            {
                instance = Object.Instantiate(prefab);
                var main = instance.transform.Find("CharaRig/Chara").GetComponent<SpriteRenderer>();
                var shadow = instance.transform.Find("CharaRig/Chara_Shadow").GetComponent<SpriteRenderer>();
                Sprite original = main.sprite;
                using var cancellation = new CancellationTokenSource();
                Task spawn = ActualAwaitSpawn(instance, new AsyncToken(cancellation.Token));
                var adapter = PortraitAdapter(instance);
                Require(adapter, "Real AwaitSpawn did not attach the corrected-portrait adapter.");
                bool pending = Get<bool>(adapter, "HasResourceHandle") && !Get<bool>(adapter, "UsingCorrected");
                Task duplicate = ApplyAdapter(adapter);
                cancellation.Cancel();
                bool canceled = false;
                try { await AwaitTask(spawn, 15, "Canceled real AwaitSpawn did not settle."); }
                catch (OperationCanceledException) { canceled = true; }
                Require(canceled, "Real AwaitSpawn ignored the caller's cancellation.");
                await AwaitTask(duplicate, 15, "The canceled spawn's shared waiter did not settle.");
                if (pending) Require(!Get<bool>(adapter, "HasResourceHandle") && main.sprite == original && shadow.sprite == original,
                    "Canceling a pending corrected load retained a lease or modified a sprite.");
                instance.SetActive(false);
                Require(!Get<bool>(adapter, "HasResourceHandle") && main.sprite == original && shadow.sprite == original,
                    "Disabling the canceled real spawn retained corrected resources.");
                cases.Add(new JObject { ["case"] = "real_AwaitSpawn_cancel", ["character"] = "Hiro", ["corrected_sprite_preloaded_by_probe"] = false,
                    ["pending_observed"] = pending, ["pending_cancellation_verified"] = pending, ["caller_cancellation_propagated"] = canceled,
                    ["shared_waiter_settled"] = true,
                    ["disabled_restored_both_slots"] = true, ["disabled_resource_handle_released"] = true });
            }
            finally { if (instance) Object.DestroyImmediate(instance); instance = null; }

            // A separate sprite has not been acquired by the probe. Disable while this
            // adapter owns its pending request; do not start a Timeline on an inactive object.
            var disablePrefab = await Load<GameObject>("Naninovel/Spawn/CutinSmall_Ema");
            try
            {
                instance = Object.Instantiate(disablePrefab);
                var main = instance.transform.Find("CharaRig/Chara").GetComponent<SpriteRenderer>();
                var shadow = instance.transform.Find("CharaRig/Chara_Shadow").GetComponent<SpriteRenderer>();
                Sprite original = main.sprite;
                var adapter = (Component)RuntimeType("WitchTrials.Views.CorrectedCutInPortraitAdapter").GetMethod("Attach").Invoke(null, new object[] { instance });
                Require(adapter, "Pending-disable adapter did not attach.");
                Task load = ApplyAdapter(adapter);
                bool pending = Get<bool>(adapter, "HasResourceHandle") && !Get<bool>(adapter, "UsingCorrected");
                instance.SetActive(false);
                Require(!Get<bool>(adapter, "HasResourceHandle") && main.sprite == original && shadow.sprite == original,
                    "Disabling a loading adapter did not immediately restore/release.");
                await AwaitTask(load, 15, "Disabled adapter's pending task did not settle.");
                Require(!Get<bool>(adapter, "HasResourceHandle") && main.sprite == original && shadow.sprite == original,
                    "A canceled disabled load adopted a late corrected sprite.");
                cases.Add(new JObject { ["case"] = "adapter_pending_disable", ["character"] = "Ema", ["corrected_sprite_preloaded_by_probe"] = false,
                    ["pending_observed"] = pending, ["pending_disable_verified"] = pending,
                    ["disabled_restored_both_slots"] = true, ["disabled_resource_handle_released"] = true, ["late_result_not_adopted"] = true });
            }
            finally { if (instance) Object.DestroyImmediate(instance); instance = null; }

            // Exercise the actual production ObjectionCutIn entry, without manually
            // attaching the adapter or manually advancing the original Timeline.
            try
            {
                instance = Object.Instantiate(prefab);
                var main = instance.transform.Find("CharaRig/Chara").GetComponent<SpriteRenderer>();
                var shadow = instance.transform.Find("CharaRig/Chara_Shadow").GetComponent<SpriteRenderer>();
                Sprite original = main.sprite;
                var mainMaterials = main.sharedMaterials.ToArray(); var shadowMaterials = shadow.sharedMaterials.ToArray();
                Task spawn = ActualAwaitSpawn(instance, default);
                var adapter = PortraitAdapter(instance);
                Require(adapter, "Real successful AwaitSpawn did not attach the portrait adapter.");
                await Until(() => Get<bool>(adapter, "UsingCorrected") || Get<string>(adapter, "LastFailure") != null, 30, "Real AwaitSpawn did not apply corrected art.");
                Require(Get<string>(adapter, "LastFailure") == null && main.sprite != original && main.sprite == shadow.sprite &&
                    main.sprite == Get<Sprite>(adapter, "CorrectedSprite"), "Real AwaitSpawn selected incorrect portrait slots.");
                var director = instance.GetComponentsInChildren<PlayableDirector>(true).Single();
                await Until(() => director.time >= .65 && main.color.a > .01f, 15, "Real AwaitSpawn never advanced its original visible Timeline pose.");
                RequirePortraitVisible(main); await Capture("Hiro-real-AwaitSpawn-corrected");
                await AwaitTask(spawn, 30, "Real ObjectionCutIn.AwaitSpawn did not complete its original Timeline.");
                Require(main.sharedMaterials.SequenceEqual(mainMaterials) && shadow.sharedMaterials.SequenceEqual(shadowMaterials),
                    "Real AwaitSpawn replaced original portrait materials.");
                instance.SetActive(false);
                Require(!Get<bool>(adapter, "HasResourceHandle") && main.sprite == original && shadow.sprite == original,
                    "A completed real spawn did not restore original slots/release when disabled.");
                cases.Add(new JObject { ["case"] = "real_AwaitSpawn_complete", ["character"] = "Hiro", ["adapter_attached_by_production_hook"] = true,
                    ["original_timeline_executed"] = true, ["both_slots_corrected"] = true, ["materials_preserved"] = true,
                    ["disabled_restored_both_slots"] = true, ["disabled_resource_handle_released"] = true });
            }
            finally { if (instance) Object.DestroyImmediate(instance); }
            Write(report);
        }

        private static async Task VerifyPortraits()
        {
            Stage("thirteen_portrait_mappings");
            string repo = Path.GetFullPath(Path.Combine(Application.dataPath, "../.."));
            string manifestPath = Path.Combine(repo, "content-overrides/corrected-cutin-portraits/manifest.json");
            var manifest = JObject.Parse(File.ReadAllText(manifestPath));
            var rows = ((JArray)manifest["portraits"]).OfType<JObject>().ToArray();
            var adapterType = RuntimeType("WitchTrials.Views.CorrectedCutInPortraitAdapter");
            var expected = (string[])adapterType.GetField("Characters").GetValue(null);
            Require(rows.Length == 13 && rows.Select(x => (string)x["character"]).Distinct().Count() == 13
                && rows.Select(x => (string)x["character"]).OrderBy(x => x).SequenceEqual(expected.OrderBy(x => x)), "Portrait manifest does not map exactly the thirteen runtime characters.");
            report["portrait_manifest_sha256"] = Hash(File.ReadAllBytes(manifestPath));
            foreach (var row in rows)
            {
                string character = (string)row["character"];
                Stage("portrait_" + character);
                string source = SafeFile(repo, (string)row["source"]);
                string originalPath = SafeFile(repo, "UnityProject/" + (string)row["original_sprite"]);
                Require(Hash(File.ReadAllBytes(source)) == (string)row["sha256"], "Corrected art source hash mismatch: " + character);
                string originalPixelHash = CorrectedCutInPortraitContentBuilder.VerifyOriginalPixels(row);
                var prefab = await Load<GameObject>(CorrectedCutInPortraitContentBuilder.PrefabAddress(character));
                var corrected = await Load<Sprite>("Naninovel/CorrectedCutInPortraits/" + character);
                await ClickSetting("CorrectedCutInPortraits", false); await CloseOptions();
                player.Stop(); await state.ResetState(); RestoreEntryVariables(); SetInTitle(false); await HideTitle();
                GameObject instance = null;
                try
                {
                    instance = Object.Instantiate(prefab);
                    var main = instance.transform.Find("CharaRig/Chara")?.GetComponent<SpriteRenderer>();
                    var shadow = instance.transform.Find("CharaRig/Chara_Shadow")?.GetComponent<SpriteRenderer>();
                    Require(main && shadow && main.sprite && main.sprite == shadow.sprite, "Built portrait slots differ: " + character);
                    Sprite original = main.sprite;
                    var adapter = (Component)adapterType.GetMethod("Attach").Invoke(null, new object[] { instance });
                    Require(adapter && Get<string>(adapter, "Character") == character && Get<Sprite>(adapter, "OriginalSprite") == original,
                        "Runtime adapter attached to the wrong original sprite: " + character);
                    var mainMaterials = main.sharedMaterials.ToArray(); var shadowMaterials = shadow.sharedMaterials.ToArray();
                    bool show = character == "Ema" || character == "Hiro" || character == "Alisa";
                    EvaluatePortrait(instance);
                    var originalPose = Pose(instance);
                    Require(!Get<bool>(adapter, "UsingCorrected") && !Get<bool>(adapter, "HasResourceHandle"), "Original mode unexpectedly holds corrected resources.");
                    if (show) { RequirePortraitVisible(main); await Capture(character + "-original"); }
                    await ClickSetting("CorrectedCutInPortraits", true); await CloseOptions();
                    await Until(() => Get<bool>(adapter, "UsingCorrected") || Get<string>(adapter, "LastFailure") != null,
                        30, "Corrected portrait did not load: " + character);
                    Require(Get<string>(adapter, "LastFailure") == null && main.sprite == corrected && shadow.sprite == corrected
                        && Get<Sprite>(adapter, "CorrectedSprite") == corrected && Get<bool>(adapter, "HasResourceHandle"),
                        "Corrected portrait did not reach both original slots: " + character);
                    Require(main.sharedMaterials.SequenceEqual(mainMaterials) && shadow.sharedMaterials.SequenceEqual(shadowMaterials), "Portrait switching replaced a material: " + character);
                    Require(JToken.DeepEquals(originalPose, Pose(instance)), "Portrait switching changed source transforms/Timeline identity: " + character);
                    var mapping = new JObject { ["character"] = character, ["prefab_name"] = prefab.name,
                        ["prefab_address"] = CorrectedCutInPortraitContentBuilder.PrefabAddress(character), ["original_sprite"] = original.name,
                        ["original_decoded_rgba_sha256"] = originalPixelHash, ["original_png_file_sha256"] = Hash(File.ReadAllBytes(originalPath)),
                        ["corrected_sprite"] = corrected.name, ["original_ppu"] = original.pixelsPerUnit, ["corrected_ppu"] = corrected.pixelsPerUnit,
                        ["original_pivot"] = new JArray(original.pivot.x, original.pivot.y), ["corrected_pivot"] = new JArray(corrected.pivot.x, corrected.pivot.y),
                        ["both_slots_changed"] = true, ["materials_and_pose_preserved"] = true };
                    VerifyAnchors(row, original, corrected, originalPath, source, mapping);
                    if (show) { RequirePortraitVisible(main); await Capture(character + "-corrected"); }
                    await ClickSetting("CorrectedCutInPortraits", false); await CloseOptions();
                    await Until(() => !Get<bool>(adapter, "HasResourceHandle"), 10, "Original selection retained a corrected resource handle: " + character);
                    Require(main.sprite == original && shadow.sprite == original && !Get<bool>(adapter, "UsingCorrected"), "Original sprite identity was not restored: " + character);
                    Require(JToken.DeepEquals(originalPose, Pose(instance)), "Restoring original art changed the pose: " + character);
                    if (show) { RequirePortraitVisible(main); await Capture(character + "-restored"); }
                    await ClickSetting("CorrectedCutInPortraits", true); await CloseOptions();
                    await Until(() => Get<bool>(adapter, "UsingCorrected"), 30, "Second corrected entry failed: " + character);
                    instance.SetActive(false);
                    await AsyncUtils.WaitEndOfFrame();
                    Require(!Get<bool>(adapter, "HasResourceHandle") && main.sprite == original && shadow.sprite == original,
                        "Disabling the instance leaked its corrected resource or left modified sprites: " + character);
                    instance.SetActive(true);
                    await Until(() => Get<bool>(adapter, "UsingCorrected"), 30, "Re-enabled instance did not reapply the preference: " + character);
                    Object.DestroyImmediate(instance); instance = null;
                    Require(!Get<bool>(adapter, "HasResourceHandle"), "Destroying the instance retained a corrected resource lease: " + character);
                    mapping["original_identity_restored"] = true; mapping["disable_released"] = true;
                    mapping["reenable_loaded"] = true; mapping["destroy_released"] = true;
                    ((JArray)report["portraits"]).Add(mapping); Write(report);
                }
                finally { if (instance) Object.DestroyImmediate(instance); }
            }
            await ClickSetting("CorrectedCutInPortraits", false); await CloseOptions();
        }
        private static void EvaluatePortrait(GameObject root)
        {
            foreach (var director in root.GetComponentsInChildren<PlayableDirector>(true))
            {
                Require(director.playableAsset, "Portrait director has no built Timeline.");
                director.timeUpdateMode = DirectorUpdateMode.Manual;
                director.RebuildGraph(); director.time = .7; director.Evaluate(); director.Pause();
            }
        }
        private static void RequirePortraitVisible(SpriteRenderer portrait)
        {
            Require(portrait && portrait.enabled && portrait.gameObject.activeInHierarchy && portrait.color.a > .01f && portrait.sprite,
                "Portrait is inactive/transparent in the sampled Timeline pose.");
            var camera = Engine.GetServiceOrErr<ICameraManager>().Camera;
            Require(GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(camera), portrait.bounds),
                "Portrait is outside the actual camera at the sampled Timeline pose.");
            Require(!Get<bool>(Title(), "Visible") && !Engine.GetServiceOrErr<IUIManager>().GetUI<ISettingsUI>().Visible,
                "Title/options UI still covers the portrait comparison.");
        }
        private static JArray Pose(GameObject root) => new JArray(root.GetComponentsInChildren<Transform>(true).Select(x => new JObject {
            ["id"] = x.GetInstanceID(), ["position"] = new JArray(x.localPosition.x, x.localPosition.y, x.localPosition.z),
            ["rotation"] = new JArray(x.localRotation.x, x.localRotation.y, x.localRotation.z, x.localRotation.w),
            ["scale"] = new JArray(x.localScale.x, x.localScale.y, x.localScale.z) }));
        private static void VerifyAnchors(JObject row, Sprite original, Sprite corrected, string originalPath, string correctedPath, JObject result)
        {
            var alignment = row["alignment"] as JObject;
            var oldBounds = AlphaBounds(originalPath, out var originalSize);
            var newBounds = AlphaBounds(correctedPath, out var correctedSize);
            Require(original.rect.size == (Vector2)originalSize && corrected.rect.size == (Vector2)correctedSize,
                "Built sprite was cropped or resized relative to the reviewed source canvas: " + row["character"]);
            Vector2 oa = alignment?["original_anchor_px"] is JArray oldAnchor ? new Vector2((float)oldAnchor[0], (float)oldAnchor[1])
                : new Vector2(oldBounds.center.x, oldBounds.yMin);
            Vector2 ca = alignment?["corrected_anchor_px"] is JArray newAnchor ? new Vector2((float)newAnchor[0], (float)newAnchor[1])
                : new Vector2(newBounds.center.x, newBounds.yMin);
            Vector2 oldLocal = (new Vector2(oa.x, original.rect.height - oa.y) - original.pivot) / original.pixelsPerUnit;
            Vector2 newLocal = (new Vector2(ca.x, corrected.rect.height - ca.y) - corrected.pivot) / corrected.pixelsPerUnit;
            float heightError = Mathf.Abs(oldBounds.height / original.pixelsPerUnit - newBounds.height / corrected.pixelsPerUnit);
            result["anchor_scope"] = alignment != null ? "Explicit reviewed source anchors" : "Visible alpha bounding-box top center (threshold 16)";
            result["original_alpha_bounds"] = new JArray(oldBounds.x, oldBounds.y, oldBounds.width, oldBounds.height);
            result["corrected_alpha_bounds"] = new JArray(newBounds.x, newBounds.y, newBounds.width, newBounds.height);
            result["alpha_height_world_error"] = heightError;
            result["head_anchor_original_local"] = new JArray(oldLocal.x, oldLocal.y);
            result["head_anchor_corrected_local"] = new JArray(newLocal.x, newLocal.y);
            result["head_anchor_error"] = Vector2.Distance(oldLocal, newLocal);
            Require(Vector2.Distance(oldLocal, newLocal) < .001f && heightError < .001f,
                "Built portrait anchor/alpha height is not aligned in sprite-local world units: " + row["character"]);
        }
        private static RectInt AlphaBounds(string path, out Vector2Int size)
        {
            var image = new Texture2D(2, 2, TextureFormat.RGBA32, false, true);
            try
            {
                Require(ImageConversion.LoadImage(image, File.ReadAllBytes(path), false), "Could not decode reviewed portrait source: " + path);
                size = new Vector2Int(image.width, image.height);
                var pixels = image.GetPixels32();
                int left = size.x, right = -1, top = size.y, bottom = -1;
                for (int y = 0; y < size.y; y++) for (int x = 0; x < size.x; x++)
                {
                    if (pixels[(size.y - y - 1) * size.x + x].a < 16) continue;
                    left = Mathf.Min(left, x); right = Mathf.Max(right, x); top = Mathf.Min(top, y); bottom = Mathf.Max(bottom, y);
                }
                Require(right >= left && bottom >= top, "Reviewed portrait has no visible alpha pixels.");
                return new RectInt(left, top, right - left + 1, bottom - top + 1);
            }
            finally { Object.DestroyImmediate(image); }
        }
        private static string SafeFile(string repo, string relative)
        {
            Require(!string.IsNullOrWhiteSpace(relative) && !Path.IsPathRooted(relative), "Manifest source must be repository-relative.");
            string path = Path.GetFullPath(Path.Combine(repo, relative));
            Require(path.StartsWith(repo.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)
                && File.Exists(path), "Manifest source is missing or outside the repository: " + relative);
            return path;
        }
        private static string Hash(byte[] bytes) { using var sha = SHA256.Create(); return BitConverter.ToString(sha.ComputeHash(bytes)).Replace("-", "").ToLowerInvariant(); }
        private static async Task Capture(string name)
        {
            await AsyncUtils.WaitEndOfFrame();
            var manager = Engine.GetServiceOrErr<ICameraManager>();
            var camera = manager.Camera;
            Require(camera, "Runtime camera is missing.");
            var uiCamera = manager.UICamera && manager.UICamera != camera ? manager.UICamera : null;
            Canvas.ForceUpdateCanvases();
            var canvases = Object.FindObjectsByType<Canvas>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                .Where(x => x.isRootCanvas && x.isActiveAndEnabled).ToArray();
            var overlays = canvases.Where(x => x.renderMode == RenderMode.ScreenSpaceOverlay)
                .Select(x => (Canvas: x, Camera: x.worldCamera, Plane: x.planeDistance)).ToArray();
            var target = RenderTexture.GetTemporary(1280, 720, 24, RenderTextureFormat.ARGB32);
            var previous = camera.targetTexture; var active = RenderTexture.active;
            var previousUI = uiCamera ? uiCamera.targetTexture : null;
            Texture2D pixels = null;
            try
            {
                // Camera.Render does not include Overlay canvases (the original-poster footer).
                // Project them through the actual UI camera only for this screenshot, preserving
                // sorting/content and restoring their original mode immediately afterwards.
                foreach (var overlay in overlays)
                {
                    overlay.Canvas.renderMode = RenderMode.ScreenSpaceCamera;
                    overlay.Canvas.worldCamera = uiCamera ? uiCamera : camera;
                    overlay.Canvas.planeDistance = overlay.Canvas.worldCamera.nearClipPlane + 1f;
                }
                Canvas.ForceUpdateCanvases();
                camera.targetTexture = target; camera.Render();
                if (uiCamera && manager.RenderUI) { uiCamera.targetTexture = target; uiCamera.Render(); }
                RenderTexture.active = target;
                pixels = new Texture2D(1280, 720, TextureFormat.RGBA32, false);
                pixels.ReadPixels(new Rect(0, 0, 1280, 720), 0, 0); pixels.Apply();
                string path = Path.Combine(Output, name + ".png"); File.WriteAllBytes(path, pixels.EncodeToPNG());
                ((JArray)report["screenshots"]).Add(new JObject { ["name"] = name, ["path"] = path, ["phase"] = activeCase,
                    ["frame_count"] = Time.frameCount, ["capture_phase"] = "PostLateUpdate",
                    ["script"] = player?.PlayedScript?.Path, ["index"] = player?.PlayedIndex, ["camera"] = camera.name,
                    ["ui_camera"] = uiCamera ? uiCamera.name : null,
                    ["projected_overlay_canvases"] = new JArray(overlays.Select(x => x.Canvas.name)),
                    ["canvases"] = new JArray(canvases.Select(x => new JObject { ["name"] = x.name, ["mode"] = x.renderMode.ToString(),
                        ["camera"] = x.worldCamera ? x.worldCamera.name : null, ["order"] = x.sortingOrder })),
                    ["capture_scope"] = "Runtime main/UI camera renders with active screen overlays temporarily camera-projected at unchanged sorting order for capture, then restored. Title visibility/raycast is verified in its normal state." });
            }
            finally
            {
                camera.targetTexture = previous; if (uiCamera) uiCamera.targetTexture = previousUI;
                foreach (var overlay in overlays) if (overlay.Canvas)
                {
                    overlay.Canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                    overlay.Canvas.worldCamera = overlay.Camera;
                    overlay.Canvas.planeDistance = overlay.Plane;
                }
                Canvas.ForceUpdateCanvases();
                RenderTexture.active = active; RenderTexture.ReleaseTemporary(target); if (pixels) Object.DestroyImmediate(pixels);
            }
        }

        private static JObject RuntimeSnapshot() => new JObject { ["case"] = activeCase, ["script"] = player?.PlayedScript?.Path,
            ["command"] = player?.PlayedCommand?.GetType().FullName, ["index"] = player?.PlayedIndex,
            ["playing"] = player?.Playing, ["waiting"] = player?.WaitingForInput };
        private static async Task Until(Func<bool> condition, double seconds, string failure)
        {
            double deadline = EditorApplication.timeSinceStartup + seconds;
            while (!condition()) { CheckCallback(); Require(EditorApplication.timeSinceStartup < deadline, failure); await Task.Delay(30); }
        }
        private static void CheckCallback() { if (callbackFailure != null) throw new InvalidOperationException(callbackFailure); }
        private static Component Title() => Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).FirstOrDefault(x => x && x.GetType().FullName == "WitchTrials.Views.TitleUi");
        private static Type RuntimeType(string name) => AppDomain.CurrentDomain.GetAssemblies().Select(x => x.GetType(name, false)).FirstOrDefault(x => x != null) ?? throw new TypeLoadException(name);
        private static T Get<T>(object obj, string property) => (T)obj.GetType().GetProperty(property).GetValue(obj);
        private static object Call(object target, string name)
        {
            var method = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public).Single(x => x.Name == name && x.GetParameters().All(p => p.IsOptional));
            return method.Invoke(target, method.GetParameters().Select(p => p.DefaultValue is DBNull || p.DefaultValue == Missing.Value ? Activator.CreateInstance(p.ParameterType) : p.DefaultValue).ToArray());
        }
        private static async Task AwaitForeign(object task)
        {
            var awaiter = task.GetType().GetMethod("GetAwaiter").Invoke(task, null);
            await Until(() => Get<bool>(awaiter, "IsCompleted"), 90, "Real game initialization timed out.");
            awaiter.GetType().GetMethod("GetResult").Invoke(awaiter, null);
        }
        private static void Log(string message, string stack, LogType type)
        {
            if (type != LogType.Error && type != LogType.Exception) return;
            ((JArray)report["errors"]).Add(new JObject { ["type"] = type.ToString(), ["message"] = message, ["stack"] = stack });
            callbackFailure ??= message;
        }
        private static async Task<T> Load<T>(string address) where T : Object
        {
            var locations = Addressables.LoadResourceLocationsAsync(address, typeof(T));
            try
            {
                await locations.Task;
                Require(locations.Status == AsyncOperationStatus.Succeeded && locations.Result.Count == 1, "Expected one built location: " + address);
                var location = locations.Result[0];
                Require(location.ProviderId.Contains("BundledAssetProvider"), "Non-bundle provider: " + address + " / " + location.ProviderId);
                var handle = Addressables.LoadAssetAsync<T>(location); handles.Add(handle); await handle.Task;
                Require(handle.Status == AsyncOperationStatus.Succeeded && handle.Result, "Built load failed: " + address);
                ((JArray)report["built_assets"]).Add(new JObject { ["address"] = address, ["name"] = handle.Result.name, ["provider"] = location.ProviderId, ["internal_id"] = location.InternalId });
                return handle.Result;
            }
            finally { if (locations.IsValid()) Addressables.Release(locations); }
        }

        private static JObject EditorSnapshot()
        {
            var active = SceneManager.GetActiveScene();
            return new JObject { ["playing_or_entering_play"] = EditorApplication.isPlayingOrWillChangePlaymode,
                ["compiling"] = EditorApplication.isCompiling, ["importing"] = EditorApplication.isUpdating,
                ["play_mode_start_scene"] = EditorSceneManager.playModeStartScene ? AssetDatabase.GetAssetPath(EditorSceneManager.playModeStartScene) : null,
                ["scenes"] = new JArray(Enumerable.Range(0, SceneManager.sceneCount).Select(i =>
                {
                    var scene = SceneManager.GetSceneAt(i);
                    return new JObject { ["path"] = scene.path, ["name"] = scene.name, ["loaded"] = scene.isLoaded,
                        ["dirty"] = scene.isDirty, ["root_count"] = scene.isLoaded ? scene.rootCount : 0, ["active"] = scene == active };
                })) };
        }
        private static SavedScene[] ReadEditorSceneSetup()
        {
            var scenes = EditorSceneManager.GetSceneManagerSetup().Select(x =>
                new SavedScene { path = x.path ?? "", isLoaded = x.isLoaded, isActive = x.isActive }).ToArray();
            if (scenes.Length > 0) return scenes;
            // A new clean empty batch scene can be omitted from Unity's saved scene setup.
            // Describe only this already-accepted state explicitly, for both capture and readback.
            Require(SceneManager.sceneCount == 1, "Editor scene setup is unavailable.");
            var scene = SceneManager.GetSceneAt(0);
            Require(scene.isLoaded && !scene.isDirty && scene.rootCount == 0 && string.IsNullOrEmpty(scene.path),
                "Editor scene setup is unavailable; only a sole clean empty unnamed scene can be normalized.");
            return new[] { new SavedScene { path = "", isLoaded = true, isActive = scene == SceneManager.GetActiveScene() } };
        }
        private static void CaptureEditorScenes()
        {
            var saved = new SavedEditorScenes { scenes = ReadEditorSceneSetup(),
                playModeStartScene = EditorSceneManager.playModeStartScene ? AssetDatabase.GetAssetPath(EditorSceneManager.playModeStartScene) : "" };
            Require(saved.scenes.Length > 0, "Editor scene setup is unavailable.");
            SessionState.SetString(Key + "scene_setup", JsonUtility.ToJson(saved));
            var hashes = new JObject();
            foreach (var scene in saved.scenes.Where(x => !string.IsNullOrEmpty(x.path)))
                hashes[scene.path] = Hash(File.ReadAllBytes(Path.Combine(Repo, "UnityProject", scene.path)));
            SessionState.SetString(Key + "scene_hashes", hashes.ToString());
            SessionState.SetBool(Key + "scene_state_captured", true);
            EditorSceneManager.playModeStartScene = null;
        }
        private static void RestoreEditorScenes()
        {
            if (!SessionState.GetBool(Key + "scene_state_captured", false)) return;
            var saved = JsonUtility.FromJson<SavedEditorScenes>(SessionState.GetString(Key + "scene_setup", ""));
            Require(saved?.scenes != null && saved.scenes.Length > 0, "The original editor scene setup is missing.");
            if (SessionState.GetBool(Key + "scene_opened", false))
            {
                // Discard only the probe's temporary Boot modifications. User scenes were
                // checked clean and their saved scene bytes are never written by this probe.
                EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
                if (saved.scenes.Length != 1 || !string.IsNullOrEmpty(saved.scenes[0].path))
                    EditorSceneManager.RestoreSceneManagerSetup(saved.scenes.Select(x =>
                        new SceneSetup { path = x.path, isLoaded = x.isLoaded, isActive = x.isActive }).ToArray());
                SessionState.SetBool(Key + "scene_opened", false);
            }
            var start = string.IsNullOrEmpty(saved.playModeStartScene) ? null : AssetDatabase.LoadAssetAtPath<SceneAsset>(saved.playModeStartScene);
            Require(string.IsNullOrEmpty(saved.playModeStartScene) || start, "The previous Play Mode start scene is missing.");
            EditorSceneManager.playModeStartScene = start;
            var actual = ReadEditorSceneSetup();
            Require(JsonUtility.ToJson(new SavedEditorScenes { scenes = actual, playModeStartScene = start ? AssetDatabase.GetAssetPath(start) : "" }) == JsonUtility.ToJson(saved),
                "Restored editor scenes/order/loaded state/active scene/start-scene do not match the original setup.");
            var hashes = JObject.Parse(SessionState.GetString(Key + "scene_hashes", "{}"));
            foreach (var hash in hashes.Properties()) Require(Hash(File.ReadAllBytes(Path.Combine(Repo, "UnityProject", hash.Name))) == (string)hash.Value,
                "The probe changed a user scene's saved bytes: " + hash.Name);
            SessionState.SetBool(Key + "scene_state_captured", false);
        }
        private static void PublishRequestResult(string status, JObject value = null)
        {
            string id = SessionState.GetString(Key + "request_id", "");
            if (string.IsNullOrEmpty(id)) return;
            File.WriteAllText(RequestResultPath, new JObject { ["id"] = id, ["status"] = status, ["output"] = Output,
                ["report"] = Path.Combine(Output, "report.json"), ["editor_before"] = JObject.Parse(SessionState.GetString(Key + "editor_before", "{}")),
                ["builder_and_scene_restored"] = value?["builder_and_scene_restored"], ["error"] = value?["error"],
                ["restore_error"] = value?["restore_error"] }.ToString());
        }
        private static void Restore()
        {
            Require(!EditorApplication.isPlayingOrWillChangePlaymode, "Restore only after Play Mode exits.");
            try { RestoreEditorScenes(); }
            finally
            {
                try
                {
                    int previous = SessionState.GetInt(Key + "builder", -1);
                    if (previous >= 0)
                    {
                        var settings = AddressableAssetSettingsDefaultObject.GetSettings(false);
                        settings.ActivePlayModeDataBuilderIndex = previous; EditorUtility.SetDirty(settings); AssetDatabase.SaveAssetIfDirty(settings);
                        SessionState.SetInt(Key + "builder", -1);
                    }
                }
                finally { SessionState.SetBool(Key + "active", false); EditorApplication.update -= Tick; }
            }
        }
        private static string Output => SessionState.GetString(Key + "output", "");
        private static void Write(JObject value) => File.WriteAllText(Path.Combine(Output, "report.json"), value.ToString());
        private static JObject Failure(Exception error)
        {
            var value = report ?? new JObject();
            value["status"] = "FAIL"; value["error"] = error.ToString(); value["builder_and_scene_restored"] = false;
            value["editor_before"] = JObject.Parse(SessionState.GetString(Key + "editor_before", "{}"));
            try { value["failure_runtime"] = RuntimeSnapshot(); }
            catch (Exception snapshotError) { value["failure_snapshot_error"] = snapshotError.ToString(); }
            return value;
        }
        private static void Finish(JObject value) { if (SessionState.GetBool(Key + "done", false)) return; Write(value); SessionState.SetBool(Key + "done", true); EditorApplication.ExitPlaymode(); }
        private static void Require(bool condition, string message) { if (!condition) throw new InvalidOperationException(message); }

    }
}
