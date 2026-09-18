// Prefab / scene object-graph builder (G2, the largest remaining slice).
//
//   Unity -batchmode -quit -projectPath UnityProject \
//         -executeMethod Manosaba.EditorTools.PrefabBuilder.Run -logFile -
//
// Reads tools/unity-rebuild/out/unity/prefab_plan_*.json (produced by
// tools/unity-rebuild/a13e_plan_prefabs.py) and rebuilds each asset from the shipped structural
// dumps:
//
//   1. one GameObject per GameObject dump (name / layer / tag / active)
//   2. components: the transform (Transform or RectTransform), and for every MonoBehaviour row the
//      reconstructed (or package) type resolved by a13b -- AddComponent on that type
//   3. hierarchy from the transform dumps (m_Father / m_Children)
//   4. field values through JsonToSerializedApplier, whose pointer resolver handles both
//      m_FileID == 0 (an object created in this asset, by path id) and m_FileID > 0 (an external
//      asset, through the plan's pre-resolved reference table)
//   5. PrefabUtility.SaveAsPrefabAsset
//
// Everything that cannot be placed is counted and reported per asset (unbound pointers, missing
// types, unreadable dumps) -- a partial prefab is reported as PARTIAL, never as done.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

namespace Manosaba.EditorTools
{
    public static class PrefabBuilder
    {
        private const string PlanDirectoryRelative = "../tools/unity-rebuild/out/unity";
        private const string PlanPatternEnv = "MANOSABA_PREFAB_PLAN";
        private const string DefaultPlanPattern = "prefab_plan_*.json";
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/prefab_build_result.json";
        private const string OutputEnv = "MANOSABA_PREFAB_OUT";
        private const string FieldDumpEnv = "MANOSABA_PREFAB_FIELD_DUMP";

        [MenuItem("Manosaba/Build Prefabs (from plan)")]
        public static void Run()
        {
            int code = Execute();
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        public static int Execute()
        {
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string repoRoot = Directory.GetParent(projectRoot).FullName;
            string planDir = Path.GetFullPath(Path.Combine(projectRoot, PlanDirectoryRelative));
            string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));
            // A probe run must not overwrite the authoritative full-build result (round 9: probing
            // individual prefabs is how a defect is chased down, and the full result is evidence).
            string outOverride = Environment.GetEnvironmentVariable(OutputEnv);
            if (!string.IsNullOrEmpty(outOverride)) outPath = Path.GetFullPath(outOverride);
            string pattern = Environment.GetEnvironmentVariable(PlanPatternEnv);
            if (string.IsNullOrEmpty(pattern)) pattern = DefaultPlanPattern;

            var result = new BuildResult { generated = DateTime.UtcNow.ToString("o"), plan_pattern = pattern };
            if (!Directory.Exists(planDir))
            {
                return Blocked(result, outPath, "missing " + planDir);
            }

            string[] planFiles = Directory.GetFiles(planDir, pattern);
            result.plan_files = planFiles.Select(Path.GetFileName).OrderBy(n => n, StringComparer.Ordinal).ToList();
            var jobs = new List<JToken>();
            foreach (string file in planFiles)
            {
                try
                {
                    JObject plan = JObject.Parse(File.ReadAllText(file));
                    foreach (JToken job in plan["jobs"] as JArray ?? new JArray())
                    {
                        job["plan_file"] = Path.GetFileName(file);
                        jobs.Add(job);
                    }
                }
                catch (Exception ex)
                {
                    result.error = "unreadable plan " + Path.GetFileName(file) + ": " + ex.Message;
                }
            }
            result.jobs_total = jobs.Count;
            if (jobs.Count == 0)
            {
                return Blocked(result, outPath, result.error ?? ("no jobs matched " + pattern));
            }

            foreach (JToken jobToken in jobs)
            {
                var job = new JobRecord
                {
                    asset_path = (string)jobToken["asset_path"],
                    target = (string)jobToken["target"],
                    plan_file = (string)jobToken["plan_file"],
                    objects_planned = (jobToken["objects"] as JArray)?.Count ?? 0,
                    references_planned = (jobToken["references"] as JObject)?.Count ?? 0,
                };
                try
                {
                    BuildOne(repoRoot, jobToken, job);
                }
                catch (Exception ex)
                {
                    job.status = "ERROR";
                    job.error = ex.GetType().Name + ": " + ex.Message;
                }

                if (job.status == "BUILT") result.built++;
                else if (job.status == "BUILT_PARTIAL") result.built_partial++;
                else result.failed++;
                if (!job.hierarchy_complete) result.incomplete++;
                result.objects_created += job.objects_created;
                result.objects_written += job.objects_written;
                result.pointers_bound += job.pointers_bound;
                result.pointers_unbound += job.pointers_unbound;
                if (job.warning_count > 0 && result.messages.Count < 60)
                {
                    result.messages.Add(job.asset_path + ": " + string.Join(" | ", job.messages.Take(3)));
                }
                result.jobs.Add(job);
            }

            AssetDatabase.SaveAssets();
            result.status = result.failed > 0 ? "PARTIAL" : (result.built_partial > 0 ? "OK_WITH_PARTIAL_ASSETS" : "OK");
            Write(outPath, result);
            Debug.Log($"[PrefabBuilder] {result.status}: built={result.built} partial={result.built_partial} " +
                      $"failed={result.failed} incomplete={result.incomplete} of {result.jobs_total}; " +
                      $"objects created={result.objects_created} written={result.objects_written} " +
                      $"pointers {result.pointers_bound} bound / {result.pointers_unbound} unbound; wrote {outPath}");
            return result.failed == 0 ? 0 : 1;
        }

        private static int Blocked(BuildResult result, string outPath, string reason)
        {
            result.status = "BLOCKED";
            result.error = reason;
            Write(outPath, result);
            Debug.LogError("[PrefabBuilder] " + reason);
            return 2;
        }

        // -- one asset ----------------------------------------------------------------------

        private class DeferredComponent
        {
            public Node node;
            public Type type;
            public GameObject host;
        }

        private class Node
        {
            public string pid;
            public string type;
            public string dump;              // repo-relative path
            public JObject json;
            public GameObject gameObject;
            public UnityEngine.Object component;   // for non-GameObject nodes
            public bool isTransform;
            public RectTransform rectTransform;
            public string transform_origin = "";   // how the transform came to be (diagnostics)
            public JObject script;
        }

        private static void BuildOne(string repoRoot, JToken jobToken, JobRecord job)
        {
            string target = ((string)jobToken["target"]).Replace('\\', '/');
            int assetsIndex = target.IndexOf("/Assets/", StringComparison.Ordinal);
            if (assetsIndex >= 0) target = target.Substring(assetsIndex + 1);
            if (!target.StartsWith("Assets/", StringComparison.Ordinal))
            {
                job.status = "BAD_TARGET";
                job.error = "target not under Assets/: " + (string)jobToken["target"];
                return;
            }
            job.target = target;

            var references = new Dictionary<string, string>(StringComparer.Ordinal);
            if (jobToken["references"] is JObject refs)
            {
                foreach (JProperty p in refs.Properties()) references[p.Name] = (string)p.Value;
            }

            // ---- load dumps -----------------------------------------------------------------
            var nodes = new List<Node>();
            foreach (JToken obj in jobToken["objects"] as JArray ?? new JArray())
            {
                var node = new Node
                {
                    pid = (string)obj["pid"],
                    type = (string)obj["type"],
                    dump = (string)obj["dump"],
                    script = obj["script"] as JObject,
                };
                string dumpPath = Path.GetFullPath(Path.Combine(repoRoot, node.dump));
                if (!File.Exists(dumpPath))
                {
                    job.warnings.Add(node.pid + " (" + node.type + "): dump missing");
                    job.warning_count++;
                    continue;
                }
                try
                {
                    node.json = JObject.Parse(File.ReadAllText(dumpPath));
                }
                catch (Exception ex)
                {
                    job.warnings.Add(node.pid + " (" + node.type + "): unreadable dump: " + ex.GetType().Name);
                    job.warning_count++;
                    continue;
                }
                nodes.Add(node);
            }

            var temp = new List<GameObject>();
            // Component nodes whose `AddComponent` was refused on the first pass; retried once every GameObject
            // has its other components (see the deferred pass below).
            var deferred = new List<DeferredComponent>();
            try
            {
                // ---- create GameObjects ------------------------------------------------------
                foreach (Node node in nodes.Where(n => n.type == "GameObject"))
                {
                    var go = new GameObject(string.IsNullOrEmpty((string)node.json["m_Name"])
                        ? "GameObject_" + node.pid
                        : (string)node.json["m_Name"]);
                    node.gameObject = go;
                    temp.Add(go);
                    job.objects_created++;

                    JToken layer = node.json["m_Layer"];
                    if (layer != null) go.layer = layer.Value<int>();
                    JToken active = node.json["m_IsActive"];
                    if (active != null) go.SetActive(active.Value<bool>());
                    JToken tag = node.json["m_Tag"];
                    if (tag != null)
                    {
                        try { go.tag = tag.ToString(); } catch (UnityException) { /* tag not in this project */ }
                    }
                }

                // ---- create transforms and components ----------------------------------------
                foreach (Node node in nodes)
                {
                    if (node.type == "GameObject") continue;

                    if (node.type == "Transform" || node.type == "RectTransform")
                    {
                        GameObject owner = ResolveOwner(node, nodes);
                        if (owner == null)
                        {
                            job.warnings.Add(node.pid + ": transform without owner GameObject");
                            job.warning_count++;
                            continue;
                        }
                        if (node.type == "RectTransform")
                        {
                            // `existing ?? owner.AddComponent<RectTransform>()` is WRONG for UnityEngine.Object:
                            // GetComponent<T>() can return a *fake-null* wrapper (real C# object, null native
                            // pointer) for a component that is not there, and `??` only tests for a real null.
                            // The result was that the RectTransform was never added, the transform node stayed
                            // component-less, its whole subtree could not be parented, and
                            // SaveAsPrefabAsset(root) wrote only the root (round 9: CreditsUI 8/3189 objects,
                            // WitchBookUI 9/2065, SaveLoadUI 9/148).  Compare with Unity's `==` instead.
                            RectTransform existing = owner.GetComponent<RectTransform>();
                            bool hasExisting = existing != null;                 // Unity's overloaded ==
                            RectTransform rt = hasExisting ? existing : owner.AddComponent<RectTransform>();
                            node.rectTransform = rt;
                            node.component = rt;
                            node.transform_origin = hasExisting ? "reused" : (rt != null ? "added" : "ADD_FAILED");
                            if (rt == null)
                            {
                                job.transform_add_failed++;
                                job.warnings.Add(node.pid + ": AddComponent<RectTransform>() returned null on '" +
                                                 owner.name + "'");
                                job.warning_count++;
                            }
                        }
                        else
                        {
                            node.component = owner.transform;
                            node.transform_origin = "owner.transform";
                        }
                        node.gameObject = owner;
                        node.isTransform = true;
                        // Count only what was really created: a component-less transform node cannot be
                        // saved, and counting it made the written-vs-created check disagree with reality.
                        if (node.component != null && node.component is Transform) job.objects_created++;
                        continue;
                    }

                    Type type = ResolveComponentType(node);
                    if (type == null)
                    {
                        job.missing_types++;
                        job.warnings.Add(node.pid + " (" + node.type + "): component type not found");
                        job.warning_count++;
                        FieldDump(job, node, null, "TYPE_NOT_FOUND script=" +
                                  (node.script == null ? "(none)" : node.script.ToString(Newtonsoft.Json.Formatting.None)));
                        continue;
                    }

                    GameObject host = ResolveOwner(node, nodes);
                    if (host == null)
                    {
                        job.warnings.Add(node.pid + ": component without owner GameObject");
                        job.warning_count++;
                        FieldDump(job, node, type, "NO_OWNER");
                        continue;
                    }

                    Component component = host.AddComponent(type);
                    if (component == null)
                    {
                        // Not a failure yet: `AddComponent` is also refused when a component the type *requires*
                        // is missing and cannot be created for us.  `[RequireComponent(typeof(Graphic))]` on
                        // `LeTai.TrueShadow.TrueShadow` is the measured case -- `Graphic` is abstract, so Unity
                        // logs "Can't add script behaviour 'Graphic'. The script class can't be abstract!" and
                        // returns null unless the GameObject already carries a concrete Graphic (Image,
                        // TextMeshProUGUI, RawImage).  With the plan listing TrueShadow *before* the Image on the
                        // same GameObject, the first pass is refused, the node keeps no component, and every PPtr
                        // aimed at it stays unbound -- measured on DebateUI: `SkipDebateButton._iconShadow` never
                        // bound, so `SkipDebateButton.Awake` NRE'd and `DebateUi.Initialize` threw, which faulted
                        // the engine's last post-initialization task and left `Engine.Initializing` true for ever.
                        // Such nodes are deferred to a second pass, after the GameObject has its other components.
                        deferred.Add(new DeferredComponent { node = node, type = type, host = host });
                        continue;
                    }
                    node.component = component;
                    node.gameObject = host;
                    job.objects_created++;
                }

                // ---- deferred component pass --------------------------------------------------
                // Retry the refusals now that every GameObject has all of its other components, so the outcome
                // no longer depends on the order the plan lists nodes in.  Only a second refusal is a real
                // failure (abstract types such as `Graphic`/`Selectable`, or a genuine duplicate).
                foreach (DeferredComponent pending in deferred)
                {
                    if (pending.node.component != null) continue;
                    Component retried = pending.host.AddComponent(pending.type);
                    if (retried == null)
                    {
                        job.component_add_failed++;
                        FieldDump(job, pending.node, pending.type, "ADD_REFUSED host=" + pending.host.name +
                                  " existing=" + string.Join(",", pending.host.GetComponents<Component>()
                                      .Where(c => c != null).Select(c => c.GetType().Name)));
                        continue;
                    }
                    pending.node.component = retried;
                    pending.node.gameObject = pending.host;
                    job.objects_created++;
                    FieldDump(job, pending.node, pending.type, "ADD_DEFERRED_OK host=" + pending.host.name);
                }

                // ---- hierarchy ---------------------------------------------------------------
                job.roots_detected = nodes.Count(n => n.isTransform && ReadPid(n.json["m_Father"]) == 0);
                foreach (Node node in nodes.Where(n => n.isTransform))
                {
                    Transform self = node.component as Transform;
                    if (self == null || node.gameObject == null) continue;
                    long fatherPid = ReadPid(node.json["m_Father"]);
                    if (fatherPid == 0) continue;
                    Node father = nodes.FirstOrDefault(n => n.isTransform && n.pid == fatherPid.ToString());
                    if (father?.component is Transform parent)
                    {
                        self.SetParent(parent, false);
                        job.parented++;
                    }
                    else
                    {
                        job.parent_missing++;
                        job.warnings.Add(node.pid + ": father transform " + fatherPid + " not in this asset");
                        job.warning_count++;
                    }
                }

                // ---- field values ------------------------------------------------------------
                // Every transform whose father is outside this asset is a root of the prefab: the shipped
                // data really does carry several (see the save step).
                var rootNodes = nodes.Where(n => n.isTransform && ReadPid(n.json["m_Father"]) == 0).ToList();
                // The MAIN asset of a prefab is the object whose name matches the asset file name -- that is the
                // convention Unity itself uses, and the shipped data does not always list it first.  Measured on
                // `GalleryUI.prefab`, which carries two roots (`GalleryStillSlot` first, `GalleryUI` second):
                // taking `FirstOrDefault()` made `SaveAsPrefabAsset` write the 4-transform `GalleryStillSlot`
                // subtree instead of the 103-object `GalleryUI` one, so the asset came out with
                // objects_written = 17 of 117 and saved_components = 13 of 94 -- an empty gallery UI whose
                // `CGGalleryGridSlot.Awake` then failed `AssertRequiredObjects(ThumbnailImage, LockedTexture)`,
                // which is the exception the Player kept logging.  Falls back to the first root when no name
                // matches, which is what every single-root prefab does anyway.
                string assetName = Path.GetFileNameWithoutExtension(
                    string.IsNullOrEmpty(job.target) ? job.asset_path : job.target);
                Node rootNode = rootNodes.FirstOrDefault(n => n.gameObject != null && n.gameObject.name == assetName)
                                ?? rootNodes.FirstOrDefault();
                GameObject root = rootNode?.gameObject ?? nodes.FirstOrDefault(n => n.gameObject != null)?.gameObject;
                List<GameObject> allRoots = rootNodes
                    .Select(n => n.gameObject)
                    .Where(go => go != null)
                    .Distinct()
                    .ToList();
                if (root != null && !allRoots.Contains(root)) allRoots.Insert(0, root);

                // Stage check: the tree as assembled by the hierarchy loop, before any field is applied.
                // Round 9's probe showed a flat tree *before save*, so the stage that loses it has to be
                // identified rather than guessed: hierarchy loop -> field application -> save.
                job.transforms_after_hierarchy = root != null ? CountDescendantTransforms(root.transform) : -1;
                if (job.transforms_after_hierarchy != nodes.Count(n => n.isTransform))
                {
                    // Record the links as they really are, so the failure is read off evidence instead of
                    // guessed from counters.
                    foreach (Node node in nodes.Where(n => n.isTransform).Take(30))
                    {
                        Transform self = node.component as Transform;
                        long fatherPid = ReadPid(node.json["m_Father"]);
                        Node father = nodes.FirstOrDefault(n => n.isTransform && n.pid == fatherPid.ToString());
                        job.graph_dump.Add(
                            "pid=" + node.pid +
                            " type=" + node.type +
                            " origin=" + node.transform_origin +
                            " owner=" + (node.gameObject == null ? "NULL" : node.gameObject.name) +
                            " component=" + (self == null ? "NULL" : self.GetType().Name) +
                            " father=" + fatherPid +
                            " fatherOrigin=" + (father == null ? "(no node)" : father.transform_origin) +
                            " fatherComponent=" + (father?.component == null ? "NULL" : father.component.GetType().Name) +
                            " nowUnder=" + (self == null || self.parent == null ? "(root)" : self.parent.name));
                    }
                }

                // ---- extra roots ------------------------------------------------------------------
                // A shipped prefab asset can hold several root GameObjects (`GalleryUI.prefab`: the screen
                // `GalleryUI` plus the slot template `GalleryStillSlot`; `SaveLoadUI.prefab`: `SaveLoadUI` plus
                // `SaveLoadSlot`; `CreditsUI.prefab`: four).  A Unity *prefab asset* can only represent one root,
                // so the extras used to be dropped and merely counted.  That turned out to be the thing keeping
                // the engine from initializing: every PPtr aimed into a dropped tree stayed unbound, and for
                // `GalleryUI` one of them is `ScriptableGrid.slotPrototype`, whose absence makes
                // `ScriptableGrid.CreateSlots` await an `Engine.Instantiate` of a null prototype for ever.
                // So each extra root is now written out as its own prefab asset next to the main one, and the
                // references into it are rebound to that asset -- the same shape as a nested prefab dependency.
                var extras = allRoots.Where(go => go != null && go != root).ToList();
                var extraRootObjects = new Dictionary<string, UnityEngine.Object>();

                // The field pass has to run in two stages when extras exist: references that will live in
                // *another asset* can only be bound after that asset exists.
                var nodeByGameObject = new Dictionary<int, Node>();
                var nodeByComponent = new Dictionary<int, Node>();
                foreach (Node node in nodes)
                {
                    if (node.gameObject != null) nodeByGameObject[node.gameObject.GetInstanceID()] = node;
                    if (node.component != null) nodeByComponent[node.component.GetInstanceID()] = node;
                }
                var extraRootNodes = new HashSet<Node>();
                foreach (Node node in nodes)
                    if (node.gameObject != null && extras.Any(e => IsDescendantOf(node.gameObject.transform, e.transform)))
                        extraRootNodes.Add(node);

                void ApplyFields (Node node, Dictionary<string, UnityEngine.Object> extraMap)
                {
                    if (node.type == "GameObject" || node.isTransform) ApplyTransform(node, job);
                    if (node.component == null)
                    {
                        FieldDump(job, node, null, "SKIPPED_NO_COMPONENT");
                        return;
                    }

                    JToken enabled = node.json["m_Enabled"];
                    if (enabled != null && node.component is Behaviour behaviour) behaviour.enabled = enabled.Value<bool>();
                    if (node.type == "GameObject" || node.isTransform) return;

                    var options = new JsonToSerializedApplier.Options
                    {
                        ResolvePointer = token => ResolvePointer(token, node, nodes, references, repoRoot, job, extraMap),
                        PathPrefix = node.pid + ".",
                    };
                    var report = JsonToSerializedApplier.ApplyAll(new SerializedObject(node.component), node.json, options);
                    job.fields_applied += report.applied;
                    job.pointers_bound += report.pointersBound;
                    job.pointers_unbound += report.pointersUnbound;
                    job.managed_references_applied += report.managedApplied;
                    job.warning_count += report.warnings;
                    // Keep enough messages per component to see ALL of its unbound pointers: with `Take(3)` the
                    // one that mattered was hidden.  Measured on GalleryUI.prefab, where the reported unbound
                    // pointers were `lockedTexture`, `loadingTexture`, `_layoutGroups[0]`, `_enable`, `_disable`
                    // and `slotPrototype` -- the pointer that actually stops the engine from initializing -- was
                    // silently dropped as a 4th message.  Every one of them points into the same place.
                    foreach (string message in report.messages.Take(12)) job.messages.Add(message);
                    // Per-component outcome: this is what tells "the component was never created" apart from
                    // "the component exists but its fields were never written", which the aggregate counters
                    // alone cannot (measured on SaveLoadUI.prefab: the component is in the asset with zero
                    // serialized fields and no warning names any of its 31 properties).
                    FieldDump(job, node, node.component.GetType(), "APPLIED applied=" + report.applied +
                              " skipped=" + report.skipped + " warnings=" + report.warnings +
                              " managedApplied=" + report.managedApplied + " managedFailed=" + report.managedFailed);
                }

                // Stage A: the extra roots' own fields, then their assets, then Stage B: everything else.
                if (extras.Count > 0)
                {
                    foreach (Node node in nodes)
                        if (extraRootNodes.Contains(node)) ApplyFields(node, null);

                    Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(target)));
                    foreach (GameObject extra in extras)
                    {
                        string extraPath = ExtraRootPath(target, extra.name);
                        GameObject savedExtra = PrefabUtility.SaveAsPrefabAsset(extra, extraPath);
                        AssetDatabase.ImportAsset(extraPath, ImportAssetOptions.ForceUpdate);
                        GameObject loadedExtra = savedExtra != null
                            ? savedExtra
                            : AssetDatabase.LoadAssetAtPath<GameObject>(extraPath);
                        if (loadedExtra == null)
                        {
                            job.warnings.Add("extra root '" + extra.name + "' could not be saved to " + extraPath);
                            job.warning_count++;
                            continue;
                        }
                        MapTree(extra, loadedExtra, nodeByGameObject, nodeByComponent, extraRootObjects);
                        job.extra_root_prefabs.Add(extraPath);
                    }
                }

                foreach (Node node in nodes)
                    if (!extraRootNodes.Contains(node)) ApplyFields(node, extraRootObjects);

                // ---- save --------------------------------------------------------------------
                // (root/rootNode were resolved before the field-application loop so the tree can be
                //  measured both before and after it)
                job.transforms_after_fields = root != null ? CountDescendantTransforms(root.transform) : -1;
                if (root == null)
                {
                    job.status = "NO_ROOT";
                    return;
                }

                Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(target)));
                if (AssetDatabase.LoadMainAssetAtPath(target) != null) AssetDatabase.DeleteAsset(target);

                // Count the hierarchy that exists in memory *before* saving: this bisects "the builder
                // never assembled the tree" against "SaveAsPrefabAsset dropped it".
                job.runtime_transforms = allRoots.Sum(r => CountDescendantTransforms(r.transform));
                job.runtime_components = allRoots.Sum(r => r.GetComponentsInChildren<Component>(true).Length);
                job.runtime_root_children = root.transform.childCount;
                job.runtime_roots_in_scene = root.scene.IsValid()
                    ? root.scene.GetRootGameObjects().Count(go => go != null && go.GetComponent<Transform>() != null)
                    : -1;

                // A shipped prefab asset can hold *several* root GameObjects (SaveLoadUI.prefab: the screen
                // 'SaveLoadUI' plus the slot template 'SaveLoadSlot'; CreditsUI.prefab: four of them, the
                // three SpecialThanksCreditLabel variants).  Round 9 established by experiment that a
                // Unity *prefab asset* can only represent one root: saving all of them under a wrapper of
                // our own works, but then the instantiated root is the wrapper (wrong name, components one
                // level down), and splicing the wrapper back out makes Unity's importer drop every root
                // except the one reachable from the file's main object.  Round 39 then measured the cost of
                // simply dropping them: references into a dropped tree stay unbound, and for GalleryUI one of
                // those is `ScriptableGrid.slotPrototype`, whose absence stalls `Engine.Initialize` for ever.
                // So the extras are now written as their own sibling prefab assets (see the two-stage field
                // pass above) and only *counted* here, for the record.
                job.root_count = allRoots.Count;
                job.root_name = root.name;
                foreach (GameObject extra in allRoots)
                {
                    if (extra == root) continue;
                    job.extra_root_names.Add(extra.name);
                    job.transforms_in_extra_roots += CountDescendantTransforms(extra.transform);
                    job.objects_in_extra_roots += CountDescendantTransforms(extra.transform)
                                                  + extra.GetComponentsInChildren<Component>(true).Length;
                }

                Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(target)));
                if (AssetDatabase.LoadMainAssetAtPath(target) != null) AssetDatabase.DeleteAsset(target);
                GameObject savedRoot = PrefabUtility.SaveAsPrefabAsset(root, target);

                // ---- verify what was actually written ------------------------------------------
                // Reporting "objects created" is not evidence that the prefab *file* holds them: round 9
                // found 58 prefabs where the builder created every planned object and the saved asset
                // kept only the root plus its components (CreditsUI 8/3189, WitchBookUI 9/2065).  So the
                // saved asset is loaded back and counted, and a shortfall is a status, not a footnote.
                AssetDatabase.ImportAsset(target, ImportAssetOptions.ForceUpdate);
                GameObject saved = savedRoot != null ? savedRoot : AssetDatabase.LoadAssetAtPath<GameObject>(target);
                if (saved == null)
                {
                    job.status = "SAVE_FAILED";
                    job.warnings.Add("SaveAsPrefabAsset produced no loadable asset at " + target);
                    job.warning_count++;
                    return;
                }
                job.saved_transforms = saved.GetComponentsInChildren<Transform>(true).Length;
                job.saved_components = saved.GetComponentsInChildren<Component>(true).Length;
                job.objects_written = job.saved_transforms + job.saved_components;   // a transform counts in both
                job.root_children = saved.transform.childCount;
                job.main_asset_name = AssetDatabase.LoadMainAssetAtPath(target)?.name;
                // Completeness is judged on *structure*: every transform of the primary root's tree must
                // be in the file (component counts also depend on AddComponent refusing abstract/duplicate
                // types, which is reported separately as objects_unbuildable).
                int expectedTransforms = job.runtime_transforms - job.transforms_in_extra_roots;
                job.hierarchy_complete = job.saved_transforms == expectedTransforms;

                if (!job.hierarchy_complete)
                {
                    job.warnings.Add("hierarchy incomplete: " + job.saved_transforms + " transforms written but " +
                                     expectedTransforms + " belong to the primary root '" + job.root_name +
                                     "' (" + job.objects_created + " objects created, " + job.root_count +
                                     " roots, root has " + job.runtime_root_children + " children)");
                    job.warning_count++;
                    job.status = "BUILT_INCOMPLETE";
                    return;
                }

                job.objects_unbuildable = job.objects_planned - job.objects_created;
                if (job.objects_unbuildable > 0)
                {
                    job.warnings.Add(job.objects_unbuildable + " planned objects could not be created (" +
                                     job.component_add_failed + " component adds refused, " +
                                     job.missing_types + " unknown types, " +
                                     job.transform_add_failed + " transform adds refused)");
                    job.warning_count++;
                }

                if (job.objects_in_extra_roots > 0)
                {
                    // No longer a deviation: the extras are written as sibling prefab assets and the references
                    // into them are rebound (see the two-stage field pass).  What is left to report is whether
                    // every extra really got its own asset.
                    int savedExtras = job.extra_root_prefabs.Count;
                    job.warnings.Add("multi-root prefab: " + job.root_count + " root GameObjects in the shipped " +
                                     "data; " + job.objects_in_extra_roots + " objects belong to " +
                                     string.Join(", ", job.extra_root_names) + " and were written to " +
                                     savedExtras + " sibling prefab asset(s)" +
                                     (savedExtras == job.extra_root_names.Count ? "" : " -- NOT ALL SAVED"));
                    job.warning_count++;
                }

                job.status = job.warning_count == 0 && job.missing_types == 0 && job.pointers_unbound == 0
                    ? "BUILT"
                    : "BUILT_PARTIAL";
            }
            finally
            {
                foreach (GameObject go in temp) UnityEngine.Object.DestroyImmediate(go);
            }
        }

        /// <summary>How many transforms hang below this one (including itself).</summary>
        private static int CountDescendantTransforms(Transform root)
        {
            if (root == null) return -1;
            int count = 1;
            foreach (Transform child in root)
            {
                count += CountDescendantTransforms(child);
            }
            return count;
        }

        /// <summary>
        /// Records one line per node describing how its component was obtained and how many fields the applier
        /// wrote, so a "the asset has the component but no fields" report can be traced to the exact branch that
        /// produced it.  Off unless MANOSABA_PREFAB_FIELD_DUMP=1: the full 156-job build does not need it.
        /// </summary>
        private static void FieldDump(JobRecord job, Node node, Type type, string outcome)
        {
            if (!FieldDumpEnabled) return;
            if (job.field_dump.Count >= 400) return;
            job.field_dump.Add(node.pid + " " + node.type +
                               (type == null ? "" : " -> " + type.FullName) +
                               " owner=" + (node.gameObject == null ? "(none)" : node.gameObject.name) +
                               " " + outcome);
        }

        private static bool FieldDumpEnabled =>
            Environment.GetEnvironmentVariable(FieldDumpEnv) == "1";

        private static GameObject ResolveOwner(Node node, List<Node> nodes)        {
            if (node.gameObject != null) return node.gameObject;
            long ownerPid = ReadPid(node.json["m_GameObject"]);
            if (ownerPid == 0) return null;
            Node owner = nodes.FirstOrDefault(n => n.type == "GameObject" && n.pid == ownerPid.ToString());
            return owner?.gameObject;
        }

        private static Type ResolveComponentType(Node node)
        {
            if (node.type == "MonoBehaviour")
            {
                if (node.script == null) return null;
                string ns = (string)node.script["namespace"];
                string cls = (string)node.script["class"];
                string asm = (string)node.script["assembly"];
                string full = string.IsNullOrEmpty(ns) ? cls : ns + "." + cls;
                return JsonToSerializedApplier.ResolveType(full, JsonToSerializedApplier.Alias(asm));
            }
            return JsonToSerializedApplier.ResolveType(node.type, null) ?? FindTypeByName(node.type);
        }

        /// <summary>Engine component short names (CanvasRenderer, CanvasGroup, ...) are unambiguous enough.</summary>
        private static Type FindTypeByName(string shortName)
        {
            foreach (System.Reflection.Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type hit = asm.GetType("UnityEngine." + shortName, throwOnError: false)
                           ?? asm.GetType("UnityEngine.UI." + shortName, throwOnError: false)
                           ?? asm.GetType("TMPro." + shortName, throwOnError: false);
                if (hit != null) return hit;
            }
            return JsonToSerializedApplier.ResolveType(shortName, null);
        }

        private static void ApplyTransform(Node node, JobRecord job)
        {
            Transform t = node.component as Transform;
            if (t == null) return;

            JToken pos = node.json["m_LocalPosition"];
            if (pos != null) t.localPosition = new Vector3(pos.Value<float>("x"), pos.Value<float>("y"), pos.Value<float>("z"));
            JToken rot = node.json["m_LocalRotation"];
            if (rot != null) t.localRotation = new Quaternion(rot.Value<float>("x"), rot.Value<float>("y"), rot.Value<float>("z"), rot.Value<float>("w"));
            JToken scale = node.json["m_LocalScale"];
            if (scale != null) t.localScale = new Vector3(scale.Value<float>("x"), scale.Value<float>("y"), scale.Value<float>("z"));

            RectTransform rt = node.rectTransform ?? t as RectTransform;
            if (rt == null) return;
            JToken anchorMin = node.json["m_AnchorMin"];
            if (anchorMin != null) rt.anchorMin = new Vector2(anchorMin.Value<float>("x"), anchorMin.Value<float>("y"));
            JToken anchorMax = node.json["m_AnchorMax"];
            if (anchorMax != null) rt.anchorMax = new Vector2(anchorMax.Value<float>("x"), anchorMax.Value<float>("y"));
            JToken anchored = node.json["m_AnchoredPosition"];
            if (anchored != null) rt.anchoredPosition = new Vector2(anchored.Value<float>("x"), anchored.Value<float>("y"));
            JToken sizeDelta = node.json["m_SizeDelta"];
            if (sizeDelta != null) rt.sizeDelta = new Vector2(sizeDelta.Value<float>("x"), sizeDelta.Value<float>("y"));
            JToken pivot = node.json["m_Pivot"];
            if (pivot != null) rt.pivot = new Vector2(pivot.Value<float>("x"), pivot.Value<float>("y"));
        }

        /// <summary>
        /// Path of the prefab asset written for an extra root: a sibling of the main asset, named after it plus
        /// the root's own name, so the relationship is visible in the project window.
        /// </summary>
        private static string ExtraRootPath (string mainTarget, string rootName)
        {
            string dir = Path.GetDirectoryName(mainTarget);
            string mainName = Path.GetFileNameWithoutExtension(mainTarget);
            var sb = new StringBuilder();
            foreach (char c in rootName ?? "ExtraRoot")
                sb.Append(Array.IndexOf(Path.GetInvalidFileNameChars(), c) >= 0 ? '_' : c);
            return (string.IsNullOrEmpty(dir) ? "" : dir + "/") + mainName + "_" + sb + ".prefab";
        }

        private static bool IsDescendantOf (Transform transform, Transform ancestor)
        {
            for (Transform t = transform; t != null; t = t.parent)
                if (t == ancestor) return true;
            return false;
        }

        /// <summary>
        /// Walks the live extra-root tree and the asset just saved from it in parallel and records, per path id,
        /// the object *inside the asset*.  Structure and component order are preserved by SaveAsPrefabAsset, so
        /// matching by child index and component index is exact; the maps are keyed by instance id because
        /// UnityEngine.Object's equality operators are not usable as dictionary keys.
        /// </summary>
        private static void MapTree (GameObject live, GameObject saved,
            Dictionary<int, Node> nodeByGameObject, Dictionary<int, Node> nodeByComponent,
            Dictionary<string, UnityEngine.Object> map)
        {
            if (live == null || saved == null) return;
            if (nodeByGameObject.TryGetValue(live.GetInstanceID(), out Node goNode))
                map[goNode.pid] = saved;

            Component[] liveComponents = live.GetComponents<Component>();
            Component[] savedComponents = saved.GetComponents<Component>();
            for (int i = 0; i < liveComponents.Length && i < savedComponents.Length; i++)
            {
                if (liveComponents[i] == null || savedComponents[i] == null) continue;
                if (nodeByComponent.TryGetValue(liveComponents[i].GetInstanceID(), out Node componentNode))
                    map[componentNode.pid] = savedComponents[i];
            }

            int children = Mathf.Min(live.transform.childCount, saved.transform.childCount);
            for (int i = 0; i < children; i++)
                MapTree(live.transform.GetChild(i).gameObject, saved.transform.GetChild(i).gameObject,
                    nodeByGameObject, nodeByComponent, map);
        }

        /// <summary>m_FileID 0 = an object created in this asset; anything else = a pre-resolved external asset.</summary>
        private static UnityEngine.Object ResolvePointer(JToken token, Node node, List<Node> nodes,
                                                         Dictionary<string, string> references,
                                                         string repoRoot, JobRecord job,
                                                         Dictionary<string, UnityEngine.Object> extraRootObjects = null)
        {
            long fileId = token["m_FileID"] != null ? token.Value<long>("m_FileID") : 0;
            long pathId = token["m_PathID"] != null ? token.Value<long>("m_PathID") : 0;
            if (pathId == 0) return null;

            if (fileId == 0)
            {
                Node target = nodes.FirstOrDefault(n => n.pid == pathId.ToString());
                if (target == null) return null;
                // An object that lives in an extra root's own prefab asset must be referenced through that asset,
                // not through the live scene object -- otherwise SaveAsPrefabAsset cannot serialise it.
                if (extraRootObjects != null && extraRootObjects.TryGetValue(target.pid, out UnityEngine.Object mapped))
                    return mapped;
                return target.component != null ? target.component : (UnityEngine.Object)target.gameObject;
            }

            string key = node.pid + ":" + fileId + ":" + pathId;
            if (!references.TryGetValue(key, out string assetPath)) return null;
            string full = Path.GetFullPath(Path.Combine(repoRoot, assetPath));
            string unityPath = full.Replace('\\', '/');
            int assetsIndex = unityPath.IndexOf("/Assets/", StringComparison.Ordinal);
            if (assetsIndex >= 0) unityPath = unityPath.Substring(assetsIndex + 1);
            return AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(unityPath);
        }

        private static long ReadPid(JToken token)
        {
            if (token == null) return 0;
            return token["m_PathID"] != null ? token.Value<long>("m_PathID") : 0;
        }

        private static void Write(string path, BuildResult result)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, JsonUtility.ToJson(result, true), new UTF8Encoding(false));
        }

        // -- result schema ------------------------------------------------------------------

        [Serializable]
        public class BuildResult
        {
            public string status = "NOT_RUN";
            public string generated;
            public string plan_pattern;
            public List<string> plan_files = new List<string>();
            public string error;
            public int jobs_total;
            public int built;
            public int built_partial;
            public int failed;
            public int incomplete;
            public int objects_created;
            public int objects_written;
            public int pointers_bound;
            public int pointers_unbound;
            public List<string> messages = new List<string>();
            public List<JobRecord> jobs = new List<JobRecord>();
        }

        [Serializable]
        public class JobRecord
        {
            public string asset_path;
            public string target;
            public string plan_file;
            public string status = "NOT_RUN";
            public string error;
            public int objects_planned;
            public int objects_created;
            public int objects_written;
            public int saved_transforms;
            public int saved_components;
            public int root_children;
            public int roots_detected;
            public int root_count;
            public int objects_in_extra_roots;
            public int transforms_in_extra_roots;
            public List<string> extra_root_names = new List<string>();
            /// <summary>Prefab assets written for the extra roots, in the order they were saved.</summary>
            public List<string> extra_root_prefabs = new List<string>();
            public string main_asset_name;
            public int runtime_transforms;
            public int runtime_components;
            public int runtime_root_children;
            public int runtime_roots_in_scene;
            public int transforms_after_hierarchy;
            public int transforms_after_fields;
            public List<string> graph_dump = new List<string>();
            public int parented;
            public int parent_missing;
            public int transform_add_failed;
            public int component_add_failed;
            public int objects_unbuildable;
            public bool hierarchy_complete;
            public string root_name;
            public int references_planned;
            public int fields_applied;
            public int pointers_bound;
            public int pointers_unbound;
            public int managed_references_applied;
            public int missing_types;
            public int warning_count;
            public List<string> messages = new List<string>();
            public List<string> warnings = new List<string>();
            /// <summary>Per-node component/field outcomes; only filled when MANOSABA_PREFAB_FIELD_DUMP=1.</summary>
            public List<string> field_dump = new List<string>();
        }
    }
}
