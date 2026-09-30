// Integration-layer boot component of the reconstruction.
//
// This is NOT game code: the reconstructed entry point is WitchTrials.Views.GameInitializer, whose
// method bodies are still IL2CPP stubs.  This component exists so a real Windows Player built from
// this project can be started and can report, at runtime, what actually made it into the build:
//
//   * which of the 43 reconstructed assemblies are loaded, and how many types each exposes
//   * whether the reconstructed entry-point types resolve
//   * whether the shipped Addressables catalog can be read through the real Addressables runtime
//     (the same ContentCatalogData.LoadFromFile path tools/audit uses inside the Editor)
//
// Run with `-manosabaSmoke` to write an evidence JSON and exit with a status code:
//   0 = every assertion passed, 1 = a probe failed, 2 = could not write evidence.
//
// Evidence path: <persistentDataPath>/manosaba_smoke.json, or `-manosabaEvidence <path>` if given.
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Manosaba.Integration
{
    public class ManosabaBoot : MonoBehaviour
    {
        private const string SmokeFlag = "-manosabaSmoke";
        private const string EvidenceFlag = "-manosabaEvidence";
        private const string ContentFlag = "-manosabaContent";

        // Environment equivalents: a Player is driven by command-line flags (g5_player_smoke.ps1), but the
        // Editor Play Mode gate (g4_play_mode.ps1) launches Unity with -executeMethod and cannot add its own
        // flags, so the same probe has to be drivable from the environment.  Both paths end in this class,
        // which keeps G4 and G5 comparing the very same evidence.
        private const string SmokeEnv = "MANOSABA_SMOKE";
        private const string ContentEnv = "MANOSABA_CONTENT";
        private const string EvidenceEnv = "MANOSABA_EVIDENCE";

        /// <summary>
        /// Addresses loaded from the project's *own* Addressables content (G3 build) when the content
        /// probe runs.  They are the shipped addresses (see
        /// tools/unity-rebuild/a13f_plan_addressables.py), so a successful load also proves the
        /// addressing scheme survived the round trip.
        /// </summary>
        private static readonly string[] ContentAddresses =
        {
            "Naninovel/Scripts/Act01_Chapter01/Act01_Chapter01_Adv01",   // Naninovel.Script (managed-ref graph)
            "General/Data/AuthorData",                                   // WitchTrials.Models.AuthorData
            "Naninovel/Localization/zh-Hans/Text/CharacterNames",        // TextAsset
            "Naninovel/Fonts/SourceHanSansKR/Assets/SourceHanSansKR",    // Font
        };

        /// <summary>
        /// Prefab addresses instantiated by the content probe.  Chosen from the reconstructed prefab
        /// ledger (`tools/unity-rebuild/pick_smoke_prefabs.py`) as prefabs whose object graph is fully
        /// bound *and* which carry a component from a reconstructed game assembly -- so a successful
        /// instantiate also proves the Player bound reconstructed scripts to reconstructed content,
        /// which asset loading alone does not.
        /// </summary>
        private static readonly string[] ContentPrefabAddresses =
        {
            "General/Prefabs/Debug/DebugLabel",                  // 3 objects, Unity built-ins only (baseline)
            "Naninovel/Spawn/Blur",                              // Naninovel.FX.Blur (reconstructed)
            "General/Prefabs/UI/Buttons/SimpleCloseButton",       // GigaCreation.Essentials.Ui.TweeningButton (reconstructed)
        };

        /// <summary>Assembly prefixes whose components count as reconstructed game code, not engine code.</summary>
        private static readonly string[] GameAssemblyPrefixes =
        {
            "WitchTrials", "GigaCreation", "Elringus", "Naninovel", "CustomInspector",
        };

        /// <summary>Assemblies the reconstruction provides; the smoke run reports which are loaded.</summary>
        private static readonly string[] ExpectedAssemblies =
        {
            // The reconstructed Assembly-CSharp ships as WitchTrials.Game: an assembly definition
            // cannot reuse Unity's predefined assembly name (see tools/unity-rebuild/apply_game_sources.py).
            "WitchTrials.Game",
            "CustomInspector", "CustomInspector.Helpers", "ppt-noise",
            "Elringus.Naninovel.Runtime", "Elringus.SpriteDicing.Runtime", "Naninovel.Common",
            "GigaCreation.Essentials.AddressablesUtils", "GigaCreation.Essentials.Dialogs",
            "GigaCreation.Essentials.Input", "GigaCreation.Essentials.Localization",
            "GigaCreation.Essentials.MasterData", "GigaCreation.Essentials.Overlays",
            "GigaCreation.Essentials.Platforms", "GigaCreation.Essentials.SaveLoad",
            "GigaCreation.Essentials.Toggleables", "GigaCreation.Essentials.UI",
            "GigaCreation.Essentials.UniTasks", "GigaCreation.Essentials.WebRequestUtils",
            "GigaCreation.NaninovelExtender.Audio", "GigaCreation.NaninovelExtender.Cameras",
            "GigaCreation.NaninovelExtender.Common", "GigaCreation.NaninovelExtender.Core",
            "GigaCreation.NaninovelExtender.Effects", "GigaCreation.NaninovelExtender.ExtendedActors",
            "GigaCreation.NaninovelExtender.ExtendedSpawn", "GigaCreation.NaninovelExtender.Localization",
            "GigaCreation.NaninovelExtender.Movies", "GigaCreation.NaninovelExtender.ResourceProvider",
            "GigaCreation.NaninovelExtender.UI", "GigaCreation.Tools.BuildTimestampDisplay",
            "GigaCreation.Tools.Debugging.Core", "GigaCreation.Tools.Debugging.General",
            "GigaCreation.Tools.Debugging.TextDisplays",
            "GigaCreation.Tools.Debugging.TextDisplays.BuildTimestamps",
            "GigaCreation.Tools.Debugging.UI", "GigaCreation.Tools.General",
            "GigaCreation.Tools.Private.PlayableDirectors", "GigaCreation.Tools.Service",
            "GigaCreation.Tools.UI", "GigaCreation.Tools.UndoExtensions", "GigaCreation.Tools.UniTasks",
            "Il2CppDummyDll",
        };

        /// <summary>Reconstructed types that must resolve in the Player for the build to be meaningful.</summary>
        private static readonly string[] ProbeTypes =
        {
            "WitchTrials.Views.GameInitializer, WitchTrials.Game",
            "WitchTrials.Debugging.DebugOptions, WitchTrials.Game",
            "WitchTrials.Models.TrialStateManager, WitchTrials.Game",
            "GigaCreation.Essentials.AddressablesUtils.AddressablesManager, GigaCreation.Essentials.AddressablesUtils",
            "Naninovel.Engine, Elringus.Naninovel.Runtime",
        };

        private SmokeReport _report;
        private string _status = "booting";

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("[ManosabaBoot] awaking (integration layer, not game code)");
        }

        private void Start()
        {
            // The game-start probe rides along when asked for: it is what turns "the project boots" into
            // "the reconstructed entry point executed, and here is where it stops".
            if (Environment.GetEnvironmentVariable("MANOSABA_GAMESTART") == "1")
            {
                var host = new GameObject("ManosabaGameStart");
                host.AddComponent<ManosabaGameStart>();
            }

            _report = Collect();
            _status = _report.status + "  assemblies=" + _report.assemblies_found + "/" +
                      ExpectedAssemblies.Length + "  probes=" + _report.types_probed_ok + "/" +
                      ProbeTypes.Length;
            if (_report.content != null)
            {
                _status += "  content=" + _report.content.status +
                           " (" + _report.content.loaded + " assets, " + _report.content.prefabs.Count +
                           " prefabs, " + _report.content.game_script_components.Count +
                           " reconstructed components)";
            }

            string path = WriteEvidence(_report);
            Debug.Log("[ManosabaBoot] " + _status + (path == null ? " (evidence not written)" : "  evidence=" + path));

            if (HasFlag(SmokeFlag))
            {
                // With the game-start probe enabled the process must outlive this method: the probe writes
                // its own evidence a few seconds later, and quitting here would kill the run before the boot
                // chain has been measured (in the Editor Application.Quit is a no-op, which is why this only
                // shows up in the standalone Player).
                if (Environment.GetEnvironmentVariable("MANOSABA_GAMESTART") == "1")
                {
                    StartCoroutine(QuitAfterGameStart());
                }
                else
                {
                    Application.Quit(_report.status == "PASS" ? 0 : 1);
                }
            }
        }

        /// <summary>Waits for the game-start evidence, then exits with a status that covers both probes.</summary>
        private IEnumerator QuitAfterGameStart()
        {
            string evidence = Environment.GetEnvironmentVariable("MANOSABA_GAMESTART_EVIDENCE");
            float deadline = Time.realtimeSinceStartup + 120f;
            while (Time.realtimeSinceStartup < deadline)
            {
                if (!string.IsNullOrEmpty(evidence) && File.Exists(evidence)) break;
                yield return null;
            }

            bool bootOk = _report != null && _report.status == "PASS";
            bool gameOk = false;
            if (!string.IsNullOrEmpty(evidence) && File.Exists(evidence))
            {
                try
                {
                    string text = File.ReadAllText(evidence);
                    gameOk = text.Contains("\"status\": \"STARTED\"") || text.Contains("\"status\":\"STARTED\"");
                }
                catch (Exception ex)
                {
                    Debug.LogError("[ManosabaBoot] could not read game-start evidence: " + ex.Message);
                }
            }
            Debug.Log("[ManosabaBoot] quitting: boot=" + bootOk + " game_start=" + gameOk);
            Application.Quit(bootOk && gameOk ? 0 : 1);
        }

        private void OnGUI()
        {
            // Deliberately IMGUI: the reconstruction has no UI assets yet, and this must not depend
            // on anything that is still a stub.
            GUI.Label(new Rect(20, 20, 1200, 200),
                "Manosaba reconstruction - integration boot\n" + _status + "\n" +
                "Unity " + Application.unityVersion + "  " + Application.platform + "\n" +
                "game code: WitchTrials.Views.GameInitializer is still an IL2CPP stub");
        }

        // -- evidence ---------------------------------------------------------------------

        private SmokeReport Collect()
        {
            var report = new SmokeReport
            {
                generated = DateTime.UtcNow.ToString("o"),
                unity_version = Application.unityVersion,
                product_name = Application.productName,
                company_name = Application.companyName,
                app_version = Application.version,
                platform = Application.platform.ToString(),
                is_editor = Application.isEditor,
                data_path = Application.dataPath,
                streaming_assets_path = Application.streamingAssetsPath,
                persistent_data_path = Application.persistentDataPath,
                command_line = string.Join(" ", Environment.GetCommandLineArgs()),
            };

            // --- loaded assemblies --------------------------------------------------------
            var loaded = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly asm in loaded.OrderBy(a => a.GetName().Name, StringComparer.Ordinal))
            {
                string name = asm.GetName().Name;
                int types = -1;
                try { types = asm.GetTypes().Length; }
                catch (ReflectionTypeLoadException ex) { types = ex.Types.Count(t => t != null); }
                catch (Exception) { /* dynamic or unloadable assembly */ }
                report.loaded_assemblies.Add(new AssemblyRecord { name = name, types = types });

                bool expected = ExpectedAssemblies.Contains(name);
                if (expected) report.assemblies_found++;
                else if (report.other_assemblies.Count < 200) report.other_assemblies.Add(name);
            }

            foreach (string expected in ExpectedAssemblies)
            {
                if (!report.loaded_assemblies.Any(a => a.name == expected)) report.assemblies_missing.Add(expected);
            }

            // --- reconstructed types ------------------------------------------------------
            foreach (string probe in ProbeTypes)
            {
                var record = new TypeProbe { probe = probe };
                try
                {
                    Type t = Type.GetType(probe, throwOnError: false);
                    record.resolved = t != null;
                    if (t != null)
                    {
                        record.full_name = t.FullName;
                        record.assembly = t.Assembly.GetName().Name;
                        record.methods = t.GetMethods(BindingFlags.Public | BindingFlags.NonPublic |
                                                      BindingFlags.Instance | BindingFlags.Static).Length;
                    }
                }
                catch (Exception ex)
                {
                    record.error = ex.GetType().Name + ": " + ex.Message;
                }
                if (record.resolved) report.types_probed_ok++;
                report.type_probes.Add(record);
            }

            // --- shipped Addressables catalog (reference) ---------------------------------
            report.catalog = ProbeCatalog(Path.Combine(Application.streamingAssetsPath, "shipped-aa", "catalog.bin"));

            // --- the project's own Addressables content -----------------------------------
            report.content = ProbeContent();

            bool assembliesOk = report.assemblies_missing.Count == 0;
            bool typesOk = report.types_probed_ok == ProbeTypes.Length;
            // A shipped catalog must parse; not shipping one yet is reported but not a failure.
            bool catalogOk = report.catalog.status == "PARSED" || report.catalog.status == "ABSENT";
            // Content loading is only a pass criterion when the run asks for it (-manosabaContent).
            bool requireContent = HasFlag(ContentFlag);
            bool contentOk = !requireContent || report.content.status == "LOADED";
            report.status = assembliesOk && typesOk && catalogOk && contentOk ? "PASS" : "FAIL";
            if (!assembliesOk) report.notes.Add("missing reconstructed assemblies: " + string.Join(", ", report.assemblies_missing));
            if (!typesOk) report.notes.Add("unresolved reconstructed types: " + string.Join(", ",
                report.type_probes.Where(p => !p.resolved).Select(p => p.probe)));
            if (report.catalog.status == "ABSENT")
            {
                report.notes.Add("no catalog at " + report.catalog.path +
                                 " -- run tools/unity-rebuild/apply_shipped_aa.ps1 and rebuild to exercise the Addressables runtime in the Player");
            }
            else if (!catalogOk)
            {
                report.notes.Add("catalog probe status=" + report.catalog.status + " error=" + report.catalog.error);
            }
            return report;
        }

        /// <summary>
        /// Reads the shipped catalog with the runtime that produced it. ContentCatalogData.LoadFromFile
        /// is `internal` in Addressables 2.4.6, so it is reached by exact signature -- the same call
        /// ContentCatalogProvider/BinaryAssetProvider make in a real player.
        /// </summary>
        private CatalogProbe ProbeCatalog(string catalogPath)
        {
            var probe = new CatalogProbe { path = catalogPath };
            // Only the shipped reference catalog retains the pre-reconstruction name.
            // Keep this alias scoped to its synchronous probe, not all runtime loads.
            ResolveEventHandler resolveLegacyAssembly = (sender, args) =>
                new AssemblyName(args.Name).Name == "Assembly-CSharp"
                    ? typeof(WitchTrials.Views.GameInitializer).Assembly : null;
            AppDomain.CurrentDomain.AssemblyResolve += resolveLegacyAssembly;
            try
            {
                if (!File.Exists(catalogPath))
                {
                    probe.status = "ABSENT";
                    return probe;
                }

                probe.bytes = new FileInfo(catalogPath).Length;

                Type ccd = Type.GetType("UnityEngine.AddressableAssets.ResourceLocators.ContentCatalogData, Unity.Addressables");
                if (ccd == null)
                {
                    probe.status = "TYPE_MISSING";
                    return probe;
                }

                const BindingFlags any = BindingFlags.Public | BindingFlags.NonPublic |
                                         BindingFlags.Static | BindingFlags.Instance;
                MethodInfo load = ccd.GetMethod("LoadFromFile", any, null, new[] { typeof(string), typeof(int) }, null);
                if (load == null)
                {
                    probe.status = "LOADER_MISSING";
                    return probe;
                }

                object data = load.Invoke(null, new object[] { catalogPath, 1024 });

                MethodInfo makeLocator = ccd.GetMethod("CreateCustomLocator", any, null,
                    new[] { typeof(string), typeof(string), typeof(int) }, null);
                if (makeLocator == null)
                {
                    probe.status = "LOCATOR_FACTORY_MISSING";
                    return probe;
                }

                var locator = makeLocator.Invoke(data, new object[] { catalogPath, null, 1024 }) as IResourceLocator;
                if (locator == null)
                {
                    probe.status = "LOCATOR_NULL";
                    return probe;
                }

                probe.locator_id = locator.LocatorId;
                foreach (object key in locator.Keys)
                {
                    probe.keys++;
                    IList<UnityEngine.ResourceManagement.ResourceLocations.IResourceLocation> locations;
                    if (locator.Locate(key, typeof(object), out locations) && locations != null)
                    {
                        probe.location_edges += locations.Count;
                        if (locations.Count > 1) probe.keys_with_multiple_locations++;
                    }
                }
                probe.status = "PARSED";
            }
            catch (Exception ex)
            {
                Exception inner = ex is TargetInvocationException && ex.InnerException != null ? ex.InnerException : ex;
                probe.status = "ERROR";
                probe.error = inner.GetType().Name + ": " + inner.Message;
            }
            finally
            {
                AppDomain.CurrentDomain.AssemblyResolve -= resolveLegacyAssembly;
            }
            return probe;
        }

        private static bool HasFlag(string flag)
        {
            if (Environment.GetCommandLineArgs().Any(a => string.Equals(a, flag, StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }
            string env = flag == SmokeFlag ? SmokeEnv : flag == ContentFlag ? ContentEnv : null;
            return env != null && Environment.GetEnvironmentVariable(env) == "1";
        }

        private static string EvidencePath()
        {
            string[] args = Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length - 1; i++)
            {
                if (string.Equals(args[i], EvidenceFlag, StringComparison.OrdinalIgnoreCase))
                {
                    return Path.GetFullPath(args[i + 1]);
                }
            }
            string fromEnvironment = Environment.GetEnvironmentVariable(EvidenceEnv);
            if (!string.IsNullOrEmpty(fromEnvironment)) return Path.GetFullPath(fromEnvironment);
            return Path.Combine(Application.persistentDataPath, "manosaba_smoke.json");
        }

        private static string WriteEvidence(SmokeReport report)
        {
            try
            {
                string path = EvidencePath();
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                File.WriteAllText(path, JsonUtility.ToJson(report, true), new UTF8Encoding(false));
                return path;
            }
            catch (Exception ex)
            {
                Debug.LogError("[ManosabaBoot] could not write evidence: " + ex);
                return null;
            }
        }

        /// <summary>
        /// Loads assets from the project's own Addressables content: initialise the runtime (which
        /// reads StreamingAssets/aa/settings.json + catalog.bin produced by the G3 content build) and
        /// then load one asset per representative address.  A successful load means the whole chain
        /// holds: conversion -> content build -> address -> runtime.
        /// </summary>
        private ContentProbe ProbeContent()
        {
            var probe = new ContentProbe();
            try
            {
                // Addressables.InitializeAsync() releases its own handle on completion, so it must not be
                // waited on (doing so throws "Attempting to use an invalid operation handle" -- the first
                // version of this probe did exactly that).  Firing it and letting each load chain on the
                // initialisation operation is the supported pattern.
                Addressables.InitializeAsync();

                foreach (string address in ContentAddresses)
                {
                    var record = new ContentRecord { address = address };
                    AsyncOperationHandle<UnityEngine.Object> op =
                        Addressables.LoadAssetAsync<UnityEngine.Object>(address);
                    op.WaitForCompletion();
                    record.status = op.Status.ToString();
                    UnityEngine.Object asset = op.Result;
                    if (asset != null)
                    {
                        record.type = asset.GetType().FullName;
                        record.name = asset.name;
                        record.detail = DescribeAsset(asset);
                        // Which provider and which bundle served this address: without it, a load in the
                        // Editor cannot be told apart from the "Use Asset Database" play-mode shortcut, so
                        // the G4 evidence would not prove the G3 content build was really consumed.
                        try
                        {
                            AsyncOperationHandle<System.Collections.Generic.IList<
                                UnityEngine.ResourceManagement.ResourceLocations.IResourceLocation>> locations =
                                Addressables.LoadResourceLocationsAsync(address);
                            locations.WaitForCompletion();
                            if (locations.Result != null && locations.Result.Count > 0)
                            {
                                UnityEngine.ResourceManagement.ResourceLocations.IResourceLocation location =
                                    locations.Result[0];
                                record.provider = location.ProviderId;
                                record.internal_id = location.InternalId;
                                var options = location.Data as
                                    UnityEngine.ResourceManagement.ResourceProviders.AssetBundleRequestOptions;
                                if (options != null) record.bundle = options.BundleName;
                            }
                            Addressables.Release(locations);
                        }
                        catch (Exception locationError)
                        {
                            record.provider_error = Describe(locationError);
                        }
                        probe.loaded++;
                        Addressables.Release(op);
                    }
                    else
                    {
                        probe.failed++;
                        if (op.OperationException != null) record.error = Describe(op.OperationException);
                    }
                    probe.records.Add(record);
                    if (string.IsNullOrEmpty(probe.initialize_status))
                    {
                        probe.initialize_status = "chained on InitializeAsync (first load status: " + record.status + ")";
                    }
                }

                // What the runtime actually loaded: the locators registered by our own catalog.
                int keys = 0;
                foreach (IResourceLocator locator in Addressables.ResourceLocators)
                {
                    if (string.IsNullOrEmpty(probe.locator_id)) probe.locator_id = locator.LocatorId;
                    foreach (object unused in locator.Keys) keys++;
                }
                probe.locator_keys = keys;

                // Instantiate reconstructed prefabs: this is the closest thing to "the game runs" that
                // is reachable while the game's own method bodies are still IL2CPP stubs -- the object
                // graph comes from the project's own content build, not from the Editor.
                foreach (string prefabAddress in ContentPrefabAddresses)
                {
                    var prefab = new PrefabRecord { address = prefabAddress };
                    try
                    {
                        AsyncOperationHandle<GameObject> inst = Addressables.InstantiateAsync(prefabAddress);
                        inst.WaitForCompletion();
                        prefab.status = inst.Status.ToString();
                        GameObject instance = inst.Result;
                        if (instance != null)
                        {
                            Component[] all = instance.GetComponentsInChildren<Component>(true);
                            prefab.instance = instance.name;
                            prefab.transforms = instance.GetComponentsInChildren<Transform>(true).Length;
                            prefab.components = all.Length;
                            // The component *types* are the interesting part: they show which assemblies
                            // the instantiated object graph actually needed (Unity built-ins only, or a
                            // reconstructed game type -- the latter is script binding, not asset loading).
                            foreach (Component component in all)
                            {
                                string type = component == null ? "(missing script)" : component.GetType().FullName;
                                prefab.component_types.Add(type);
                                if (IsGameComponent(component)) prefab.game_component_types.Add(type);
                            }
                            prefab.detail = "instance '" + instance.name + "' with " + prefab.transforms +
                                            " transforms / " + all.Length + " components [" +
                                            string.Join(", ", prefab.component_types) + "]";
                            Addressables.ReleaseInstance(instance);
                        }
                        else if (inst.OperationException != null)
                        {
                            prefab.error = Describe(inst.OperationException);
                        }
                    }
                    catch (Exception ex)
                    {
                        prefab.error = Describe(ex);
                    }
                    if (prefab.game_component_types.Count > 0)
                    {
                        probe.game_script_components.AddRange(prefab.game_component_types);
                    }
                    probe.prefabs.Add(prefab);
                }

                // A missing prefab instantiation is not a side note: the whole point of the content probe
                // is that the project's own built catalog yields a usable object graph.  Loading the leaf
                // assets but failing an instantiate is PARTIAL, not LOADED -- and so is instantiating
                // only engine components, because that would prove asset loading but not script binding.
                bool prefabsOk = probe.prefabs.Count > 0 &&
                                 probe.prefabs.TrueForAll(p => !string.IsNullOrEmpty(p.instance));
                probe.status = probe.loaded > 0 && probe.failed == 0 && prefabsOk &&
                               probe.game_script_components.Count > 0
                    ? "LOADED"
                    : (probe.loaded > 0 ? "PARTIAL" : "FAILED");
            }
            catch (Exception ex)
            {
                probe.status = "ERROR";
                probe.error = Describe(ex);
            }
            return probe;
        }

        private static string Describe(Exception ex)
        {
            Exception inner = ex is AggregateException agg && agg.InnerException != null ? agg.InnerException : ex;
            return inner.GetType().Name + ": " + inner.Message;
        }

        /// <summary>Type-specific evidence that the loaded object really carries shipped data.</summary>
        private static string DescribeAsset(UnityEngine.Object asset)
        {
            if (asset is TextAsset text)
            {
                return "text length " + (text.text == null ? 0 : text.text.Length);
            }
            if (asset is Font font)
            {
                return "fontNames " + (font.fontNames == null ? 0 : font.fontNames.Length);
            }
            // Collection fields that prove the managed-reference graph / list data survived:
            // Naninovel.Script.lines, WitchTrials.Models.*._items, ...
            foreach (string fieldName in new[] { "lines", "_items", "playlist" })
            {
                FieldInfo field = asset.GetType().GetField(fieldName,
                    BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
                object value = field?.GetValue(asset);
                if (value is System.Collections.ICollection collection)
                {
                    return fieldName + " = " + collection.Count + " entries";
                }
            }
            return "(no detail field)";
        }

        // -- report schema -----------------------------------------------------------------

        [Serializable]
        public class SmokeReport
        {
            public string status = "NOT_RUN";
            public string generated;
            public string unity_version;
            public string product_name;
            public string company_name;
            public string app_version;
            public string platform;
            public bool is_editor;
            public string data_path;
            public string streaming_assets_path;
            public string persistent_data_path;
            public string command_line;

            public int assemblies_found;
            public List<string> assemblies_missing = new List<string>();
            public List<AssemblyRecord> loaded_assemblies = new List<AssemblyRecord>();
            public List<string> other_assemblies = new List<string>();

            public int types_probed_ok;
            public List<TypeProbe> type_probes = new List<TypeProbe>();

            public CatalogProbe catalog = new CatalogProbe();
            public ContentProbe content = new ContentProbe();
            public List<string> notes = new List<string>();
        }

        /// <summary>
        /// True when a component's type comes from a reconstructed game assembly rather than the engine
        /// or a package.  Assembly-name prefixes are used because that is what survives into the Player;
        /// the point is only to separate "engine instantiated its own components" from "our code ran".
        /// </summary>
        private static bool IsGameComponent(Component component)
        {
            if (component == null) return false;
            string assembly = component.GetType().Assembly.GetName().Name ?? "";
            foreach (string prefix in GameAssemblyPrefixes)
            {
                if (assembly.StartsWith(prefix, StringComparison.Ordinal)) return true;
            }
            return false;
        }

        [Serializable]
        public class PrefabRecord
        {
            public string address;
            public string status;
            public string instance;
            public int transforms;
            public int components;
            public List<string> component_types = new List<string>();
            public List<string> game_component_types = new List<string>();
            public string detail;
            public string error;
        }

        [Serializable]
        public class ContentProbe
        {
            public string status = "NOT_RUN";
            public string initialize_status;
            public string locator_id;
            public int locator_keys = -1;
            public int loaded;
            public int failed;
            public string error;
            public List<PrefabRecord> prefabs = new List<PrefabRecord>();
            public List<string> game_script_components = new List<string>();
            public List<ContentRecord> records = new List<ContentRecord>();
        }

        [Serializable]
        public class ContentRecord
        {
            public string address;
            public string status;
            public string type;
            public string name;
            public string detail;
            public string provider;
            public string bundle;
            public string internal_id;
            public string provider_error;
            public string error;
        }

        [Serializable]
        public class AssemblyRecord
        {
            public string name;
            public int types;
        }

        [Serializable]
        public class TypeProbe
        {
            public string probe;
            public bool resolved;
            public string full_name;
            public string assembly;
            public int methods;
            public string error;
        }

        [Serializable]
        public class CatalogProbe
        {
            public string status = "NOT_RUN";
            public string path;
            public long bytes;
            public string locator_id;
            public int keys;
            public int location_edges;
            public int keys_with_multiple_locations;
            public string error;
        }
    }
}
