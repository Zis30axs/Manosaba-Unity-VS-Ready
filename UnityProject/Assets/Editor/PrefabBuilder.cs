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

        /// <summary>
        /// Object-header keys of a component dump that the builder creates or sets itself, so the applier must not
        /// touch them.  Everything else under m_* is component data and is applied (m_Enabled included: Renderer and
        /// Collider carry it too, not only Behaviour).
        /// </summary>
        private static readonly HashSet<string> BuilderOwnedKeys = new HashSet<string>
        {
            "references", "m_GameObject", "m_Script", "m_Name", "m_EditorClassIdentifier", "m_ObjectHideFlags",
            "m_CorrespondingSourceObject", "m_PrefabInstance", "m_PrefabAsset",
        };

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
                result.pointers_adapted += job.pointers_adapted;
                result.pointers_sidecar += job.pointers_sidecar;
                result.assembly_names_remapped += job.assembly_names_remapped;
                result.components_adopted += job.components_adopted;
                if (job.guid_changed) result.guid_changed++;
                result.unresolved_target_not_in_plan += job.unresolved_target_not_in_plan;
                result.unresolved_target_not_built += job.unresolved_target_not_built;
                result.unresolved_no_reference_entry += job.unresolved_no_reference_entry;
                result.unresolved_asset_missing += job.unresolved_asset_missing;
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
                      $"pointers {result.pointers_bound} bound ({result.pointers_adapted} type-adapted, {result.pointers_sidecar} via ledger) / {result.pointers_unbound} unbound " +
                      $"[not in plan {result.unresolved_target_not_in_plan}, not built {result.unresolved_target_not_built}, " +
                      $"no reference entry {result.unresolved_no_reference_entry}, asset missing {result.unresolved_asset_missing}]; " +
                      $"components adopted {result.components_adopted}; GUID changed {result.guid_changed}; wrote {outPath}");
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
            // Standalone ScriptableObjects which were sub-assets of the shipped prefab (TimelineAsset,
            // AnimationTrack and AnimationClip).  They are kept in this same map so intra-prefab PPtrs can
            // bind before the prefab is saved.
            public UnityEngine.Object asset;
            public bool isTransform;
            public RectTransform rectTransform;
            public string transform_origin = "";   // how the transform came to be (diagnostics)
            public JObject script;
        }

        [Serializable]
        public class EditResult
        {
            public string status = "ERROR";
            public string target;
            public string error;
            public string editability = "PARTIAL";
            public string guid_before;
            public string guid_after;
            public int fields_applied;
            public List<string> warnings = new List<string>();
            public List<JsonToSerializedApplier.FieldEditability> fields = new List<JsonToSerializedApplier.FieldEditability>();
            public List<SourceEditResult> source_results = new List<SourceEditResult>();
        }

        [Serializable]
        public class SourceEditResult
        {
            public string source;
            public string status = "READ_ONLY";
            public string editability = "READ_ONLY";
            public string error;
            public int fields_applied;
            public List<string> warnings = new List<string>();
            public List<JsonToSerializedApplier.FieldEditability> fields = new List<JsonToSerializedApplier.FieldEditability>();
        }

        private class ReadOnlyMappingException : Exception
        {
            public ReadOnlyMappingException(string reason) : base(reason) { }
        }

        /// <summary>Patch uniquely identified existing components without rebuilding their hierarchy or file ids.</summary>
        public static EditResult ApplyEditable(string repoRoot, JObject planJob, string changedSource)
            => ApplyEditable(repoRoot, planJob, new[] { changedSource });

        public static EditResult ApplyEditable(string repoRoot, JObject planJob, IEnumerable<string> changedSources)
        {
            string target = (string)planJob["target"];
            var result = new EditResult { target = target, guid_before = AssetDatabase.AssetPathToGUID(target) };
            GameObject root = null;
            try
            {
                bool targetExisted = AssetDatabase.LoadMainAssetAtPath(target) != null;
                if (!targetExisted)
                {
                    var initial = new JobRecord { target = target, asset_path = (string)planJob["asset_path"] };
                    BuildOne(repoRoot, planJob, initial);
                    if (initial.status != "BUILT" && initial.status != "BUILT_PARTIAL")
                        throw new InvalidOperationException(initial.error ?? initial.status);
                    result.warnings.AddRange(initial.warnings);
                }
                var nodes = new List<Node>();
                foreach (JToken obj in planJob["objects"] as JArray ?? new JArray())
                {
                    string dump = (string)obj["dump"];
                    nodes.Add(new Node { pid = (string)obj["pid"], type = (string)obj["type"], dump = dump,
                        script = obj["script"] as JObject,
                        json = JObject.Parse(File.ReadAllText(Path.GetFullPath(Path.Combine(repoRoot, dump)))) });
                }
                try { root = PrefabUtility.LoadPrefabContents(target); }
                catch (ArgumentException unsupported) when (targetExisted &&
                    unsupported.Message.IndexOf("Prefabs should have exactly 1 root GameObject", StringComparison.Ordinal) >= 0)
                {
                    // The existing reconstruction is not editable through Unity's prefab contents API.
                    // Preserve it and expose the limitation; a source sync cannot repair its imported hierarchy.
                    string reason = "existing target cannot load as one prefab hierarchy; retained: " + unsupported.Message;
                    result.status = "READ_ONLY_HIERARCHY";
                    result.editability = "READ_ONLY";
                    result.guid_after = result.guid_before;
                    result.warnings.Add(reason);
                    foreach (string changed in changedSources.Distinct(StringComparer.OrdinalIgnoreCase))
                    {
                        var sourceResult = new SourceEditResult { source = changed, status = "READ_ONLY_HIERARCHY" };
                        sourceResult.warnings.Add(reason);
                        sourceResult.fields.Add(new JsonToSerializedApplier.FieldEditability
                            { field = "*", editability = "READ_ONLY", reason = reason });
                        result.source_results.Add(sourceResult);
                    }
                    return result;
                }
                var bySource = nodes.ToDictionary(n => Path.GetFullPath(Path.Combine(repoRoot, n.dump)), StringComparer.OrdinalIgnoreCase);
                var byPid = nodes.ToDictionary(n => n.pid, StringComparer.Ordinal);
                var transforms = nodes.Where(n => n.type == "Transform" || n.type == "RectTransform")
                    .GroupBy(n => ReadPid(n.json["m_GameObject"]).ToString()).ToDictionary(g => g.Key, g => g.First());
                var hierarchy = root.GetComponentsInChildren<Transform>(true).GroupBy(PrefabBreadcrumb)
                    .ToDictionary(g => g.Key, g => g.ToArray(), StringComparer.Ordinal);
                var breadcrumbs = new Dictionary<string, string>(StringComparer.Ordinal);
                var ignored = new HashSet<string>(BuilderOwnedKeys)
                {
                    "m_Component", "m_Father", "m_Children", "m_RootOrder", "m_PrefabInternal", "m_TagString"
                };
                foreach (string changed in changedSources.Distinct(StringComparer.OrdinalIgnoreCase))
                {
                    var sourceResult = new SourceEditResult { source = changed };
                    result.source_results.Add(sourceResult);
                    try
                    {
                        if (!bySource.TryGetValue(Path.GetFullPath(changed), out Node selected))
                            throw new InvalidOperationException("changed object is not in this prefab plan");
                        Node owner = selected;
                        if (selected.type != "GameObject" && !byPid.TryGetValue(ReadPid(selected.json["m_GameObject"]).ToString(), out owner))
                            throw new ReadOnlyMappingException("component has no mapped owner");
                        string breadcrumb = SourceBreadcrumb(owner, byPid, transforms, breadcrumbs);
                        if (!hierarchy.TryGetValue(breadcrumb, out Transform[] candidates) || candidates.Length != 1)
                            throw new ReadOnlyMappingException("ambiguous or changed hierarchy path: " + breadcrumb);
                        UnityEngine.Object component = candidates[0].gameObject;
                        if (selected.type != "GameObject")
                        {
                            Type type = ResolveComponentType(selected);
                            Component[] components = type == null || !typeof(Component).IsAssignableFrom(type) ? new Component[0] : candidates[0].GetComponents(type);
                            if (components.Length != 1) throw new ReadOnlyMappingException("missing or ambiguous component type: " + selected.type);
                            component = components[0];
                        }
                        var report = JsonToSerializedApplier.ApplyAll(new SerializedObject(component), selected.json,
                            new JsonToSerializedApplier.Options { SkipEngineFields = false, IgnoreKeys = ignored, SafeEditableOnly = true,
                                PreserveUnresolvedPointers = true, ResolvePointer = JsonToSerializedApplier.ResolveStablePointer });
                        sourceResult.fields_applied = report.applied;
                        sourceResult.warnings.AddRange(report.messages);
                        sourceResult.fields = JsonToSerializedApplier.DescribeEditableFields(new SerializedObject(component), selected.json, false, false, ignored);
                        sourceResult.status = "UPDATED_PARTIAL";
                        sourceResult.editability = "PARTIAL";
                        result.fields_applied += report.applied;
                    }
                    catch (ReadOnlyMappingException unsupported)
                    {
                        sourceResult.warnings.Add(unsupported.Message);
                        sourceResult.fields.Add(new JsonToSerializedApplier.FieldEditability { field = "*", editability = "READ_ONLY", reason = unsupported.Message });
                    }
                }
                if (result.fields_applied > 0)
                {
                    PrefabUtility.SaveAsPrefabAsset(root, target, out bool saved);
                    if (!saved) throw new InvalidOperationException("prefab save failed");
                }
                result.guid_after = AssetDatabase.AssetPathToGUID(target);
                if (!string.IsNullOrEmpty(result.guid_before) && result.guid_before != result.guid_after) throw new InvalidOperationException("prefab GUID changed");
                result.status = "UPDATED_PARTIAL";
            }
            catch (Exception error) { result.error = error.Message; result.status = "ERROR"; }
            finally { if (root != null) PrefabUtility.UnloadPrefabContents(root); }
            return result;
        }

        /// <summary>
        /// A field patch cannot change generated sub-assets: Timeline graphs and the curves decoded from
        /// packed AnimationClips.  Prefabs carrying them are rebuilt when the conversion recipe changes.
        /// Every such prefab is addressed only by GUID (no other asset points at its internal file ids),
        /// so delete + save is safe for them; callers must rerun Addressables setup afterwards because
        /// deleting the asset drops its group entry.
        /// </summary>
        public static bool RebuildsOnRecipeChange(JObject planJob) =>
            (planJob["objects"] as JArray ?? new JArray()).Any(o => (string)o["type"] == "AnimationClip");

        public static EditResult RebuildEditable(string repoRoot, JObject planJob, IEnumerable<string> changedSources)
        {
            var job = new JobRecord { target = (string)planJob["target"], asset_path = (string)planJob["asset_path"] };
            var result = new EditResult { target = job.target, guid_before = AssetDatabase.AssetPathToGUID(job.target) };
            try
            {
                BuildOne(repoRoot, planJob, job);
                if (job.status != "BUILT" && job.status != "BUILT_PARTIAL")
                    throw new InvalidOperationException(job.error ?? job.status);
                result.guid_after = AssetDatabase.AssetPathToGUID(job.target);
                if (!string.IsNullOrEmpty(result.guid_before) && result.guid_before != result.guid_after)
                    throw new InvalidOperationException("prefab GUID changed");
                result.warnings.AddRange(job.warnings);
                result.status = "REBUILT";
                foreach (string changed in changedSources.Distinct(StringComparer.OrdinalIgnoreCase))
                    result.source_results.Add(new SourceEditResult { source = changed, status = "REBUILT", editability = "PARTIAL" });
            }
            catch (Exception error) { result.error = error.Message; result.status = "ERROR"; }
            return result;
        }

        private static string SourceBreadcrumb(Node owner, Dictionary<string, Node> byPid,
            Dictionary<string, Node> transforms, Dictionary<string, string> cached)
        {
            if (cached.TryGetValue(owner.pid, out string known)) return known;
            var names = new List<string>();
            var seen = new HashSet<string>();
            Node cursor = owner;
            while (cursor != null)
            {
                if (!seen.Add(cursor.pid)) throw new InvalidOperationException("cyclic prefab hierarchy");
                names.Insert(0, (string)cursor.json["m_Name"]);
                if (!transforms.TryGetValue(cursor.pid, out Node tr) ||
                    !byPid.TryGetValue(ReadPid(tr.json["m_Father"]).ToString(), out Node father) ||
                    !byPid.TryGetValue(ReadPid(father.json["m_GameObject"]).ToString(), out cursor)) break;
            }
            return cached[owner.pid] = string.Join("/", names);
        }

        private static string PrefabBreadcrumb(Transform transform)
        {
            var names = new List<string>();
            for (Transform cursor = transform; cursor != null; cursor = cursor.parent) names.Insert(0, cursor.name);
            return string.Join("/", names);
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
            var tempAssets = new List<UnityEngine.Object>();
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
                // Order matters: `AddComponent` of a type that carries [RequireComponent] silently adds the required
                // component itself, and a DisallowMultipleComponent type (every UI Graphic, every Selectable) then
                // refuses the planned node for it.  The plan lists nodes sorted by (type, dump file name), which has
                // nothing to do with the order the components were authored in, so a `LocalizeImage` listed before its
                // GameObject's `Image` made Unity create the Image, the planned Image node was refused twice, its
                // node stayed component-less and `LocalizeImage._image` had no target (measured: 64 of 127 `_image`
                // fields, 30 of 55 LocalizeImages, 34 of 68 PointerStayingSpriteSwapper).  Creation therefore follows
                // the shipped GameObject.m_Component order, then hoists whatever a type requires (see CreationOrder).
                // `claimed` is every component already bound to a node: a refused AddComponent may still adopt an
                // unclaimed component of exactly the planned type that an earlier requirement created.
                var claimed = new HashSet<int>();
                foreach (Node node in CreationOrder(nodes))
                {
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
                            if (rt != null) claimed.Add(rt.GetInstanceID());
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
                            claimed.Add(owner.transform.GetInstanceID());
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
                        // Timeline data is serialized as MonoBehaviour rows with m_GameObject = 0 in the
                        // shipped prefab.  It is a ScriptableObject graph, not a component to attach to a
                        // GameObject.  Recreate it in memory and attach it as a prefab sub-asset below; this
                        // also gives PlayableDirector.m_PlayableAsset a real target instead of fileID 0.
                        if (typeof(ScriptableObject).IsAssignableFrom(type) || type == typeof(AnimationClip))
                        {
                            // AnimationClip derives from Motion/UnityEngine.Object rather than
                            // ScriptableObject, but Timeline AnimationTracks hold it as their
                            // m_InfiniteClip pointer.  Keep it in the same local object map and
                            // persist it as a sub-asset alongside the TimelineAsset/track nodes.
                            UnityEngine.Object asset = type == typeof(AnimationClip)
                                ? new AnimationClip()
                                : (UnityEngine.Object)ScriptableObject.CreateInstance(type);
                            asset.name = (string)node.json["m_Name"] ?? node.type;
                            node.asset = asset;
                            node.component = asset;
                            tempAssets.Add(asset);
                            job.objects_created++;
                            continue;
                        }
                        job.warnings.Add(node.pid + ": component without owner GameObject");
                        job.warning_count++;
                        FieldDump(job, node, type, "NO_OWNER");
                        continue;
                    }

                    Component component = host.AddComponent(type);
                    if (component == null)
                    {
                        // Refused because an unclaimed component of exactly this type is already on the host (created
                        // by another component's [RequireComponent]): that component IS this node.
                        component = AdoptAutoAdded(host, type, claimed);
                        if (component != null)
                        {
                            job.components_adopted++;
                            FieldDump(job, node, type, "ADOPTED_AUTO_ADDED host=" + host.name);
                        }
                    }
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
                    claimed.Add(component.GetInstanceID());
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
                    if (retried == null) retried = AdoptAutoAdded(pending.host, pending.type, claimed);
                    if (retried == null)
                    {
                        job.component_add_failed++;
                        FieldDump(job, pending.node, pending.type, "ADD_REFUSED host=" + pending.host.name +
                                  " existing=" + string.Join(",", pending.host.GetComponents<Component>()
                                      .Where(c => c != null).Select(c => c.GetType().Name)));
                        continue;
                    }
                    claimed.Add(retried.GetInstanceID());
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

                // ---- sibling order -----------------------------------------------------------
                // SetParent appends, so the loop above leaves every parent's children in plan order.  The shipped
                // m_Children order is data: it is the UI draw order (a later sibling draws on top) and the order
                // layout groups place children in.  Measured on NormalPrinter: shipped Wrapper = [Stream,
                // Default-Stream(frames), Screen, Default-Stream(grass), AuthorPanel, MessageLabel, Indicators] came
                // out [AuthorPanel, Stream, Screen, MessageLabel, Indicators, Default-Stream, Default-Stream], so the
                // dark Screen gradient no longer covered the bottom frame and AuthorBase was drawn over AuthorLabel.
                var transformNodeByPid = new Dictionary<string, Node>();
                foreach (Node node in nodes.Where(n => n.isTransform))
                    transformNodeByPid[node.pid] = node;
                foreach (Node node in nodes.Where(n => n.isTransform))
                {
                    Transform parent = node.component as Transform;
                    JArray children = node.json["m_Children"] as JArray;
                    if (parent == null || children == null || children.Count < 2) continue;
                    int siblingIndex = 0;
                    foreach (JToken childToken in children)
                    {
                        if (!transformNodeByPid.TryGetValue(ReadPid(childToken).ToString(), out Node child)) continue;
                        Transform childTransform = child.component as Transform;
                        if (childTransform == null || childTransform.parent != parent) continue;
                        if (childTransform.GetSiblingIndex() != siblingIndex) job.siblings_reordered++;
                        childTransform.SetSiblingIndex(siblingIndex++);
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
                        // Component data lives under m_* (Image.m_Sprite, Graphic.m_Color, TMP m_fontAsset,
                        // Selectable.m_TargetGraphic, UnityEvent m_PersistentCalls, ...): dropping every m_* key
                        // left each UI prefab structurally complete and visually empty.  Only the object header the
                        // builder owns is skipped.
                        SkipEngineFields = false,
                        IgnoreKeys = BuilderOwnedKeys,
                        // An engine field whose default is an object (SpriteRenderer.m_Materials[0]) must survive a
                        // pointer that cannot be resolved (built-in resources are not in the plan).
                        PreserveUnresolvedPointers = true,
                    };
                    var report = JsonToSerializedApplier.ApplyAll(new SerializedObject(node.component), node.json, options);
                    job.fields_applied += report.applied;
                    job.pointers_bound += report.pointersBound;
                    job.pointers_adapted += report.pointersAdapted;
                    job.assembly_names_remapped += report.assemblyNamesRemapped;
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
                // The prefab's .meta GUID is the shipped one (align_asset_guids.py writes it) and Boot.unity's
                // `_gamePrefab` plus the Manosaba.Prefabs Addressables group are keyed by it.  Delete + save has
                // preserved it in every measured run, but that is a property of the Editor session, so it is
                // checked here for every prefab instead of assumed: a change is reported as GUID_CHANGED.
                string guidBefore = AssetDatabase.LoadMainAssetAtPath(target) != null ? AssetDatabase.AssetPathToGUID(target) : "";
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

                // Persist the standalone timeline graph inside the prefab.  AddObjectToAsset must happen after
                // SaveAsPrefabAsset created the main asset; the component fields already point at these objects,
                // and Unity will serialize the references with their generated local file IDs on the save above.
                foreach (UnityEngine.Object asset in tempAssets)
                {
                    if (asset == null) continue;
                    asset.hideFlags = HideFlags.HideInHierarchy;
                    AssetDatabase.AddObjectToAsset(asset, target);
                }
                if (tempAssets.Count > 0)
                {
                    // The first save creates the prefab file, while AddObjectToAsset attaches the
                    // Timeline graph immediately afterwards.  Reapply graph fields now that every
                    // local target is a persistent sub-asset; otherwise Unity can retain the live
                    // object references only in memory and serialize PlayableDirector.m_PlayableAsset
                    // or AnimationTrack.m_InfiniteClip as fileID 0.
                    foreach (Node node in nodes)
                        if (node.component is ScriptableObject || node.component is AnimationClip || node.type == "PlayableDirector")
                            ApplyFields(node, extraRootObjects);

                    // AnimationClip's generic (humanoid) representation stores its actual curves in
                    // m_MuscleClip.  That field is intentionally not exposed by Unity's
                    // SerializedObject API, so the generic JSON applier cannot copy it.  Rebuild the
                    // stream/constant curves through the supported AnimationUtility API before the
                    // final prefab save.  Leaving the AnimationClip subasset non-null but empty makes
                    // Timeline appear valid while every puppet remains frozen/invisible.
                    foreach (Node node in nodes)
                        if (node.component is AnimationClip clip)
                            RebuildPackedAnimationClip(node, clip, nodes, job);
                    PrefabUtility.SaveAsPrefabAsset(root, target);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.ImportAsset(target, ImportAssetOptions.ForceUpdate);
                }

                // ---- verify what was actually written ------------------------------------------
                // Reporting "objects created" is not evidence that the prefab *file* holds them: round 9
                // found 58 prefabs where the builder created every planned object and the saved asset
                // kept only the root plus its components (CreditsUI 8/3189, WitchBookUI 9/2065).  So the
                // saved asset is loaded back and counted, and a shortfall is a status, not a footnote.
                AssetDatabase.ImportAsset(target, ImportAssetOptions.ForceUpdate);
                job.guid_before = guidBefore;
                job.guid_after = AssetDatabase.AssetPathToGUID(target);
                if (guidBefore.Length > 0 && guidBefore != job.guid_after)
                {
                    job.guid_changed = true;
                    job.warnings.Add("GUID changed " + guidBefore + " -> " + job.guid_after +
                                     " (run tools/unity-rebuild/align_asset_guids.py)");
                    job.warning_count++;
                }
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

                job.status = job.guid_changed
                    ? "GUID_CHANGED"
                    : (job.warning_count == 0 && job.missing_types == 0 && job.pointers_unbound == 0
                        ? "BUILT"
                        : "BUILT_PARTIAL");
            }
            finally
            {
                foreach (GameObject go in temp) UnityEngine.Object.DestroyImmediate(go);
                foreach (UnityEngine.Object asset in tempAssets)
                    if (asset != null && !EditorUtility.IsPersistent(asset)) UnityEngine.Object.DestroyImmediate(asset);
                // Every ledger-bound pointer loads a texture into the Editor, and a batch session never unloads on
                // its own: the 2026-09-21 full build (2,191 such pointers) died with a native crash inside
                // LoadAssetAtPath on job 137 of 156 after 55 identical Reenact_* jobs had loaded fine.
                EditorUtility.UnloadUnusedAssetsImmediate();
                GC.Collect();
            }
        }

        // -- component creation order -------------------------------------------------------

        /// <summary>
        /// The non-GameObject nodes in the order their components must be created: the shipped
        /// GameObject.m_Component order of each GameObject (that is the order Unity itself had them in, which is
        /// always dependency-correct), nodes no GameObject lists after that, and finally any component whose
        /// [RequireComponent] dependency the plan lists later hoisted in front of it.
        /// </summary>
        private static List<Node> CreationOrder(List<Node> nodes)
        {
            var byPid = new Dictionary<string, Node>(StringComparer.Ordinal);
            foreach (Node n in nodes)
                if (n.pid != null && !byPid.ContainsKey(n.pid)) byPid[n.pid] = n;

            var ordered = new List<Node>();
            var seen = new HashSet<Node>();
            foreach (Node go in nodes)
            {
                if (go.type != "GameObject" || !(go.json["m_Component"] is JArray listed)) continue;
                foreach (JToken entry in listed)
                {
                    long pid = ReadPid(entry["component"]);
                    if (pid != 0 && byPid.TryGetValue(pid.ToString(), out Node member) &&
                        member.type != "GameObject" && seen.Add(member))
                        ordered.Add(member);
                }
            }
            foreach (Node n in nodes)
                if (n.type != "GameObject" && seen.Add(n)) ordered.Add(n);

            return HoistRequiredComponents(ordered);
        }

        /// <summary>
        /// Safety net for plans whose GameObject.m_Component order does not satisfy [RequireComponent]:
        /// within one GameObject, a node whose type requires R is moved behind the first node that provides R
        /// (R.IsAssignableFrom).  This is what makes `EnableDebugModeButton` ([RequireComponent(typeof(Selectable))])
        /// wait for the planned `Button`, and `ChoiceHandlerButton` for the planned `TweeningButton`: creating the
        /// requirer first would make Unity add a plain Selectable/Button of its own and refuse the planned subclass.
        /// </summary>
        private static List<Node> HoistRequiredComponents(List<Node> ordered)
        {
            var types = new Dictionary<Node, Type>();
            foreach (Node n in ordered) types[n] = ResolveComponentType(n);

            var byHost = ordered.GroupBy(n => ReadPid(n.json["m_GameObject"])).ToList();
            var result = new List<Node>(ordered.Count);
            var placed = new HashSet<Node>();
            var groupOf = new Dictionary<Node, List<Node>>();
            foreach (var g in byHost)
            {
                List<Node> members = g.ToList();
                for (int pass = 0; pass < members.Count; pass++)
                {
                    bool moved = false;
                    for (int i = 0; i < members.Count && !moved; i++)
                    {
                        Type requirer = types[members[i]];
                        if (requirer == null) continue;
                        foreach (Type required in RequiredTypes(requirer))
                        {
                            int index = i;
                            if (members.Take(index).Any(m => types[m] != null && required.IsAssignableFrom(types[m]))) continue;
                            int provider = members.FindIndex(index + 1, m => types[m] != null && required.IsAssignableFrom(types[m]));
                            if (provider < 0) continue;
                            Node moving = members[provider];
                            members.RemoveAt(provider);
                            members.Insert(index, moving);
                            moved = true;
                            break;
                        }
                    }
                    if (!moved) break;
                }
                foreach (Node m in members) groupOf[m] = members;
            }

            // Re-emit in the original sequence, one host at a time, so the m_Component order between hosts is kept.
            foreach (Node n in ordered)
            {
                if (placed.Contains(n)) continue;
                foreach (Node m in groupOf[n])
                {
                    if (placed.Add(m)) result.Add(m);
                }
            }
            return result;
        }

        private static readonly Dictionary<Type, List<Type>> RequiredTypeCache = new Dictionary<Type, List<Type>>();

        private static List<Type> RequiredTypes(Type type)
        {
            if (RequiredTypeCache.TryGetValue(type, out List<Type> cached)) return cached;
            var required = new List<Type>();
            foreach (object attribute in type.GetCustomAttributes(typeof(RequireComponent), true))
            {
                var rc = (RequireComponent)attribute;
                if (rc.m_Type0 != null) required.Add(rc.m_Type0);
                if (rc.m_Type1 != null) required.Add(rc.m_Type1);
                if (rc.m_Type2 != null) required.Add(rc.m_Type2);
            }
            RequiredTypeCache[type] = required;
            return required;
        }

        /// <summary>
        /// A component of exactly `type` already on the host that no node has claimed: created behind our back by
        /// another component's [RequireComponent].  Exact-type equality, not assignability, so a plain Button is never
        /// mistaken for a planned TweeningButton.
        /// </summary>
        private static Component AdoptAutoAdded(GameObject host, Type type, HashSet<int> claimed)
        {
            foreach (Component c in host.GetComponents(type))
            {
                if (c != null && c.GetType() == type && !claimed.Contains(c.GetInstanceID())) return c;
            }
            return null;
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
                           ?? asm.GetType("TMPro." + shortName, throwOnError: false)
                           // PlayableDirector (188 nodes), SortingGroup (59), RotationConstraint (4) were reported as
                           // "type not found": they live in these engine namespaces.
                           ?? asm.GetType("UnityEngine.Playables." + shortName, throwOnError: false)
                           ?? asm.GetType("UnityEngine.Rendering." + shortName, throwOnError: false)
                           ?? asm.GetType("UnityEngine.Animations." + shortName, throwOnError: false)
                           ?? asm.GetType("UnityEngine.Timeline." + shortName, throwOnError: false);
                if (hit != null) return hit;
            }
            return JsonToSerializedApplier.ResolveType(shortName, null);
        }

        // Unity's serialized AnimationClip has two representations.  Clips imported from the
        // original game use the packed generic representation (m_MuscleClip.m_Clip), whose
        // stream is not a SerializedProperty in the current Unity editor.  This decoder mirrors
        // Unity's packed stream layout and writes normal editor curves, which Unity serializes
        // back into the clip binding/curve tables used by PlayableDirector.
        private sealed class PackedKey
        {
            public int index;
            public float time;
            public float value;
            public float outSlope;
            public float inSlope;
            public float c1;
            public float c2;
        }

        private sealed class PackedFrame
        {
            public float time;
            public List<PackedKey> keys = new List<PackedKey>();
        }

        private static void RebuildPackedAnimationClip(Node node, AnimationClip clip, List<Node> nodes, JobRecord job)
        {
            JObject muscle = node.json["m_MuscleClip"] as JObject;
            JObject packed = muscle?["m_Clip"]?["data"] as JObject;
            if (packed == null) return;
            JObject streamed = packed["m_StreamedClip"] as JObject;
            JObject dense = packed["m_DenseClip"] as JObject;
            JObject constant = packed["m_ConstantClip"] as JObject;
            JArray bindings = node.json["m_ClipBindingConstant"]?["genericBindings"] as JArray;
            if (bindings == null || bindings.Count == 0) return;

            var pathByHash = new Dictionary<uint, string>();
            foreach (Node transform in nodes.Where(n => n.isTransform && n.gameObject != null))
            {
                string path = AnimationPath(transform, nodes);
                // AnimationClip generic-binding hashes are relative to the Animator
                // bound to the Timeline track, rather than the prefab root.  A
                // prefab can therefore contain a binding such as `black_R` while
                // the full hierarchy path is `BackgroundBlack/black_R`, or
                // `CurtainRoot/Curtain/bone_2` while the prefab path also has the
                // outer `Curtain` object.  Index every suffix so the serialized
                // hash resolves to the same path Unity expects at runtime.
                string[] segments = path.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                for (int start = 0; start < segments.Length; start++)
                {
                    string suffix = string.Join("/", segments.Skip(start));
                    uint hash = Crc32(suffix);
                    if (!pathByHash.ContainsKey(hash)) pathByHash[hash] = suffix;
                }
            }

            var curves = new Dictionary<int, AnimationCurve>();
            var curveBindings = new Dictionary<int, EditorCurveBinding>();
            int curveIndex = 0;
            foreach (JObject binding in bindings.OfType<JObject>())
            {
                uint pathHash = ReadUInt32(binding["path"]);
                uint attribute = ReadUInt32(binding["attribute"]);
                int typeId = binding["typeID"]?.Value<int>() ?? 0;
                string path = pathByHash.TryGetValue(pathHash, out string known) ? known : "";
                int componentCount = BindingComponentCount(typeId, attribute);
                if (pathHash != 0 && path.Length == 0)
                {
                    job.warnings.Add(node.pid + ".m_ClipBindingConstant.genericBindings: path hash " + pathHash + " not found in prefab hierarchy");
                    job.warning_count++;
                    // Unity leaves an unbound curve unused.  Writing it to "" would retarget a
                    // bone rotation (e.g. a shared Leia clip in a Leia-less prefab) onto the
                    // Animator root, colliding with the clip's real root curves.
                    curveIndex += componentCount;
                    continue;
                }
                for (int component = 0; component < componentCount; component++)
                {
                    int index = curveIndex++;
                    if (!TryMakeCurveBinding(typeId, attribute, path, component, out EditorCurveBinding editorBinding))
                    {
                        job.warnings.Add(node.pid + ".m_ClipBindingConstant.genericBindings: unsupported typeID=" +
                                         typeId + " attribute=" + attribute + " pathHash=" + pathHash);
                        job.warning_count++;
                        continue;
                    }
                    curveBindings[index] = editorBinding;
                    curves[index] = new AnimationCurve();
                }
            }

            List<PackedFrame> frames = ReadStreamedFrames(streamed?["data"] as JArray);
            int streamedCount = streamed?["curveCount"]?.Value<int>() ?? 0;
            foreach (PackedFrame frame in frames)
            {
                foreach (PackedKey key in frame.keys)
                {
                    if (!curves.TryGetValue(key.index, out AnimationCurve curve)) continue;
                    curve.AddKey(new Keyframe(frame.time, key.value, key.inSlope, key.outSlope));
                }
            }

            int denseCount = dense?["m_CurveCount"]?.Value<int>() ?? 0;
            int denseFrames = dense?["m_FrameCount"]?.Value<int>() ?? 0;
            float denseRate = dense?["m_SampleRate"]?.Value<float>() ?? 0f;
            float denseBegin = dense?["m_BeginTime"]?.Value<float>() ?? 0f;
            JArray denseValues = dense?["m_SampleArray"] as JArray;
            if (denseCount > 0 && denseFrames > 0 && denseRate > 0f && denseValues != null)
            {
                for (int frame = 0; frame < denseFrames; frame++)
                    for (int component = 0; component < denseCount; component++)
                    {
                        int index = streamedCount + component;
                        if (!curves.TryGetValue(index, out AnimationCurve curve)) continue;
                        int offset = frame * denseCount + component;
                        if (offset < denseValues.Count)
                            curve.AddKey(new Keyframe(denseBegin + frame / denseRate,
                                                      denseValues[offset].Value<float>()));
                    }
            }

            JArray constantValues = constant?["data"] as JArray;
            float stop = muscle["m_StopTime"]?.Value<float>() ?? 0f;
            for (int i = 0; constantValues != null && i < constantValues.Count; i++)
            {
                int index = streamedCount + denseCount + i;
                if (!curves.TryGetValue(index, out AnimationCurve curve)) continue;
                float value = constantValues[i].Value<float>();
                curve.AddKey(new Keyframe(0f, value));
                if (stop > 0f) curve.AddKey(new Keyframe(stop, value));
            }

            var written = new List<EditorCurveBinding>();
            foreach (KeyValuePair<int, EditorCurveBinding> pair in curveBindings)
            {
                AnimationCurve curve = curves[pair.Key];
                if (curve.length == 0) continue;
                AnimationUtility.SetEditorCurve(clip, pair.Value, curve);
                written.Add(pair.Value);
            }
            if (written.Count > 0)
            {
                clip.frameRate = node.json["m_SampleRate"]?.Value<float>() ?? 60f;
                // Timeline uses the AnimationClip's settings as the playable range.  Without
                // copying these bounds, clips such as CurtainOpen (1.5 seconds) are truncated
                // at the newly-created clip's default one-second stop time.
                AnimationClipSettings settings = AnimationUtility.GetAnimationClipSettings(clip);
                settings.startTime = muscle["m_StartTime"]?.Value<float>() ?? 0f;
                settings.stopTime = stop;
                settings.loopTime = muscle["m_LoopTime"]?.Value<bool>() ?? false;
                AnimationUtility.SetAnimationClipSettings(clip, settings);
                EditorUtility.SetDirty(clip);
                job.messages.Add(node.pid + ": rebuilt packed AnimationClip curves=" + written.Count);
            }
        }

        private static List<PackedFrame> ReadStreamedFrames(JArray words)
        {
            var frames = new List<PackedFrame>();
            if (words == null || words.Count == 0) return frames;
            byte[] bytes = new byte[words.Count * 4];
            for (int i = 0; i < words.Count; i++)
                Array.Copy(BitConverter.GetBytes(ReadUInt32(words[i])), 0, bytes, i * 4, 4);
            using (var reader = new BinaryReader(new MemoryStream(bytes)))
            {
                while (reader.BaseStream.Position + 8 <= reader.BaseStream.Length)
                {
                    float time = reader.ReadSingle();
                    int count = reader.ReadInt32();
                    if (count < 0 || count > 10000 || reader.BaseStream.Position + count * 20L > reader.BaseStream.Length)
                        break;
                    var frame = new PackedFrame { time = time };
                    for (int i = 0; i < count; i++)
                    {
                        // The tangent below needs the segment length (frame.time - prior.time);
                        // left at 0, every segment after a curve's first was treated as starting
                        // at t=0 (16,483 deg/s on the curtain instead of -27.9).
                        var key = new PackedKey { index = reader.ReadInt32(), time = time };
                        key.c1 = reader.ReadSingle();
                        key.c2 = reader.ReadSingle();
                        key.outSlope = reader.ReadSingle();
                        key.value = reader.ReadSingle();
                        key.inSlope = key.c2;
                        frame.keys.Add(key);
                    }
                    frames.Add(frame);
                }
            }
            // The packed stream starts with a sentinel frame and ends with an infinity frame;
            // neither is an animation sample.  Unity's own importer uses the same interior range.
            if (frames.Count > 2)
            {
                List<PackedFrame> interior = frames.GetRange(1, frames.Count - 2);
                var previous = new Dictionary<int, PackedKey>();
                foreach (PackedFrame frame in interior)
                {
                    foreach (PackedKey key in frame.keys)
                    {
                        if (previous.TryGetValue(key.index, out PackedKey prior))
                        {
                            // c1/c2/outSlope belong to the cubic segment that starts at
                            // the *previous* key.  The incoming tangent at this key is the
                            // derivative of that polynomial at its right endpoint.  Using
                            // the current key's coefficients here treats the next segment
                            // as if it were the previous one and produces absurd slopes
                            // (the curtain clip reached ~16,000 degrees/sec between two
                            // ordinary 20-degree keys).
                            // A segment whose cubic terms are all zero holds prior.value until
                            // this key, even when the value jumps (ObjectionCutIn 0/1 toggles).
                            // Mark it stepped; a zero tangent would turn the jump into a ramp.
                            float dx = Mathf.Max(frame.time - prior.time, 0.0001f);
                            if (prior.c1 == 0f && prior.c2 == 0f && prior.outSlope == 0f)
                                key.inSlope = float.PositiveInfinity;
                            else
                                key.inSlope = 3f * prior.c1 * dx * dx
                                            + 2f * prior.c2 * dx
                                            + prior.outSlope;
                        }
                        else key.inSlope = 0f;
                        previous[key.index] = key;
                    }
                }
                return interior;
            }
            return new List<PackedFrame>();
        }

        private static string AnimationPath(Node node, List<Node> nodes)
        {
            var names = new List<string>();
            Node cursor = node;
            var seen = new HashSet<string>();
            while (cursor != null && seen.Add(cursor.pid))
            {
                names.Insert(0, cursor.gameObject.name);
                long father = ReadPid(cursor.json["m_Father"]);
                cursor = father == 0 ? null : nodes.FirstOrDefault(n => n.pid == father.ToString() && n.isTransform);
            }
            if (names.Count > 1) names.RemoveAt(0); // animation paths omit the prefab root
            return string.Join("/", names);
        }

        private static uint Crc32(string text)
        {
            uint crc = 0xFFFFFFFFu;
            byte[] bytes = Encoding.UTF8.GetBytes(text ?? "");
            foreach (byte value in bytes)
            {
                crc ^= value;
                for (int bit = 0; bit < 8; bit++) crc = (crc >> 1) ^ (0xEDB88320u & (uint)-(int)(crc & 1));
            }
            return ~crc;
        }

        private static uint ReadUInt32(JToken token) => unchecked((uint)(token?.Value<long>() ?? 0L));

        private static int BindingComponentCount(int typeId, uint attribute)
        {
            if (typeId == 4 && (attribute == 1 || attribute == 3 || attribute == 4)) return 3;
            if (typeId == 4 && attribute == 2) return 4;
            return 1;
        }

        private static bool TryMakeCurveBinding(int typeId, uint attribute, string path, int component,
                                                out EditorCurveBinding binding)
        {
            binding = default(EditorCurveBinding);
            string suffix = component == 0 ? ".x" : component == 1 ? ".y" : component == 2 ? ".z" : ".w";
            if (typeId == 4)
            {
                string property = attribute == 1 ? "m_LocalPosition" :
                                  attribute == 3 ? "m_LocalScale" :
                                  attribute == 4 ? "localEulerAnglesRaw" : null;
                if (property == null) return false;
                binding = EditorCurveBinding.FloatCurve(path, typeof(Transform), property + suffix);
                return true;
            }
            if (typeId == 212 && attribute == 304273561u && component == 0)
            {
                binding = EditorCurveBinding.FloatCurve(path, typeof(SpriteRenderer), "m_Color.a");
                return true;
            }
            return false;
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

            // Unresolved pointers are classified, because "unbound" alone hid three different problems: a target the
            // plan never contained (the shipped file points into another asset's object, nothing to build), a target
            // the plan has but whose component could not be created, and an external asset that is not on disk.
            if (fileId == 0)
            {
                Node target = nodes.FirstOrDefault(n => n.pid == pathId.ToString());
                if (target == null)
                {
                    // An object of the SAME bundle that this job does not rebuild, recorded by the planner as a
                    // `pid:0:pathId` reference and loaded like any external asset:
                    //   * a Sprite/Texture2D the importer wrote out as its own file (import ledger): GalleryUI's
                    //     SlotLocked/SlotLoading textures, whose absence threw in CGGalleryGridSlot.Awake, and the
                    //     LayeredCharacters/Reenact sprites (2,191 pointers);
                    //   * a component or root of ANOTHER prefab of the plan (Game.prefab's DialogPresenter.
                    //     _providableDialogPrefab -> ProvidableDialog.prefab): the planner orders that prefab first, and
                    //     the applier takes the component of the field's type on its root.
                    string sidecarKey = node.pid + ":0:" + pathId;
                    if (references.TryGetValue(sidecarKey, out string sidecarPath))
                    {
                        string sidecarUnityPath = MapShippedAssetPath(sidecarPath);
                        if (sidecarUnityPath == null)
                        {
                            job.unresolved_asset_missing++;
                            if (job.missing_asset_paths.Count < 20 && !job.missing_asset_paths.Contains(sidecarPath))
                                job.missing_asset_paths.Add(sidecarPath);
                            return null;
                        }
                        // Counted only once the asset is really loaded; the applier then converts it to the field's
                        // type (a sprite-mode PNG's Texture2D -> its Sprite sub-asset for SpriteRenderer.m_Sprite)
                        // exactly as for an external pointer, and counts the binding in pointers_bound.
                        UnityEngine.Object sidecarAsset = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(sidecarUnityPath);
                        if (sidecarAsset != null) job.pointers_sidecar++;
                        else job.unresolved_asset_missing++;
                        return sidecarAsset;
                    }
                    job.unresolved_target_not_in_plan++;
                    return null;
                }
                // An object that lives in an extra root's own prefab asset must be referenced through that asset,
                // not through the live scene object -- otherwise SaveAsPrefabAsset cannot serialise it.
                if (extraRootObjects != null && extraRootObjects.TryGetValue(target.pid, out UnityEngine.Object mapped))
                    return mapped;
                UnityEngine.Object live = target.component != null ? target.component : (UnityEngine.Object)target.gameObject;
                if (live == null) job.unresolved_target_not_built++;
                return live;
            }

            string key = node.pid + ":" + fileId + ":" + pathId;
            if (!references.TryGetValue(key, out string assetPath))
            {
                job.unresolved_no_reference_entry++;
                return null;
            }
            string unityPath = MapShippedAssetPath(assetPath);
            if (unityPath == null)
            {
                job.unresolved_asset_missing++;
                if (job.missing_asset_paths.Count < 20 && !job.missing_asset_paths.Contains(assetPath))
                    job.missing_asset_paths.Add(assetPath);
                return null;
            }
            return AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(unityPath);
        }

        /// <summary>
        /// The plan carries the SHIPPED asset path (Assets/#WitchTrials/Textures/.../CloseButton_Default.png); the
        /// rebuilt project keeps generated content elsewhere.  Candidates, first one on disk wins:
        ///   1. the same path under Assets/GameContent/ (prefabs, materials, ScriptableObjects, plain textures)
        ///   2. sprite PNGs, which the importer places one per folder:  dir/Name.sprites/Name.png
        ///   3. fonts and their materials, which live in Assets/GameContent/RuntimeFonts/&lt;Family&gt;/
        /// Returns null when none exists, which the caller reports as a missing asset (never silently as null).
        /// </summary>
        private static string MapShippedAssetPath(string shipped)
        {
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string p = shipped.Replace('\\', '/');
            string generated = p.StartsWith("Assets/GameContent/", StringComparison.Ordinal) ? p     // ledger paths are already project paths
                : p.StartsWith("Assets/", StringComparison.Ordinal) ? "Assets/GameContent/" + p.Substring("Assets/".Length) : p;

            var candidates = new List<string> { generated };
            if (p.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
            {
                string dir = generated.Substring(0, generated.LastIndexOf('/'));
                string name = Path.GetFileNameWithoutExtension(generated);
                candidates.Add(dir + "/" + name + ".sprites/" + name + ".png");
                // Localized variants: the folder keeps the shipped name (Agreement@Ja.sprites) but the PNG inside has
                // '@' replaced by '_' (Agreement_Ja.png).  100 pointers (94 distinct sprites; TitleUI, ControlPanel,
                // OptionsUI, the choice buttons...) were "asset missing" until this candidate existed.
                if (name.IndexOf('@') >= 0)
                    candidates.Add(dir + "/" + name + ".sprites/" + name.Replace('@', '_') + ".png");
            }
            const string fonts = "Assets/#WitchTrials/Fonts/";
            if (p.StartsWith(fonts, StringComparison.Ordinal))
                candidates.Add("Assets/GameContent/RuntimeFonts/" + p.Substring(fonts.Length));

            foreach (string candidate in candidates)
            {
                if (File.Exists(Path.Combine(projectRoot, candidate))) return candidate;
            }
            return null;
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
            public int pointers_adapted;
            public int pointers_sidecar;
            public int assembly_names_remapped;
            public int components_adopted;
            public int guid_changed;
            public int unresolved_target_not_in_plan;
            public int unresolved_target_not_built;
            public int unresolved_no_reference_entry;
            public int unresolved_asset_missing;
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
            public int siblings_reordered;
            public int transform_add_failed;
            public int component_add_failed;
            public int objects_unbuildable;
            public bool hierarchy_complete;
            public string root_name;
            public int references_planned;
            public int fields_applied;
            public int pointers_bound;
            public int pointers_unbound;
            /// <summary>Pointers bound after converting the resolved asset to the field's type (Texture2D -> Sprite, prefab GameObject -> its component).</summary>
            public int pointers_adapted;
            /// <summary>Intra-bundle pointers (fileId 0) bound to an asset outside this job (ledger file or another prefab).</summary>
            public int pointers_sidecar;
            /// <summary>String fields naming a type in the renamed Assembly-CSharp (UnityEvent m_TargetAssemblyTypeName).</summary>
            public int assembly_names_remapped;
            /// <summary>Planned components that were already on the host, auto-added by another component's [RequireComponent], and adopted.</summary>
            public int components_adopted;
            public bool guid_changed;
            public string guid_before;
            public string guid_after;
            /// <summary>Internal pointer to an object the plan does not contain (the shipped file points into another asset).</summary>
            public int unresolved_target_not_in_plan;
            /// <summary>Internal pointer to a planned node whose component could not be created.</summary>
            public int unresolved_target_not_built;
            /// <summary>External pointer with no entry in the plan's reference table.</summary>
            public int unresolved_no_reference_entry;
            /// <summary>External pointer whose asset is not on disk under any of the mapped locations.</summary>
            public int unresolved_asset_missing;
            public List<string> missing_asset_paths = new List<string>();
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
