// Attempts to start the *reconstructed game* and records precisely how far it gets.
//
// Enabled by ManosabaBoot when MANOSABA_GAMESTART=1; writes MANOSABA_GAMESTART_EVIDENCE.
//
// Why this exists: G4/G5 prove the project boots, loads its own content and instantiates reconstructed
// prefabs -- but they never touch the game's own entry point, so "runs" and "plays" were still
// separated by an unmeasured line.  This component crosses it in the most direct way available:
//
//   * WitchTrials.Views.GameInitializer is the game's boot component (the reconstructed Assembly-CSharp
//     source has its Awake/Start with Confidence A bodies recovered from native evidence);
//   * everything is done through reflection, because the integration assembly deliberately does not
//     reference the game assemblies;
//   * the outcome is read from evidence, not from hope: Awake's effect is checked by reading the private
//     field it assigns, and Start's failure is captured as the exception object plus every log entry
//     Unity produced in the window.
//
// A "STARTED" status therefore means "the reconstructed entry point executed", not "the game plays":
// the boot chain continues in InitializeAsync, whose state machine MoveNext() is still an empty IL2CPP
// stub, and Start() needs serialized references (_steamManager, _splashScreen, _gamePrefab) that only
// the game's own scene supplies.  Both boundaries are reported as data.
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Manosaba.Integration
{
    public class ManosabaGameStart : MonoBehaviour
    {
        private const string TypeName = "WitchTrials.Views.GameInitializer, WitchTrials.Game";
        private const string EvidenceEnv = "MANOSABA_GAMESTART_EVIDENCE";
        /// <summary>When "1", supply the serialized references the game's own scene would provide.</summary>
        private const string WireEnv = "MANOSABA_GAMESTART_WIRE";
        /// <summary>GUID of the imported boot prefab (Assets/GameContent/.../General/Core/Game.prefab).</summary>
        private const string GamePrefabGuidEnv = "MANOSABA_GAMESTART_GAMEPREFAB_GUID";
        /// <summary>Instance name the game gives the boot prefab (recovered value, else a placeholder).</summary>
        private const string GamePrefabNameEnv = "MANOSABA_GAMESTART_GAMEPREFAB_NAME";
        /// <summary>Set to "1" to also wire Steam components (needs a running Steam client, see below).</summary>
        private const string WireSteamEnv = "MANOSABA_GAMESTART_WIRE_STEAM";
        //: How long the boot chain is allowed before it is reported as still running.  It is a constant by default
        //: so runs stay comparable, and overridable so "slow" and "hung" can be told apart with evidence: a chain
        //: that completes after 25 s is a different finding from one that never completes.
        private const string ChainSecondsEnv = "MANOSABA_GAMESTART_CHAIN_SECONDS";
        /// <summary>When "1", aggressively advance Naninovel during the diagnostic window.</summary>
        private const string AutoAdvanceEnv = "MANOSABA_GAMESTART_AUTO_ADVANCE";
        /// <summary>Duration of the opt-in script-player advance window.</summary>
        private const string AutoAdvanceSecondsEnv = "MANOSABA_GAMESTART_AUTO_ADVANCE_SECONDS";
        private const float WindowSeconds = 4f;

        /// <summary>The chain observation window: 2 x the per-phase window, or the value from the environment.</summary>
        private static float ChainSeconds
        {
            get
            {
                string configured = Environment.GetEnvironmentVariable(ChainSecondsEnv);
                if (!string.IsNullOrEmpty(configured) &&
                    float.TryParse(configured, NumberStyles.Float, CultureInfo.InvariantCulture, out float parsed) &&
                    parsed > 0f && parsed <= 600f)
                {
                    return parsed;
                }
                return WindowSeconds * 2f;
            }
        }

        private static float AutoAdvanceSeconds
        {
            get
            {
                string configured = Environment.GetEnvironmentVariable(AutoAdvanceSecondsEnv);
                if (!string.IsNullOrEmpty(configured) &&
                    float.TryParse(configured, NumberStyles.Float, CultureInfo.InvariantCulture, out float parsed) &&
                    parsed > 0f && parsed <= 120f)
                {
                    return parsed;
                }
                return 12f;
            }
        }
        /// <summary>Bounds for the probe's manufactured data: deep enough to measure, small enough to stay safe.</summary>
        private const int MaxWireDepth = 3;
        private const int MaxWiredComponents = 48;

        private void Start()
        {
            StartCoroutine(Run());
        }

        private IEnumerator Run()
        {
            var evidence = new GameStartEvidence
            {
                generated = DateTime.UtcNow.ToString("o"),
                unity_version = Application.unityVersion,
                is_editor = Application.isEditor,
                platform = Application.platform.ToString(),
                entry_type = TypeName,
            };
            var entries = new List<LogEntry>();
            Application.LogCallback handler = (message, stack, type) =>
            {
                if (type == LogType.Log) return;
                entries.Add(new LogEntry { type = type.ToString(), message = message, stack = FirstLines(stack, 6) });
            };
            Application.logMessageReceived += handler;

            // Installed first so the engine's own post-initialization call goes through the capture wrapper.
            bool captureInstalled = InstallPostInitCapture(out string captureError);
            if (!captureInstalled)
                evidence.post_init_capture = "(capture not installed: " + captureError + ")";

            // C# forbids `yield` inside a try block that has a catch clause, so the reflection work (no
            // waiting) and the waiting are kept in separate phases.
            GameObject host = null;
            Component component = null;
            Type type = null;
            bool hostCreated = false;
            int waitedFrames = 0;
            // Attached before InitializeAsync is invoked so the very first progress event is captured.
            NaninovelProgressWatch progressWatch = null;
            try
            {
                type = Type.GetType(TypeName);
                evidence.type_resolved = type != null;
                if (type != null)
                {
                    // Prefer the component the *scene* already has.  Once the boot scene is rebuilt from the
                    // shipped level0 (Level0SceneBuilder), that instance carries the real decoded references, so
                    // measuring it is the whole point; a throwaway host is only created when the scene has none.
                    component = FindSceneComponent(type);
                    evidence.component_source = component != null ? "scene" : "created";
                    if (component != null)
                    {
                        host = component.gameObject;
                        evidence.component_created = false;
                    }
                    else
                    {
                        host = new GameObject("ManosabaGameStart_Host");
                        component = host.AddComponent(type);   // Awake() runs here, synchronously
                        hostCreated = true;
                        evidence.component_created = component != null;
                    }
                    evidence.component_type = component != null ? component.GetType().FullName : null;

                    // Awake's reconstructed body ends with `_addressablesManager = new AddressablesManager()`
                    // plus a ServiceLocator registration.  Reading the field back is direct proof that the
                    // recovered game code executed, rather than merely compiled.
                    FieldInfo field = type.GetField("_addressablesManager",
                        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    evidence.addressables_field_found = field != null;
                    object manager = null;
                    try { manager = field != null && component != null ? field.GetValue(component) : null; }
                    catch (Exception fieldError) { evidence.field_error = Describe(fieldError); }
                    evidence.addressables_manager_created = manager != null;
                    evidence.addressables_manager_type = manager != null ? manager.GetType().FullName : null;

                    MethodInfo[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic |
                                                           BindingFlags.Public | BindingFlags.DeclaredOnly);
                    evidence.declared_method_count = methods.Length;
                    evidence.declared_methods = methods.Select(m => m.Name).Distinct()
                        .OrderBy(n => n, StringComparer.Ordinal).Take(40).ToList();

                    // The boot chain stops at the first serialized reference because the game's own scene
                    // supplies it.  Supplying them here is what moves the measured boundary from "missing
                    // data" to "whatever is actually missing in the code".
                    if (Environment.GetEnvironmentVariable(WireEnv) == "1" && component != null)
                    {
                        WireReferences(type, component, evidence, ref host);
                    }
                }
            }
            catch (Exception ex)
            {
                evidence.status = "ERROR";
                evidence.error = Describe(ex);
            }

            if (component == null)
            {
                if (evidence.status == "NOT_RUN") evidence.status = "BLOCKED_TYPE";
            }
            else
            {
                yield return null;   // let Unity run the component's own Start()

                MethodInfo start = type.GetMethod("Start",
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                evidence.start_method_found = start != null;
                // Root objects present before the boot chain runs, so what it spawns can be reported as a
                // difference -- "the recovered chain instantiated the game prefab" is only evidence if the
                // object is named and counted, not inferred from the absence of exceptions.
                var rootsBefore = new HashSet<string>();
                if (host != null && host.scene.IsValid())
                {
                    foreach (GameObject root in host.scene.GetRootGameObjects())
                    {
                        if (root != null) rootsBefore.Add(root.GetInstanceID().ToString());
                    }
                }
                // The scene's own GameInitializer, with its scene-provided SteamManager, already had Start() run by
                // Unity on the previous frame, and Start() fire-and-forgets InitializeAsync.  Invoking Start() again
                // and then InitializeAsync directly (both written for the host-created component, whose Start() stops
                // at the missing SteamManager) booted the game THREE times concurrently: three DefaultLocalePresenters
                // fighting over one IDefaultLocaleSelector registration and three Game prefab instances (G4b round 5,
                // 2026-09-21).  In that case the probe only observes.
                bool chainStartedByUnity = !evidence.component_created && component != null &&
                                           ((Behaviour)component).isActiveAndEnabled && SteamManagerWired(type, component);
                evidence.chain_started_by_unity = chainStartedByUnity;
                if (chainStartedByUnity)
                {
                    evidence.start_invocation = "SKIPPED (Unity ran the scene component's Start(), which starts InitializeAsync)";
                }
                else if (start != null)
                {
                    // Invoking it ourselves captures the exception object and its stack, which Unity's
                    // lifecycle call would only have logged.
                    try
                    {
                        start.Invoke(component, null);
                        evidence.start_invocation = "OK";
                    }
                    catch (TargetInvocationException wrapped)
                    {
                        evidence.start_invocation = Describe(wrapped.InnerException ?? wrapped);
                    }
                    catch (Exception ex)
                    {
                        evidence.start_invocation = Describe(ex);
                    }
                }

                int startFrame = Time.frameCount;
                float deadline = Time.realtimeSinceStartup + WindowSeconds;
                while (Time.realtimeSinceStartup < deadline) yield return null;
                waitedFrames = Time.frameCount - startFrame;     // frames actually spent in the window
                Debug.Log("[ManosabaGameStart] start window closed: frames=" + waitedFrames +
                          " start=" + evidence.start_invocation);
                WritePartial(evidence);

                // Start()'s first line needs the scene-provided SteamManager, which this probe deliberately
                // does not create (see the skip in WireComponentGraph), so Start() stops there.  The rest of
                // the boot chain lives in the recovered InitializeAsync, so call it directly: that is what
                // makes the reconstruction measurable even while the Steam reference is absent.
                MethodInfo initialize = type.GetMethod("InitializeAsync",
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                evidence.initialize_method_found = initialize != null;
                if (initialize != null && component != null)
                {
                    Cysharp.Threading.Tasks.UniTask returnedTask = default;
                    bool haveTask = false;
                    progressWatch = NaninovelProgressWatch.TryAttach();
                    if (chainStartedByUnity)
                    {
                        evidence.initialize_invocation = "SKIPPED (already started by Start(); observed only)";
                    }
                    else
                    try
                    {
                        object[] arguments = initialize.GetParameters().Length == 1
                            ? new object[] { default(CancellationToken) }
                            : new object[0];
                        object returned = initialize.Invoke(component, arguments);
                        evidence.initialize_invocation = returned == null
                            ? "OK (null return)" : "OK (" + returned.GetType().Name + ")";
                        if (returned is Cysharp.Threading.Tasks.UniTask task)
                        {
                            returnedTask = task;
                            haveTask = true;
                        }
                    }
                    catch (TargetInvocationException wrapped)
                    {
                        evidence.initialize_invocation = Describe(wrapped.InnerException ?? wrapped);
                    }
                    catch (Exception ex)
                    {
                        evidence.initialize_invocation = Describe(ex);
                    }

                    // Await it here: discarding the returned UniTask (the first version of this probe did)
                    // hides everything the chain does, because a UniTask carries its exception until someone
                    // observes it.  Polling the awaiter also tells apart "completed" from "waiting forever",
                    // which is the difference that matters for a boot chain.
                    if (haveTask || chainStartedByUnity)
                    {
                        // Without a task of our own (chain started by Unity) there is nothing to await: the window
                        // is sampled for its full length and completion is judged from the engine samples.
                        var awaiter = returnedTask.GetAwaiter();
                        float chainDeadline = Time.realtimeSinceStartup + ChainSeconds;
                        float nextSample = 0f;
                        var sampleTrace = new List<string>();
                        while ((!haveTask || !awaiter.IsCompleted) && Time.realtimeSinceStartup < chainDeadline)
                        {
                            // Per-second engine samples: a chain stuck at one progress value names the
                            // service it is stuck on; a chain whose value keeps moving is merely slow.
                            float elapsed = Time.realtimeSinceStartup - (chainDeadline - ChainSeconds);
                            if (elapsed >= nextSample)
                            {
                                nextSample = elapsed + 1f;
                                // The post-initialization list is filled by the engine services' constructors, which
                                // run inside RuntimeInitializer.Initialize -- i.e. AFTER the chain is kicked off.  So
                                // the capture is retried on every sample until the list exists and is non-empty; the
                                // engine only reaches the last entry once every service has been initialized.
                                if (!captureInstalled)
                                {
                                    captureInstalled = InstallPostInitCapture(out string lateCaptureError);
                                    if (!captureInstalled && elapsed > 20f)
                                        evidence.post_init_capture = "(capture not installed: " + lateCaptureError + ")";
                                }
                                SampleEngine(out bool isInitializing, out bool isInitialized);
                                if (isInitialized) TryHookPlaybackTrace();
                                string sample = elapsed.ToString("F1", CultureInfo.InvariantCulture) + ":" +
                                                (isInitialized ? "INITIALIZED" : isInitializing ? "initializing" : "idle") +
                                                ":" + (progressWatch != null
                                                    ? progressWatch.Last.ToString("F3", CultureInfo.InvariantCulture)
                                                    : "n/a");
                                if (sampleTrace.Count < 24) sampleTrace.Add(sample);
                                evidence.engine_samples = sampleTrace.Count;
                                evidence.engine_sample_trace = string.Join(" ", sampleTrace);
                                if (progressWatch != null)
                                {
                                    evidence.engine_progress_events = progressWatch.Count;
                                    evidence.engine_progress_last = progressWatch.Last;
                                    evidence.engine_progress_trace = progressWatch.Trace;
                                }
                                // Heartbeat on the Unity log too, with the frame number: the sample trace is only
                                // written to disk at the END of the probe, so when the main thread is blocked in
                                // one long frame the run leaves nothing at all behind.  These lines survive the
                                // gate's own timeout and tell apart "many slow frames" from "one blocked frame".
                                Debug.Log("[ManosabaGameStart] t=" + sample +
                                          " frame=" + Time.frameCount +
                                          " services=" + evidence.engine_services_count);
                                WritePartial(evidence);
                            }
                            yield return null;
                        }
                        if (!haveTask)
                        {
                            evidence.initialize_completion = "NOT AWAITED (started by Unity's Start(); see engine samples)";
                        }
                        else if (awaiter.IsCompleted)
                        {
                            try
                            {
                                awaiter.GetResult();
                                evidence.initialize_completion = "COMPLETED";
                            }
                            catch (Exception ex)
                            {
                                evidence.initialize_completion = Describe(ex);
                            }
                        }
                        else
                        {
                            evidence.initialize_completion = "STILL RUNNING after " + ChainSeconds + "s";
                        }
                        Debug.Log("[ManosabaGameStart] chain window closed: " + evidence.initialize_completion +
                                  " frame=" + Time.frameCount + " samples=" + evidence.engine_sample_trace);
                        WritePartial(evidence);
                    }

            // Naninovel's own file IO, exercised directly.  The measured chain stops inside
            // `StateManager.PerformPostEngineInitializationTasks` -> `IOSaveSlotManager<SettingsStateMap>
            // .LoadOrDefault` -> `IOUtils.ReadTextFile` / `WriteTextFile`: the settings slot file is produced
            // (or was already there) but the global slot -- the very next statement -- is never created, so
            // the first of those two awaits never returns.  Running the same primitive here, with our own
            // polling, separates "this call never completes in this environment" from "the engine is merely
            // waiting on something else".
            yield return ProbeFileIo(evidence);

            // Isolate the step the chain most likely waits on: the boot prefab's AssetReference.
                    // The probe builds that reference itself (an uninitialized instance with only
                    // m_AssetGUID set), so "the chain hangs" and "the manufactured reference never
                    // completes" have to be told apart before anything is concluded about the recovery.
                    FieldInfo prefabField = type.GetField("_gamePrefab",
                        BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                    object reference = prefabField != null ? prefabField.GetValue(component) : null;
                    evidence.gameprefab_reference_type = reference != null ? reference.GetType().Name : null;
                    if (reference != null)
                    {
                        // Optional parameters are not part of the metadata signature: `InstantiateAsync`
                        // declares (Transform parent = null, bool instantiateInWorldSpace = false,
                        // bool trackHandle = true), so a Type.EmptyTypes lookup finds nothing and the
                        // arguments have to be supplied explicitly.
                        MethodInfo instantiate = reference.GetType()
                            .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                            .FirstOrDefault(m => m.Name == "InstantiateAsync");
                        evidence.gameprefab_instantiate_found = instantiate != null;
                        object handle = null;
                        if (instantiate != null)
                        {
                            // No `yield` may appear inside a try/catch with a catch clause, so the call and
                            // the waiting are separate phases (same shape as elsewhere in this probe).
                            try
                            {
                                object[] instantiateArguments = instantiate.GetParameters()
                                    .Select(p => p.ParameterType.IsValueType
                                        ? Activator.CreateInstance(p.ParameterType) : null)
                                    .ToArray();
                                handle = instantiate.Invoke(reference, instantiateArguments);
                                evidence.gameprefab_handle_type = handle != null ? handle.GetType().Name : null;
                            }
                            catch (TargetInvocationException wrapped)
                            {
                                evidence.gameprefab_error = Describe(wrapped.InnerException ?? wrapped);
                            }
                            catch (Exception ex)
                            {
                                evidence.gameprefab_error = Describe(ex);
                            }
                        }

                        if (handle != null)
                        {
                            PropertyInfo isDone = handle.GetType().GetProperty("IsDone");
                            if (isDone == null)
                            {
                                evidence.gameprefab_handle_status = "no IsDone property";
                            }
                            else
                            {
                                float handleDeadline = Time.realtimeSinceStartup + WindowSeconds * 2f;
                                bool done = false;
                                try { done = (bool)isDone.GetValue(handle); }
                                catch (Exception ex) { evidence.gameprefab_error = Describe(ex); }
                                while (!done && Time.realtimeSinceStartup < handleDeadline)
                                {
                                    yield return null;
                                    try { done = (bool)isDone.GetValue(handle); }
                                    catch (Exception ex) { evidence.gameprefab_error = Describe(ex); break; }
                                }

                                try
                                {
                                    PropertyInfo status = handle.GetType().GetProperty("Status");
                                    PropertyInfo result = handle.GetType().GetProperty("Result");
                                    PropertyInfo exception = handle.GetType().GetProperty("OperationException");
                                    evidence.gameprefab_handle_status = done
                                        ? (status != null ? "done: " + status.GetValue(handle) : "done")
                                        : "STILL PENDING after " + (WindowSeconds * 2f) + "s";
                                    object instance = result != null ? result.GetValue(handle) : null;
                                    evidence.gameprefab_instance = instance is GameObject spawned
                                        ? spawned.name + " (" + spawned.GetComponentsInChildren<Component>(true).Length + " components)"
                                        : (instance == null ? "null result" : instance.GetType().Name);
                                    object error = exception != null ? exception.GetValue(handle) : null;
                                    if (error != null) evidence.gameprefab_error = error.ToString();
                                }
                                catch (Exception ex)
                                {
                                    evidence.gameprefab_error = Describe(ex);
                                }
                            }
                        }
                    }
                }

                if (host != null && host.scene.IsValid())
                {
                    Type entrySequenceType = Type.GetType("WitchTrials.Views.EntrySequence, WitchTrials.Game");
                    foreach (GameObject root in host.scene.GetRootGameObjects())
                    {
                        if (root == null || rootsBefore.Contains(root.GetInstanceID().ToString())) continue;
                        int components = root.GetComponentsInChildren<Component>(true).Length;
                        evidence.spawned_roots.Add(root.name + " (" + components + " components)");
                        // Where does EntrySequence live on the spawned prefab?  The recovered chain calls
                        // `gameObject.GetComponent<EntrySequence>()` -- the *root* only -- so "present
                        // somewhere in the hierarchy" and "reachable from the root" are different facts and
                        // only the second one decides whether that line can work.
                        if (entrySequenceType != null)
                        {
                            bool onRoot = root.GetComponent(entrySequenceType) != null;
                            bool inChildren = root.GetComponentInChildren(entrySequenceType, true) != null;
                            evidence.entry_sequence_on_root = onRoot;
                            evidence.entry_sequence_in_hierarchy = inChildren;
                        }
                    }
                }

                // Isolation for the recovered chain's last unresolved step: `UniTask.WhenAll` over the two
                // sub-tasks.  Both come from stub kickoffs that `return default(UniTask)`, so if WhenAll does
                // not treat an unsourced task as completed, the recovered chain would wait there forever --
                // which is exactly what "STILL RUNNING after 8s" could mean.
                try
                {
                    Cysharp.Threading.Tasks.UniTask whenAll =
                        Cysharp.Threading.Tasks.UniTask.WhenAll(default(Cysharp.Threading.Tasks.UniTask),
                                                               default(Cysharp.Threading.Tasks.UniTask));
                    evidence.whenall_defaults_completed = whenAll.GetAwaiter().IsCompleted;
                }
                catch (Exception ex)
                {
                    evidence.whenall_defaults_error = Describe(ex);
                }

                evidence.status = evidence.addressables_manager_created ? "STARTED" : "COMPONENT_ONLY";
            }

            // The engine's own state, sampled after the chain has had its window.  This is the difference between
            // "our code ran to the engine boundary" and "the engine came up and the game's scenarios are actually
            // loaded": `Engine.Initialized`, the number of commands the script manager sees across every available
            // script, and what the player is playing right now.  All of it is read through reflection because the
            // integration assembly deliberately does not depend on Naninovel at compile time.
            CollectNaninovelState(evidence);
            if (Environment.GetEnvironmentVariable(AutoAdvanceEnv) == "1")
            {
                yield return DriveScriptPlayer(evidence);
                // Re-sample after the driver so the final script/spot is also present in the ordinary fields.
                CollectNaninovelState(evidence);
            }
            // The post-initialization chain is where the engine stops (progress freezes at 0.750), so it gets its
            // own attribution pass: which tasks were registered, and which of the state-loading awaits returns.
            yield return ProbePostInitializationChain(evidence);
            if (progressWatch != null)
            {
                evidence.engine_progress_events = progressWatch.Count;
                evidence.engine_progress_last = progressWatch.Last;
                evidence.engine_progress_trace = progressWatch.Trace;
                progressWatch.Detach();
            }

            Application.logMessageReceived -= handler;
            if (hostCreated && host != null) Destroy(host);   // never destroy the scene's own entry object

            evidence.log = entries;
            evidence.log_errors = entries.Count(e => e.type == "Error" || e.type == "Assert");
            evidence.log_exceptions = entries.Count(e => e.type == "Exception");
            evidence.first_exception = entries.FirstOrDefault(e => e.type == "Exception")?.message;
            evidence.frames_waited = waitedFrames;
            evidence.window_seconds = WindowSeconds;
            Write(evidence);
        }

        /// <summary>
        /// Subscribes to `Naninovel.Engine.OnInitializationProgress` for the duration of the boot chain and
        /// keeps a timestamped trace of it.  This is the difference between the two readings the earlier
        /// runs could not tell apart: `Engine.Initialize` fires the event with `0.25 + 0.5 * i / count`
        /// immediately before awaiting `services[i].InitializeService()` (Engine.cs:147-152), so a trace
        /// that stops advancing at one value names the exact service the chain is stuck on, while a trace
        /// that keeps advancing means "slow, not hung".
        /// </summary>
        private sealed class NaninovelProgressWatch
        {
            private const string RuntimeAssembly = ", Elringus.Naninovel.Runtime";
            private readonly EventInfo engineEvent;
            private readonly Action<float> handler;
            private readonly List<string> trace = new List<string>();
            private float startedAt;

            public int Count { get; private set; }
            public float Last { get; private set; }
            public string Trace => string.Join(" ", trace);

            private NaninovelProgressWatch(EventInfo engineEvent, Action<float> handler)
            {
                this.engineEvent = engineEvent;
                this.handler = handler;
                Last = -1f;
                startedAt = Time.realtimeSinceStartup;
            }

            /// <summary>Attaches when the Naninovel runtime is loaded; returns null (recording nothing) otherwise.</summary>
            public static NaninovelProgressWatch TryAttach()
            {
                try
                {
                    Type engineType = Type.GetType("Naninovel.Engine" + RuntimeAssembly);
                    EventInfo engineEvent = engineType?.GetEvent("OnInitializationProgress",
                        BindingFlags.Public | BindingFlags.Static);
                    if (engineEvent == null) return null;
                    NaninovelProgressWatch watch = null;
                    Action<float> handler = value => watch?.Record(value);
                    watch = new NaninovelProgressWatch(engineEvent, handler);
                    engineEvent.AddEventHandler(null, handler);
                    return watch;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            public void Detach()
            {
                try
                {
                    engineEvent.RemoveEventHandler(null, handler);
                }
                catch (Exception)
                {
                    // Detaching is best-effort: the process is about to end anyway.
                }
            }

            private void Record(float value)
            {
                Count++;
                Last = value;
                if (trace.Count < 24)
                    trace.Add((Time.realtimeSinceStartup - startedAt).ToString("F2", CultureInfo.InvariantCulture) +
                              ":" + value.ToString("F3", CultureInfo.InvariantCulture));
            }
        }

        /// <summary>Reads `Engine.Initializing` / `Engine.Initialized` without a compile-time Naninovel reference.</summary>
        private static void SampleEngine(out bool initializing, out bool initialized)
        {
            initializing = false;
            initialized = false;
            try
            {
                Type engineType = Type.GetType("Naninovel.Engine, Elringus.Naninovel.Runtime");
                if (engineType == null) return;
                if (engineType.GetProperty("Initializing", BindingFlags.Public | BindingFlags.Static)
                        ?.GetValue(null) is bool isInitializing) initializing = isInitializing;
                if (engineType.GetProperty("Initialized", BindingFlags.Public | BindingFlags.Static)
                        ?.GetValue(null) is bool isInitialized) initialized = isInitialized;
            }
            catch (Exception)
            {
                // An unreadable engine state is reported as false/false, never as an exception from the probe.
            }
        }

        /// <summary>
        /// Runs `Naninovel.IOUtils.ReadTextFile` and `WriteTextFile` and polls the returned `Naninovel.UniTask`
        /// values for completion.  Everything is reflective, and polling `IsCompleted` on the *boxed* struct copy
        /// is valid: the struct wraps a single awaiter reference that the real task shares, so a completed task
        /// reports completed through the copy as well.
        ///
        /// The write goes to a scratch file of the probe's own, never to the engine's slot path: an earlier
        /// revision wrote the probe payload over `Saves_v1/Settings.json` (14 bytes replacing the engine's 73),
        /// which is a measurement that damages the thing it measures.
        /// </summary>
        private static IEnumerator ProbeFileIo(GameStartEvidence evidence)
        {
            const string ioUtilsName = "Naninovel.IOUtils, Elringus.Naninovel.Runtime";
            string root = Path.Combine(Application.dataPath, "NaninovelData/.nani/Transient").Replace('\\', '/');
            string engineSlotPath = root + "/Saves_v1/Settings.json";
            string scratchPath = root + "/manosaba_probe_io.json";
            evidence.naninovel_file_io_path = engineSlotPath;
            evidence.naninovel_file_io_exists = File.Exists(engineSlotPath);

            Type ioUtils = Type.GetType(ioUtilsName);
            if (ioUtils == null)
            {
                evidence.naninovel_file_io_read = "type Naninovel.IOUtils not found";
                evidence.naninovel_file_io_write = "type Naninovel.IOUtils not found";
                yield break;
            }

            yield return PollNaninovelTask(evidence, ioUtils, "ReadTextFile", engineSlotPath, true);
            yield return PollNaninovelTask(evidence, ioUtils, "WriteTextFile", scratchPath, false);
        }

        /// <summary>
        /// Attributes the post-initialization stall.  `Engine.Initialize` reports progress per *task*
        /// (`Engine.cs:154-159`), and the measured trace freezes on the first one, so this pass answers two
        /// questions the trace cannot: which delegates are in `Engine.postInitializationTasks` at all, and which
        /// of the state-loading awaits inside the first of them returns.  The first task is
        /// `StateManager.PerformPostEngineInitializationTasks` (StateManager has `InitializeAtRuntime(int.MinValue)`
        /// and its constructor touches no other service on purpose), and its body is only:
        ///     SettingsState = await SettingsSlotManager.LoadOrDefault(DefaultSettingsSlotId);
        ///     await LoadAllServicesFromStateAsync&lt;IStatefulService&lt;SettingsStateMap&gt;, SettingsStateMap&gt;(SettingsState);
        ///     GlobalState   = await GlobalSlotManager.LoadOrDefault(DefaultGlobalSlotId);
        ///     await LoadAllServicesFromStateAsync&lt;IStatefulService&lt;GlobalStateMap&gt;, GlobalStateMap&gt;(GlobalState);
        /// so each of those four calls is reproduced here, with its own budget, and the per-service
        /// `LoadServiceState` calls are attributed one by one.
        /// </summary>
        private static IEnumerator ProbePostInitializationChain(GameStartEvidence evidence)
        {
            const string runtimeAssembly = ", Elringus.Naninovel.Runtime";
            Type engineType = Type.GetType("Naninovel.Engine" + runtimeAssembly);
            Type stateManagerType = Type.GetType("Naninovel.IStateManager" + runtimeAssembly);
            if (engineType == null || stateManagerType == null)
            {
                evidence.post_init_state = "Naninovel.Engine / IStateManager not found";
                yield break;
            }

            // 1. Which tasks the engine registered, in order.
            try
            {
                FieldInfo tasksField = engineType.GetField("postInitializationTasks",
                    BindingFlags.NonPublic | BindingFlags.Static);
                if (tasksField?.GetValue(null) is IEnumerable<Delegate> tasks)
                {
                    var names = new List<string>();
                    foreach (Delegate task in tasks)
                        names.Add(task?.Target?.GetType().Name + "." + task?.Method?.Name);
                    evidence.post_init_tasks = string.Join(",", names);
                    evidence.post_init_task_count = names.Count;
                }
                else evidence.post_init_tasks = "(postInitializationTasks not readable)";
            }
            catch (Exception ex)
            {
                evidence.post_init_tasks = Describe(ex);
            }

            MethodInfo getService = engineType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                .FirstOrDefault(m => m.Name == "GetService" && m.IsGenericMethodDefinition &&
                                     m.GetParameters().Length == 0);
            object stateManager = null;
            try { stateManager = getService?.MakeGenericMethod(stateManagerType).Invoke(null, null); }
            catch (Exception ex) { evidence.post_init_state = Describe(ex); }
            if (stateManager == null)
            {
                evidence.post_init_state = "Engine.GetService<IStateManager>() returned null";
                yield break;
            }
            evidence.post_init_state = stateManager.GetType().FullName;

            // 2. The two slot managers and their LoadOrDefault calls, one at a time.  Both call sites in the
            //    engine read their slot id from `Configuration` (StateManager.cs:311/317) and store the result
            //    into the public `SettingsState` / `GlobalState` properties, so those are read back too: a
            //    non-null state proves the *engine's* own call returned, independently of this probe's.
            yield return PollSlotLoad(evidence, stateManager, "SettingsSlotManager", "DefaultSettingsSlotId",
                "SettingsState", true);
            yield return PollSlotLoad(evidence, stateManager, "GlobalSlotManager", "DefaultGlobalSlotId",
                "GlobalState", false);

            // 3. `LoadSettings()` has two statements, and the measurements above close the first of them: the
            //    engine's own `SettingsState` is already non-null (its `LoadOrDefault` returned) while
            //    `GlobalState` is still null.  So the engine's first post-init task is stopped on the SECOND
            //    statement, `LoadAllServicesFromStateAsync<IStatefulService<SettingsStateMap>, SettingsStateMap>`
            //    (StateManager.cs:267-273, called from :312), which awaits
            //    `service.LoadServiceState(state)` once per matching service.  This pass calls each of those
            //    with its own budget and names the one that never returns.
            //
            //    Re-invoking the services is a deliberate measurement side effect: every `LoadServiceState`
            //    implementation in this build is a pure state applier, and `SettingsState` here is the very
            //    map the engine already handed them, so a second call cannot change the outcome -- it can only
            //    reveal which call does not come back.
            yield return ProbeSettingsStateServices(evidence, engineType, stateManager);

            // 4. The last post-initialization task is `UiManagerExtended.InstantiateUIs`, and the engine's
            //    progress freezes on it at 0.750 + 0.25 * 2/3.  Measured fact that narrows it down: the task is
            //    STILL PENDING, not faulted -- a fault would complete `RuntimeInitializer.Initialize`'s UniTask
            //    (a faulted task reports IsCompleted = true) and the probe's own chain await would finish.  Its
            //    body is
            //        var resources = await uiLoader.LoadAll(holder: this);
            //        await UniTask.WhenAll(resources.Select(r => InstantiatePrefab(r, uiLoader.GetLocalPath(r))));
            //        await UniTask.WhenAll(managedUIs.Select(u => u.UIComponent.Initialize()));   <-- suspect
            //        await ApplyFontAssociatedWithLocale(default);
            //        l10n.AddChangeLocaleTask(...); ShowVisibleOnAwake();
            //    and the first two plainly returned (the UI hierarchy is in the hierarchy dump, 5430 components),
            //    so this pass times `Initialize()` on every registered managed UI, one at a time -- the same
            //    attribution that located the `AudioManagerExtended` fault.  `Naninovel.UI.CustomUI.Initialize`
            //    is `=> UniTask.CompletedTask`, so a pending one can only come from a game-side override.
            yield return ProbeManagedUiInitialization(evidence);

            // 5. Where the chain is now: `InstantiateUIs` has four awaits and all four are accounted for --
            //    the UI resources loaded (19 of 19 catalog UIs are registered), every managed UI's
            //    `Initialize()` returns, and `ApplyFontAssociatedWithLocale` is fully synchronous
            //    (`UIManager.cs:374-395`: every path returns `UniTask.CompletedTask`).  Yet the task never
            //    completes, so one of those awaits is waiting on something that never signals.  The resource
            //    providers keep exactly that state on the record: `LocateRunners` is a
            //    `Dictionary<Tuple<string, Type>, ResourceRunner>` and a non-empty entry means a locate was
            //    started and never finished -- and `ResourceProvider.LocateResources<T>` awaits that same
            //    runner on every later call for the same (path, type), so one stuck runner blocks everything
            //    behind it for ever.  `CGGalleryPanel.Initialize` (the last UI measured, and the one whose
            //    second invocation was fast only because the locate result had been cached by then) awaits
            //    `loader.Locate()` for its Texture2D sources, which is exactly that path.
            CollectResourceProviderState(evidence);

            // 6. Last resort, and the only measurement that can name the suspension point directly: re-invoke
            //    the stuck post-initialization delegate and, if the new task also pends, walk its own promise to
            //    the compiler-generated state machine and read `<>1__state`.  Every await of `InstantiateUIs` has
            //    been accounted for by then, so the state index is what is left to look at.  Opt-in
            //    (MANOSABA_GAMESTART_REINVOKE_POSTINIT=1) because it instantiates the whole UI set a second time.
            yield return ProbeStuckPostInitTask(evidence);

            // 7. And the answer the re-invocation could not give: the state machine of the task the engine itself
            //    received from `postInitializationTasks[2]()` (see InstallPostInitCapture).  Called unconditionally:
            //    the helper reports "never called" / "not installed" on its own, and this iterator method cannot see
            //    Run()'s local flag anyway.
            ReportCapturedPostInitTask(evidence);
            yield return ProbeOriginalPostInitTask(evidence);

            // 8. Both invokable forms of the last post-init task complete, yet the engine still reports
            //    `Initializing = true`.  `Engine.Initialized` is `initializeTCS != null && initializeTCS.Task
            //    .IsCompleted` (Engine.cs:62-66), so the engine's own completion source is the authority: its
            //    `state` says whether it is Pending/Succeeded/Faulted/Canceled, and its `continuation` names the
            //    state machine waiting on it -- which says whether the wait is inside `Engine.Initialize`'s
            //    post-initialization loop or later, in `RuntimeInitializer.Initialize`.
            ReportEngineCompletionSource(evidence);
            Debug.Log("[ManosabaGameStart] post-init capture: " + evidence.post_init_capture);
        }

        /// <summary>Reads `Engine.initializeTCS`: its state, and the state machine awaiting it.</summary>
        private static void ReportEngineCompletionSource (GameStartEvidence evidence)
        {
            try
            {
                Type engineType = Type.GetType("Naninovel.Engine, Elringus.Naninovel.Runtime");
                object tcs = engineType?
                    .GetField("initializeTCS", BindingFlags.NonPublic | BindingFlags.Static)?.GetValue(null);
                if (tcs == null) { evidence.engine_tcs = "initializeTCS is null"; return; }
                object state = tcs.GetType()
                    .GetField("state", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(tcs);
                object continuation = tcs.GetType()
                    .GetField("continuation", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(tcs);
                Action action = continuation as Action;
                if (action == null && continuation is IEnumerable many)
                    foreach (object item in many) { action = item as Action; if (action != null) break; }
                object runner = action?.Target;
                object machine = runner?.GetType()
                    .GetField("StateMachine", BindingFlags.Instance | BindingFlags.Public)?.GetValue(runner);
                string description = "state=" + (state?.ToString() ?? "?") +
                                     " (0=Pending 1=Succeeded 2=Faulted 3=Canceled), awaiter=" +
                                     (machine?.GetType().Name ?? runner?.GetType().Name ?? "none");
                if (machine != null)
                {
                    var fields = new List<string>();
                    foreach (FieldInfo field in machine.GetType().GetFields(BindingFlags.Instance |
                                                                            BindingFlags.Public | BindingFlags.NonPublic))
                    {
                        object value;
                        try { value = field.GetValue(machine); } catch (Exception) { continue; }
                        if (value == null) continue;
                        if (value is int number) { fields.Add(field.Name + "=" + number); continue; }
                        fields.Add(field.Name + "=" + (value is string text && text.Length <= 40
                            ? text : value.GetType().Name));
                    }
                    description += " {" + string.Join(", ", fields) + "}";
                }
                evidence.engine_tcs = description;
            }
            catch (Exception ex)
            {
                evidence.engine_tcs = "unreadable: " + Describe(ex);
            }
        }

        /// <summary>
        /// Re-invokes the last post-initialization task and, when it pends again, reports the exact await it is
        /// suspended on: delegate -> Naninovel.UniTask.awaiter (the promise) -> promise.continuation (the
        /// MoveNext action) -> MoveNextRunner&lt;TStateMachine&gt;.StateMachine -> `&lt;&gt;1__state`.
        /// </summary>
        private static IEnumerator ProbeStuckPostInitTask (GameStartEvidence evidence)
        {
            if (Environment.GetEnvironmentVariable("MANOSABA_GAMESTART_REINVOKE_POSTINIT") != "1")
            {
                evidence.post_init_reinvoke = "(skipped; set MANOSABA_GAMESTART_REINVOKE_POSTINIT=1)";
                yield break;
            }
            Type engineType = Type.GetType("Naninovel.Engine, Elringus.Naninovel.Runtime");
            FieldInfo tasksField = engineType?.GetField("postInitializationTasks",
                BindingFlags.NonPublic | BindingFlags.Static);
            if (tasksField?.GetValue(null) is not IEnumerable<Delegate> tasks)
            {
                evidence.post_init_reinvoke = "postInitializationTasks not readable";
                yield break;
            }
            var list = tasks.ToList();
            if (list.Count == 0) { evidence.post_init_reinvoke = "no post-init tasks"; yield break; }
            Delegate last = list[list.Count - 1];

            object task;
            try { task = last.DynamicInvoke(); }
            catch (Exception ex) { evidence.post_init_reinvoke = "invoke threw: " + Describe(ex); yield break; }
            if (task == null) { evidence.post_init_reinvoke = "null task"; yield break; }

            PropertyInfo isCompleted = task.GetType().GetProperty("IsCompleted");
            bool done = false;
            float deadline = Time.realtimeSinceStartup + 3f;
            while (Time.realtimeSinceStartup < deadline)
            {
                try { done = isCompleted != null && (bool)isCompleted.GetValue(task); }
                catch (Exception) { break; }
                if (done) break;
                yield return null;
            }
            if (done)
            {
                evidence.post_init_reinvoke = last.Method.Name + " COMPLETED on a fresh call";
                Debug.Log("[ManosabaGameStart] post-init re-invoke: " + evidence.post_init_reinvoke);
                yield break;
            }

            // Still pending: read the state machine the promise is holding.
            try
            {
                object awaiter = task.GetType()
                    .GetField("awaiter", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(task);
                object continuation = awaiter?.GetType()
                    .GetField("continuation", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(awaiter);
                Action action = continuation as Action;
                if (action == null && continuation is IEnumerable many)
                    foreach (object item in many) { action = item as Action; if (action != null) break; }
                object runner = action?.Target;
                object machine = runner?.GetType()
                    .GetField("StateMachine", BindingFlags.Instance | BindingFlags.Public)?.GetValue(runner);
                if (machine == null)
                {
                    evidence.post_init_reinvoke = "PENDING; could not reach the state machine (awaiter=" +
                        (awaiter?.GetType().Name ?? "null") + ", continuation=" +
                        (continuation?.GetType().Name ?? "null") + ")";
                }
                else
                {
                    Type machineType = machine.GetType();
                    var fields = new List<string>();
                    foreach (FieldInfo field in machineType.GetFields(BindingFlags.Instance | BindingFlags.Public |
                                                                     BindingFlags.NonPublic))
                    {
                        object value;
                        try { value = field.GetValue(machine); } catch (Exception) { continue; }
                        if (value == null) continue;
                        if (value is int number) { fields.Add(field.Name + "=" + number); continue; }
                        string name = value.ToString();
                        fields.Add(field.Name + "=" + (name.Length > 60 ? value.GetType().Name : name));
                    }
                    evidence.post_init_reinvoke = "PENDING; state machine " + machineType.Name +
                                                  " {" + string.Join(", ", fields) + "}";
                }
                Debug.Log("[ManosabaGameStart] post-init re-invoke: " + evidence.post_init_reinvoke);
            }
            catch (Exception ex)
            {
                evidence.post_init_reinvoke = "PENDING; inspection failed: " + Describe(ex);
            }
        }

        /// <summary>
        /// Dumps each resource provider's cache and in-flight runner tables.  Reflection only, and it reads
        /// state instead of invoking anything, so it cannot perturb what it measures.
        /// </summary>
        private static void CollectResourceProviderState (GameStartEvidence evidence)
        {
            try
            {
                Type engineType = Type.GetType("Naninovel.Engine, Elringus.Naninovel.Runtime");
                Type managerType = Type.GetType("Naninovel.IResourceProviderManager, Elringus.Naninovel.Runtime");
                if (engineType == null || managerType == null)
                {
                    evidence.provider_state = "Naninovel.Engine / IResourceProviderManager not found";
                    return;
                }
                MethodInfo getService = engineType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(m => m.Name == "GetService" && m.IsGenericMethodDefinition &&
                                         m.GetParameters().Length == 0);
                object manager = getService?.MakeGenericMethod(managerType).Invoke(null, null);
                if (manager == null)
                {
                    evidence.provider_state = "IResourceProviderManager is null";
                    return;
                }

                // The provider collection's field name is not part of the public surface, so the providers are
                // found by shape: any enumerable instance field whose items implement Naninovel.IResourceProvider.
                var providers = new List<object>();
                for (Type t = manager.GetType(); t != null; t = t.BaseType)
                {
                    foreach (FieldInfo field in t.GetFields(BindingFlags.Instance | BindingFlags.NonPublic |
                                                            BindingFlags.Public | BindingFlags.DeclaredOnly))
                    {
                        object value;
                        try { value = field.GetValue(manager); }
                        catch (Exception) { continue; }
                        if (value is not IEnumerable items || value is string) continue;
                        // A Dictionary<K,V> enumerates KeyValuePair<K,V>, whose type is not an IResourceProvider,
                        // so its Values have to be walked instead -- the provider table is a dictionary.
                        IEnumerable candidates = value is IDictionary map ? map.Values : items;
                        foreach (object item in candidates)
                            if (item != null && item.GetType().GetInterfaces().Any(i => i.Name == "IResourceProvider"))
                                providers.Add(item);
                    }
                }

                var lines = new List<string>();
                foreach (object provider in providers.Distinct())
                {
                    Type type = provider.GetType();
                    lines.Add(type.Name +
                              "{cache=" + CountMember(provider, "LocationsCache") +
                              ", locatedFolders=" + CountMember(provider, "LocatedFolders") +
                              ", locateRunners=[" + MemberKeys(provider, "LocateRunners") + "]" +
                              ", loadRunners=[" + MemberKeys(provider, "LoadRunners") + "]" +
                              ", loaded=" + CountMember(provider, "LoadedResources") + "}");
                }
                evidence.provider_state = lines.Count == 0
                    ? "(no IResourceProvider found on " + manager.GetType().Name + ")"
                    : string.Join("; ", lines);
                Debug.Log("[ManosabaGameStart] providers: " + evidence.provider_state);
            }
            catch (Exception ex)
            {
                evidence.provider_state = Describe(ex);
            }
        }

        private static object GetCollectionMember (object instance, string name)
        {
            for (Type t = instance.GetType(); t != null; t = t.BaseType)
            {
                PropertyInfo property = t.GetProperty(name, BindingFlags.Instance | BindingFlags.NonPublic |
                                                            BindingFlags.Public | BindingFlags.DeclaredOnly);
                if (property != null) return property.GetValue(instance);
                FieldInfo field = t.GetField(name, BindingFlags.Instance | BindingFlags.NonPublic |
                                                   BindingFlags.Public | BindingFlags.DeclaredOnly);
                if (field != null) return field.GetValue(instance);
            }
            return null;
        }

        private static int CountMember (object instance, string name)
        {
            object value = GetCollectionMember(instance, name);
            return value is ICollection collection ? collection.Count : -1;
        }

        /// <summary>Keys of a runner dictionary, so a stuck runner names its own (path, type).</summary>
        private static string MemberKeys (object instance, string name)
        {
            object value = GetCollectionMember(instance, name);
            if (value is not IDictionary dictionary) return value == null ? "?" : "not-a-dictionary";
            var keys = new List<string>();
            foreach (DictionaryEntry entry in dictionary)
            {
                if (keys.Count >= 12) { keys.Add("..."); break; }
                keys.Add(entry.Key?.ToString() ?? "(null)");
            }
            return string.Join(", ", keys);
        }

        /// <summary>
        /// Times `Initialize()` on every UI the manager registered.  Opt-in (`MANOSABA_GAMESTART_PROBE_UI_INIT=1`)
        /// because re-invoking a UI's Initialize repeats its own subscriptions; this is a diagnostic, not a
        /// routine measurement.
        /// </summary>
        private static IEnumerator ProbeManagedUiInitialization (GameStartEvidence evidence)
        {
            if (Environment.GetEnvironmentVariable("MANOSABA_GAMESTART_PROBE_UI_INIT") != "1")
            {
                evidence.managed_ui_initialize = "(skipped; set MANOSABA_GAMESTART_PROBE_UI_INIT=1)";
                yield break;
            }
            const float budget = 5f;
            Type engineType = Type.GetType("Naninovel.Engine, Elringus.Naninovel.Runtime");
            Type uiManagerType = Type.GetType("Naninovel.IUIManager, Elringus.Naninovel.Runtime");
            if (engineType == null || uiManagerType == null)
            {
                evidence.managed_ui_initialize = "Naninovel.Engine / IUIManager not found";
                yield break;
            }
            object manager = null;
            try
            {
                MethodInfo getService = engineType.GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(m => m.Name == "GetService" && m.IsGenericMethodDefinition &&
                                         m.GetParameters().Length == 0);
                manager = getService?.MakeGenericMethod(uiManagerType).Invoke(null, null);
            }
            catch (Exception ex) { evidence.managed_ui_initialize = Describe(ex); yield break; }
            if (manager == null) { evidence.managed_ui_initialize = "IUIManager is null"; yield break; }

            object managedUIs = GetMemberValue(manager.GetType(), manager, "managedUIs");
            if (managedUIs is not IEnumerable list)
            {
                evidence.managed_ui_initialize = "UIManager.managedUIs not readable";
                yield break;
            }

            var results = new List<string>();
            foreach (object managed in list.Cast<object>().ToArray())
            {
                if (managed == null) continue;
                object component = GetMemberValue(managed.GetType(), managed, "UIComponent");
                string name = component is Component unityComponent
                    ? unityComponent.name
                    : component?.GetType().Name ?? "(null)";
                if (component == null)
                {
                    results.Add(name + "=NO_COMPONENT");
                    evidence.managed_ui_initialize = string.Join(", ", results);
                    continue;
                }
                MethodInfo initialize = component.GetType()
                    .GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    .FirstOrDefault(m => m.Name == "Initialize" && m.GetParameters().Length == 0);
                if (initialize == null)
                {
                    results.Add(name + "=no-method");
                    evidence.managed_ui_initialize = string.Join(", ", results);
                    continue;
                }
                object task;
                try { task = initialize.Invoke(component, null); }
                catch (Exception ex)
                {
                    results.Add(name + "=THREW(" + Describe(ex) + ")");
                    evidence.managed_ui_initialize = string.Join(", ", results);
                    continue;
                }
                bool done = task == null;
                PropertyInfo isCompleted = task?.GetType().GetProperty("IsCompleted");
                float deadline = Time.realtimeSinceStartup + budget;
                while (!done && Time.realtimeSinceStartup < deadline)
                {
                    try { done = isCompleted != null && (bool)isCompleted.GetValue(task); }
                    catch (Exception) { break; }
                    if (!done) yield return null;
                }
                results.Add(name + "=" + (done ? "COMPLETED" : "PENDING>" + budget + "s"));
                evidence.managed_ui_initialize = string.Join(", ", results);
            }
            Debug.Log("[ManosabaGameStart] UI Initialize(): " + evidence.managed_ui_initialize);
        }

        /// <summary>Times `LoadServiceState(SettingsStateMap)` on every service that implements it.</summary>
        private static IEnumerator ProbeSettingsStateServices (GameStartEvidence evidence, Type engineType,
            object stateManager)
        {
            const float budget = 4f;
            object settingsState = null;
            try { settingsState = GetMemberValue(stateManager.GetType(), stateManager, "SettingsState"); }
            catch (Exception ex) { evidence.settings_state_services = "SettingsState unreadable: " + Describe(ex); yield break; }
            if (settingsState == null)
            {
                evidence.settings_state_services = "(engine SettingsState is null -- nothing for the loop to load)";
                yield break;
            }

            IEnumerable<object> all;
            try
            {
                all = engineType.GetProperty("Services", BindingFlags.Public | BindingFlags.Static)
                    ?.GetValue(null) as IEnumerable<object>;
            }
            catch (Exception ex)
            {
                evidence.settings_state_services = "Engine.Services unreadable: " + Describe(ex);
                yield break;
            }
            if (all == null)
            {
                evidence.settings_state_services = "Engine.Services is null";
                yield break;
            }

            var results = new List<string>();
            foreach (object service in all.ToArray())
            {
                if (service == null) continue;
                // The closed interface is taken from the instance, so no assembly-qualified generic name has to
                // be constructed by hand.
                Type stateful = service.GetType().GetInterfaces().FirstOrDefault(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition().Name == "IStatefulService`1" &&
                    i.GetGenericArguments()[0].Name == "SettingsStateMap");
                if (stateful == null) continue;

                string name = service.GetType().Name;
                MethodInfo load = stateful.GetMethod("LoadServiceState");
                if (load == null)
                {
                    results.Add(name + "=no-method");
                    evidence.settings_state_services = string.Join(", ", results);
                    continue;
                }

                object task = null;
                string outcome;
                try
                {
                    task = load.Invoke(service, new[] { settingsState });
                    outcome = "returned";
                }
                catch (Exception ex)
                {
                    results.Add(name + "=THREW(" + Describe(ex) + ")");
                    evidence.settings_state_services = string.Join(", ", results);
                    continue;
                }

                bool done = task == null;
                PropertyInfo isCompleted = task?.GetType().GetProperty("IsCompleted");
                float deadline = Time.realtimeSinceStartup + budget;
                while (!done && Time.realtimeSinceStartup < deadline)
                {
                    try { done = isCompleted != null && (bool)isCompleted.GetValue(task); }
                    catch (Exception ex)
                    {
                        outcome = "poll-failed(" + Describe(ex) + ")";
                        done = true;
                        break;
                    }
                    if (!done) yield return null;
                }
                results.Add(name + "=" + (done ? "COMPLETED" : "PENDING>" + budget + "s"));
                evidence.settings_state_services = string.Join(", ", results);
            }
            Debug.Log("[ManosabaGameStart] LoadServiceState(SettingsStateMap): " + evidence.settings_state_services);
        }

        /// <summary>Reads a slot manager off the state manager and polls its `LoadOrDefault` for one slot.</summary>
        private static IEnumerator PollSlotLoad (GameStartEvidence evidence, object stateManager, string managerMember,
            string slotIdMember, string stateMember, bool settings)
        {
            const float budget = 15f;
            Type type = stateManager.GetType();

            // The engine's own call, observed through the state property it assigns.
            string engineState = "(unreadable)";
            try
            {
                object state = GetMemberValue(type, stateManager, stateMember);
                engineState = state == null ? "null" : state.GetType().Name;
            }
            catch (Exception ex) { engineState = Describe(ex); }
            if (settings) evidence.engine_settings_state = engineState;
            else evidence.engine_global_state = engineState;

            object task = null;
            string slotId = null;
            try
            {
                object manager = GetMemberValue(type, stateManager, managerMember);
                object configuration = GetMemberValue(type, stateManager, "Configuration");
                slotId = configuration == null ? null : GetMemberValue(configuration.GetType(), configuration, slotIdMember) as string;
                if (manager == null)
                {
                    SetSlotResult(evidence, settings, "slot manager '" + managerMember + "' is null");
                    yield break;
                }
                MethodInfo load = manager.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public)
                    .FirstOrDefault(m => m.Name == "LoadOrDefault" && m.GetParameters().Length >= 1);
                if (load == null)
                {
                    SetSlotResult(evidence, settings, "LoadOrDefault not found on " + manager.GetType().Name);
                    yield break;
                }
                object[] args = load.GetParameters().Length == 1
                    ? new object[] { slotId }
                    : new[] { (object)slotId, null };
                task = load.Invoke(manager, args);
            }
            catch (Exception ex)
            {
                SetSlotResult(evidence, settings, Describe(ex));
                yield break;
            }
            if (task == null)
            {
                SetSlotResult(evidence, settings, "null task");
                yield break;
            }

            yield return PollBoxedTask(task, budget, done => SetSlotResult(evidence, settings, done
                ? "LoadOrDefault('" + slotId + "') COMPLETED (engine state before: " + engineState + ")"
                : "LoadOrDefault('" + slotId + "') STILL PENDING after " + budget + "s (engine state before: " +
                  engineState + ")"));
            Debug.Log("[ManosabaGameStart] slot " + (settings ? "settings" : "global") + ": " +
                      (settings ? evidence.settings_slot_load : evidence.global_slot_load));
        }

        /// <summary>Polls a boxed `Naninovel.UniTask`/`UniTask&lt;T&gt;` through its `IsCompleted` property.</summary>
        private static IEnumerator PollBoxedTask (object task, float budget, Action<bool> report)
        {
            PropertyInfo isCompleted = task.GetType().GetProperty("IsCompleted");
            bool done = false;
            float deadline = Time.realtimeSinceStartup + budget;
            while (Time.realtimeSinceStartup < deadline)
            {
                try { done = isCompleted != null && (bool)isCompleted.GetValue(task); }
                catch (Exception) { break; }
                if (done) break;
                yield return null;
            }
            report(done);
        }

        private static void SetSlotResult (GameStartEvidence evidence, bool settings, string value)
        {
            if (settings) evidence.settings_slot_load = value;
            else evidence.global_slot_load = value;
        }

        /// <summary>`GetProperty` then `GetField`, public and non-public: the state manager exposes its slot
        /// managers as properties in the package source but the decompiled game types may surface them either way.</summary>
        private static MemberInfo FindMember (Type type, string name)
        {
            for (Type t = type; t != null; t = t.BaseType)
            {
                PropertyInfo property = t.GetProperty(name,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                if (property != null) return property;
                FieldInfo field = t.GetField(name,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
                if (field != null) return field;
            }
            return null;
        }

        private static object GetMemberValue (Type type, object instance, string name)
        {
            MemberInfo member = FindMember(type, name);
            if (member is PropertyInfo property) return property.GetValue(instance);
            if (member is FieldInfo field) return field.GetValue(instance);
            return null;
        }

        /// <summary>Invokes one `Naninovel.IOUtils` member and polls its `Naninovel.UniTask` for up to ten seconds.</summary>
        private static IEnumerator PollNaninovelTask (GameStartEvidence evidence, Type ioUtils, string member,
            string filePath, bool read)
        {
            const float budget = 10f;
            string label = read ? "read" : "write";
            object task = null;
            try
            {
                MethodInfo method = ioUtils.GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(m => m.Name == member && m.GetParameters().Length == (read ? 1 : 2));
                if (method == null)
                {
                    SetFileIoResult(evidence, read, "method " + member + " not found");
                    yield break;
                }
                object[] args = read
                    ? new object[] { filePath }
                    : new object[] { filePath, "{\"probe\":true}" };
                task = method.Invoke(null, args);
            }
            catch (Exception ex)
            {
                SetFileIoResult(evidence, read, Describe(ex));
                yield break;
            }
            if (task == null)
            {
                SetFileIoResult(evidence, read, "null task");
                yield break;
            }

            PropertyInfo isCompleted = task.GetType().GetProperty("IsCompleted");
            bool done = false;
            float deadline = Time.realtimeSinceStartup + budget;
            while (Time.realtimeSinceStartup < deadline)
            {
                try { done = isCompleted != null && (bool)isCompleted.GetValue(task); }
                catch (Exception ex) { SetFileIoResult(evidence, read, Describe(ex)); yield break; }
                if (done) break;
                yield return null;
            }
            SetFileIoResult(evidence, read, done
                ? "COMPLETED at t=" + Time.realtimeSinceStartup.ToString("F2", CultureInfo.InvariantCulture)
                : "STILL PENDING after " + budget + "s");
            Debug.Log("[ManosabaGameStart] Naninovel IOUtils." + member + " -> " +
                      (read ? evidence.naninovel_file_io_read : evidence.naninovel_file_io_write));
        }

        private static void SetFileIoResult (GameStartEvidence evidence, bool read, string value)
        {
            if (read) evidence.naninovel_file_io_read = value;
            else evidence.naninovel_file_io_write = value;
        }

        /// <summary>
        /// Captures the task the engine's last post-initialization delegate returns, so the FIRST invocation can be
        /// inspected instead of a fresh one.  Needed because re-invoking proved useless: `InstantiateUIs` completes
        /// on a second call (its own awaits are satisfied by state the first call has already set up), so only the
        /// original task carries the suspension point.
        /// </summary>
        public static void CapturePostInitTask (object task)
        {
            lock (CapturedPostInitTasks)
            {
                CapturedPostInitTasks.Add(task);
                // Who called the delegate?  This is the one fact that separates "the engine's
                // post-initialization loop reached this entry" from "only the probe called it": the stack names
                // `Naninovel.Engine/<Initialize>d__NN:MoveNext` in the first case and this class in the second.
                if (capturedPostInitStacks.Count < 4)
                {
                    string[] lines = (Environment.StackTrace ?? "").Split('\n');
                    capturedPostInitStacks.Add(string.Join(" <- ", lines
                        .Where(l => l.Contains("MoveNext") || l.Contains("Engine") ||
                                    l.Contains("ManosabaGameStart"))
                        .Take(4).Select(l => l.Trim())));
                }
            }
        }

        private static readonly List<string> capturedPostInitStacks = new List<string>();

        private static readonly List<object> CapturedPostInitTasks = new List<object>();
        /// <summary>The delegate that was replaced by the capture wrapper, so it can be probed on its own.</summary>
        private static Delegate capturedOriginalPostInitTask;

        /// <summary>
        /// Invokes the ORIGINAL last post-init delegate (the one the wrapper replaced) and reports whether it
        /// completes.  The wrapper's own invocation completed while the engine still reports `Initializing = true`,
        /// so the question is whether the original can complete at all -- if it cannot, the difference between the
        /// two calls is state that the first call itself established, and that state is the thing to find.
        /// </summary>
        private static IEnumerator ProbeOriginalPostInitTask (GameStartEvidence evidence)
        {
            Delegate original = capturedOriginalPostInitTask;
            if (original == null)
            {
                evidence.post_init_original = "(the original delegate was not captured)";
                yield break;
            }
            object task;
            try { task = original.DynamicInvoke(); }
            catch (Exception ex) { evidence.post_init_original = "invoke threw: " + Describe(ex); yield break; }
            PropertyInfo isCompleted = task?.GetType().GetProperty("IsCompleted");
            bool done = task == null;
            float deadline = Time.realtimeSinceStartup + 4f;
            while (!done && Time.realtimeSinceStartup < deadline)
            {
                try { done = isCompleted != null && (bool)isCompleted.GetValue(task); }
                catch (Exception) { break; }
                if (!done) yield return null;
            }
            evidence.post_init_original = done
                ? original.Method.Name + " COMPLETED on a direct call"
                : original.Method.Name + " STILL PENDING after 4s on a direct call";
            Debug.Log("[ManosabaGameStart] post-init original: " + evidence.post_init_original);
        }

        /// <summary>
        /// Replaces `Engine.postInitializationTasks[last]` with a wrapper that calls the original and stores the
        /// returned task.  The wrapper has to be built with an expression tree because the delegate's return type
        /// is `Naninovel.UniTask`, which this integration assembly deliberately has no compile-time reference to.
        /// Installed before `Start()` is invoked so the engine's own call goes through it.
        /// </summary>
        private static bool InstallPostInitCapture (out string error)
        {
            error = null;
            try
            {
                Type engineType = Type.GetType("Naninovel.Engine, Elringus.Naninovel.Runtime");
                object listObject = engineType?
                    .GetField("postInitializationTasks", BindingFlags.NonPublic | BindingFlags.Static)?.GetValue(null);
                if (listObject is not IList list || list.Count == 0)
                {
                    error = "postInitializationTasks not readable or empty";
                    return false;
                }
                int index = list.Count - 1;
                Delegate original = list[index] as Delegate;
                if (original == null) { error = "last post-init task is not a delegate"; return false; }
                capturedOriginalPostInitTask = original;

                MethodInfo capture = typeof(ManosabaGameStart)
                    .GetMethod(nameof(CapturePostInitTask), BindingFlags.Public | BindingFlags.Static);
                InvocationExpression call = Expression.Invoke(Expression.Constant(original));
                Expression body = Expression.Block(
                    Expression.Call(capture, Expression.Convert(call, typeof(object))), call);
                list[index] = Expression.Lambda(original.GetType(), body).Compile();
                return true;
            }
            catch (Exception ex)
            {
                error = Describe(ex);
                return false;
            }
        }

        /// <summary>
        /// Reads the captured first-invocation task and, when it is still pending, walks its state machine to the
        /// await it is suspended on (same walk as ProbeStuckPostInitTask, different task).
        /// </summary>
        private static void ReportCapturedPostInitTask (GameStartEvidence evidence)
        {
            object[] captured;
            lock (CapturedPostInitTasks)
            {
                captured = CapturedPostInitTasks.ToArray();
                evidence.post_init_capture_stack = capturedPostInitStacks.Count == 0
                    ? "(no stack recorded)" : string.Join(" || ", capturedPostInitStacks);
            }
            if (captured.Length == 0)
            {
                // Keep the install-time diagnosis when there is one.
                if (string.IsNullOrEmpty(evidence.post_init_capture) ||
                    evidence.post_init_capture == "not run")
                    evidence.post_init_capture = "(the engine never called the captured delegate)";
                return;
            }
            var outcomes = new List<string>();
            object pending = null;
            foreach (object item in captured)
            {
                PropertyInfo completedProperty = item.GetType().GetProperty("IsCompleted");
                bool isDone = completedProperty != null && (bool)completedProperty.GetValue(item);
                outcomes.Add(isDone ? "COMPLETED" : "PENDING");
                if (!isDone && pending == null) pending = item;
            }
            // A count above one means `Engine.Initialize` ran its post-initialization loop more than once, which
            // is itself a finding: the loop is only reached after the whole service list has been initialized.
            string prefix = captured.Length + " invocation(s) [" + string.Join(", ", outcomes) + "]: ";
            if (pending == null)
            {
                evidence.post_init_capture = prefix + "all completed";
                return;
            }
            object task = pending;
            try
            {
                object awaiter = task.GetType()
                    .GetField("awaiter", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(task);
                object continuation = awaiter?.GetType()
                    .GetField("continuation", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(awaiter);
                Action action = continuation as Action;
                if (action == null && continuation is IEnumerable many)
                    foreach (object item in many) { action = item as Action; if (action != null) break; }
                object machine = action?.Target?.GetType()
                    .GetField("StateMachine", BindingFlags.Instance | BindingFlags.Public)?.GetValue(action.Target);
                if (machine == null)
                {
                    evidence.post_init_capture = prefix + "PENDING; state machine unreachable (continuation=" +
                        (continuation?.GetType().Name ?? "null") + ")";
                    return;
                }
                var fields = new List<string>();
                foreach (FieldInfo field in machine.GetType().GetFields(BindingFlags.Instance |
                                                                        BindingFlags.Public | BindingFlags.NonPublic))
                {
                    object value;
                    try { value = field.GetValue(machine); } catch (Exception) { continue; }
                    if (value == null) continue;
                    if (value is int number) { fields.Add(field.Name + "=" + number); continue; }
                    fields.Add(field.Name + "=" + (value is string text && text.Length <= 50 ? text : value.GetType().Name));
                }
                evidence.post_init_capture = prefix + "PENDING; " + machine.GetType().Name +
                                             " {" + string.Join(", ", fields) + "}";
            }
            catch (Exception ex)
            {
                evidence.post_init_capture = prefix + "inspection failed: " + Describe(ex);
            }
        }

        /// <summary>
        /// Opt-in playthrough driver for G4/G5.  It does not synthesize Unity input and it is never enabled in
        /// ordinary game runs.  Instead it talks to IScriptPlayer through reflection: temporarily allows skipping
        /// unread commands, enables skip, and releases any explicit "waiting for input" state.  Every distinct
        /// playback snapshot is captured, so a gate run identifies the first position that fails to advance.
        /// </summary>
        private static IEnumerator DriveScriptPlayer(GameStartEvidence evidence)
        {
            const string runtimeAssembly = ", Elringus.Naninovel.Runtime";
            object player = null;
            Type playerType = null;
            PropertyInfo skipModeProperty = null;
            MethodInfo setSkipEnabled = null;
            MethodInfo setWaitingForInputEnabled = null;
            object originalSkipMode = null;
            bool originalSkipActive = false;
            string setupError = null;

            try
            {
                Type engineType = Type.GetType("Naninovel.Engine" + runtimeAssembly);
                playerType = Type.GetType("Naninovel.IScriptPlayer" + runtimeAssembly);
                MethodInfo getService = engineType?
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(m => m.Name == "GetService" && m.IsGenericMethodDefinition &&
                                         m.GetParameters().Length == 0);
                if (getService == null || playerType == null)
                {
                    setupError = "Naninovel engine/player type unavailable";
                }
                else
                {
                    player = getService.MakeGenericMethod(playerType).Invoke(null, null);
                    if (player == null)
                    {
                        setupError = "Engine.GetService<IScriptPlayer>() returned null";
                    }
                    else
                    {
                        skipModeProperty = playerType.GetProperty("SkipMode");
                        PropertyInfo skipActiveProperty = playerType.GetProperty("SkipActive");
                        originalSkipMode = skipModeProperty?.GetValue(player);
                        if (skipActiveProperty?.GetValue(player) is bool active) originalSkipActive = active;
                        if (skipModeProperty != null && skipModeProperty.PropertyType.IsEnum)
                        {
                            object everything = Enum.Parse(skipModeProperty.PropertyType, "Everything");
                            skipModeProperty.SetValue(player, everything);
                        }
                        setSkipEnabled = playerType.GetMethod("SetSkipEnabled", new[] { typeof(bool) });
                        setWaitingForInputEnabled = playerType.GetMethod("SetWaitingForInputEnabled", new[] { typeof(bool) });
                        setSkipEnabled?.Invoke(player, new object[] { true });
                    }
                }
            }
            catch (Exception ex)
            {
                setupError = Describe(ex);
            }

            if (!string.IsNullOrEmpty(setupError))
            {
                evidence.auto_advance = "BLOCKED: " + setupError;
                yield break;
            }

            evidence.auto_advance = "RUNNING";
            float started = Time.realtimeSinceStartup;
            float deadline = started + AutoAdvanceSeconds;
            string lastSnapshot = null;
            var trace = new List<string>();

            while (Time.realtimeSinceStartup < deadline)
            {
                string snapshot = TryAdvanceAndSnapshot(player, playerType, setSkipEnabled,
                    setWaitingForInputEnabled, evidence, Time.realtimeSinceStartup - started);
                if (!string.IsNullOrEmpty(snapshot) && snapshot != lastSnapshot)
                {
                    lastSnapshot = snapshot;
                    evidence.auto_advance_transitions++;
                    if (trace.Count < 64) trace.Add(snapshot);
                    evidence.auto_advance_trace = string.Join(" | ", trace);
                    WritePartial(evidence);
                }
                yield return null;
            }

            try
            {
                setSkipEnabled?.Invoke(player, new object[] { originalSkipActive });
                if (skipModeProperty != null && originalSkipMode != null)
                    skipModeProperty.SetValue(player, originalSkipMode);
            }
            catch (Exception ex)
            {
                evidence.auto_advance_restore = Describe(ex);
            }

            evidence.auto_advance = "COMPLETED";
            evidence.auto_advance_seconds = Time.realtimeSinceStartup - started;
            evidence.auto_advance_final = lastSnapshot ?? "(no readable player snapshot)";
        }

        private static string TryAdvanceAndSnapshot(object player, Type playerType, MethodInfo setSkipEnabled,
                                                     MethodInfo setWaitingForInputEnabled,
                                                     GameStartEvidence evidence, float elapsed)
        {
            try
            {
                bool waiting = playerType.GetProperty("WaitingForInput")?.GetValue(player) is bool w && w;
                bool skip = playerType.GetProperty("SkipActive")?.GetValue(player) is bool s && s;
                bool playing = playerType.GetProperty("Playing")?.GetValue(player) is bool p && p;
                if (!skip) setSkipEnabled?.Invoke(player, new object[] { true });
                if (waiting)
                {
                    setWaitingForInputEnabled?.Invoke(player, new object[] { false });
                    evidence.auto_advance_wait_releases++;
                }

                string script = null;
                object played = playerType.GetProperty("PlayedScript")?.GetValue(player);
                if (played != null) script = played.GetType().GetProperty("Path")?.GetValue(played) as string;
                object spot = playerType.GetProperty("PlaybackSpot")?.GetValue(player);
                object index = playerType.GetProperty("PlayedIndex")?.GetValue(player);
                return elapsed.ToString("F2", CultureInfo.InvariantCulture) + "s " +
                       (script ?? "(no-script)") + " " + (spot?.ToString() ?? "(no-spot)") +
                       " index=" + (index?.ToString() ?? "?") +
                       " playing=" + playing + " waiting=" + waiting + " skip=" + skip;
            }
            catch (Exception ex)
            {
                evidence.auto_advance = "ERROR: " + Describe(ex);
                return null;
            }
        }

        /// <summary>
        /// Reads Naninovel's engine/service state after the boot-chain window, so a run can state facts instead of
        /// impressions: did the engine initialise, how much scenario content did the script manager load, and what
        /// is the player playing.  Everything is reflective (the integration assembly has no compile-time
        /// dependency on Naninovel), and every value that cannot be read is recorded as the reason, not as a zero.
        /// </summary>
        private static void CollectNaninovelState(GameStartEvidence evidence)
        {
            const string runtimeAssembly = ", Elringus.Naninovel.Runtime";
            try
            {
                Type engineType = Type.GetType("Naninovel.Engine" + runtimeAssembly);
                if (engineType == null)
                {
                    evidence.naninovel_state = "type Naninovel.Engine not found (package missing or not compiled)";
                    return;
                }
                PropertyInfo initialized = engineType.GetProperty("Initialized", BindingFlags.Public | BindingFlags.Static);
                PropertyInfo initializing = engineType.GetProperty("Initializing", BindingFlags.Public | BindingFlags.Static);
                // `as bool?` was the first attempt and it worked - but the JSON writer dropped the nullable field,
                // so the flag/value pair is read explicitly here.
                if (initialized != null && initialized.GetValue(null) is bool isInitialized)
                {
                    evidence.naninovel_initialized_known = true;
                    evidence.naninovel_initialized = isInitialized;
                }
                if (initializing != null && initializing.GetValue(null) is bool isInitializing)
                {
                    evidence.naninovel_initializing_known = true;
                    evidence.naninovel_initializing = isInitializing;
                }

                // The service list the engine was handed, plus the ones that actually got constructed: this is
                // what turns "initialising" into "initialising, and here is how far the loop got".
                try
                {
                    if (engineType.GetProperty("Services", BindingFlags.Public | BindingFlags.Static)
                            ?.GetValue(null) is IEnumerable<object> services)
                    {
                        var names = services.Select(s => s?.GetType().Name ?? "(null)").ToList();
                        evidence.engine_services_known = true;
                        evidence.engine_services_count = names.Count;
                        evidence.engine_services = string.Join(",", names.Take(40));
                    }
                }
                catch (Exception ex)
                {
                    evidence.engine_services = Describe(ex);
                }

                MethodInfo getService = engineType
                    .GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(m => m.Name == "GetService" && m.IsGenericMethodDefinition &&
                                         m.GetParameters().Length == 0);

                Type scriptManagerType = Type.GetType("Naninovel.IScriptManager" + runtimeAssembly);
                if (getService != null && scriptManagerType != null)
                {
                    object manager = getService.MakeGenericMethod(scriptManagerType).Invoke(null, null);
                    evidence.naninovel_script_manager = manager != null;
                    if (manager != null)
                    {
                        PropertyInfo total = scriptManagerType.GetProperty("TotalCommandsCount");
                        // TotalCommandsCount is a direct measure of loaded scenario content: it counts the commands
                        // of every script the manager can see, so a non-zero value cannot be produced by an empty
                        // resource provider.
                        if (total != null && total.GetValue(manager) is int commandCount)
                        {
                            evidence.naninovel_total_commands_known = true;
                            evidence.naninovel_total_commands = commandCount;
                        }
                    }
                }

                Type playerType = Type.GetType("Naninovel.IScriptPlayer" + runtimeAssembly);
                if (getService != null && playerType != null)
                {
                    object player = getService.MakeGenericMethod(playerType).Invoke(null, null);
                    evidence.naninovel_script_player = player != null;
                    if (player != null)
                    {
                        PropertyInfo playing = playerType.GetProperty("Playing");
                        if (playing != null && playing.GetValue(player) is bool isPlaying)
                        {
                            evidence.naninovel_playing_known = true;
                            evidence.naninovel_playing = isPlaying;
                        }
                        object played = playerType.GetProperty("PlayedScript")?.GetValue(player);
                        if (played != null)
                        {
                            object path = played.GetType().GetProperty("Path")?.GetValue(played);
                            evidence.naninovel_played_script = path as string;
                            if (!string.IsNullOrEmpty(evidence.naninovel_played_script))
                                evidence.played_scripts.Add(evidence.naninovel_played_script);
                        }
                        object spot = playerType.GetProperty("PlaybackSpot")?.GetValue(player);
                        if (spot != null && !(spot is string)) evidence.naninovel_playback_spot = spot.ToString();
                    }
                }
                evidence.naninovel_state = "read";
            }
            catch (Exception ex)
            {
                evidence.naninovel_state = Describe(ex);
            }

            CollectResourceChainState(evidence);
        }

        /// <summary>
        /// Reads the two inputs the rebuilt resource-provider path depends on, both through reflection:
        ///
        ///   * `GigaCreation.Essentials.MasterData.MasterDataProvider.Data` -- the asset the boot chain reads
        ///     `AssetKeyNames` from.  `AddressablesManager.LoadResourceLocationsAsyncCore` returns an EMPTY list
        ///     (and logs "The provided master data does not include the data for Addressable Asset System.")
        ///     whenever this is null, so a null here produces a silent empty location list, not an exception.
        ///   * the `IAddressablesManager` service's `ResourceLocations` count -- the list that
        ///     `AddressableResourceProviderExtended.LoadAllLocationsCore` feeds into `CacheLocations`, and therefore
        ///     the list every Naninovel path lookup is served from once it is non-empty.
        /// </summary>
        private static void CollectResourceChainState(GameStartEvidence evidence)
        {
            try
            {
                Type providerType = Type.GetType(
                    "GigaCreation.Essentials.MasterData.MasterDataProvider, GigaCreation.Essentials.MasterData");
                object data = providerType?
                    .GetProperty("Data", BindingFlags.Public | BindingFlags.Static)?.GetValue(null);
                if (providerType == null) evidence.master_data_state = "type MasterDataProvider not found";
                else if (data == null) evidence.master_data_state = "null (the empty-location-list branch is taken)";
                else
                {
                    evidence.master_data_state = data.GetType().FullName;
                    object keys = data.GetType().GetProperty("AssetKeyNames")?.GetValue(data);
                    if (keys is IEnumerable<string> names) evidence.master_data_asset_key_names = string.Join(",", names);
                    else if (keys is IEnumerable loose)
                        evidence.master_data_asset_key_names =
                            string.Join(",", loose.Cast<object>().Select(o => o?.ToString()));
                }
            }
            catch (Exception ex)
            {
                evidence.master_data_state = Describe(ex);
            }

            try
            {
                Type managerInterface = Type.GetType(
                    "GigaCreation.Essentials.AddressablesUtils.IAddressablesManager, GigaCreation.Essentials.AddressablesUtils");
                Type serviceLocator = Type.GetType(
                    "GigaCreation.Tools.Service.ServiceLocator, GigaCreation.Tools.Service");
                if (managerInterface == null || serviceLocator == null)
                {
                    evidence.addressables_manager_state = "type not found";
                    return;
                }
                // IAddressablesManager is the type `AddressableResourceProviderExtended` itself asks for
                // (ServiceLocator.Get<IAddressablesManager>(true)), and it is the one interface here that
                // provably satisfies Get<TService>'s `class, IService` constraint.
                MethodInfo get = serviceLocator.GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(m => m.Name == "Get" && m.IsGenericMethodDefinition && m.GetParameters().Length == 2);
                object manager = get?.MakeGenericMethod(managerInterface)
                    .Invoke(null, new object[] { true });
                if (manager == null)
                {
                    evidence.addressables_manager_state = "ServiceLocator.Get returned null";
                    return;
                }
                evidence.addressables_manager_state = manager.GetType().FullName;
                PropertyInfo locations = manager.GetType().GetProperty("ResourceLocations",
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                object list = locations?.GetValue(manager);
                if (list is ICollection collection) evidence.addressables_resource_locations = collection.Count;
                else if (list == null) evidence.addressables_resource_locations = 0;
                PropertyInfo additional = manager.GetType().GetProperty("IsAdditionalCatalogLoaded");
                if (additional?.GetValue(manager) is bool loaded) evidence.addressables_additional_catalog_loaded = loaded;
            }
            catch (Exception ex)
            {
                evidence.addressables_manager_state = Describe(ex);
            }
        }

        /// <summary>
        /// The entry component as the loaded scene provides it, or null when the scene has none (inactive objects
        /// are searched too: a scene may legitimately ship the entry object disabled).
        /// </summary>
        private static Component FindSceneComponent(Type type)
        {
            for (int index = 0; index < SceneManager.sceneCount; index++)
            {
                Scene scene = SceneManager.GetSceneAt(index);
                if (!scene.isLoaded) continue;
                foreach (GameObject root in scene.GetRootGameObjects())
                {
                    if (root == null) continue;
                    Component found = root.GetComponentInChildren(type, true);
                    if (found != null) return found;
                }
            }
            return null;
        }

        /// <summary>
        /// Fills in the serialized references <c>GameInitializer.Start()</c> dereferences, using the types
        /// and GUIDs the caller passes in.  Everything is recorded step by step: which reference was set,
        /// which could not be, and why -- the point is to measure the boot chain, not to pretend the game's
        /// scene has been reconstructed.
        /// </summary>
        private static void WireReferences(Type type, Component component, GameStartEvidence evidence,
                                            ref GameObject host)
        {
            // Generic pass first: every serialized field whose type is a component gets a freshly created
            // instance, and the same is then done for that instance -- so the boot chain can be measured
            // layer by layer instead of one hard-coded field at a time (round 12 wired two by hand and the
            // chain immediately stopped at the next missing reference, SplashScreen.Awake).
            WireComponentGraph(component, evidence, host.transform, 0, new HashSet<Type>());
            // The holders were created *inactive* on purpose: AddComponent on an active GameObject calls
            // Awake immediately, which is exactly how the probe used to log SplashScreen.Awake's NRE -- its
            // references could only be assigned after Awake had already run.  Activating once, after the
            // graph is wired, lets every Awake see its data.
            ActivateWired(host.transform, evidence);

            // _gamePrefabInstanceName: a plain string.
            FieldInfo nameField = type.GetField("_gamePrefabInstanceName",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            string instanceName = Environment.GetEnvironmentVariable(GamePrefabNameEnv);
            if (nameField != null && !string.IsNullOrEmpty(instanceName))
            {
                try
                {
                    nameField.SetValue(component, instanceName);
                    evidence.wiring.Add("_gamePrefabInstanceName: set to '" + instanceName + "'");
                }
                catch (Exception ex)
                {
                    evidence.wiring.Add("_gamePrefabInstanceName: " + Describe(ex));
                }
            }

            // _gamePrefab: an AssetReferenceGameObject.  Its runtime identity is the serialized
            // m_AssetGUID, which the caller reads from the imported prefab's .meta; Addressables then
            // resolves it through the catalog's implicit GUID keys.
            FieldInfo prefabField = type.GetField("_gamePrefab",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            string guid = Environment.GetEnvironmentVariable(GamePrefabGuidEnv);
            if (prefabField == null || string.IsNullOrEmpty(guid))
            {
                evidence.wiring.Add("_gamePrefab: " + (prefabField == null ? "field not found" : "no GUID given"));
                return;
            }
            try
            {
                // AssetReferenceGameObject declares no parameterless constructor (Addressables 2.x), so
                // Activator.CreateInstance throws MissingMethodException.  An uninitialized instance is
                // enough here: only the serialized m_AssetGUID matters, and it is set below.  This is the
                // same route JsonToSerializedApplier uses for managed references.
                object reference;
                try
                {
                    reference = Activator.CreateInstance(prefabField.FieldType);
                }
                catch (MissingMethodException)
                {
                    reference = System.Runtime.Serialization.FormatterServices
                        .GetUninitializedObject(prefabField.FieldType);
                    evidence.wiring.Add("_gamePrefab: created via GetUninitializedObject (no default constructor)");
                }
                FieldInfo guidField = null;
                for (Type t = prefabField.FieldType; t != null && guidField == null; t = t.BaseType)
                {
                    guidField = t.GetField("m_AssetGUID", BindingFlags.Instance | BindingFlags.NonPublic |
                                                         BindingFlags.Public);
                }
                if (guidField == null)
                {
                    evidence.wiring.Add("_gamePrefab: m_AssetGUID field not found on " + prefabField.FieldType.Name);
                    return;
                }
                guidField.SetValue(reference, guid);
                prefabField.SetValue(component, reference);
                evidence.wiring.Add("_gamePrefab: " + prefabField.FieldType.Name + " with m_AssetGUID=" + guid);
            }
            catch (Exception ex)
            {
                evidence.wiring.Add("_gamePrefab: " + Describe(ex));
            }
        }

        /// <summary>True for components that initialise Steam (see the skip in WireComponentGraph).</summary>
        private static bool IsSteamComponent(Type type)
        {
            string name = type.FullName ?? type.Name;
            return name.IndexOf("SteamManager", StringComparison.OrdinalIgnoreCase) >= 0;
        }

        /// <summary>Serialized instance fields of a component type (public, or [SerializeField]).</summary>
        private static IEnumerable<FieldInfo> SerializedFields(Type type)
        {
            foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.Public |
                                                       BindingFlags.NonPublic))
            {
                if (field.IsStatic || field.IsInitOnly || field.IsLiteral) continue;
                if (!field.IsPublic && !field.IsDefined(typeof(SerializeField), true)) continue;
                yield return field;
            }
        }

        /// <summary>
        /// Gives every null serialized component reference a real instance, recursively (bounded), and
        /// records each step.  This is deliberately a *probe*: it manufactures the data the game's own
        /// scene would supply so the reconstructed boot chain can run past the point where it would
        /// otherwise stop, and every line it reports says what was invented.
        /// </summary>
        private static void WireComponentGraph(Component component, GameStartEvidence evidence, Transform parent,
                                               int depth, HashSet<Type> visited)
        {
            if (component == null || depth > MaxWireDepth || evidence.wired_components >= MaxWiredComponents) return;
            Type type = component.GetType();
            if (!visited.Add(type)) return;

            foreach (FieldInfo field in SerializedFields(type))
            {
                if (!typeof(Component).IsAssignableFrom(field.FieldType)) continue;
                if (field.FieldType == typeof(Transform) || field.FieldType == typeof(RectTransform)) continue;

                object existing = null;
                try { existing = field.GetValue(component); }
                catch (Exception readError) { evidence.wiring.Add(Label(type, field) + ": unreadable (" + readError.GetType().Name + ")"); continue; }
                if (existing != null) continue;                       // already assigned (specific wiring, or scene data)

                if (field.FieldType.IsAbstract)
                {
                    evidence.wiring.Add(Label(type, field) + ": skipped, abstract " + field.FieldType.Name);
                    continue;
                }
                // Steam-derived components are skipped by default.  With the *genuine* Steamworks.NET in
                // place, creating one runs SteamAPI.Init(), and without a Steam client that is a native
                // crash rather than a managed exception -- measured: the Editor crashed right after this
                // probe had already written its evidence (status STARTED, 0 exceptions).  The game itself
                // only does this under Steam, so the headless gates must not; set WireSteamEnv=1 when a
                // Steam client is actually running.
                if (IsSteamComponent(field.FieldType) && Environment.GetEnvironmentVariable(WireSteamEnv) != "1")
                {
                    evidence.wiring.Add(Label(type, field) + ": skipped " + field.FieldType.Name +
                                        " (Steam init crashes without a Steam client; set " + WireSteamEnv +
                                        "=1 to include it)");
                    continue;
                }
                try
                {
                    var holder = new GameObject("ManosabaWired_" + field.FieldType.Name);
                    holder.SetActive(false);          // no Awake until the whole graph is wired
                    holder.transform.SetParent(parent, false);
                    Component created = holder.AddComponent(field.FieldType);
                    if (created == null)
                    {
                        evidence.wiring.Add(Label(type, field) + ": AddComponent returned null");
                        continue;
                    }
                    field.SetValue(component, created);
                    evidence.wired_components++;
                    evidence.wiring.Add(Label(type, field) + " := new " + field.FieldType.Name + "()");
                    WireComponentGraph(created, evidence, holder.transform, depth + 1, visited);
                }
                catch (Exception ex)
                {
                    evidence.wiring.Add(Label(type, field) + ": " + Describe(ex));
                }
            }
        }

        /// <summary>Activates the wired holders (top level once; the subtrees follow), so Awake runs last.</summary>
        private static void ActivateWired(Transform host, GameStartEvidence evidence)
        {
            int activated = 0;
            for (int i = 0; i < host.childCount; i++)
            {
                Transform child = host.GetChild(i);
                if (child == null || child.gameObject.activeSelf) continue;
                child.gameObject.SetActive(true);
                activated++;
            }
            evidence.activated_holders = activated;
        }

        private static string Label(Type owner, FieldInfo field)
        {
            return owner.Name + "." + field.Name;
        }

        private static string FirstLines(string text, int count)
        {
            if (string.IsNullOrEmpty(text)) return "";
            string[] lines = text.Split('\n');
            return string.Join("\n", lines.Take(count)).TrimEnd();
        }

        /// <summary>GameInitializer.Start()'s only precondition: its scene-provided _steamManager reference.</summary>
        private static bool SteamManagerWired(Type type, Component component)
        {
            FieldInfo steam = type.GetField("_steamManager", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            object value = steam != null ? steam.GetValue(component) : null;
            return value is UnityEngine.Object unityObject ? unityObject != null : value != null;
        }

        private static bool s_playTraceHooked;
        private static int s_playTraceCount;

        /// <summary>
        /// Logs every IScriptPlayer.OnPlay (first 8) once the engine is initialized.  Unity's Editor log records the managed
        /// caller stack of each Debug.Log, so each line names who (re)started playback.  Added for G4b round 7, where
        /// System_Title restarted from its first @back every ~0.2 s in skip mode and never reached @showUI / @stop.
        /// </summary>
        private static void TryHookPlaybackTrace()
        {
            if (s_playTraceHooked) return;
            try
            {
                Type engineType = Type.GetType("Naninovel.Engine, Elringus.Naninovel.Runtime");
                Type playerType = Type.GetType("Naninovel.IScriptPlayer, Elringus.Naninovel.Runtime");
                MethodInfo getService = engineType?.GetMethods(BindingFlags.Public | BindingFlags.Static)
                    .FirstOrDefault(m => m.Name == "GetService" && m.IsGenericMethodDefinition && m.GetParameters().Length == 0);
                object player = getService != null && playerType != null
                    ? getService.MakeGenericMethod(playerType).Invoke(null, null) : null;
                EventInfo onPlay = playerType != null ? playerType.GetEvent("OnPlay") : null;
                if (player == null || onPlay == null) return;
                MethodInfo handler = typeof(ManosabaGameStart).GetMethod(nameof(OnScriptPlay), BindingFlags.NonPublic | BindingFlags.Static);
                onPlay.AddEventHandler(player, Delegate.CreateDelegate(onPlay.EventHandlerType, handler));
                s_playTraceHooked = true;
                Debug.Log("[ManosabaGameStart] playback trace hooked on IScriptPlayer.OnPlay");
            }
            catch (Exception ex)
            {
                s_playTraceHooked = true;
                Debug.Log("[ManosabaGameStart] playback trace NOT hooked: " + Describe(ex));
            }
        }

        private static void OnScriptPlay(object script)
        {
            if (++s_playTraceCount > 8) return;
            string path = script?.GetType().GetProperty("Path")?.GetValue(script) as string;
            Debug.Log("[ManosabaGameStart] OnPlay #" + s_playTraceCount + " " + path + " t=" +
                      Time.realtimeSinceStartup.ToString("F2", CultureInfo.InvariantCulture) + " frame=" + Time.frameCount);
        }

        private static string Describe(Exception ex)
        {
            if (ex == null) return "(null)";
            string first = (ex.StackTrace ?? "").Split('\n').FirstOrDefault() ?? "";
            return ex.GetType().Name + ": " + ex.Message + "  @ " + first.Trim();
        }

        private static void Write(GameStartEvidence evidence)
        {
            try
            {
                string path = Environment.GetEnvironmentVariable(EvidenceEnv);
                if (string.IsNullOrEmpty(path))
                {
                    path = Path.Combine(Application.persistentDataPath, "manosaba_gamestart.json");
                }
                path = Path.GetFullPath(path);
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllText(path, JsonUtility.ToJson(evidence, true), new UTF8Encoding(false));
                Debug.Log("[ManosabaGameStart] " + evidence.status + " -> " + path);
            }
            catch (Exception ex)
            {
                Debug.LogError("[ManosabaGameStart] could not write evidence: " + ex);
            }
        }

        /// <summary>
        /// Writes the same evidence to a sidecar path (`&lt;evidence&gt;.partial.json`) that the Play Mode gate
        /// does NOT watch.  The gate leaves Play Mode the moment the real evidence file exists, so partial state
        /// cannot go there without truncating the run; but when the main thread is blocked in one very long frame
        /// the probe never reaches its final Write at all and the run leaves nothing behind.  The sidecar makes
        /// every run readable, including the runs that were cut short.
        /// </summary>
        private static void WritePartial(GameStartEvidence evidence)
        {
            try
            {
                string path = Environment.GetEnvironmentVariable(EvidenceEnv);
                if (string.IsNullOrEmpty(path))
                {
                    path = Path.Combine(Application.persistentDataPath, "manosaba_gamestart.json");
                }
                path = Path.GetFullPath(path) + ".partial.json";
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllText(path, JsonUtility.ToJson(evidence, true), new UTF8Encoding(false));
            }
            catch (Exception)
            {
                // Best-effort diagnostics only; never let it affect the measurement.
            }
        }

        [Serializable]
        public class LogEntry
        {
            public string type;
            public string message;
            public string stack;
        }

        [Serializable]
        public class GameStartEvidence
        {
            public string status = "NOT_RUN";
            public string generated;
            public string unity_version;
            public bool is_editor;
            public string platform;
            public string entry_type;
            public bool type_resolved;
            public bool component_created;
            public string component_source;   // "scene" (the rebuilt boot scene's own component) or "created"

            // Naninovel engine state, sampled after the chain window (see CollectNaninovelState).
            // NOTE: these are deliberately *not* nullable.  Unity's JsonUtility - which writes this evidence -
            // silently drops `bool?`/`int?` fields, so the first version of this block produced `null` for every
            // value and looked like "the engine reported nothing" when in fact it was never serialized.  Each
            // value therefore carries an explicit `..._known` flag instead.
            public string naninovel_state;             // "read", or why it could not be read
            public bool naninovel_initialized_known;
            public bool naninovel_initialized;
            public bool naninovel_initializing_known;
            public bool naninovel_initializing;
            public bool naninovel_script_manager;
            public bool naninovel_total_commands_known;
            public int naninovel_total_commands;       // commands across every script the manager can see
            public bool naninovel_script_player;
            public bool naninovel_playing_known;
            public bool naninovel_playing;
            public string naninovel_played_script;
            public string naninovel_playback_spot;
            // Opt-in automatic script-player advance telemetry (MANOSABA_GAMESTART_AUTO_ADVANCE=1).
            public string auto_advance = "not run";
            public float auto_advance_seconds;
            public int auto_advance_transitions;
            public int auto_advance_wait_releases;
            public string auto_advance_trace = "";
            public string auto_advance_final = "";
            public string auto_advance_restore = "";
            public List<string> played_scripts = new List<string>();   // consumed by gate G6's runtime coverage
            public string component_type;
            public bool addressables_field_found;
            public bool addressables_manager_created;
            public string addressables_manager_type;
            public string field_error;
            public int declared_method_count;
            public List<string> declared_methods = new List<string>();
            public bool start_method_found;
            public string start_invocation;
            public bool chain_started_by_unity;
            public bool initialize_method_found;
            public string initialize_invocation;
            public string initialize_completion;
            public string gameprefab_reference_type;
            public bool gameprefab_instantiate_found;
            public string gameprefab_handle_type;
            public string gameprefab_handle_status;
            public string gameprefab_instance;
            public string gameprefab_error;
            public List<string> wiring = new List<string>();
            public List<string> spawned_roots = new List<string>();
            public bool entry_sequence_on_root;
            public bool entry_sequence_in_hierarchy;
            public bool whenall_defaults_completed;
            public string whenall_defaults_error;
            // Engine-initialisation telemetry.  `Engine.Initialize` awaits `services[i].InitializeService()`
            // one service at a time and fires `Engine.OnInitializationProgress` with 0.25 + 0.5 * i / count
            // before each one, so a progress trace is a direct, per-service measure of how far the engine got.
            // Without it "still initialising after 8 s" and "hung on the first service" are indistinguishable.
            public int engine_progress_events;
            public float engine_progress_last = -1f;
            public string engine_progress_trace = "";   // "seconds:value" pairs, first 24 events
            public bool engine_services_known;
            public int engine_services_count;
            public string engine_services = "";         // first 40 service type names, comma separated
            public int engine_samples;
            public string engine_sample_trace = "";     // "seconds:initializing/initialized:progress" samples
            // Addressables / master-data state, the two inputs the resource provider path depends on.
            public string master_data_state = "not read";
            public string master_data_asset_key_names = "";
            public string addressables_manager_state = "not read";
            public int addressables_resource_locations = -1;
            public bool addressables_additional_catalog_loaded;
            // Direct exercise of the engine's own file-IO primitive (see ProbeFileIo): the measured boot chain
            // stops on its first call, so whether that call can complete here at all is a fact worth having.
            public string naninovel_file_io_path = "";
            public bool naninovel_file_io_exists;
            public string naninovel_file_io_read = "not run";
            public string naninovel_file_io_write = "not run";
            // Post-initialization attribution (see ProbePostInitializationChain): the engine freezes on the
            // first post-init task, so these name the registered tasks and the individual awaits inside it.
            public string post_init_state = "not read";
            public string post_init_tasks = "";
            public int post_init_task_count;
            public string settings_slot_load = "not run";
            public string global_slot_load = "not run";
            // What the *engine's* own post-init task has already stored: non-null means that await returned.
            public string engine_settings_state = "not read";
            public string engine_global_state = "not read";
            // Per-service timing of `LoadServiceState(SettingsStateMap)`, the call the engine's first
            // post-initialization task is stopped on.
            public string settings_state_services = "not run";
            // Per-UI timing of `IManagedUI.Initialize()`, the third await inside the last post-init task.
            public string managed_ui_initialize = "not run";
            // Resource-provider caches and in-flight runner tables (see CollectResourceProviderState).
            public string provider_state = "not read";
            // Re-invocation of the stuck post-init delegate and, when it pends, its state-machine state.
            public string post_init_reinvoke = "not run";
            // State machine of the task the ENGINE received from the last post-init delegate.
            public string post_init_capture = "not run";
            /// <summary>Direct invocation of the delegate the capture wrapper replaced.</summary>
            public string post_init_original = "not run";
            /// <summary>`Engine.initializeTCS`: state + the state machine awaiting it.</summary>
            public string engine_tcs = "not read";
            /// <summary>Call stack of the captured delegate invocation, to name the caller.</summary>
            public string post_init_capture_stack = "not run";
            public int frames_waited;
            public float window_seconds;
            public int wired_components;
            public int activated_holders;
            public int log_errors;
            public int log_exceptions;
            public string first_exception;
            public string error;
            public List<LogEntry> log = new List<LogEntry>();
        }
    }
}

