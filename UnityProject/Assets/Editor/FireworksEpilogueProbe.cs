// A fresh bundled-content Play Mode probe. All Naninovel save handlers are
// in-memory; the Boot scene is modified only in memory and is never saved.
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

namespace Manosaba.EditorTools
{
    public static class FireworksEpilogueProbe
    {
        private const string Key = "Manosaba.FireworksEpilogueProbe.";
        private const string ScriptPath = "System/System_FireworksEpilogue";
        private const string Loop = "Bgm_40_1_Loop";
        private const string Intro = "Bgm_40_1_Intro";
        private const string GamePrefab = "Assets/GameContent/#WitchTrials/Prefabs/General/Core/Game.prefab";
        private static JObject report;
        private static readonly List<AsyncOperationHandle> handles = new();
        private static IScriptPlayer player;
        private static IAudioManager audio;
        private static Type session;
        private static readonly List<string> pages = new();
        private static readonly List<string> phases = new();
        private static AudioTrack firstTrack;
        private static double musicStart;
        private static int playEvents;
        private static string callbackFailure;
        private static float masterBefore, bgmBefore;
        private static bool observing;
        private static bool verifyAudio;
        private static bool creditsCaptured;
        private static Texture2D creditsTexture;
        private static GameObject lastEpilogueEffect;
        private const string ProbeSlot = "FireworksProbeExisting";
        private static MemoryGame memoryGame;
        private static JObject seededSlotHashes;
        private static int seededGameWrites;
        private static string quickSlot;
        private static readonly List<string> savedBgm = new();

        [MenuItem("Manosaba/Verify Fireworks Epilogue (Isolated Saves, Built Content)")]
        public static void Run()
        {
            if (SessionState.GetBool(Key + "active", false)) throw new InvalidOperationException("Epilogue probe is already active.");
            string repo = Path.GetFullPath(Path.Combine(Application.dataPath, "../.."));
            string output = Path.Combine(repo, "tools/unity-rebuild/out/unity/fireworks-epilogue", DateTime.UtcNow.ToString("yyyyMMdd-HHmmss-fff"));
            Directory.CreateDirectory(output);
            SessionState.SetString(Key + "output", output);
            SessionState.SetInt(Key + "builder", -1);
            SessionState.SetBool(Key + "scene_opened", false);
            pages.Clear(); phases.Clear(); firstTrack = null; callbackFailure = null; playEvents = 0; observing = false;
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
                JObject plan = FireworksEpilogueContentBuilder.ReadValidatedPlan();
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
                SessionState.SetFloat(Key + "deadline", (float)EditorApplication.timeSinceStartup + 720f);
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
                Debug.Log("[FireworksEpilogueProbe] " + saved["status"] + " " + Output);
                if (Application.isBatchMode) EditorApplication.Exit((string)saved["status"] == "PASS" ? 0 : 1);
                return;
            }
            if (EditorApplication.timeSinceStartup > SessionState.GetFloat(Key + "deadline", 0)) { Finish(Failure(new TimeoutException("Epilogue probe exceeded 720 seconds."))); return; }
            if (!EditorApplication.isPlaying || EditorApplication.isCompiling || SessionState.GetBool(Key + "started", false)) return;
            SessionState.SetBool(Key + "started", true); Execute();
        }

        private static async void Execute()
        {
            report = new JObject { ["status"] = "RUNNING", ["generated_utc"] = DateTime.UtcNow.ToString("o"),
                ["signature"] = SessionState.GetString(Key + "signature", ""), ["save_isolation"] = "All three handlers use TransientSaveSlotManager in memory; no user save file is opened.",
                ["boot_scope"] = "Real Boot presenters + built Game prefab + real EntrySequence. Steam/splash and GameInitializer.Start are bypassed to inject isolated configuration.",
                ["built_assets"] = new JArray(), ["title_progress"] = new JArray(), ["audio_moods"] = new JArray(),
                ["screenshots"] = new JArray(), ["errors"] = new JArray(), ["builder_and_scene_restored"] = false };
            Application.logMessageReceived += Log;
            object localePresenter = null;
            GameObject game = null;
            try
            {
                Require(!Engine.Initialized && !Engine.Initializing, "Boot started Naninovel before isolated configuration was installed.");
                Stage("loading_bundles");
                JObject plan = FireworksEpilogueContentBuilder.ReadValidatedPlan();
                Require((string)plan["signature"] == (string)report["signature"], "Recipe changed during probe.");
                session = RuntimeType("WitchTrials.Views.FireworksEpilogueSession");
                var init = Addressables.InitializeAsync(false); await init.Task; Addressables.Release(init);
                foreach (var document in ((JArray)plan["text_documents"]).OfType<JObject>())
                {
                    string address = (string)((JArray)plan["addresses"]).OfType<JObject>()
                        .Single(x => (string)x["path"] == (string)document["target"])["address"];
                    FireworksEpilogueContentBuilder.VerifyTextDocument(await Load<TextAsset>(address), document);
                }
                Script script = await Load<Script>("Naninovel/Scripts/" + ScriptPath);
                FireworksEpilogueContentBuilder.VerifyScript(script, (JObject)plan["scenario"]);
                Script ending = await Load<Script>("Naninovel/Scripts/" + FireworksEpilogueContentBuilder.EndingPath);
                var jump = ending.Playlist.Last() as Goto;
                Require(jump != null && jump.Path.Value.Name == ScriptPath, "Built ending does not route into the epilogue.");
                var emptyTexture = await Load<Texture2D>("Naninovel/Backgrounds/Stills/EpilogueEmpty");
                var fullTexture = await Load<Texture2D>("Naninovel/Backgrounds/Stills/3_1");
                report["background_texture_dimensions"] = new JObject {
                    ["empty"] = new JArray(emptyTexture.width, emptyTexture.height),
                    ["full_cg"] = new JArray(fullTexture.width, fullTexture.height) };
                Require(emptyTexture.width == fullTexture.width && emptyTexture.height == fullTexture.height
                    && fullTexture.width == 2048 && fullTexture.height == 1024,
                    "Built empty and full CG textures must both be 2048 x 1024 for a stable reveal canvas.");
                await Load<GameObject>("Naninovel/Spawn/TitleScreenEffect_3");
                creditsTexture = await Load<Texture2D>("Naninovel/FireworksEpilogue/SourcePoster");
                Require(creditsTexture.width == 1260 && creditsTexture.height == 709, "Original attribution poster dimensions changed.");
                AudioClip intro = await Load<AudioClip>("Naninovel/Audio/" + Intro);
                await Load<AudioClip>("Naninovel/Audio/" + Loop);
                var prefab = await Load<GameObject>(SessionState.GetString(Key + "game_guid", ""));
                Stage("initializing_real_services_with_memory_saves");
                localePresenter = Activator.CreateInstance(RuntimeType("WitchTrials.Presenters.DefaultLocalePresenter"));
                await AwaitForeign(Call(localePresenter, "LoadAsync"));
                Require(!prefab.GetComponentsInChildren<RuntimeInitializer>(true).Any(), "Game prefab must not contain an automatic RuntimeInitializer; check before Instantiate can invoke Awake.");
                game = Object.Instantiate(prefab);
                var provider = new ProjectConfigurationProvider();
                var stateConfig = provider.GetConfiguration<StateConfiguration>();
                stateConfig.GameStateHandler = typeof(MemoryGame).AssemblyQualifiedName;
                stateConfig.GlobalStateHandler = typeof(MemoryGlobal).AssemblyQualifiedName;
                stateConfig.SettingsStateHandler = typeof(MemorySettings).AssemblyQualifiedName;
                provider.GetConfiguration<EngineConfiguration>().ShowInitializationUI = false;
                provider.GetConfiguration<EngineConfiguration>().EnableBridging = false;
                provider.GetConfiguration<ScriptsConfiguration>().InitializationScript = null;
                provider.GetConfiguration<MoviesConfiguration>().PlayIntroMovie = false;
                await RuntimeInitializer.Initialize(provider);
                var state = Engine.GetServiceOrErr<IStateManager>();
                Require(state.GameSlotManager is MemoryGame && state.GlobalSlotManager is MemoryGlobal && state.SettingsSlotManager is MemorySettings,
                    "Save isolation failed; refusing to run scenario.");
                report["save_handler_types"] = new JArray(state.GameSlotManager.GetType().Name, state.GlobalSlotManager.GetType().Name, state.SettingsSlotManager.GetType().Name);
                player = Engine.GetServiceOrErr<IScriptPlayer>(); audio = Engine.GetServiceOrErr<IAudioManager>();
                masterBefore = audio.MasterVolume; bgmBefore = audio.BgmVolume;
                var entry = game.GetComponentsInChildren<MonoBehaviour>(true).Single(x => x && x.GetType().FullName == "WitchTrials.Views.EntrySequence");
                Stage("entering_real_title");
                await AwaitForeign(Call(entry, "PlayInitialSequenceAsync"));
                report["selected_locale"] = Engine.GetServiceOrErr<ILocalizationManager>().SelectedLocale;
                await Until(() => Title() != null && Get<bool>(Title(), "Visible") && player.PlayedScript?.Path == "System/System_Title", 60, "Actual title did not appear.");
                await Task.Delay(3500);
                Require(!Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                    .Any(x => x && x.GetType().FullName == "WitchTrials.Views.SplashScreen" && x.gameObject.activeInHierarchy),
                    "Temporary Boot splash still covers the real title.");
                report["boot_splash_inactive"] = true;
                var vars = Engine.GetServiceOrErr<ICustomVariableManager>();
                Stage("checking_title_gates");
                foreach (int progress in new[] { 0, 11, 12 })
                {
                    vars.TrySetVariableValue("g_gameProgress", progress);
                    Call(Title(), "OnBeginToPresentEvent");
                    if (progress == 12) await Until(() => Get<bool>(Title(), "CanReplayFireworksEpilogue"), 20, "Epilogue entry did not unlock.");
                    await Task.Delay(150);
                    bool enabled = Get<bool>(Title(), "CanReplayFireworksEpilogue");
                    var button = ReplayButton();
                    Require(enabled == (progress >= 12) && button.gameObject.activeSelf == enabled && button.interactable == enabled, "Title progress gate is wrong at " + progress);
                    ((JArray)report["title_progress"]).Add(new JObject { ["progress"] = progress, ["can_replay"] = enabled, ["button_active"] = button.gameObject.activeSelf });
                    await Capture("title-progress-" + progress);
                    if (enabled) VerifyReplayButtonVisible();
                }
                await SeedMemorySlots(state);
                player.OnCommandExecutionStart += CommandStart;
                player.OnCommandExecutionFinish += CommandFinish;
                Stage("first_full_play");
                observing = true; verifyAudio = true; creditsCaptured = false; ReplayButton().onClick.Invoke();
                await Until(() => Active && player.PlayedScript?.Path == ScriptPath, 45, "Title button did not enter the built epilogue.");
                report["entry_button_executed"] = true;
                await CompleteFirstPlay(script, intro.length);
                report["complete_play"] = true;
                CheckGallery();
                await CheckReturned("full_play");
                VerifySeededSlots("full_play");
                observing = false; verifyAudio = false;

                Stage("early_exit_reentry");
                var delayedSave = ExecuteRuntimeCommand("AutoSave", ("DelayDuration", (DecimalParameter)20f), ("Wait", (BooleanParameter)true));
                // A second actual button entry exits during the scheduled Intro.
                ReplayButton().onClick.Invoke();
                await Until(() => Active, 30, "Repeat title entry did not begin.");
                await AdvanceToReveal();
                var abandoned = audio.GetAudioTrack(Loop) as AudioTrack;
                Require(abandoned?.Source && abandoned.IntroClip, "Repeat Intro track was not created.");
                int earlyStopEvents = 0;
                Action earlyStopped = () => earlyStopEvents++;
                abandoned.OnStop += earlyStopped;
                var returnCommand = (Command)Activator.CreateInstance(RuntimeType("WitchTrials.Views.ReturnToTitle"));
                try { await returnCommand.Execute(); await CheckReturned("early_exit"); }
                finally { abandoned.OnStop -= earlyStopped; }
                VerifyReleasedAudio(abandoned, earlyStopEvents, "early_exit");
                report["early_exit_during_intro"] = true;
                Require(!delayedSave.IsCompleted, "Delayed-save experiment expired before leaving the session; cannot prove the epoch guard.");
                await delayedSave;
                VerifySeededSlots("expired_save_after_exit");
                report["delayed_autosave_epoch_guard"] = true;

                Stage("fast_replay");
                // Fast replay uses the real player's skip state, not direct command execution.
                if (firstTrack != null) firstTrack.OnPlay -= CountPlay;
                pages.Clear(); phases.Clear(); firstTrack = null; playEvents = 0;
                observing = true; ReplayButton().onClick.Invoke();
                await Until(() => Active, 30, "Third title entry did not begin.");
                player.SkipMode = PlayerSkipMode.Everything; player.SetSkipEnabled(true);
                await Until(() => !Active && player.PlayedScript?.Path == "System/System_Title", 90, "Fast replay failed to return to title.");
                player.SetSkipEnabled(false); observing = false;
                Require(pages.Count == script.Playlist.OfType<PrintText>().Count(), "Fast replay omitted dialogue commands.");
                await CheckReturned("fast_replay");
                report["fast_replay_pages"] = pages.Count;
                Stage("memory_load_cleanup");
                await VerifyLoadCleanup(state, false);
                await VerifyLoadCleanup(state, true);
                await VerifyEndingTail(ending, PageId(script.Playlist.OfType<PrintText>().First()), fullTexture);
                VerifySeededSlots("after_load_checks");
                report["memory_write_counts"] = new JObject { ["game"] = memoryGame.Writes,
                    ["global"] = ((MemoryGlobal)state.GlobalSlotManager).Writes,
                    ["settings"] = ((MemorySettings)state.SettingsSlotManager).Writes,
                    ["seeded_game_writes"] = seededGameWrites };
                Require(((JArray)report["errors"]).Count == 0, "Unity logged runtime errors; see errors list.");
                report["status"] = "PASS";
                report["scope_limits"] = "Button listeners and Naninovel Continue/Auto/Skip inputs are exercised programmatically; real UI center raycasting is checked, but physical mouse input, OS audio listening, manual ControlPanel confirmation dialog and the missing-resource button gate are not covered. Save guards are checked at real game command/UI entrances; core LoadGame/QuickLoad use isolated pre-existing memory slots. New-game ending route is validated from the built final command; the entire main story is not replayed.";
            }
            catch (Exception error)
            {
                report["status"] = "FAIL"; report["error"] = error.ToString();
                try { report["failure_runtime"] = RuntimeSnapshot(); }
                catch (Exception snapshotError) { report["failure_snapshot_error"] = snapshotError.ToString(); }
            }
            finally
            {
                observing = false;
                if (player != null) { player.OnCommandExecutionStart -= CommandStart; player.OnCommandExecutionFinish -= CommandFinish; }
                try
                {
                    if (firstTrack != null) firstTrack.OnPlay -= CountPlay;
                    if (session != null) session.GetMethod("Exit").Invoke(null, null);
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

        private static async Task CompleteFirstPlay(Script script, float introLength)
        {
            var expected = script.Playlist.OfType<PrintText>().Select(PageId).ToArray();
            int manual = 0;
            double emptyDeadline = EditorApplication.timeSinceStartup + 60;
            while (Phase != "reveal")
            {
                CheckCallback();
                Require(EditorApplication.timeSinceStartup < emptyDeadline,
                    "Empty-scene progression exceeded 60 seconds at page " + (pages.LastOrDefault() ?? "none") + ", phase " + Phase);
                if (player.WaitingForInput && player.PlayedScript?.Path == ScriptPath)
                {
                    Require(expected.Take(3).Contains(pages.LastOrDefault()), "A post-reveal page ran while the scene was still empty.");
                    if (manual == 0) { Require(Phase == "empty" && audio.GetAudioTrack(Loop) == null, "Empty scene is not silent."); await Capture("empty"); await VerifySaveGuards(); }
                    // The player selects its NEXT command before waiting for this page's input;
                    // PlayedCommand may already be Spawn while the third Print remains visible.
                    await Continue(); manual++;
                }
                await Task.Delay(80);
            }
            Require(pages.Take(3).SequenceEqual(expected.Take(3)), "Reveal skipped an opening dialogue page.");
            report["opening_continue_count"] = manual;
            report["opening_page_ids"] = new JArray(pages.Take(3));
            await SampleEffectReveal("reveal-start", 0f, .5f);
            await Task.Delay(900);
            await SampleEffectReveal("reveal-midpoint", .2f, .95f);
            await Capture("reveal-midpoint");
            await Until(() => player.WaitingForInput && Phase == "reveal" && firstTrack != null && Mathf.Abs(firstTrack.Volume - .65f) < .0001f,
                20, "Reveal did not reach its dialogue and settled audio gain.");
            // Check both rendered frames; do not discard an initial bad frame in order to settle.
            int completeFrame = await SampleEffectReveal("reveal-complete", .999f, 1f);
            int nextFrame = await SampleEffectReveal("reveal-complete-next-frame", .999f, 1f);
            Require(nextFrame == completeFrame + 1, "Reveal completion samples did not cover consecutive rendered frames.");
            await Capture("full-cg");
            lastEpilogueEffect = Engine.GetServiceOrErr<ISpawnManager>().GetSpawned("TitleScreenEffect_3")?.GameObject;
            Require(lastEpilogueEffect, "Fireworks effect did not spawn for full playback.");
            await Until(() => firstTrack != null, 5, "No Intro/Loop audio track observed.");
            // Observe the natural scheduled handoff without timeSamples seeking or timeScale changes.
            await Until(() => AudioSettings.dspTime - musicStart > introLength + .4 && firstTrack.Source && firstTrack.Source.timeSamples > 0,
                Math.Max(15, introLength + 8), "Intro did not hand off to Loop naturally.");
            report["intro_to_loop"] = new JObject { ["intro_seconds"] = introLength, ["elapsed_dsp_seconds"] = AudioSettings.dspTime - musicStart,
                ["loop_time_samples"] = firstTrack.Source.timeSamples, ["looping"] = firstTrack.Loop, ["same_track"] = true };
            int autoAt = pages.Count;
            player.SetAutoPlayEnabled(true);
            await Until(() => pages.Count >= autoAt + 2, 55, "Auto play did not advance two dialogue pages.");
            player.SetAutoPlayEnabled(false);
            report["auto_advanced_pages"] = pages.Count - autoAt;
            int lastIndex = -1;
            double lastInput = 0;
            double deadline = EditorApplication.timeSinceStartup + 180;
            while (Active)
            {
                CheckCallback();
                Require(EditorApplication.timeSinceStartup < deadline, "Full playback did not finish.");
                if (!creditsCaptured && pages.Count == expected.Length && player.WaitingForInput &&
                    pages.Last() == expected.Last() && VisiblePrinterFullyRevealed())
                    await VerifyAndCaptureCredits();
                if ((player.WaitingForInput || player.PlayedCommand is PrintText) &&
                    (lastIndex != player.PlayedIndex || EditorApplication.timeSinceStartup - lastInput > .3))
                { lastIndex = player.PlayedIndex; lastInput = EditorApplication.timeSinceStartup; await Continue(); manual++; }
                await Task.Delay(40);
            }
            Require(pages.SequenceEqual(expected), "Full playback did not execute the exact ordered page IDs.");
            Require(phases.SequenceEqual(new[] { "reveal", "quiet", "together" }), "Audio moods were missing/out of order.");
            Require(playEvents == 0, "Mood changes replayed the existing audio track/Intro.");
            Require(creditsCaptured, "Final-page attribution was not captured while the final text waited for input.");
            report["full_play_page_ids"] = new JArray(pages);
            report["manual_continue_count"] = manual;
            report["mood_track_restarts"] = playEvents;
        }

        private static async Task SeedMemorySlots(IStateManager state)
        {
            memoryGame = (MemoryGame)state.GameSlotManager;
            quickSlot = state.Configuration.IndexToQuickSaveSlotId(1);
            var snapshot = await state.SaveGame(ProbeSlot);
            await memoryGame.Save(quickSlot, snapshot);
            seededSlotHashes = memoryGame.Hashes();
            seededGameWrites = memoryGame.Writes;
            savedBgm.Clear(); audio.GetPlayedBgm(savedBgm);
            Require(savedBgm.Count > 0 && !savedBgm.Contains(Loop), "Memory load fixture lacks the actual title BGM.");
            report["memory_slots_seeded"] = seededSlotHashes.DeepClone();
            report["memory_slot_source"] = "Two probe-created title snapshots, serialized and cloned in memory; no real user slot is loaded.";
        }

        private static void VerifySeededSlots(string stage)
        {
            var after = memoryGame.Hashes();
            Require(memoryGame.Writes == seededGameWrites && JToken.DeepEquals(after, seededSlotHashes),
                "Epilogue changed an existing in-memory slot or wrote another game slot at " + stage);
            report["memory_slots_unchanged_" + stage] = after;
        }

        private static async Task ExecuteRuntimeCommand(string name, params (string Name, object Value)[] parameters)
        {
            var command = (Command)Activator.CreateInstance(RuntimeType("WitchTrials.Views." + name));
            foreach (var parameter in parameters) command.GetType().GetField(parameter.Name).SetValue(command, parameter.Value);
            await command.Execute();
        }

        private static async Task VerifySaveGuards()
        {
            Require(Active, "Save guards must be tested in the actual epilogue.");
            await ExecuteRuntimeCommand("AutoSave", ("Wait", (BooleanParameter)true));
            await ExecuteRuntimeCommand("QuickSave", ("SlotNumber", (IntegerParameter)1), ("Wait", (BooleanParameter)true));
            var panel = Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                .Single(x => x && x.GetType().FullName == "WitchTrials.Views.ControlPanel");
            Button PanelButton(string field) => (Button)panel.GetType().GetField(field, BindingFlags.Instance | BindingFlags.NonPublic).GetValue(panel);
            var saveUI = Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                .Single(x => x && x.GetType().FullName == "WitchTrials.Views.SaveLoadUi");
            Require(!Get<bool>(saveUI, "Visible"), "Save/load panel was already visible before the save guard experiment.");
            PanelButton("_openButton").onClick.Invoke();
            await Task.Delay(100);
            Require(!PanelButton("_saveButton").interactable, "ControlPanel save button remains enabled in the epilogue.");
            // Exercise its defensive listener even though normal UI interaction is disabled.
            PanelButton("_saveButton").onClick.Invoke();
            await Task.Delay(100);
            Require(!Get<bool>(saveUI, "Visible"), "Blocked save listener opened the save panel.");
            PanelButton("_closeButton").onClick.Invoke();
            VerifySeededSlots("save_guards");
            report["save_guards"] = new JObject { ["autosave_command_blocked"] = true, ["quicksave_command_blocked"] = true,
                ["control_panel_save_disabled"] = true, ["control_panel_listener_blocked"] = true };
        }

        private static async Task VerifyLoadCleanup(IStateManager state, bool quick)
        {
            observing = false;
            ReplayButton().onClick.Invoke();
            await Until(() => Active, 30, "Memory load experiment could not enter epilogue.");
            await AdvanceToReveal();
            var abandoned = audio.GetAudioTrack(Loop) as AudioTrack;
            Require(abandoned?.Source, "Memory load experiment has no epilogue audio source.");
            int stopEvents = 0;
            Action stopped = () => stopEvents++;
            abandoned.OnStop += stopped;
            GameStateMap loaded;
            try { loaded = quick ? await state.QuickLoad() : await state.LoadGame(ProbeSlot); await Task.Delay(750); }
            finally { abandoned.OnStop -= stopped; }
            Require(!Active, "Loading the previous memory slot left epilogue session alive.");
            VerifyReleasedAudio(abandoned, stopEvents, quick ? "quickload" : "loadgame");
            VerifyCreditsRemoved(quick ? "quickload" : "loadgame");
            Require(!lastEpilogueEffect, "Loading the previous slot retained the old epilogue effect object.");
            Require(loaded.PlaybackSpot.ScriptPath == "System/System_Title", "Load did not restore the seeded title snapshot.");
            Require(savedBgm.All(audio.IsBgmPlaying), "Old epilogue cleanup stopped the music restored by Load/QuickLoad.");
            VerifySeededSlots(quick ? "quickload" : "loadgame");
            report[quick ? "quickload_cleanup" : "loadgame_cleanup"] = new JObject { ["slot"] = quick ? quickSlot : ProbeSlot,
                ["restored_script"] = loaded.PlaybackSpot.ScriptPath, ["restored_bgm"] = new JArray(savedBgm), ["epilogue_audio_removed"] = true };
            await ExecuteRuntimeCommand("ReturnToTitle");
            await CheckReturned(quick ? "quickload" : "loadgame");
        }

        private static async Task VerifyEndingTail(Script ending, string firstPageId, Texture2D fullTexture)
        {
            Stage("checking_ending_tail");
            const string expression = "g_gameProgress = max(12, g_gameProgress)";
            var anchors = ending.Playlist.Select((command, index) => (command, index))
                .Where(x => x.command is SetCustomVariable set && set.Expression.Value == expression).ToArray();
            Require(anchors.Length == 1 && anchors[0].index == ending.Playlist.Count - 4,
                "Built ending no longer has the reviewed final progress/EndAdv/ClearBacklog/Goto tail.");
            // This fixture starts at the ending tail instead of playing the whole game.
            // Reproduce StartGame's real title-state transition before the direct script jump;
            // otherwise ReturnToTitle's true -> true assignment cannot emit OnEnterTitleScreen.
            var manager = TitleManager();
            Require(manager != null && InTitleScreen(manager) == true, "Ending-tail fixture must start on the actual title.");
            manager.GetType().GetMethod("SetTitleScreenShown").Invoke(manager, new object[] { false });
            Require(InTitleScreen(manager) == false, "Ending-tail fixture failed to enter the real manager's game state.");
            report["ending_tail_fixture"] = new JObject { ["title_state_before"] = true,
                ["title_state_after_leave"] = InTitleScreen(manager),
                ["scope"] = "Calls the same manager transition as TitleUi.StartGame, then jumps to the built ending tail; does not replay the full main story." };
            var variables = Engine.GetServiceOrErr<ICustomVariableManager>();
            variables.TrySetVariableValue("g_gameProgress", 11);
            string enteredPage = null;
            void RecordEntry(Command command)
            {
                if (command.PlaybackSpot.ScriptPath == ScriptPath && command is PrintText print)
                    enteredPage = PageId(print);
            }
            player.OnCommandExecutionStart += RecordEntry;
            try
            {
                await player.LoadAndPlay(FireworksEpilogueContentBuilder.EndingPath, anchors[0].index);
                await Until(() => Active && player.PlayedScript?.Path == ScriptPath, 40, "Built ending tail did not automatically enter the epilogue.");
                await Until(() => enteredPage == firstPageId && player.WaitingForInput && VisiblePrinterFullyRevealed(),
                    30, "Ending-tail entry did not settle on its first fully revealed dialogue page.");
            }
            finally { player.OnCommandExecutionStart -= RecordEntry; }
            Require(variables.TryGetVariableValue<int>("g_gameProgress", out int progress) && progress == 12,
                "Ending tail did not preserve the original completion progress semantics.");
            VerifySeededSlots("ending_tail");
            report["ending_tail_route"] = new JObject { ["start_index"] = anchors[0].index, ["start_expression"] = expression,
                ["from_progress"] = 11, ["to_progress"] = progress, ["target"] = player.PlayedScript.Path,
                ["first_page_id"] = enteredPage, ["first_page_fully_revealed"] = true,
                ["scope"] = "Actual built final four commands only; the earlier main-story ending/cinematic was not replayed." };
            await ExecuteRuntimeCommand("ReturnToTitle");
            await CheckReturned("ending_tail");
            Require(InTitleScreen(manager) == true, "Ending-tail return did not restore the manager's title state.");
            await VerifyReturnedFireworksTitle(fullTexture);
        }

        private static async Task VerifyReturnedFireworksTitle(Texture2D fullTexture)
        {
            await AsyncUtils.WaitEndOfFrame();
            var actor = Engine.GetServiceOrErr<IBackgroundManager>().GetActor("Stills") as MonoBehaviourActor<BackgroundMetadata>;
            var background = actor?.GameObject.GetComponentInChildren<TransitionalSpriteRenderer>();
            var spawns = Engine.GetServiceOrErr<ISpawnManager>();
            Require(actor != null && actor.Visible && actor.Appearance == "3_1" && background
                && background.MainTexture == fullTexture && !background.TransitionTexture,
                "Returned title does not show the actual bundled fireworks CG 3_1 in its settled Stills actor.");
            Require(spawns.IsSpawned("TitleScreenEffect_3"), "Returned fireworks title is missing TitleScreenEffect_3.");
            var effect = spawns.GetSpawned("TitleScreenEffect_3");
            Require(effect.GameObject && effect.GameObject.activeInHierarchy, "Returned fireworks title effect is inactive.");
            var component = effect.GameObject.GetComponentsInChildren<MonoBehaviour>(true)
                .Single(x => x && x.GetType().FullName == "WitchTrials.Views.TitleScreenEffect");
            bool stationary = (bool)component.GetType().GetField("_stationary", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(component);
            Require(!stationary && Mathf.Approximately(Get<float>(component, "RevealOpacity"), 1f),
                "Returned title reused epilogue-only effect state instead of its normal title entrance.");
            report["returned_fireworks_title"] = new JObject { ["appearance"] = actor.Appearance,
                ["texture"] = background.MainTexture.name, ["same_built_texture"] = background.MainTexture == fullTexture,
                ["texture_dimensions"] = new JArray(background.MainTexture.width, background.MainTexture.height),
                ["effect"] = "TitleScreenEffect_3", ["effect_instance_id"] = effect.GameObject.GetInstanceID(),
                ["stationary"] = stationary, ["reveal_opacity"] = Get<float>(component, "RevealOpacity"),
                ["title_manager_in_title"] = InTitleScreen(TitleManager()) };
            await Capture("returned-fireworks-title");
        }

        private static async Task VerifyAndCaptureCredits()
        {
            await AsyncUtils.WaitEndOfFrame();
            Require((bool)session.GetProperty("CreditsVisible").GetValue(null), "Final page has no visible attribution strip.");
            var raw = Object.FindObjectsByType<RawImage>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                .Single(x => x.name == "OriginalPosterFooter" && x.transform.parent.name == "FireworksEpilogueCredits");
            Require(raw.texture == creditsTexture && raw.uvRect == new Rect(0, 0, 1, 39f / 709f),
                "Attribution does not use the reviewed original poster footer.");
            Require(!raw.raycastTarget && raw.GetComponentInParent<CanvasGroup>().alpha > .99f,
                "Attribution is invisible or blocks dialogue input.");
            await Capture("final-page-042-with-credits");
            report["final_page_credits"] = new JObject { ["page_id"] = pages.Last(),
                ["texture"] = raw.texture.name, ["width"] = raw.texture.width, ["height"] = raw.texture.height,
                ["uv"] = new JArray(raw.uvRect.x, raw.uvRect.y, raw.uvRect.width, raw.uvRect.height), ["waiting_for_input"] = player.WaitingForInput };
            creditsCaptured = true;
        }

        private static void VerifyCreditsRemoved(string stage)
        {
            Require(!(bool)session.GetProperty("CreditsVisible").GetValue(null) &&
                !Object.FindObjectsByType<RawImage>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                    .Any(x => x.name == "OriginalPosterFooter" && x.transform.parent && x.transform.parent.name == "FireworksEpilogueCredits"),
                "Attribution overlay survived " + stage);
            report[stage + "_credits_removed"] = true;
        }

        private static void CheckGallery()
        {
            var unlock = Engine.GetServiceOrErr<IUnlockableManager>();
            Require(!unlock.ItemUnlocked("EpilogueEmpty"), "Auxiliary empty art was unlocked as a CG.");
            var gallery = Object.FindObjectsByType<Naninovel.UI.CGGalleryPanel>(FindObjectsInactive.Include, FindObjectsSortMode.None)
                .Single(x => x.GetType().FullName == "WitchTrials.Views.GalleryUi");
            var grid = gallery.GetComponentsInChildren<CGGalleryGrid>(true).Single();
            var data = (IEnumerable<CGSlotData>)typeof(CGGalleryGrid).GetProperty("SlotData", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(grid);
            var slots = data.ToArray();
            Require(slots.Length == gallery.CGCount && slots.Length > 0, "Gallery data did not initialize.");
            Require(!slots.Any(x => x.Id.Contains("EpilogueEmpty") || x.TexturePaths.Any(path => path.Contains("EpilogueEmpty"))), "Auxiliary empty art entered the gallery.");
            report["gallery"] = new JObject { ["cg_count"] = gallery.CGCount, ["empty_art_unlocked"] = false, ["empty_art_slot_present"] = false };
        }

        private static async Task AdvanceToReveal()
        {
            double deadline = EditorApplication.timeSinceStartup + 40;
            while (Phase != "reveal")
            {
                Require(EditorApplication.timeSinceStartup < deadline, "Repeat entry did not reveal.");
                if (player.WaitingForInput) await Continue();
                await Task.Delay(50);
            }
            await Until(() => audio.GetAudioTrack(Loop) is AudioTrack, 10, "Repeat music missing.");
            lastEpilogueEffect = Engine.GetServiceOrErr<ISpawnManager>().GetSpawned("TitleScreenEffect_3")?.GameObject;
            Require(lastEpilogueEffect, "Repeat entry lacks its fireworks effect.");
        }
        private static async Task Continue()
        {
            var input = Engine.GetServiceOrErr<IInputManager>().GetSampler("Continue");
            Require(input != null && input.Enabled, "Continue input is unavailable.");
            input.Activate(1); await Task.Delay(80); input.Activate(0); await Task.Delay(100);
        }
        private static void CommandStart(Command command)
        {
            TraceCommand("start", command);
            if (!observing || command.PlaybackSpot.ScriptPath != ScriptPath) return;
            if (command is PrintText print) pages.Add(PageId(print));
        }
        private static async void CommandFinish(Command command)
        {
            TraceCommand("finish", command);
            if (!observing || !verifyAudio || command.GetType().Name != "SetFireworksEpilogueMood") return;
            try
            {
                string mood = ((StringParameter)command.GetType().GetField("Mood").GetValue(command)).Value;
                float expected = mood == "quiet" ? .4f : .65f;
                // reveal wait:false ends the command before its real two-second fade completes.
                // Observe that live track, then sample its settled gain without changing playback.
                await Until(() => audio.GetAudioTrack(Loop) is AudioTrack ready && ready.Source, 10, "Mood audio track did not initialize.");
                var track = audio.GetAudioTrack(Loop) as AudioTrack;
                Require(track?.Source && track.IntroClip && track.Clip, "Mood audio is incomplete.");
                if (firstTrack == null) { firstTrack = track; musicStart = AudioSettings.dspTime; track.OnPlay += CountPlay; }
                else Require(ReferenceEquals(firstTrack, track), "Mood change replaced the music track.");
                await Until(() => Mathf.Abs(track.Volume - expected) < .0001f, 5, "Actual track gain did not settle to " + expected);
                Require(Phase == mood, "Mood changed before its gain settled.");
                Require(Mathf.Approximately(audio.MasterVolume, masterBefore) && Mathf.Approximately(audio.BgmVolume, bgmBefore), "User mixer settings changed.");
                phases.Add(mood);
                ((JArray)report["audio_moods"]).Add(new JObject { ["phase"] = mood, ["target"] = expected, ["actual"] = track.Volume,
                    ["source_id"] = track.Source.GetInstanceID(), ["time_samples"] = track.Source.timeSamples });
            }
            catch (Exception error) { callbackFailure = error.ToString(); }
        }
        private static void CountPlay() { playEvents++; }
        private static void VerifyReleasedAudio(AudioTrack oldTrack, int stopEvents, string stage)
        {
            Require(stopEvents > 0 && audio.GetAudioTrack(Loop) == null && !audio.IsBgmPlaying(Loop),
                stage + " did not stop and unregister the owned epilogue track.");
            var source = oldTrack.Source;
            var currentOwners = Object.FindObjectsByType<AudioController>(FindObjectsInactive.Exclude, FindObjectsSortMode.None)
                .SelectMany(x => x.GetAllTracks()).Where(x => source && x.Source == source).ToArray();
            bool reused = source && source.isPlaying;
            if (reused)
            {
                // AudioController pools a stopped source; the old AudioTrack.Source reference
                // remains valid even after a new title/load track acquires that source.
                Require(source.clip != oldTrack.Clip && source.clip != oldTrack.IntroClip &&
                    currentOwners.Any(x => !ReferenceEquals(x, oldTrack) && x.Clip == source.clip),
                    stage + " left the old Intro/Loop playing or a playing source without a new owner.");
            }
            report[stage + "_audio_release"] = new JObject { ["stop_events"] = stopEvents,
                ["owned_loop"] = oldTrack.Clip ? oldTrack.Clip.name : null,
                ["owned_intro"] = oldTrack.IntroClip ? oldTrack.IntroClip.name : null,
                ["source_id"] = source ? source.GetInstanceID() : 0, ["source_reused"] = reused,
                ["current_clip"] = source && source.clip ? source.clip.name : null,
                ["current_owner_clips"] = new JArray(currentOwners.Select(x => x.Clip ? x.Clip.name : null)) };
        }

        private static async Task<int> SampleEffectReveal(string stage, float minimum, float maximum)
        {
            // Background mesh/UV updates run in Update; the effect aligns in LateUpdate.
            // Use Naninovel's rendering-safe PostLateUpdate phase, never a Task.Delay midpoint.
            await AsyncUtils.WaitEndOfFrame();
            var spawned = Engine.GetServiceOrErr<ISpawnManager>().GetSpawned("TitleScreenEffect_3");
            Require(spawned?.GameObject, "Reveal effect is absent at " + stage);
            var component = spawned.GameObject.GetComponentsInChildren<MonoBehaviour>(true)
                .Single(x => x && x.GetType().FullName == "WitchTrials.Views.TitleScreenEffect");
            float opacity = Get<float>(component, "RevealOpacity");
            var properties = new JArray();
            var block = new MaterialPropertyBlock();
            foreach (var renderer in spawned.GameObject.GetComponentsInChildren<Renderer>(true))
            {
                renderer.GetPropertyBlock(block);
                float actual = block.GetFloat("_EpilogueOpacity");
                Require(Mathf.Abs(actual - opacity) < .0001f, "Renderer opacity does not follow the effect: " + renderer.name);
                Require(renderer.sharedMaterials.All(x => x && x.HasProperty("_EpilogueOpacity")),
                    "A title effect material lacks the epilogue reveal multiplier: " + renderer.name);
                properties.Add(new JObject { ["renderer"] = renderer.name, ["opacity"] = actual });
            }
            var fog = spawned.GameObject.GetComponentsInChildren<SpriteRenderer>(true).Single(x => x.name == "Title_ED3_Fog");
            var actor = Engine.GetServiceOrErr<IBackgroundManager>().GetActor("Stills") as MonoBehaviourActor<BackgroundMetadata>;
            var background = actor?.GameObject.GetComponentInChildren<TransitionalSpriteRenderer>();
            Require(fog.sprite && background, "Cannot compare the real effect and CG canvases.");
            var sprite = fog.sprite;
            Vector2 from = -sprite.pivot / sprite.pixelsPerUnit;
            Vector2 to = (sprite.rect.size - sprite.pivot) / sprite.pixelsPerUnit;
            var rect = background.Bounds;
            var material = background.TextureMaterial;
            bool transitioning = background.TransitionTexture;
            var uvScale = transitioning ? material.TransitionTextureScale : material.MainTextureScale;
            var uvOffset = transitioning ? material.TransitionTextureOffset : material.MainTextureOffset;
            Require(uvScale.x > 0 && uvScale.y > 0, "The displayed CG UV scale is invalid.");
            var source = new[] { from, new Vector2(to.x, from.y), to, new Vector2(from.x, to.y) };
            // Compare texture-image corners, not the potentially larger intermediate quad:
            // shader textureUV = quadUV * scale + offset, so solve quadUV at texture UV 0/1.
            var textureCorners = new[] { Vector2.zero, Vector2.right, Vector2.one, Vector2.up };
            var target = textureCorners.Select(uv => rect.min + Vector2.Scale((uv - uvOffset) / uvScale, rect.size)).ToArray();
            var camera = Engine.GetServiceOrErr<ICameraManager>().Camera;
            var corners = new JArray();
            float maximumError = 0;
            for (int i = 0; i < 4; i++)
            {
                Vector2 a = camera.WorldToViewportPoint(fog.transform.TransformPoint(source[i]));
                Vector2 b = camera.WorldToViewportPoint(background.transform.TransformPoint(target[i]));
                float error = Vector2.Distance(a, b); maximumError = Mathf.Max(maximumError, error);
                corners.Add(new JObject { ["effect"] = new JArray(a.x, a.y), ["background"] = new JArray(b.x, b.y), ["distance"] = error });
            }
            var samples = report["effect_reveal_samples"] as JArray;
            if (samples == null) report["effect_reveal_samples"] = samples = new JArray();
            samples.Add(new JObject { ["stage"] = stage, ["time"] = EditorApplication.timeSinceStartup,
                ["frame_count"] = Time.frameCount, ["sample_phase"] = "PostLateUpdate",
                ["opacity"] = opacity, ["renderers"] = properties,
                ["transitioning"] = transitioning,
                ["image_texture"] = (transitioning ? background.TransitionTexture : background.MainTexture).name,
                ["raw_quad_bounds"] = new JArray(rect.x, rect.y, rect.width, rect.height),
                ["uv_scale"] = new JArray(uvScale.x, uvScale.y), ["uv_offset"] = new JArray(uvOffset.x, uvOffset.y),
                ["viewport_corners"] = corners, ["max_viewport_error"] = maximumError });
            // Preserve the failed sample as evidence before enforcing either acceptance threshold.
            Require(opacity >= minimum && opacity <= maximum, stage + " has an unexpected effect opacity: " + opacity);
            Require(maximumError < .002f, "Effect canvas is misaligned with the CG viewport at " + stage + ": " + maximumError);
            return Time.frameCount;
        }
        private static bool VisiblePrinterFullyRevealed()
        {
            var printers = Engine.GetServiceOrErr<ITextPrinterManager>();
            return printers.Actors.Any(x => x.Visible && x.Id == printers.DefaultPrinterId && x.RevealProgress >= .999f);
        }

        private static void TraceCommand(string transition, Command command)
        {
            if (report == null) return;
            var trace = report["command_trace"] as JArray;
            if (trace == null) report["command_trace"] = trace = new JArray();
            trace.Add(new JObject { ["event"] = transition, ["time"] = EditorApplication.timeSinceStartup,
                ["type"] = command.GetType().FullName, ["script"] = command.PlaybackSpot.ScriptPath,
                ["line"] = command.PlaybackSpot.LineIndex, ["player_index"] = player?.PlayedIndex,
                ["waiting"] = player?.WaitingForInput, ["phase"] = Phase });
            while (trace.Count > 40) trace.RemoveAt(0);
        }

        private static JObject RuntimeSnapshot()
        {
            var input = Engine.GetService<IInputManager>();
            var sampler = input?.GetSampler("Continue");
            var printers = Engine.GetService<ITextPrinterManager>();
            var spawns = Engine.GetService<ISpawnManager>();
            // MultipliableSpawn.GetSpawned indexes its dictionary and throws for an absent effect.
            var effect = spawns != null && spawns.IsSpawned("TitleScreenEffect_3") ? spawns.GetSpawned("TitleScreenEffect_3") : null;
            var go = effect?.GameObject;
            var component = go ? go.GetComponentsInChildren<MonoBehaviour>(true)
                .FirstOrDefault(x => x && x.GetType().FullName == "WitchTrials.Views.TitleScreenEffect") : null;
            var rootTransform = component ? (Transform)component.GetType().GetField("_rootTransform", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(component) : null;
            return new JObject {
                ["script"] = player?.PlayedScript?.Path, ["played_index"] = player?.PlayedIndex,
                ["played_command"] = player?.PlayedCommand?.GetType().FullName, ["waiting_for_input"] = player?.WaitingForInput,
                ["playing"] = player?.Playing, ["completing"] = player?.Completing,
                ["executing_commands"] = new JArray(player?.ExecutingCommands.Select(x => x.GetType().FullName) ?? Enumerable.Empty<string>()),
                ["phase"] = Phase, ["active"] = Active, ["pages_seen"] = new JArray(pages),
                ["title_manager_in_title"] = InTitleScreen(TitleManager()),
                ["input_processing"] = input?.ProcessInput, ["continue_enabled"] = sampler?.Enabled,
                ["continue_active"] = sampler?.Active, ["continue_value"] = sampler?.Value,
                ["printers"] = printers == null ? new JArray() : new JArray(printers.Actors.Select(x => new JObject {
                    ["id"] = x.Id, ["visible"] = x.Visible, ["reveal_progress"] = x.RevealProgress })),
                ["effect_spawned"] = effect != null, ["effect_active"] = go && go.activeInHierarchy,
                ["effect_parameters"] = new JArray(effect?.Parameters ?? Array.Empty<string>()),
                ["effect_stationary"] = component ? (bool?)component.GetType().GetField("_stationary", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(component) : null,
                ["effect_root_scale"] = rootTransform ? new JArray(rootTransform.localScale.x, rootTransform.localScale.y, rootTransform.localScale.z) : null
            };
        }
        private static async Task CheckReturned(string name)
        {
            await Until(() => !Active && player.PlayedScript?.Path == "System/System_Title" && Title() != null && Get<bool>(Title(), "Visible"), 40, name + " did not return to actual title.");
            await Task.Delay(3200);
            Require(audio.GetAudioTrack(Loop) == null, name + " left epilogue BGM registered.");
            VerifyCreditsRemoved(name);
            Require(!lastEpilogueEffect, name + " retained the previous epilogue effect instance.");
            Require(Mathf.Approximately(audio.MasterVolume, masterBefore) && Mathf.Approximately(audio.BgmVolume, bgmBefore), "Mixer volume was not preserved.");
            Require(Engine.GetServiceOrErr<ICustomVariableManager>().TryGetVariableValue<int>("g_gameProgress", out int progress) && progress == 12, "Playback changed story progress.");
            Require(ReplayButton().gameObject.activeInHierarchy && ReplayButton().interactable, "Replay entry did not recover.");
            report[name + "_returned"] = true;
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
                ((JArray)report["screenshots"]).Add(new JObject { ["name"] = name, ["path"] = path, ["phase"] = Phase,
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

        private static void VerifyReplayButtonVisible()
        {
            var button = ReplayButton();
            var canvas = button.GetComponentInParent<Canvas>().rootCanvas;
            var manager = Engine.GetServiceOrErr<ICameraManager>();
            Require(canvas.isActiveAndEnabled && canvas.renderMode == RenderMode.ScreenSpaceCamera
                && (canvas.worldCamera == manager.Camera || canvas.worldCamera == manager.UICamera),
                "Title Canvas is not included in the captured runtime cameras.");
            Require(button.IsActive() && button.IsInteractable(), "Replay button is not actually interactable.");
            var groups = button.GetComponentsInParent<CanvasGroup>(false);
            Require(groups.All(x => x.alpha > .95f), "Replay button has an invisible/fading CanvasGroup ancestor.");
            var rect = (RectTransform)button.transform;
            var center = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, rect.TransformPoint(rect.rect.center));
            Require(center.x >= 0 && center.x < Screen.width && center.y >= 0 && center.y < Screen.height,
                "Replay button center lies outside the actual screen.");
            var graphics = button.GetComponentsInChildren<Graphic>().Where(x => x.isActiveAndEnabled && x.color.a > .01f && !x.canvasRenderer.cull).ToArray();
            Require(graphics.Length > 0, "Replay button has no visible UI graphics.");
            Require(EventSystem.current, "Actual UI EventSystem is missing.");
            var hits = new List<RaycastResult>();
            EventSystem.current.RaycastAll(new PointerEventData(EventSystem.current) { position = center }, hits);
            Require(hits.Count > 0 && (hits[0].gameObject == button.gameObject || hits[0].gameObject.transform.IsChildOf(button.transform)),
                "Replay button is occluded at its center by " + (hits.Count > 0 ? hits[0].gameObject.name : "no UI raycast target"));
            report["title_button_visual"] = new JObject { ["canvas"] = canvas.name, ["camera"] = canvas.worldCamera.name,
                ["screen_x"] = center.x, ["screen_y"] = center.y, ["visible_graphics"] = graphics.Length,
                ["raycast_first"] = hits[0].gameObject.name, ["ancestor_canvas_alphas"] = new JArray(groups.Select(x => x.alpha)) };
        }

        private static async Task Until(Func<bool> condition, double seconds, string failure)
        {
            double deadline = EditorApplication.timeSinceStartup + seconds;
            while (!condition()) { CheckCallback(); Require(EditorApplication.timeSinceStartup < deadline, failure); await Task.Delay(30); }
            CheckCallback();
        }
        private static void Stage(string name) { report["stage"] = name; Write(report); Debug.Log("[FireworksEpilogueProbe] " + name); }
        private static void CheckCallback() { if (callbackFailure != null) throw new InvalidOperationException(callbackFailure); }
        private static string PageId(PrintText print) => string.Join("+", print.Text.RawValue.Value.Parts.Where(x => !string.IsNullOrEmpty(x.Id)).Select(x => x.Id));
        private static Component Title() => Object.FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).FirstOrDefault(x => x && x.GetType().FullName == "WitchTrials.Views.TitleUi");
        private static object TitleManager()
        {
            var title = Title();
            return title ? title.GetType().GetProperty("WitchTrialsNaninovelManager", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(title) : null;
        }
        private static bool? InTitleScreen(object manager)
        {
            var state = manager?.GetType().GetField("_isInTitleScreen", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(manager);
            return state == null ? null : Get<bool>(state, "Value");
        }
        private static Button ReplayButton() => Title().GetComponentsInChildren<Button>(true).Single(x => x.name == "Button_FireworksEpilogue");
        private static Type RuntimeType(string name) => AppDomain.CurrentDomain.GetAssemblies().Select(x => x.GetType(name, false)).FirstOrDefault(x => x != null) ?? throw new TypeLoadException(name);
        private static T Get<T>(object obj, string property) => (T)obj.GetType().GetProperty(property).GetValue(obj);
        private static bool Active => session != null && (bool)session.GetProperty("Active").GetValue(null);
        private static string Phase => session == null ? "uninitialized" : (string)session.GetProperty("Phase").GetValue(null);
        private static object Call(object target, string name)
        {
            var method = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public).Single(x => x.Name == name && x.GetParameters().All(p => p.IsOptional));
            return method.Invoke(target, method.GetParameters().Select(p => p.DefaultValue is DBNull || p.DefaultValue == Missing.Value ? Activator.CreateInstance(p.ParameterType) : p.DefaultValue).ToArray());
        }
        private static async Task AwaitForeign(object task)
        {
            var awaiter = task.GetType().GetMethod("GetAwaiter").Invoke(task, null);
            await Until(() => Get<bool>(awaiter, "IsCompleted"), 90, "Game initialization task timed out.");
            awaiter.GetType().GetMethod("GetResult").Invoke(awaiter, null);
        }
        private static void Log(string message, string stack, LogType type)
        {
            if (type != LogType.Error && type != LogType.Exception) return;
            ((JArray)report["errors"]).Add(new JObject { ["type"] = type.ToString(), ["message"] = message, ["stack"] = stack });
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

        public class CountedMemory<T> : TransientSaveSlotManager<T> where T : StateMap, new()
        {
            public int Writes { get; private set; }
            // Clone the serialized payload like a disk save: deserialization must not mutate
            // the stored snapshot by sharing references with the newly loaded live services.
            public override void SaveBlocking(string slotId, T data) { Writes++; base.SaveBlocking(slotId, Clone(data)); }
            public override T LoadBlocking(string slotId) => Clone(base.LoadBlocking(slotId));
            private static T Clone(T data) => JsonUtility.FromJson<T>(JsonUtility.ToJson(data));
            public JObject Hashes()
            {
                var result = new JObject();
                foreach (var pair in SlotToData.OrderBy(x => x.Key, StringComparer.Ordinal))
                {
                    using var sha = SHA256.Create();
                    result[pair.Key] = BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(JsonUtility.ToJson(pair.Value))))
                        .Replace("-", "").ToLowerInvariant();
                }
                return result;
            }
        }
        public sealed class MemoryGame : CountedMemory<GameStateMap> { public MemoryGame(StateConfiguration config, string directory) { } }
        public sealed class MemoryGlobal : CountedMemory<GlobalStateMap> { public MemoryGlobal(StateConfiguration config, string directory) { } }
        public sealed class MemorySettings : CountedMemory<SettingsStateMap> { public MemorySettings(StateConfiguration config, string directory) { } }
    }
}
