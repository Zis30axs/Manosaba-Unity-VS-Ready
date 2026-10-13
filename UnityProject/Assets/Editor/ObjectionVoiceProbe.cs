// Narrow built-content objection voice probe; all save handlers are in memory.
// Shared Boot/presenter setup follows the independently verified epilogue probe.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
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
    public static class ObjectionVoiceProbe
    {
        private const string Key = "Manosaba.ObjectionVoiceProbe.";
        private const string GamePrefab = "Assets/GameContent/#WitchTrials/Prefabs/General/Core/Game.prefab";
        private const string ScriptPath = "Act02_Chapter06/Act02_Chapter06_Trial09";
        private const string Ema = "CommonTrial_Ema05122", Hiro = "CommonTrial_Hiro06083";
        private const string PairEma = "CommonTrial_Ema06197", PairHiro = "CommonTrial_Hiro06086";
        private static JObject report, savedHashes;
        private static readonly List<AsyncOperationHandle> handles = new();
        private static readonly Dictionary<string, AudioClip> clips = new();
        private static IScriptPlayer player;
        private static IAudioManager audio;
        private static IStateManager state;
        private static MonoBehaviour entrySequence;
        private static FireworksEpilogueProbe.MemoryGame memory;
        private static int savedWrites;
        private static string callbackFailure, activeCase;
        [MenuItem("Manosaba/Verify Objection Voices (Isolated Saves, Built Content)")]
        public static void Run()
        {
            if (SessionState.GetBool(Key + "active", false)) throw new InvalidOperationException("Objection voice probe is already active.");
            string repo = Path.GetFullPath(Path.Combine(Application.dataPath, "../.."));
            string output = Path.Combine(repo, "tools/unity-rebuild/out/unity/objection-voice", DateTime.UtcNow.ToString("yyyyMMdd-HHmmss-fff"));
            Directory.CreateDirectory(output);
            SessionState.SetString(Key + "output", output);
            SessionState.SetInt(Key + "builder", -1);
            SessionState.SetBool(Key + "scene_opened", false);
            handles.Clear(); clips.Clear(); callbackFailure = null; activeCase = "starting";
            try
            {
                Require(!EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isCompiling, "Stop Play Mode and finish compilation first.");
                Require(EditorSceneManager.playModeStartScene == null, "Clear the Play Mode start scene first.");
                Require(SystemInfo.graphicsDeviceType != GraphicsDeviceType.Null, "This probe requires graphics; omit -nographics.");
                for (int i = 0; i < SceneManager.sceneCount; i++)
                {
                    var scene = SceneManager.GetSceneAt(i);
                    Require(scene.isLoaded && !scene.isDirty && scene.rootCount == 0, "Only clean empty scenes are accepted; user scenes are never closed.");
                }
                Require(!Engine.Initialized && !Engine.Initializing && !Configuration.GetOrDefault<EngineConfiguration>().InitializeOnApplicationLoad,
                    "Automatic Naninovel initialization would bypass save isolation.");
                JObject plan = SwitchStoryContentBuilder.ReadValidatedPlan();
                SessionState.SetString(Key + "signature", (string)plan["signature"]);
                SessionState.SetString(Key + "game_guid", AssetDatabase.AssetPathToGUID(GamePrefab));
                var settings = AddressableAssetSettingsDefaultObject.GetSettings(false);
                Require(settings, "Addressables settings are missing.");
                int existing = settings.DataBuilders.FindIndex(x => x is IDataBuilder b && b.Name.IndexOf("Existing Build", StringComparison.OrdinalIgnoreCase) >= 0);
                Require(existing >= 0, "Existing Build mode is missing.");
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
                SessionState.SetFloat(Key + "deadline", (float)EditorApplication.timeSinceStartup + 420f);
                Hook(); EditorApplication.EnterPlaymode();
            }
            catch (Exception error) { Write(Failure(error)); Restore(); if (Application.isBatchMode) EditorApplication.Exit(1); }
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
                var saved = JObject.Parse(File.ReadAllText(Path.Combine(Output, "report.json")));
                try { Restore(); saved["builder_and_scene_restored"] = true; }
                catch (Exception error) { saved["status"] = "FAIL"; saved["restore_error"] = error.ToString(); }
                Write(saved);
                Debug.Log("[ObjectionVoiceProbe] " + saved["status"] + " " + Output);
                if (Application.isBatchMode) EditorApplication.Exit((string)saved["status"] == "PASS" ? 0 : 1);
                return;
            }
            if (EditorApplication.timeSinceStartup > SessionState.GetFloat(Key + "deadline", 0)) { Finish(Failure(new TimeoutException("Objection voice probe exceeded 420 seconds."))); return; }
            if (!EditorApplication.isPlaying || EditorApplication.isCompiling || SessionState.GetBool(Key + "started", false)) return;
            SessionState.SetBool(Key + "started", true); Execute();
        }


        private static async void Execute()
        {
            report = new JObject { ["status"] = "RUNNING", ["generated_utc"] = DateTime.UtcNow.ToString("o"),
                ["signature"] = SessionState.GetString(Key + "signature", ""), ["built_assets"] = new JArray(),
                ["cases"] = new JArray(), ["screenshots"] = new JArray(), ["errors"] = new JArray(),
                ["save_isolation"] = "Real Boot presenters and built Game prefab; all three save handlers are clone-on-read/write memory handlers. No user save is loaded.",
                ["scope"] = "Only the reviewed built Trial09 objection commands and their real shared subroutine run. A pre-execution boundary stops before subsequent story commands.",
                ["builder_and_scene_restored"] = false };
            Application.logMessageReceived += Log;
            object localePresenter = null;
            GameObject game = null;
            try
            {
                Require(!Engine.Initialized && !Engine.Initializing, "Boot bypassed memory-save isolation.");
                var plan = SwitchStoryContentBuilder.ReadValidatedPlan();
                Require((string)plan["signature"] == (string)report["signature"], "Recipe changed during probe.");
                var init = Addressables.InitializeAsync(false); await init.Task; Addressables.Release(init);
                var script = await Load<Script>("Naninovel/Scripts/" + ScriptPath);
                await Load<Script>("Naninovel/Scripts/System/System_Subroutine");
                foreach (string path in new[] { Ema, Hiro, PairEma, PairHiro }) clips[path] = await Load<AudioClip>("Naninovel/Voice/" + path);
                foreach (string kind in new[] { "Ema", "Hiro", "w" }) await Load<GameObject>("Naninovel/Spawn/ObjectionCutIn_" + kind);
                var prefab = await Load<GameObject>(SessionState.GetString(Key + "game_guid", ""));
                localePresenter = Activator.CreateInstance(RuntimeType("WitchTrials.Presenters.DefaultLocalePresenter"));
                await AwaitForeign(Call(localePresenter, "LoadAsync"));
                Require(!prefab.GetComponentsInChildren<RuntimeInitializer>(true).Any(), "Built Game has an unsafe automatic initializer.");
                game = Object.Instantiate(prefab);
                var provider = new ProjectConfigurationProvider();
                var config = provider.GetConfiguration<StateConfiguration>();
                config.GameStateHandler = typeof(FireworksEpilogueProbe.MemoryGame).AssemblyQualifiedName;
                config.GlobalStateHandler = typeof(FireworksEpilogueProbe.MemoryGlobal).AssemblyQualifiedName;
                config.SettingsStateHandler = typeof(FireworksEpilogueProbe.MemorySettings).AssemblyQualifiedName;
                provider.GetConfiguration<EngineConfiguration>().ShowInitializationUI = false;
                provider.GetConfiguration<EngineConfiguration>().EnableBridging = false;
                provider.GetConfiguration<ScriptsConfiguration>().InitializationScript = null;
                provider.GetConfiguration<MoviesConfiguration>().PlayIntroMovie = false;
                await RuntimeInitializer.Initialize(provider);
                state = Engine.GetServiceOrErr<IStateManager>();
                memory = state.GameSlotManager as FireworksEpilogueProbe.MemoryGame;
                Require(memory != null && state.GlobalSlotManager is FireworksEpilogueProbe.MemoryGlobal
                    && state.SettingsSlotManager is FireworksEpilogueProbe.MemorySettings, "Memory handlers were not installed.");
                player = Engine.GetServiceOrErr<IScriptPlayer>(); audio = Engine.GetServiceOrErr<IAudioManager>();
                entrySequence = game.GetComponentsInChildren<MonoBehaviour>(true).Single(x => x && x.GetType().FullName == "WitchTrials.Views.EntrySequence");
                await AwaitForeign(Call(entrySequence, "PlayInitialSequenceAsync"));
                await Until(() => Title() && Get<bool>(Title(), "Visible") && player.PlayedScript?.Path == "System/System_Title", 60, "Real title did not appear.");
                await Task.Delay(3200);
                await state.SaveGame("ObjectionVoiceProbeExisting");
                savedHashes = memory.Hashes(); savedWrites = memory.Writes;
                report["memory_slots_before"] = savedHashes.DeepClone();
                int ema = FindCall(script, "Ema", Ema), hiro = FindCall(script, "Hiro", Hiro);
                int pair = script.Playlist.Select((c, i) => (c, i)).Single(x => x.c.GetType().Name == "PlayObjectionVoicePair").i;
                int dual = FindCall(script, "w", null);
                Require(dual == pair + 1, "Double voice pair must immediately precede the source w cut-in call.");
                Require(Parameter<string>(script.Playlist[pair], "VoicePath") == PairEma
                    && Parameter<string>(script.Playlist[pair], "SecondVoicePath") == PairHiro,
                    "Built paired-voice command does not contain the two reviewed source clips.");
                VerifyRecipeCalls(plan, script, ema, hiro);
                player.OnCommandExecutionStart += Trace;
                await RunSegment(script, ema, ema, "Ema", new[] { Ema }, "ema-built", "natural");
                await RunSegment(script, hiro, hiro, "Hiro", new[] { Hiro }, "hiro-built", "natural");
                await RunSegment(script, pair, dual, "w", new[] { PairEma, PairHiro }, "double-built", "natural");
                await RunSegment(script, pair, dual, "w", new[] { PairEma, PairHiro }, "double-stop-voice", "stop");
                await RunSegment(script, pair, dual, "w", new[] { PairEma, PairHiro }, "double-return-to-title", "exit");
                await RunSegment(script, pair, dual, "w", new[] { PairEma, PairHiro }, "double-skip", "skip");
                await RunSegment(script, pair, dual, "w", new[] { PairEma, PairHiro }, "double-repeat-same-paths", "replace");
                await RunSegment(script, pair, dual, "w", new[] { PairEma, PairHiro }, "double-replaced-by-one-member", "replace-single");
                await RunSegment(script, ema, ema, "Ema", new[] { Ema }, "ema-return-to-title", "exit");
                await RunSegment(script, hiro, hiro, "Hiro", new[] { Hiro }, "hiro-skip", "skip");
                await VerifyOptionalFields(script, ema);
                await VerifySingleReplacement();
                Require(memory.Writes == savedWrites && JToken.DeepEquals(savedHashes, memory.Hashes()), "Objection playback changed a memory game slot.");
                report["memory_slots_after"] = memory.Hashes();
                Require(((JArray)report["errors"]).Count == 0, "Runtime errors were logged.");
                report["scope_limits"] = "No entire trial is played, no physical audio listening is inferred from AudioSource state, and cold-load cancellation racing a later voice group is not simulated. Author/volume and absent VoicePath cases temporarily edit only the already-loaded command fields, restored in finally; no Script asset is saved.";
                report["status"] = "PASS";
            }
            catch (Exception error) { report["status"] = "FAIL"; report["error"] = error.ToString(); report["failure_runtime"] = RuntimeSnapshot(); }
            finally
            {
                if (player != null) player.OnCommandExecutionStart -= Trace;
                try
                {
                    if (Engine.Initialized || Engine.Initializing) Engine.Destroy();
                    if (game) Object.DestroyImmediate(game);
                    (localePresenter as IDisposable)?.Dispose();
                    foreach (var handle in handles) if (handle.IsValid()) Addressables.Release(handle);
                    handles.Clear();
                }
                catch (Exception error) { report["cleanup_error"] = error.ToString(); report["status"] = "FAIL"; }
                Application.logMessageReceived -= Log;
                Finish(report);
            }
        }

        private static int FindCall(Script script, string kind, string voice)
        {
            var calls = script.Playlist.Select((c, i) => (c, i)).Where(x => x.c.GetType().Name == "GosubToObjectionCutIn"
                && Parameter<string>(x.c, "CutInKind") == kind && Parameter<string>(x.c, "VoicePath") == voice).ToArray();
            Require(calls.Length == 1, "Expected one built cut-in call for " + kind + "/" + voice);
            return calls[0].i;
        }
        private static T Parameter<T>(Command command, string field)
        {
            var parameter = command.GetType().GetField(field)?.GetValue(command);
            if (parameter == null || !Get<bool>(parameter, "HasValue")) return default;
            return Get<T>(parameter, "Value");
        }
        private static void VerifyRecipeCalls(JObject plan, Script script, params int[] indices)
        {
            var scenario = ((JArray)plan["scenarios"]).OfType<JObject>().Single(x => ((string)x["target"]).EndsWith("/Act02_Chapter06_Trial09.asset", StringComparison.Ordinal));
            var evidence = (JArray)scenario["merge"]?["objection_voice_calls"];
            Require(evidence != null, "Story recipe lacks the new objection voice evidence.");
            var result = new JArray();
            foreach (int index in indices)
            {
                var command = script.Playlist[index];
                string voice = Parameter<string>(command, "VoicePath");
                var row = evidence.OfType<JObject>().Single(x => (string)x["voice_path"] == voice);
                Require((int)row["merged_command"] == index, "Built voice command index differs from the validated recipe.");
                Require(Mathf.Approximately(Parameter<float>(command, "Volume"), 1f)
                    && string.IsNullOrEmpty(Parameter<string>(command, "AuthorId")) && string.IsNullOrEmpty(Parameter<string>(command, "GroupPath")),
                    "Built source voice parameters differ from reviewed Volume 1 and unassigned author/group.");
                result.Add(new JObject { ["index"] = index, ["kind"] = Parameter<string>(command, "CutInKind"),
                    ["voice_path"] = voice, ["volume"] = Parameter<float>(command, "Volume"),
                    ["author"] = Parameter<string>(command, "AuthorId"), ["group"] = Parameter<string>(command, "GroupPath") });
            }
            report["built_script_voice_calls"] = result;
            string repo = Path.GetFullPath(Path.Combine(Application.dataPath, "../.."));
            string sourcePath = Path.GetFullPath(Path.Combine(repo, (string)scenario["source"]));
            Require(sourcePath.StartsWith(repo + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase), "Voice recipe source escaped the repository.");
            var source = JObject.Parse(File.ReadAllText(sourcePath));
            report["built_pair_parameters"] = SwitchStoryContentBuilder.VerifyObjectionVoicePairs(script, source, scenario);
        }

        private static async Task RunSegment(Script script, int first, int last, string kind, string[] voicePaths, string name, string action,
            float expectedVolume = 1f, bool preserveSentinel = false)
        {
            activeCase = name; report["stage"] = name; Write(report);
            player.SetSkipEnabled(false); player.Stop(); await state.ResetState();
            var debugVariables = RestoreEntryVariables();
            var title = Title();
            var manager = title.GetType().GetProperty("WitchTrialsNaninovelManager", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(title);
            manager.GetType().GetMethod("SetTitleScreenShown").Invoke(manager, new object[] { false });
            // System_Title.StartGame also hides TitleUI; the manager flag alone does not hide its canvas.
            var titleUI = Engine.GetServiceOrErr<IUIManager>().GetUI("TitleUI");
            Require(titleUI != null, "The real TitleUI is unavailable.");
            await titleUI.ChangeVisibility(false, 0);
            await AsyncUtils.WaitEndOfFrame();
            RequireTitleHidden();
            player.SetWaitingForInputEnabled(false);
            AudioTrack sentinel = null;
            var row = new JObject { ["name"] = name, ["script"] = script.Path, ["first_index"] = first, ["cutin_index"] = last,
                ["kind"] = kind, ["action"] = action, ["samples"] = new JArray(), ["status"] = "RUNNING",
                ["command_author"] = Parameter<string>(script.Playlist[first], "AuthorId"), ["expected_source_volume"] = expectedVolume,
                ["entry_debug_variables"] = debugVariables };
            ((JArray)report["cases"]).Add(row);
            bool boundary = false, firstAudio = false, together = false, acted = false, captured = false, replaced = false;
            double started = EditorApplication.timeSinceStartup, firstAudioTime = -1, firstSpawnTime = -1;
            int overlapFrames = 0;
            int retainedAudioSamples = 0;
            var firstFrames = new Dictionary<string, int>();
            var firstSamples = new Dictionary<string, int>();
            var largestSamples = new Dictionary<string, int>();
            var peakRms = new Dictionary<string, float>();
            var lastSample = new JObject();
            UniTask Boundary(Command command)
            {
                if (preserveSentinel && command.PlaybackSpot.ScriptPath == script.Path && player.PlayedIndex == first)
                {
                    RequireLegacyVoice(sentinel, "before unassigned VoicePath command");
                    row["legacy_at_command_entry"] = VoiceSample(Hiro);
                }
                if (preserveSentinel && command.PlaybackSpot.ScriptPath == "System/System_Subroutine" && row["legacy_after_navigation"] == null)
                {
                    RequireLegacyVoice(sentinel, "after unassigned VoicePath navigation");
                    row["legacy_after_navigation"] = VoiceSample(Hiro);
                }
                if (command.PlaybackSpot.ScriptPath == script.Path && player.PlayedIndex > last)
                { boundary = true; player.Stop(); }
                return UniTask.CompletedTask;
            }
            player.AddPreExecutionTask(Boundary);
            try
            {
                // This is LoadAndPlay's official sequence, split so the short legacy sentinel
                // starts after loading instead of expiring during several seconds of preload.
                double loadStart = EditorApplication.timeSinceStartup;
                await Engine.GetServiceOrErr<IScriptLoader>().Load(script.Path, first);
                row["preload_seconds"] = EditorApplication.timeSinceStartup - loadStart;
                if (preserveSentinel)
                {
                    await audio.PlayVoice(Hiro); sentinel = audio.GetVoiceTrack(Hiro) as AudioTrack;
                    RequireLegacyVoice(sentinel, "after preload");
                    row["legacy_sentinel_clip_seconds"] = clips[Hiro].length;
                }
                started = EditorApplication.timeSinceStartup;
                player.Play(script.Path, first);
                double deadline = EditorApplication.timeSinceStartup + 25;
                while (!boundary)
                {
                    await AsyncUtils.WaitEndOfFrame();
                    CheckCallback();
                    Require(EditorApplication.timeSinceStartup < deadline, "Cut-in segment timed out: " + name);
                    var spawns = Engine.GetServiceOrErr<ISpawnManager>();
                    var spawned = spawns.IsSpawned("ObjectionCutIn_" + kind) ? spawns.GetSpawned("ObjectionCutIn_" + kind)?.GameObject : null;
                    bool active = spawned && spawned.activeInHierarchy;
                    var camera = Engine.GetServiceOrErr<ICameraManager>().Camera;
                    var planes = GeometryUtility.CalculateFrustumPlanes(camera);
                    var glass = active ? spawned.GetComponentsInChildren<SpriteRenderer>(false).Where(x => x.enabled && x.sprite
                        && x.color.a > .001f && (x.name == "RefuteCutIn_StainedGlass_001" || x.name == "RefuteCutIn_StainedGlass_002" || x.name == "RefuteCutIn_StainedGlass_003")
                        && GeometryUtility.TestPlanesAABB(planes, x.bounds)).ToArray() : Array.Empty<SpriteRenderer>();
                    bool visible = glass.Length > 0;
                    var portraits = active ? spawned.GetComponentsInChildren<SpriteRenderer>(false).Where(x => x.enabled && x.sprite
                        && x.color.a > .01f && IsPortrait(x.name, kind) && GeometryUtility.TestPlanesAABB(planes, x.bounds)).ToArray()
                        : Array.Empty<SpriteRenderer>();
                    bool portraitsVisible = kind == "w" ? portraits.Any(x => x.name.StartsWith("RefuteCutIn_Ema_", StringComparison.Ordinal))
                        && portraits.Any(x => x.name.StartsWith("RefuteCutIn_Hiro_", StringComparison.Ordinal)) : portraits.Length > 0;
                    var sources = voicePaths.Select(path => Sources(path).Where(x => x.isPlaying).ToArray()).ToArray();
                    bool simultaneous = voicePaths.Length > 0 && sources.All(x => x.Length == 1)
                        && sources.Select(x => x[0].GetInstanceID()).Distinct().Count() == voicePaths.Length;
                    double now = EditorApplication.timeSinceStartup;
                    for (int i = 0; i < voicePaths.Length; i++)
                    {
                        if (sources[i].Length != 1) continue;
                        string path = voicePaths[i]; var source = sources[i][0];
                        if (!firstFrames.ContainsKey(path)) { firstFrames[path] = Time.frameCount; firstSamples[path] = source.timeSamples; }
                        largestSamples[path] = Mathf.Max(largestSamples.GetValueOrDefault(path), source.timeSamples);
                        peakRms[path] = Mathf.Max(peakRms.GetValueOrDefault(path), OutputRms(source));
                    }
                    if (voicePaths.Length == 2 && firstAudio)
                    {
                        var remaining = voicePaths.Where(path => Sources(path).Any(x => x.isPlaying)).ToArray();
                        if (remaining.Length == 1 && action == "natural")
                        {
                            Require(audio.GetPlayedVoice() == remaining[0], "The longer paired voice became unobservable after the shorter one ended.");
                            row["longer_voice_remained_observable"] = remaining[0];
                        }
                    }
                    if (simultaneous && !firstAudio) { firstAudio = true; firstAudioTime = now; }
                    if (visible && firstSpawnTime < 0) firstSpawnTime = now;
                    var director = active ? spawned.GetComponentsInChildren<PlayableDirector>(true).FirstOrDefault() : null;
                    lastSample = new JObject { ["frame"] = Time.frameCount, ["elapsed"] = now - started,
                        ["spawned"] = active, ["visible_glass_renderers"] = new JArray(glass.Select(x => x.name)),
                        ["visible_portraits"] = new JArray(portraits.Select(x => x.name)),
                        ["timeline_time"] = director ? director.time : (double?)null,
                        ["simultaneous"] = simultaneous, ["voices"] = new JArray(voicePaths.Select(path => VoiceSample(path))) };
                    // Retain first frame, on-screen overlap, action and failure evidence without flooding the report.
                    bool retainAudio = simultaneous && retainedAudioSamples < 3;
                    if (((JArray)row["samples"]).Count == 0 || retainAudio || (simultaneous && visible && overlapFrames < 3))
                        ((JArray)row["samples"]).Add(lastSample);
                    if (retainAudio) retainedAudioSamples++;
                    // Keep early audio/glass timing checks, but capture the developed composition.
                    // Renderer/frustum checks are structural evidence; retain the PNG for pixel review.
                    if (!captured && visible && portraitsVisible && director && director.time >= .65
                        && (simultaneous || preserveSentinel))
                    {
                        RequireTitleHidden();
                        await Capture(name);
                        captured = true;
                        row["capture_state"] = new JObject { ["frame"] = Time.frameCount, ["timeline_time"] = director.time,
                            ["portrait_names"] = new JArray(portraits.Select(x => x.name)), ["title_hidden"] = true,
                            ["voices"] = new JArray((preserveSentinel ? new[] { Hiro } : voicePaths).Select(VoiceSample)) };
                    }
                    if (simultaneous && visible && !acted)
                    {
                        together = true; overlapFrames++;
                        var voiceGroup = audio.AudioMixer.FindMatchingGroups(audio.Configuration.VoiceGroupPath).FirstOrDefault();
                        Require(voiceGroup, "Configured voice mixer group is absent.");
                        foreach (var source in sources.SelectMany(x => x))
                        {
                            Require(Mathf.Abs(source.volume - expectedVolume) < .0001f, "Voice gain differs from command/author gain in " + name);
                            Require(source.outputAudioMixerGroup == voiceGroup, "Voice bypassed the configured Voice mixer group in " + name);
                        }
                        Require(voicePaths.All(path => audio.IsVoicePlaying(path)), "AudioManager does not recognize every audible voice in " + name);
                        Require(voicePaths.All(path => audio.GetVoiceTrack(path) is AudioTrack track && track.Clip == clips[path]
                            && track.Source && track.Source.isPlaying && track.Source.clip == track.Clip),
                            "Registered voice track does not own the actual playing source in " + name);
                        if (captured && overlapFrames >= 3 && now - firstAudioTime >= .10)
                        {
                            if (action == "stop") { audio.StopVoice(); acted = true; await AsyncUtils.WaitEndOfFrame(); RequireSilent(voicePaths, name); }
                            else if (action == "exit")
                            {
                                acted = true;
                                var command = (Command)Activator.CreateInstance(RuntimeType("WitchTrials.Views.ReturnToTitle"));
                                command.GetType().GetField("Wait").SetValue(command, (BooleanParameter)true);
                                await command.Execute();
                                await Until(() => player.PlayedScript?.Path == "System/System_Title" && Title() && Get<bool>(Title(), "Visible"), 40, "Voice early exit did not return to title.");
                                RequireSilent(voicePaths, name); boundary = true;
                            }
                            else if (action == "skip") { player.SkipMode = PlayerSkipMode.Everything; player.SetSkipEnabled(true); acted = true; }
                            else if (action == "replace" && !replaced)
                            {
                                await script.Playlist[first].Execute(); replaced = true;
                                await AsyncUtils.WaitEndOfFrame();
                                Require(voicePaths.All(path => Sources(path).Count(x => x.isPlaying) == 1 && audio.IsVoicePlaying(path)), "Replacing the same pair stopped or duplicated its new group.");
                                row["same_paths_replacement"] = new JArray(voicePaths.Select(VoiceSample));
                            }
                            else if (action == "replace-single")
                            {
                                await audio.PlayVoice(PairEma); acted = true;
                                await AsyncUtils.WaitEndOfFrame();
                                Require(audio.IsVoicePlaying(PairEma) && Sources(PairEma).Count(x => x.isPlaying) == 1,
                                    "A normal same-path voice did not survive replacing the pair.");
                                RequireSilent(new[] { PairHiro }, name);
                                row["pair_to_single"] = new JArray(new[] { PairEma, PairHiro }.Select(VoiceSample));
                            }
                        }
                    }
                    if (preserveSentinel && visible && portraitsVisible && captured && !acted)
                    {
                        RequireLegacyVoice(sentinel, "on-screen unassigned VoicePath cut-in");
                        Require(!audio.IsVoicePlaying(Ema), "An unassigned VoicePath created unexpected voice playback.");
                        row["legacy_prior_voice_preserved"] = VoiceSample(Hiro); acted = true;
                    }
                }
                if (voicePaths.Length > 0) Require(together && overlapFrames >= 3, "Did not observe real on-screen voice playback for " + name);
                else Require(acted, "Legacy unassigned VoicePath case was not observed on screen.");
                Require(captured, "No developed portrait composition was captured during voice playback: " + name);
                if (preserveSentinel) Require(row["legacy_at_command_entry"] != null && row["legacy_after_navigation"] != null,
                    "Legacy voice command entry/navigation boundaries were not observed.");
                if (action == "replace") Require(replaced, "Same-path replacement was not exercised.");
                row["first_voice_elapsed"] = firstAudioTime < 0 ? null : firstAudioTime - started;
                row["first_spawn_elapsed"] = firstSpawnTime < 0 ? null : firstSpawnTime - started;
                row["voice_to_spawn_seconds"] = firstAudioTime < 0 || firstSpawnTime < 0 ? null : firstSpawnTime - firstAudioTime;
                row["overlap_frames"] = overlapFrames;
                row["voice_progress"] = new JArray(voicePaths.Select(path => new JObject {
                    ["path"] = path, ["first_playing_frame"] = firstFrames.GetValueOrDefault(path),
                    ["first_time_samples"] = firstSamples.GetValueOrDefault(path), ["max_time_samples"] = largestSamples.GetValueOrDefault(path),
                    ["peak_output_rms"] = peakRms.GetValueOrDefault(path) }));
                if (voicePaths.Length > 0)
                    Require(voicePaths.All(path => largestSamples.GetValueOrDefault(path) > firstSamples.GetValueOrDefault(path)),
                        "A playing voice's sample position did not advance in " + name);
                if (voicePaths.Length == 2)
                    Require(firstFrames.Values.Distinct().Count() == 1, "Paired voice sources did not first appear in the same rendered frame.");
                row["rms_scope"] = "Per-source GetOutputData observation; nonzero RMS is recorded when batch audio mixes output, but unavailable/silent batch output is not an OS-audio failure.";
                row["boundary_reached"] = boundary;
                player.SetSkipEnabled(false); await state.ResetState();
                RestoreEntryVariables();
                RequireSilent(voicePaths.Concat(new[] { Ema, Hiro, PairEma, PairHiro }).Distinct(), name + " reset");
                row["reset_cleared_all_voices"] = true; row["status"] = "PASS";
            }
            catch { row["last_sample"] = lastSample; row["failure_runtime_before_stop"] = RuntimeSnapshot(); row["status"] = "FAIL"; throw; }
            finally { player.RemovePreExecutionTask(Boundary); player.SetSkipEnabled(false); player.Stop(); }
        }

        private static bool IsPortrait(string name, string kind)
        {
            foreach (var character in kind == "w" ? new[] { "Ema", "Hiro" } : new[] { kind })
                foreach (var stage in new[] { "001", "002", "003" })
                    if (name == "RefuteCutIn_" + character + "_" + stage) return true;
            return false;
        }

        private static void RequireLegacyVoice(AudioTrack sentinel, string phase)
        {
            Require(sentinel?.Source && sentinel.Source.isPlaying && sentinel.Source.clip == clips[Hiro]
                && ReferenceEquals(audio.GetVoiceTrack(Hiro), sentinel), "Legacy prior voice was stopped/replaced " + phase);
        }

        private static void RequireTitleHidden()
        {
            var title = Title();
            Require(title && !Get<bool>(title, "Visible"), "Title UI remains visible in the cut-in fixture.");
            var canvas = title.GetComponent<CanvasGroup>();
            Require(canvas && canvas.alpha <= .001f && !canvas.blocksRaycasts, "Title canvas still obscures the cut-in fixture.");
        }

        private static JObject RestoreEntryVariables()
        {
            // ResetState removes local variables. A direct jump into the real shared cut-in
            // subroutine must reproduce EntrySequence's normal typed initialization first.
            Require(entrySequence, "The real EntrySequence is unavailable after reset.");
            var method = entrySequence.GetType().GetMethod("ResetDebugVariables", BindingFlags.Instance | BindingFlags.NonPublic);
            Require(method != null, "EntrySequence.ResetDebugVariables is missing.");
            method.Invoke(entrySequence, null);
            var variables = Engine.GetServiceOrErr<ICustomVariableManager>();
            var values = new JObject();
            foreach (var name in new[] { "allowRollback", "allowContinueDebate", "isDebugDirectJump", "enableDebugSkip", "isDemo" })
            {
                Require(variables.VariableExists(name), "Entry initialization omitted " + name);
                var value = variables.GetVariableValue(name);
                Require(value.Type == CustomVariableValueType.Boolean, "Entry variable is not boolean: " + name);
                values[name] = value.Boolean;
            }
            Require(!(bool)values["enableDebugSkip"] && !(bool)values["isDemo"], "Probe requires the normal non-skipping game configuration.");
            return values;
        }

        private static async Task VerifyOptionalFields(Script script, int index)
        {
            var command = script.Playlist[index];
            var names = new[] { "VoicePath", "Volume", "AuthorId", "GroupPath" };
            var fields = names.Select(x => command.GetType().GetField(x)).ToArray();
            var originals = fields.Select(x => x.GetValue(command)).ToArray();
            float previous = audio.GetAuthorVolume("Ema");
            try
            {
                fields[1].SetValue(command, (DecimalParameter).6f);
                fields[2].SetValue(command, (StringParameter)"Ema");
                audio.SetAuthorVolume("Ema", .5f);
                await RunSegment(script, index, index, "Ema", new[] { Ema }, "ema-author-volume", "natural", .3f);
                fields[0].SetValue(command, null);
                await RunSegment(script, index, index, "Ema", Array.Empty<string>(), "legacy-no-voice-path", "natural", preserveSentinel: true);
            }
            finally
            {
                for (int i = 0; i < fields.Length; i++) fields[i].SetValue(command, originals[i]);
                audio.SetAuthorVolume("Ema", previous);
            }
            Require(fields.Select((x, i) => ReferenceEquals(x.GetValue(command), originals[i])).All(x => x), "Temporary command fields were not restored.");
            report["temporary_parameter_fields_restored"] = true;
        }
        private static async Task VerifySingleReplacement()
        {
            activeCase = "single-repeated-same-path";
            await audio.PlayVoice(Hiro);
            await AsyncUtils.WaitEndOfFrame();
            Require(audio.GetVoiceTrack(Hiro) is AudioTrack before && before.Playing, "Initial single voice did not start.");
            await audio.PlayVoice(Hiro);
            await AsyncUtils.WaitEndOfFrame();
            Require(audio.IsVoicePlaying(Hiro) && Sources(Hiro).Count(x => x.isPlaying) == 1,
                "Repeated same-path single voice was released or duplicated during replacement.");
            report["single_repeated_same_path"] = VoiceSample(Hiro);
            audio.StopVoice(); await AsyncUtils.WaitEndOfFrame(); RequireSilent(new[] { Hiro }, activeCase);
        }
        private static AudioSource[] Sources(string path) => Object.FindObjectsByType<AudioSource>(FindObjectsInactive.Include, FindObjectsSortMode.None)
            .Where(x => x && x.clip == clips[path]).ToArray();
        private static float OutputRms(AudioSource source)
        {
            var values = new float[512]; source.GetOutputData(values, 0);
            double sum = 0; foreach (float value in values) sum += value * value;
            return (float)Math.Sqrt(sum / values.Length);
        }
        private static JObject VoiceSample(string path) => new JObject { ["path"] = path,
            ["manager_playing"] = audio.IsVoicePlaying(path), ["track_exists"] = audio.GetVoiceTrack(path) != null,
            ["track_source_id"] = audio.GetVoiceTrack(path) is AudioTrack track && track.Source ? track.Source.GetInstanceID() : 0,
            ["sources"] = new JArray(Sources(path).Select(x => new JObject { ["id"] = x.GetInstanceID(), ["playing"] = x.isPlaying,
                ["clip"] = x.clip.name, ["volume"] = x.volume, ["time_samples"] = x.timeSamples, ["output_rms"] = OutputRms(x),
                ["mixer"] = x.outputAudioMixerGroup ? x.outputAudioMixerGroup.name : null })) };
        private static void RequireSilent(IEnumerable<string> paths, string context)
        {
            foreach (string path in paths) Require(!Sources(path).Any(x => x.isPlaying) && !audio.IsVoicePlaying(path), "Voice survived " + context + ": " + path);
        }
        private static void Trace(Command command)
        {
            var trace = report["command_trace"] as JArray;
            if (trace == null) report["command_trace"] = trace = new JArray();
            trace.Add(new JObject { ["case"] = activeCase, ["type"] = command.GetType().FullName,
                ["script"] = command.PlaybackSpot.ScriptPath, ["index"] = player.PlayedIndex, ["time"] = EditorApplication.timeSinceStartup });
            while (trace.Count > 45) trace.RemoveAt(0);
        }
        private static async Task Capture(string name)
        {
            await AsyncUtils.WaitEndOfFrame();
            var camera = Engine.GetServiceOrErr<ICameraManager>().Camera;
            var target = RenderTexture.GetTemporary(1280, 720, 24, RenderTextureFormat.ARGB32);
            var previous = camera.targetTexture; var active = RenderTexture.active; Texture2D pixels = null;
            try
            {
                camera.targetTexture = target; camera.Render(); RenderTexture.active = target;
                pixels = new Texture2D(1280, 720, TextureFormat.RGBA32, false);
                pixels.ReadPixels(new Rect(0, 0, 1280, 720), 0, 0); pixels.Apply();
                string path = Path.Combine(Output, name + ".png"); File.WriteAllBytes(path, pixels.EncodeToPNG());
                ((JArray)report["screenshots"]).Add(new JObject { ["name"] = name, ["path"] = path, ["frame"] = Time.frameCount });
            }
            finally { camera.targetTexture = previous; RenderTexture.active = active; RenderTexture.ReleaseTemporary(target); if (pixels) Object.DestroyImmediate(pixels); }
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

        private static void Restore()
        {
            Require(!EditorApplication.isPlayingOrWillChangePlaymode, "Restore only after Play Mode exits.");
            if (SessionState.GetBool(Key + "scene_opened", false)) { EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single); SessionState.SetBool(Key + "scene_opened", false); }
            int previous = SessionState.GetInt(Key + "builder", -1);
            if (previous >= 0)
            {
                var settings = AddressableAssetSettingsDefaultObject.GetSettings(false);
                settings.ActivePlayModeDataBuilderIndex = previous; EditorUtility.SetDirty(settings); AssetDatabase.SaveAssetIfDirty(settings);
                SessionState.SetInt(Key + "builder", -1);
            }
            SessionState.SetBool(Key + "active", false); EditorApplication.update -= Tick;
        }
        private static string Output => SessionState.GetString(Key + "output", "");
        private static void Write(JObject value) => File.WriteAllText(Path.Combine(Output, "report.json"), value.ToString());
        private static JObject Failure(Exception error)
        {
            var value = report ?? new JObject();
            value["status"] = "FAIL"; value["error"] = error.ToString(); value["builder_and_scene_restored"] = false;
            try { value["failure_runtime"] = RuntimeSnapshot(); }
            catch (Exception snapshotError) { value["failure_snapshot_error"] = snapshotError.ToString(); }
            return value;
        }
        private static void Finish(JObject value) { if (SessionState.GetBool(Key + "done", false)) return; Write(value); SessionState.SetBool(Key + "done", true); EditorApplication.ExitPlaymode(); }
        private static void Require(bool condition, string message) { if (!condition) throw new InvalidOperationException(message); }

    }
}
