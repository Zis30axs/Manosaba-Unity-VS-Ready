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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

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

        // Court/Court_Final are the only shipped prefabs whose Mesh and Material objects are embedded in the
        // prefab bundle but intentionally omitted from the generic prefab plan.  Keep this small recipe in the
        // converter so a collaborator can regenerate the assets from the normal res workspace; generated .asset
        // and .mat files remain under the ignored GameContent tree.
        private const long CourtMesh52PathId = -3954970830415787521L;
        private const long CourtMesh171PathId = 5831335090265640653L;
        private const string CourtPrefabBundle = "general-prefabs_assets_all.bundle";
        private const string CourtGeneratedFolder =
            "Assets/GameContent/#WitchTrials/Prefabs/General/PerspectiveStage/Generated";
        private const string CourtMeshFolder = CourtGeneratedFolder + "/Meshes";
        private const string CourtMaterialFolder = CourtGeneratedFolder + "/Materials";

        private sealed class CourtMaterialRecipe
        {
            public string bundle;
            public long pathId;
            public string name;
        }

        private static readonly Dictionary<long, CourtMaterialRecipe> CourtMaterials =
            new Dictionary<long, CourtMaterialRecipe>
            {
                { -8477907749381902243L, new CourtMaterialRecipe { bundle = CourtPrefabBundle, pathId = -8477907749381902243L, name = "Court_Floor" } },
                { 1275342495298109738L, new CourtMaterialRecipe { bundle = CourtPrefabBundle, pathId = 1275342495298109738L, name = "Fire" } },
                { 1847911709981235371L, new CourtMaterialRecipe { bundle = CourtPrefabBundle, pathId = 1847911709981235371L, name = "Court_Wall@Flip" } },
                { -7375473515524017034L, new CourtMaterialRecipe { bundle = CourtPrefabBundle, pathId = -7375473515524017034L, name = "Court_Stand" } },
                { -2186335138721067432L, new CourtMaterialRecipe { bundle = CourtPrefabBundle, pathId = -2186335138721067432L, name = "Court_Wall" } },
                { -1479458066123357679L, new CourtMaterialRecipe { bundle = CourtPrefabBundle, pathId = -1479458066123357679L, name = "Court_Step" } },
                { 4935044996147944358L, new CourtMaterialRecipe { bundle = CourtPrefabBundle, pathId = 4935044996147944358L, name = "Court_Wall@7" } },
            };

        private sealed class CourtTextureRecipe
        {
            public string bundle;
            public long pathId;
        }

        private static readonly Dictionary<long, CourtTextureRecipe> CourtTextures =
            new Dictionary<long, CourtTextureRecipe>
            {
                { 6313933559636115173L, new CourtTextureRecipe { bundle = CourtPrefabBundle, pathId = 6313933559636115173L } },
                { 188620688936377846L, new CourtTextureRecipe { bundle = CourtPrefabBundle, pathId = 188620688936377846L } },
                { -3720777132935737500L, new CourtTextureRecipe { bundle = CourtPrefabBundle, pathId = -3720777132935737500L } },
                { 83921366182283349L, new CourtTextureRecipe { bundle = CourtPrefabBundle, pathId = 83921366182283349L } },
                { -1208622118293267194L, new CourtTextureRecipe { bundle = CourtPrefabBundle, pathId = -1208622118293267194L } },
                { -4374219984011537508L, new CourtTextureRecipe { bundle = CourtPrefabBundle, pathId = -4374219984011537508L } },
                { 8694104602785124754L, new CourtTextureRecipe { bundle = CourtPrefabBundle, pathId = 8694104602785124754L } },
                { -7895505446595129764L, new CourtTextureRecipe { bundle = CourtPrefabBundle, pathId = -7895505446595129764L } },
                { 5166769313130036686L, new CourtTextureRecipe { bundle = CourtPrefabBundle, pathId = 5166769313130036686L } },
                { 2137707876492198678L, new CourtTextureRecipe { bundle = CourtPrefabBundle, pathId = 2137707876492198678L } },
                { -469680402320609555L, new CourtTextureRecipe { bundle = CourtPrefabBundle, pathId = -469680402320609555L } },
                { -3494541382177401206L, new CourtTextureRecipe { bundle = "general-sprites_assets_all.bundle", pathId = -3494541382177401206L } },
                { 8906857723818035118L, new CourtTextureRecipe { bundle = "general-sprites_assets_all.bundle", pathId = 8906857723818035118L } },
            };

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
                        // A converted Court light intensity and a cut-in glow's sorting order are owned by the recipe;
                        // the res value must not undo them.
                        HashSet<string> sourceIgnored = IsCourtCandelaLight(planJob, component, selected.json)
                            ? new HashSet<string>(ignored) { "m_Intensity" }
                            : IsCutInGlowRenderer(planJob, component) ? new HashSet<string>(ignored) { "m_SortingOrder" } : ignored;
                        var report = JsonToSerializedApplier.ApplyAll(new SerializedObject(component), selected.json,
                            new JsonToSerializedApplier.Options { SkipEngineFields = false, IgnoreKeys = sourceIgnored, SafeEditableOnly = true,
                                PreserveUnresolvedPointers = true, ResolvePointer = JsonToSerializedApplier.ResolveStablePointer });
                        sourceResult.fields_applied = report.applied;
                        sourceResult.warnings.AddRange(report.messages);
                        sourceResult.fields = JsonToSerializedApplier.DescribeEditableFields(new SerializedObject(component), selected.json, false, false, sourceIgnored);
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
        /// A field patch cannot change generated sub-assets: Timeline graphs, decoded packed AnimationClips,
        /// the Court meshes/materials reconstructed from embedded bundle objects, or a deferred TMP asset that labels reference.
        /// Prefabs carrying them are rebuilt when the conversion recipe changes.
        /// Every such prefab is addressed only by GUID (no other asset points at its internal file ids),
        /// so delete + save is safe for them; callers must rerun Addressables setup afterwards because
        /// deleting the asset drops its group entry.
        /// </summary>
        public static bool RebuildsOnRecipeChange(JObject planJob)
        {
            string assetPath = ((string)planJob["asset_path"] ?? "").Replace('\\', '/');
            return (planJob["objects"] as JArray ?? new JArray()).Any(o => (string)o["type"] == "AnimationClip") ||
                   IsCourtPrefab(assetPath) || ReferencesDeferredTmpAsset(planJob);
        }

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
                if ((string)planJob["adapter"] == "switch-story-cutin-v1")
                    result.warnings.AddRange(job.messages.Where(message => message.Contains(": source_ignored_binding ")));
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
            // RebuildEditable constructs a fresh JobRecord; only the batch Run caller used to
            // initialize this count. Keep missing/unreadable dumps in the planned total as well.
            job.objects_planned = (jobToken["objects"] as JArray)?.Count ?? 0;
            // Optional, narrowly scoped recipe for the three Switch title effects. Generated assets
            // stay inside SwitchTitle and ordinary prefab jobs keep the existing resolver unchanged.
            var titleAssets = SwitchTitleAssets.Prepare(jobToken, target);
            var storyAssets = SwitchStoryAssets.Prepare(jobToken, target);

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
                        string tagName = storyAssets != null && tag.Type == JTokenType.Integer && tag.Value<int>() == 0
                            ? "Untagged" : tag.ToString();
                        try { go.tag = tagName; } catch (UnityException) { /* tag not in this project */ }
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
                        ResolvePointer = token => titleAssets?.Resolve(token) ?? storyAssets?.Resolve(token) ??
                            ResolvePointer(token, node, nodes, references, repoRoot, job, extraMap),
                        PathPrefix = node.pid + ".",
                        // Component data lives under m_* (Image.m_Sprite, Graphic.m_Color, TMP m_fontAsset,
                        // Selectable.m_TargetGraphic, UnityEvent m_PersistentCalls, ...): dropping every m_* key
                        // left each UI prefab structurally complete and visually empty.  Only the object header the
                        // builder owns is skipped.
                        SkipEngineFields = false,
                        IgnoreKeys = storyAssets != null ? storyAssets.IgnoredKeys(node.component, BuilderOwnedKeys) :
                            CourtPointerKeys(node, job) ?? BuilderOwnedKeys,
                        // An engine field whose default is an object (SpriteRenderer.m_Materials[0]) must survive a
                        // pointer that cannot be resolved (built-in resources are not in the plan).
                        PreserveUnresolvedPointers = true,
                    };
                    var report = JsonToSerializedApplier.ApplyAll(new SerializedObject(node.component), node.json, options);
                    storyAssets?.ApplyTypedFields(node.component, node.json, options.ResolvePointer);
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

                // Label pointers (m_spriteAsset, m_StyleSheet) resolve by file existence, so the assets must exist first.
                EnsureDeferredTmpAssets(repoRoot, references, job);

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

                // Mesh/Material objects embedded in the Court bundle are not represented by prefab-plan nodes.
                // Bind them after the generic field pass, while the live components still exist, so the same
                // BuildOne path can persist the assignments into the prefab.  This is deliberately before the
                // delete/save below: the generated mesh/material assets are external and survive a prefab rebuild.
                if (IsCourtPrefab(job.asset_path))
                    ApplyCourtBindings(repoRoot, nodes, job);

                // ---- save --------------------------------------------------------------------
                // (root/rootNode were resolved before the field-application loop so the tree can be
                //  measured both before and after it)
                job.transforms_after_fields = root != null ? CountDescendantTransforms(root.transform) : -1;
                if (root == null)
                {
                    job.status = "NO_ROOT";
                    return;
                }
                if (titleAssets != null)
                    job.messages.Add("Switch title: bound " + titleAssets.BindParticleTextures(root) + " particle Sprite textures to dedicated materials");
                storyAssets?.BindParticleTextures(root);

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

                if (IsCutInPrefab(job.asset_path)) OrderCutInGlowBehindGlass(root);
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
                            RebuildPackedAnimationClip(node, clip, nodes, job,
                                storyAssets?.AnimationPaths(clip, root, nodes.Where(n => n.component != null)
                                    .ToDictionary(n => n.pid, n => n.component, StringComparer.Ordinal)));
                    if (titleAssets != null)
                    {
                        titleAssets.CompleteController(nodes.Where(n => n.component != null)
                            .ToDictionary(n => n.pid, n => n.component, StringComparer.Ordinal));
                        job.messages.Add("Switch title: reconstructed simple controller, material compatibility recipe applied");
                    }
                    storyAssets?.CompleteControllers(nodes.Where(n => n.component != null)
                        .ToDictionary(n => n.pid, n => n.component, StringComparer.Ordinal));
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
                if (titleAssets != null)
                    titleAssets.RebindSavedClips(target, nodes.Where(n => n.type == "AnimationClip")
                        .ToDictionary(n => n.pid, n => (string)n.json["m_Name"], StringComparer.Ordinal));
                storyAssets?.RebindSavedClips(target, nodes.Where(n => n.type == "AnimationClip")
                    .ToDictionary(n => n.pid, n => (string)n.json["m_Name"], StringComparer.Ordinal));
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
                if (IsCutInPrefab(job.asset_path)) EnsureCutInSpriteMeshTypes(saved);
                storyAssets?.Verify(saved);
                if (storyAssets != null)
                {
                    foreach (Node node in nodes.Where(n => n.component is ParticleSystem))
                    {
                        string path = AnimationUtility.CalculateTransformPath(((ParticleSystem)node.component).transform, root.transform);
                        Transform written = path.Length == 0 ? saved.transform : saved.transform.Find(path);
                        var particle = written != null ? written.GetComponent<ParticleSystem>() : null;
                        if (particle == null || JsonToSerializedApplier.VerifyAnimationCurves(new SerializedObject(particle), node.json) == 0)
                            throw new InvalidOperationException("Story particle/curves missing after save: " + path);
                    }
                    if (job.warnings.Any(w => w.Contains("genericBindings: unsupported")))
                        throw new InvalidOperationException("Unsupported Switch story animation binding: " +
                            string.Join("; ", job.warnings.Where(w => w.Contains("genericBindings: unsupported"))));
                }
                if (titleAssets != null)
                {
                    titleAssets.VerifyController(saved);
                    titleAssets.VerifyParticleTextures(saved);
                    int verifiedCurves = 0;
                    foreach (Node node in nodes.Where(n => n.component is ParticleSystem))
                    {
                        var live = (ParticleSystem)node.component;
                        string particlePath = AnimationUtility.CalculateTransformPath(live.transform, root.transform);
                        Transform written = particlePath.Length == 0 ? saved.transform : saved.transform.Find(particlePath);
                        ParticleSystem particle = written != null ? written.GetComponent<ParticleSystem>() : null;
                        if (particle == null) throw new InvalidOperationException("Title particle missing after save: " + particlePath);
                        int particleCurves = JsonToSerializedApplier.VerifyAnimationCurves(new SerializedObject(particle), node.json);
                        if (particleCurves == 0) throw new InvalidOperationException("No title particle curves could be verified: " + particlePath);
                        verifiedCurves += particleCurves;
                    }
                    job.messages.Add("Switch title: verified " + verifiedCurves + " serialized particle curves after prefab save");
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

                if (titleAssets != null && (job.pointers_unbound != 0 || job.missing_types != 0 || job.objects_unbuildable != 0))
                    throw new InvalidOperationException("Switch title prefab has unresolved pointers/types/objects: " +
                        job.pointers_unbound + "/" + job.missing_types + "/" + job.objects_unbuildable);
                if (storyAssets != null && (job.pointers_unbound != 0 || job.missing_types != 0 || job.objects_unbuildable != 0))
                    throw new InvalidOperationException("Switch story prefab has unresolved pointers/types/objects: " +
                        job.pointers_unbound + "/" + job.missing_types + "/" + job.objects_unbuildable);
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

        // Timeline AudioClips of the ObjectionCutIn prefabs (bundle naninovel-spawn_assets_all): path id -> generated asset.
        // tools/content/content.py ensure_cutin_audio writes the files; keep both tables in step.
        private static readonly Dictionary<long, string> CutInAudio = new Dictionary<long, string>
        {
            { 4370497403900473225L, "Assets/GameContent/#WitchTrials/Prefabs/Naninovel/Spawn/CutIn/ObjectionCutIn_Crack_01.ogg" },
            { -3434779548957938459L, "Assets/GameContent/#WitchTrials/Prefabs/Naninovel/Spawn/CutIn/ObjectionCutIn_Crack_02.ogg" },
        };

        // The refutation cut-ins' sprites use Shader Graph materials (Background_0Fix, Glasses_0Fix, Iuminescence_*,
        // Shadow_Fix) that are in no ledger row and whose shaders ship only as compiled programs.  Without them every
        // sprite gets the default material: an opaque white square and opaque luminance maps cover the scene in solid
        // black or white.  Assets/Shaders/ManosabaCutIn/*.shader are translations of those compiled programs
        // (tools/unity-rebuild/port_cutin_shaders.py) with the shipped property names, so the original Timeline curves
        // (material._Float / _Tick / _Fader / _Fader2) drive them.  The values below are the materials' saved
        // properties in naninovel-spawn_assets_all.bundle; the keys are the material objects' path ids.  Ema, Hiro and
        // CreatureHiro are covered (CreatureHiro uses Hiro's materials plus Ema's silhouette for its `Hiro_C_Effect`).
        private sealed class CutInMaterialRecipe
        {
            public string name;
            public string shader;
            public readonly Dictionary<string, Color> colors = new Dictionary<string, Color>();
            public readonly Dictionary<string, float> floats = new Dictionary<string, float>();
            public readonly Dictionary<string, long> textures = new Dictionary<string, long>();   // property -> Texture2D path id
        }

        private const string CutInMaterialShaderRoot = "Manosaba/CutIn/";
        private const string CutInSpawnBundle = "naninovel-spawn_assets_all.bundle";

        private static readonly CutInMaterialRecipe CutInShadow = new CutInMaterialRecipe
        {
            name = "CutIn_Shadow", shader = CutInMaterialShaderRoot + "Shadow",
            colors = { { "_Color", new Color(0.451f, 0.49f, 0.6f, 1f) } },
        };

        private static readonly CutInMaterialRecipe CutInEmaBackground = new CutInMaterialRecipe
        {
            name = "CutIn_Ema_Background", shader = CutInMaterialShaderRoot + "Background",
            colors = { { "_BackgroundA", new Color(1f, 0f, 0.688f, 0f) }, { "_BackgroundB", new Color(0f, 0f, 0f, 0f) } },
            floats = { { "_Float", 0.3f } },
        };

        private static readonly CutInMaterialRecipe CutInEmaDissolve = new CutInMaterialRecipe
        {
            name = "CutIn_Ema_LuminescenceDissolve", shader = CutInMaterialShaderRoot + "LuminescenceDissolve",
            colors = { { "_Color", new Color(1f, 0.871f, 1f, 1f) } }, floats = { { "_Tick", 0f } },
        };

        private static readonly CutInMaterialRecipe CutInEmaSilhouette = new CutInMaterialRecipe
        {
            name = "CutIn_Ema_LuminescenceSilhouette", shader = CutInMaterialShaderRoot + "LuminescenceSilhouette",
            colors = { { "_Color", new Color(1f, 0.871f, 1f, 1f) } },
        };

        private static readonly CutInMaterialRecipe CutInEmaGlasses = new CutInMaterialRecipe
        {
            name = "CutIn_Ema_Glasses", shader = CutInMaterialShaderRoot + "Glasses",
            colors = { { "_EclipseFlame", new Color(1f, 0f, 0.94f, 0f) } },
            floats = { { "_Fader", 1f }, { "_Fader2", 1f }, { "_Speed", 3f }, { "_Tick", 1f }, { "_EdgeSize", 0.02f } },
            textures = { { "_kirakira", -3830398693511988194L }, { "_kirakira2", 506248237000484121L } },
        };

        private static readonly CutInMaterialRecipe CutInHiroBackground = new CutInMaterialRecipe
        {
            name = "CutIn_Hiro_Background", shader = CutInMaterialShaderRoot + "Background",
            colors = { { "_BackgroundA", new Color(1f, 0.236f, 0.269f, 0f) }, { "_BackgroundB", new Color(0f, 0f, 0f, 0f) } },
            floats = { { "_Float", 0.3f } },
        };

        private static readonly CutInMaterialRecipe CutInHiroDissolve = new CutInMaterialRecipe
        {
            name = "CutIn_Hiro_LuminescenceDissolve", shader = CutInMaterialShaderRoot + "LuminescenceDissolve",
            colors = { { "_Color", new Color(0.992f, 0.647f, 0.643f, 1f) } }, floats = { { "_Tick", 0.952f } },
        };

        private static readonly CutInMaterialRecipe CutInHiroSilhouette = new CutInMaterialRecipe
        {
            name = "CutIn_Hiro_LuminescenceSilhouette", shader = CutInMaterialShaderRoot + "LuminescenceSilhouette",
            colors = { { "_Color", new Color(0.992f, 0.647f, 0.643f, 1f) } },
        };

        private static readonly CutInMaterialRecipe CutInHiroGlasses = new CutInMaterialRecipe
        {
            name = "CutIn_Hiro_Glasses", shader = CutInMaterialShaderRoot + "Glasses",
            colors = { { "_EclipseFlame", new Color(1f, 0f, 0.042f, 0f) } },
            floats = { { "_Fader", 1f }, { "_Fader2", 1f }, { "_Speed", 3f }, { "_Tick", 1f }, { "_EdgeSize", 0.02f } },
            textures = { { "_kirakira", 1391563002887383734L }, { "_kirakira2", -5678023905743583822L } },
        };

        private static readonly Dictionary<string, Dictionary<long, CutInMaterialRecipe>> CutInMaterials =
            new Dictionary<string, Dictionary<long, CutInMaterialRecipe>>
            {
                {
                    "ObjectionCutIn_Ema", new Dictionary<long, CutInMaterialRecipe>
                    {
                        { 7796377464895998716L, CutInShadow },
                        { -2611326965432545055L, CutInEmaBackground },
                        { 978521274601713976L, CutInEmaDissolve },
                        { -8014026575719811303L, CutInEmaSilhouette },
                        { -5183371573251200786L, CutInEmaGlasses },
                    }
                },
                {
                    "ObjectionCutIn_Hiro", new Dictionary<long, CutInMaterialRecipe>
                    {
                        { 7796377464895998716L, CutInShadow },
                        { 904597682965511602L, CutInHiroBackground },
                        { 6986961580769073850L, CutInHiroDissolve },
                        { -1981712515745401374L, CutInHiroSilhouette },
                        { -4345803368799099511L, CutInHiroGlasses },
                    }
                },
                {
                    "ObjectionCutIn_CreatureHiro", new Dictionary<long, CutInMaterialRecipe>
                    {
                        { 7796377464895998716L, CutInShadow },
                        { 904597682965511602L, CutInHiroBackground },
                        { 6986961580769073850L, CutInHiroDissolve },
                        { -1981712515745401374L, CutInHiroSilhouette },
                        { -4345803368799099511L, CutInHiroGlasses },
                        { -8014026575719811303L, CutInEmaSilhouette },
                    }
                },
            };

        private const string CutInMaterialFolder = "Assets/GameContent/#WitchTrials/Prefabs/Naninovel/Spawn/CutIn/Generated/Materials";

        private static Material ResolveCutInMaterial(string repoRoot, JobRecord job, long pathId)
        {
            string prefab = Path.GetFileNameWithoutExtension((job.asset_path ?? "").Replace('\\', '/'));
            if (!CutInMaterials.TryGetValue(prefab, out Dictionary<long, CutInMaterialRecipe> recipes) ||
                !recipes.TryGetValue(pathId, out CutInMaterialRecipe recipe))
                return null;
            Shader shader = Shader.Find(recipe.shader);
            if (shader == null) throw new InvalidOperationException("Cut-in shader is unavailable: " + recipe.shader);
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string assetPath = CutInMaterialFolder + "/" + recipe.name + ".mat";
            EnsureAssetFolder(projectRoot, CutInMaterialFolder);
            Material material = AssetDatabase.LoadAssetAtPath<Material>(assetPath);
            if (material == null)
            {
                if (File.Exists(Path.Combine(projectRoot, assetPath.Replace('/', Path.DirectorySeparatorChar))))
                    throw new InvalidDataException("Cut-in material exists but is not a Material: " + assetPath);
                material = new Material(shader);
                AssetDatabase.CreateAsset(material, assetPath);
            }
            material.shader = shader;
            material.name = recipe.name;
            foreach (KeyValuePair<string, Color> color in recipe.colors) material.SetColor(color.Key, color.Value);
            foreach (KeyValuePair<string, float> number in recipe.floats) material.SetFloat(number.Key, number.Value);
            foreach (KeyValuePair<string, long> entry in recipe.textures)
            {
                string texturePath = FindCourtTextureTarget(repoRoot, new CourtTextureRecipe { bundle = CutInSpawnBundle, pathId = entry.Value });
                Texture texture = AssetDatabase.LoadAssetAtPath<Texture>(texturePath);
                if (texture == null) throw new InvalidDataException("Cut-in texture is not imported: " + texturePath);
                material.SetTexture(entry.Key, texture);
            }
            EditorUtility.SetDirty(material);
            AssetDatabase.SaveAssets();
            return material;
        }

        /// <summary>
        /// The shader-graph cut-in layers take their shape from the sprite mesh (their alpha ignores the texture), and the
        /// shipped sprites are Tight.  Importing them FullRect draws the shader over the whole rectangle, which shows as a
        /// black fill around the glass and the portrait.  Sprites already imported keep their .meta, so correct them here;
        /// the importer policy covers new imports.  Reimporting a single-mode crop keeps its GUID and sprite fileID.
        /// </summary>
        private static void EnsureCutInSpriteMeshTypes(GameObject prefab)
        {
            var wanted = new Dictionary<string, SpriteMeshType>(StringComparer.Ordinal);
            foreach (SpriteRenderer renderer in prefab.GetComponentsInChildren<SpriteRenderer>(true))
            {
                Material material = renderer.sharedMaterial;
                if (renderer.sprite == null || material == null ||
                    !(AssetDatabase.GetAssetPath(material) ?? "").StartsWith(CutInMaterialFolder, StringComparison.Ordinal)) continue;
                string spritePath = AssetDatabase.GetAssetPath(renderer.sprite);
                if (!SpriteImportPolicy.IsCutInCrop(spritePath)) continue;
                wanted[spritePath] = SpriteImportPolicy.ShippedMeshType(spritePath);
            }
            foreach (KeyValuePair<string, SpriteMeshType> item in wanted)
            {
                var importer = AssetImporter.GetAtPath(item.Key) as TextureImporter;
                if (importer == null) throw new InvalidDataException("Cut-in sprite is not a texture importer asset: " + item.Key);
                var settings = new TextureImporterSettings();
                importer.ReadTextureSettings(settings);
                if (settings.spriteMeshType == item.Value) continue;
                settings.spriteMeshType = item.Value;
                importer.SetTextureSettings(settings);
                importer.SaveAndReimport();
            }
        }

        private static bool IsCutInPrefab(string assetPath) =>
            (assetPath ?? "").Replace('\\', '/').Contains("/Prefabs/Naninovel/Spawn/CutIn/ObjectionCutIn_");

        /// <summary>
        /// The band's glow (RefuteCutIn_StainedGlass_luminescence, LuminescenceDissolve) and its stained glass share
        /// sorting layer and order 200 in the shipped prefab, so which one draws last is left to the pipeline's
        /// tie-break.  The glow is a backing layer, a little larger than the glass, whose rim frames it (the portrait's
        /// silhouette glow sits the same way, at -100 behind the portrait at 300).  The built-in pipeline draws it last,
        /// which covers the glass with a solid pink-white band for most of the cut-in.  Pin the intended layering: a
        /// dissolve glow that shares its order with a Glasses renderer goes one order below it.
        /// </summary>
        private static void OrderCutInGlowBehindGlass(GameObject root)
        {
            SpriteRenderer[] renderers = root.GetComponentsInChildren<SpriteRenderer>(true);
            var glassOrders = new HashSet<long>(renderers.Where(r => UsesCutInShader(r, "Glasses")).Select(SortKey));
            foreach (SpriteRenderer renderer in renderers)
                if (UsesCutInShader(renderer, "LuminescenceDissolve") && glassOrders.Contains(SortKey(renderer)))
                    renderer.sortingOrder -= 1;
        }

        private static long SortKey(Renderer renderer) => ((long)renderer.sortingLayerID << 32) | (uint)renderer.sortingOrder;

        private static bool UsesCutInShader(Renderer renderer, string shader) =>
            renderer.sharedMaterial != null && renderer.sharedMaterial.shader != null &&
            renderer.sharedMaterial.shader.name == CutInMaterialShaderRoot + shader;

        private static bool IsCutInGlowRenderer(JObject planJob, UnityEngine.Object component) =>
            IsCutInPrefab((string)planJob["asset_path"]) && component is SpriteRenderer renderer && UsesCutInShader(renderer, "LuminescenceDissolve");

        private static bool IsCourtPrefab(string assetPath)
        {
            string path = (assetPath ?? "").Replace('\\', '/');
            return path.EndsWith("/Prefabs/General/PerspectiveStage/Court.prefab", StringComparison.Ordinal) ||
                   path.EndsWith("/Prefabs/General/PerspectiveStage/Court_Final.prefab", StringComparison.Ordinal);
        }

        private static HashSet<string> CourtPointerKeys(Node node, JobRecord job)
        {
            if (!IsCourtPrefab(job.asset_path)) return null;
            if (!(node.component is MeshFilter) && !(node.component is MeshCollider) && !(node.component is Renderer))
                return null;
            var ignored = new HashSet<string>(BuilderOwnedKeys);
            if (node.component is MeshFilter || node.component is MeshCollider) ignored.Add("m_Mesh");
            if (node.component is Renderer) ignored.Add("m_Materials");
            return ignored;
        }

        /// <summary>
        /// The generic planner deliberately leaves embedded Court Mesh/Material PPtrs out of its reference table.
        /// Resolve those pointers from the original JSON while the generated prefab hierarchy is still live.
        /// Every pointer is checked: a new path ID must be added to this recipe rather than silently becoming null.
        /// </summary>
        private static void ApplyCourtBindings(string repoRoot, List<Node> nodes, JobRecord job)
        {
            // The index and ledger are parsed once per prefab and then released: a static cache would outlive
            // a pull that changes res in an Editor that stays open.
            try { ApplyCourtBindingsCore(repoRoot, nodes, job); }
            finally { ClearCourtCaches(); }
        }

        private static void ApplyCourtBindingsCore(string repoRoot, List<Node> nodes, JobRecord job)
        {
            var meshes = new Dictionary<long, Mesh>();
            var materials = new Dictionary<long, Material>();
            int meshBindings = 0;
            int materialBindings = 0;
            foreach (Node node in nodes)
            {
                MeshFilter meshFilter = node.component as MeshFilter;
                MeshCollider meshCollider = node.component as MeshCollider;
                if (meshFilter != null || meshCollider != null)
                {
                    JToken pointer = node.json["m_Mesh"];
                    if (pointer != null)
                    {
                        long fileId = pointer.Value<long?>("m_FileID") ?? 0L;
                        long pathId = pointer.Value<long?>("m_PathID") ?? 0L;
                        Mesh mesh = ResolveCourtMesh(repoRoot, fileId, pathId, meshes);
                        if (mesh == null) throw new InvalidDataException("Court mesh pointer resolved to null: " + fileId + ":" + pathId);
                        if (meshFilter != null) meshFilter.sharedMesh = mesh;
                        if (meshCollider != null) meshCollider.sharedMesh = mesh;
                        meshBindings++;
                    }
                }

                if (node.component is Renderer renderer && node.json["m_Materials"] is JArray materialPointers)
                {
                    var assigned = new Material[materialPointers.Count];
                    for (int i = 0; i < materialPointers.Count; i++)
                    {
                        JToken pointer = materialPointers[i];
                        long fileId = pointer.Value<long?>("m_FileID") ?? 0L;
                        long pathId = pointer.Value<long?>("m_PathID") ?? 0L;
                        assigned[i] = ResolveCourtMaterial(repoRoot, fileId, pathId, materials);
                        if (assigned[i] == null) throw new InvalidDataException("Court material pointer resolved to null: " + fileId + ":" + pathId);
                    }
                    renderer.sharedMaterials = assigned;
                    materialBindings += assigned.Length;
                }
            }
            if (meshBindings == 0 || materialBindings == 0)
                throw new InvalidOperationException(job.asset_path + ": Court recipe found no Mesh/Material bindings");
            foreach (Node node in nodes)
                if (node.component is Light light) ConvertCourtLightIntensity(light, node.json, job);
            AssetDatabase.SaveAssets();
            job.messages.Add("Court bindings: meshes=" + meshBindings + " materials=" + materialBindings);
        }

        /// <summary>
        /// The original URP light stores a spot/point intensity in candela (m_LightUnit 1: Court's spot is 2000 at
        /// 30 m) and falls off with the inverse square of the distance.  The built-in pipeline reads a unitless
        /// multiplier with a range-based falloff, so the raw 2000 renders every surface pure white.  Use the lux the
        /// original delivered at the stage origin (candela / distance^2).  Rendered in Unity this reads well with the
        /// built-in falloff, but it is a visual approximation, not a unit-exact conversion.
        /// </summary>
        private static void ConvertCourtLightIntensity(Light light, JObject json, JobRecord job)
        {
            if (!IsCandelaLight(light, json)) return;
            float candela = json["m_Intensity"]?.Value<float?>() ?? light.intensity;
            float distance = (light.transform.position - light.transform.root.position).magnitude;
            if (distance < 1f)
                throw new InvalidDataException(job.asset_path + ": Court light is " + distance + " m from the stage origin");
            light.intensity = candela / (distance * distance);
            job.messages.Add("Court light: " + candela + " cd / " + distance + " m^2 = " + light.intensity);
        }

        private static bool IsCandelaLight(Light light, JObject json) =>
            light.type != LightType.Directional && (json["m_LightUnit"]?.Value<int?>() ?? 0) == 1;

        private static bool IsCourtCandelaLight(JObject planJob, UnityEngine.Object component, JObject json) =>
            IsCourtPrefab((string)planJob["asset_path"]) && component is Light light && IsCandelaLight(light, json);

        // The DebateUI timer draws its digits as <sprite name="N"> tags from TrialsClockFont, a TMP_SpriteAsset the
        // importer defers (its MonoScript type has no mapping and its shader is not recoverable).  Without it TMP finds
        // no sprite and prints "?" for every digit.  Rebuild the asset from the dump at the first path
        // MapShippedAssetPath tries, so the label's m_spriteAsset pointer resolves through the normal reference table.
        private const string ClockBundle = "general-fonts-common_assets_all.bundle";
        private const long ClockSpriteAssetPathId = 8008108086768730382L;
        private const long ClockSpriteSheetPathId = -188070706469636662L;
        private const long ClockMaterialPathId = -3180311218604113650L;
        private const string ClockFolder = "Assets/GameContent/#WitchTrials/Fonts/Common";
        private const string ClockSpriteAssetPath = ClockFolder + "/TrialsClockFont.asset";

        // TMP assets the importer defers (their MonoScript type has no mapping) and that prefab labels point at by
        // reference.  Each is rebuilt from its dump when a prefab that references it is built, and such a prefab is
        // rebuilt, not patched, when the recipe changes (a field patch cannot bind a newly generated asset).
        // LogMessageStyleSheet is not listed: LogUI's generated hierarchy has no label that carries it yet.
        private const string DeferredTmpFolder = "Assets/#WitchTrials/Fonts/Common/";
        private const long DebatePrinterStyleSheetPathId = 8401695592146518297L;
        private static readonly string[] DeferredTmpAssets = { "TrialsClockFont.asset", "DebatePrinterStyleSheet.asset" };

        private static IEnumerable<string> DeferredTmpReferences(IEnumerable<string> shippedPaths) =>
            shippedPaths.Select(p => (p ?? "").Replace('\\', '/'))
                .Where(p => p.StartsWith(DeferredTmpFolder, StringComparison.Ordinal) &&
                            DeferredTmpAssets.Contains(p.Substring(DeferredTmpFolder.Length)))
                .Distinct();

        private static bool ReferencesDeferredTmpAsset(JObject planJob) =>
            DeferredTmpReferences((planJob["references"] as JObject)?.Properties().Select(p => (string)p.Value) ??
                                  Enumerable.Empty<string>()).Any();

        private static void EnsureDeferredTmpAssets(string repoRoot, Dictionary<string, string> references, JobRecord job)
        {
            foreach (string shipped in DeferredTmpReferences(references.Values))
            {
                string name = shipped.Substring(DeferredTmpFolder.Length);
                if (name == "TrialsClockFont.asset") EnsureClockSpriteAsset(repoRoot, job);
                else if (name == "DebatePrinterStyleSheet.asset") EnsureStyleSheet(repoRoot, name, DebatePrinterStyleSheetPathId, job);
            }
        }

        private static void EnsureClockSpriteAsset(string repoRoot, JobRecord job)
        {
            try
            {
                JObject json = JObject.Parse(File.ReadAllText(Path.GetFullPath(
                    FindCourtDump(repoRoot, "MonoBehaviour", ClockBundle, ClockSpriteAssetPathId))));
                JObject materialJson = JObject.Parse(File.ReadAllText(Path.GetFullPath(
                    FindCourtDump(repoRoot, "Material", ClockBundle, ClockMaterialPathId))));
                JArray characters = json["m_SpriteCharacterTable"] as JArray;
                JArray glyphs = json["m_GlyphTable"] as JArray;
                if (characters == null || glyphs == null || characters.Count == 0 || characters.Count != glyphs.Count)
                    throw new InvalidDataException("TrialsClockFont: empty or inconsistent sprite tables");
                string atlasPath = FindCourtTextureTarget(repoRoot,
                    new CourtTextureRecipe { bundle = ClockBundle, pathId = ClockSpriteSheetPathId });
                Texture atlas = AssetDatabase.LoadAssetAtPath<Texture>(atlasPath);
                if (atlas == null) throw new InvalidDataException("TrialsClockFont atlas is not imported: " + atlasPath);
                Shader shader = Shader.Find("TextMeshPro/Sprite");
                if (shader == null) throw new InvalidOperationException("TextMeshPro/Sprite shader is unavailable");

                string projectRoot = Directory.GetParent(Application.dataPath).FullName;
                EnsureAssetFolder(projectRoot, ClockFolder);
                TMPro.TMP_SpriteAsset asset = AssetDatabase.LoadAssetAtPath<TMPro.TMP_SpriteAsset>(ClockSpriteAssetPath);
                if (asset == null)
                {
                    if (File.Exists(Path.Combine(projectRoot, ClockSpriteAssetPath.Replace('/', Path.DirectorySeparatorChar))))
                        throw new InvalidDataException("TrialsClockFont asset exists but is not a TMP_SpriteAsset");
                    asset = ScriptableObject.CreateInstance<TMPro.TMP_SpriteAsset>();
                    AssetDatabase.CreateAsset(asset, ClockSpriteAssetPath);
                }
                // Reuse the sub-asset on reruns: adding a new one each time would pile up materials.
                Material material = AssetDatabase.LoadAllAssetsAtPath(ClockSpriteAssetPath).OfType<Material>().FirstOrDefault();
                if (material == null)
                {
                    material = new Material(shader);
                    AssetDatabase.AddObjectToAsset(material, asset);
                }
                material.shader = shader;
                material.name = "TrialsClockFont Material";
                JObject properties = materialJson["m_SavedProperties"] as JObject;
                ApplyCourtColors(material, properties?["m_Colors"] as JArray);
                ApplyCourtFloats(material, properties?["m_Floats"] as JArray);
                material.SetTexture("_MainTex", atlas);
                EditorUtility.SetDirty(material);

                // The glyph `sprite` pointers are editor-only previews; they stay unresolved.  m_Version must be
                // written before the asset is used: TMP rebuilds the tables from the empty legacy list otherwise.
                var options = new JsonToSerializedApplier.Options
                {
                    SkipEngineFields = false,
                    IgnoreKeys = new HashSet<string>(BuilderOwnedKeys) { "m_Enabled" },
                    PreserveUnresolvedPointers = true,
                    ResolvePointer = token =>
                    {
                        long id = token.Value<long?>("m_PathID") ?? 0L;
                        return id == ClockSpriteSheetPathId ? (UnityEngine.Object)atlas
                             : id == ClockMaterialPathId ? material : null;
                    },
                };
                JsonToSerializedApplier.ApplyAll(new SerializedObject(asset), json, options);
                EditorUtility.SetDirty(asset);
                AssetDatabase.SaveAssets();

                // A serialized-field mismatch only warns in the applier, so verify the result itself.
                asset.UpdateLookupTables();
                if (asset.spriteSheet != atlas || asset.material != material ||
                    asset.spriteCharacterTable.Count != characters.Count || asset.spriteGlyphTable.Count != glyphs.Count)
                    throw new InvalidDataException("TrialsClockFont: tables or references were not applied");
                foreach (JToken character in characters)
                {
                    string name = (string)character["m_Name"];
                    if (asset.GetSpriteIndexFromName(name) < 0)
                        throw new InvalidDataException("TrialsClockFont has no sprite named '" + name + "'");
                }
                job.messages.Add("TrialsClockFont sprites=" + characters.Count);
            }
            finally { ClearCourtCaches(); }
        }

        // A TMP_StyleSheet carries the <style="Objection"> definitions the debate text uses; without it the tag is
        // printed literally.
        private static void EnsureStyleSheet(string repoRoot, string name, long pathId, JobRecord job)
        {
            try
            {
                JObject json = JObject.Parse(File.ReadAllText(Path.GetFullPath(
                    FindCourtDump(repoRoot, "MonoBehaviour", ClockBundle, pathId))));
                JArray styles = json["m_StyleList"] as JArray;
                if (styles == null || styles.Count == 0)
                    throw new InvalidDataException(name + ": empty style list");
                string assetPath = ClockFolder + "/" + name;
                string projectRoot = Directory.GetParent(Application.dataPath).FullName;
                EnsureAssetFolder(projectRoot, ClockFolder);
                TMPro.TMP_StyleSheet sheet = AssetDatabase.LoadAssetAtPath<TMPro.TMP_StyleSheet>(assetPath);
                if (sheet == null)
                {
                    if (File.Exists(Path.Combine(projectRoot, assetPath.Replace('/', Path.DirectorySeparatorChar))))
                        throw new InvalidDataException(name + " exists but is not a TMP_StyleSheet");
                    sheet = ScriptableObject.CreateInstance<TMPro.TMP_StyleSheet>();
                    AssetDatabase.CreateAsset(sheet, assetPath);
                }
                var options = new JsonToSerializedApplier.Options
                {
                    SkipEngineFields = false,
                    IgnoreKeys = new HashSet<string>(BuilderOwnedKeys) { "m_Enabled" },
                    PreserveUnresolvedPointers = true,
                    ResolvePointer = token => null,
                };
                JsonToSerializedApplier.ApplyAll(new SerializedObject(sheet), json, options);
                EditorUtility.SetDirty(sheet);
                AssetDatabase.SaveAssets();

                // A serialized-field mismatch only warns in the applier, so verify the result itself.
                sheet.RefreshStyles();
                foreach (JToken style in styles)
                {
                    TMPro.TMP_Style found = sheet.GetStyle((string)style["m_Name"]);
                    if (found == null || found.styleOpeningDefinition != (string)style["m_OpeningDefinition"] ||
                        found.styleClosingDefinition != (string)style["m_ClosingDefinition"])
                        throw new InvalidDataException(name + ": style '" + (string)style["m_Name"] + "' was not applied");
                }
                job.messages.Add(name + " styles=" + styles.Count);
            }
            finally { ClearCourtCaches(); }
        }

        private static void ClearCourtCaches()
        {
            CourtDumpCache.Clear();
            CourtTextureTargetCache.Clear();
            CourtCsvCache.Clear();
        }

        private static Mesh ResolveCourtMesh(string repoRoot, long fileId, long pathId,
                                             Dictionary<long, Mesh> cache)
        {
            if (pathId == 0) return null;
            if (fileId == 2)
            {
                string builtinName;
                switch (pathId)
                {
                    case 10202: builtinName = "Cube.fbx"; break;
                    case 10206: builtinName = "Cylinder.fbx"; break;
                    case 10207: builtinName = "Sphere.fbx"; break;
                    case 10208: builtinName = "Capsule.fbx"; break;
                    case 10209: builtinName = "Plane.fbx"; break;
                    case 10210: builtinName = "Quad.fbx"; break;
                    default: throw new InvalidDataException("Court mesh has unknown Unity builtin path ID " + pathId);
                }
                Mesh builtin = Resources.GetBuiltinResource<Mesh>(builtinName);
                if (builtin == null) throw new InvalidDataException("Unity builtin mesh did not load: " + builtinName);
                return builtin;
            }
            if (fileId != 0)
                throw new InvalidDataException("Court mesh has unsupported file ID " + fileId + ":" + pathId);
            if (pathId != CourtMesh52PathId && pathId != CourtMesh171PathId)
                throw new InvalidDataException("Court mesh path ID is not in the tracked recipe: " + pathId);
            if (cache.TryGetValue(pathId, out Mesh known)) return known;
            Mesh mesh = LoadOrBuildCourtMesh(repoRoot, pathId);
            cache[pathId] = mesh;
            return mesh;
        }

        private static Mesh LoadOrBuildCourtMesh(string repoRoot, long pathId)
        {
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string assetPath = CourtMeshFolder + "/CourtMesh_" + (pathId < 0 ? "N" + (-pathId) : pathId.ToString()) + ".asset";
            string source = FindCourtDump(repoRoot, "Mesh", CourtPrefabBundle, pathId);
            string sourcePath = Path.GetFullPath(source);
            if (!File.Exists(sourcePath)) throw new FileNotFoundException("Court mesh source is missing", sourcePath);
            JObject json = JObject.Parse(File.ReadAllText(sourcePath));
            int vertexCount = json["m_VertexData"]?["m_VertexCount"]?.Value<int>() ?? 0;
            if (vertexCount <= 0) throw new InvalidDataException(source + ": m_VertexData.m_VertexCount is empty");
            JArray channels = json["m_VertexData"]?["m_Channels"] as JArray;
            byte[] vertexBytes = DecodePythonBytes((string)json["m_VertexData"]?["m_DataSize"]);
            if (channels == null || vertexBytes.Length == 0) throw new InvalidDataException(source + ": vertex data is empty");
            int stride = 0;
            foreach (JObject channel in channels.OfType<JObject>())
            {
                int dimension = channel.Value<int?>("dimension") ?? 0;
                int format = channel.Value<int?>("format") ?? 0;
                int offset = channel.Value<int?>("offset") ?? 0;
                if (dimension > 0 && format != 0)
                    throw new InvalidDataException(source + ": compressed vertex channel format " + format + " is unsupported");
                stride = Math.Max(stride, offset + dimension * 4);
            }
            if (stride <= 0 || vertexBytes.Length != vertexCount * stride)
                throw new InvalidDataException(source + ": vertex byte count " + vertexBytes.Length + " does not match " +
                                                vertexCount + "*" + stride);
            var vertices = new Vector3[vertexCount];
            var normals = new Vector3[vertexCount];
            var tangents = new Vector4[vertexCount];
            var uvs = new Vector2[vertexCount];
            int positionOffset = ChannelOffset(channels, 0, 3, source);
            int normalOffset = ChannelOffset(channels, 1, 3, source);
            int tangentOffset = ChannelOffset(channels, 2, 4, source);
            int uvOffset = ChannelOffset(channels, 4, 2, source);
            for (int i = 0; i < vertexCount; i++)
            {
                int at = i * stride;
                vertices[i] = new Vector3(ReadFloat(vertexBytes, at + positionOffset),
                                          ReadFloat(vertexBytes, at + positionOffset + 4),
                                          ReadFloat(vertexBytes, at + positionOffset + 8));
                normals[i] = new Vector3(ReadFloat(vertexBytes, at + normalOffset),
                                         ReadFloat(vertexBytes, at + normalOffset + 4),
                                         ReadFloat(vertexBytes, at + normalOffset + 8));
                tangents[i] = new Vector4(ReadFloat(vertexBytes, at + tangentOffset),
                                          ReadFloat(vertexBytes, at + tangentOffset + 4),
                                          ReadFloat(vertexBytes, at + tangentOffset + 8),
                                          ReadFloat(vertexBytes, at + tangentOffset + 12));
                uvs[i] = new Vector2(ReadFloat(vertexBytes, at + uvOffset),
                                     ReadFloat(vertexBytes, at + uvOffset + 4));
            }

            JArray indexBytes = json["m_IndexBuffer"] as JArray;
            if (indexBytes == null || indexBytes.Count % 2 != 0)
                throw new InvalidDataException(source + ": invalid m_IndexBuffer");
            var indices = new int[indexBytes.Count / 2];
            for (int i = 0; i < indices.Length; i++)
                indices[i] = (indexBytes[i * 2].Value<int>() & 0xff) | ((indexBytes[i * 2 + 1].Value<int>() & 0xff) << 8);
            JArray subMeshes = json["m_SubMeshes"] as JArray;
            if (subMeshes == null || subMeshes.Count == 0)
                throw new InvalidDataException(source + ": no submeshes");
            int expectedIndices = subMeshes.Sum(s => s.Value<int>("indexCount"));
            if (expectedIndices != indices.Length)
                throw new InvalidDataException(source + ": submesh index count " + expectedIndices + " != " + indices.Length);

            EnsureAssetFolder(projectRoot, CourtMeshFolder);
            Mesh mesh = AssetDatabase.LoadAssetAtPath<Mesh>(assetPath);
            if (mesh == null)
            {
                if (File.Exists(Path.Combine(projectRoot, assetPath.Replace('/', Path.DirectorySeparatorChar))))
                    throw new InvalidDataException("Court mesh asset exists but is not a Mesh: " + assetPath);
                mesh = new Mesh();
                AssetDatabase.CreateAsset(mesh, assetPath);
            }
            // Keep the object name equal to the file name so a reimport does not rename the asset.
            mesh.name = Path.GetFileNameWithoutExtension(assetPath);
            mesh.Clear();
            mesh.indexFormat = IndexFormat.UInt16;
            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.tangents = tangents;
            mesh.uv = uvs;
            int indexOffset = 0;
            for (int sub = 0; sub < subMeshes.Count; sub++)
            {
                int count = subMeshes[sub].Value<int>("indexCount");
                var part = new int[count];
                Array.Copy(indices, indexOffset, part, 0, count);
                mesh.SetIndices(part, MeshTopology.Triangles, sub, false, subMeshes[sub].Value<int?>("baseVertex") ?? 0);
                indexOffset += count;
            }
            mesh.RecalculateBounds();
            JObject aabb = json["m_LocalAABB"] as JObject;
            if (aabb != null)
            {
                Vector3 expectedCenter = ReadVector3(aabb["m_Center"]);
                Vector3 expectedExtent = ReadVector3(aabb["m_Extent"]);
                Bounds bounds = mesh.bounds;
                if ((bounds.center - expectedCenter).sqrMagnitude > 0.00001f ||
                    (bounds.extents - expectedExtent).sqrMagnitude > 0.00001f)
                    throw new InvalidDataException(source + ": decoded bounds " + bounds + " differ from m_LocalAABB");
            }
            EditorUtility.SetDirty(mesh);
            return mesh;
        }

        private static int ChannelOffset(JArray channels, int index, int dimension, string source)
        {
            JObject channel = channels.Count > index ? channels[index] as JObject : null;
            if (channel == null || (channel.Value<int?>("dimension") ?? 0) != dimension)
                throw new InvalidDataException(source + ": missing vertex channel " + index);
            return channel.Value<int?>("offset") ?? 0;
        }

        private static float ReadFloat(byte[] data, int offset) => BitConverter.ToSingle(data, offset);

        private static Vector3 ReadVector3(JToken token) =>
            new Vector3(token?.Value<float?>("x") ?? 0f, token?.Value<float?>("y") ?? 0f, token?.Value<float?>("z") ?? 0f);

        private static byte[] DecodePythonBytes(string text)
        {
            if (string.IsNullOrEmpty(text) || text.Length < 3 || text[0] != 'b' || text[1] != '\'' || text[text.Length - 1] != '\'')
                throw new InvalidDataException("vertex m_DataSize is not a Python bytes literal");
            var bytes = new List<byte>();
            for (int i = 2; i < text.Length - 1; i++)
            {
                char c = text[i];
                if (c != '\\') { bytes.Add((byte)c); continue; }
                if (++i >= text.Length - 1) throw new InvalidDataException("truncated Python bytes escape");
                char escape = text[i];
                if (escape == 'x')
                {
                    if (i + 2 >= text.Length - 1) throw new InvalidDataException("truncated Python hex escape");
                    bytes.Add(Convert.ToByte(text.Substring(i + 1, 2), 16));
                    i += 2;
                }
                else
                {
                    switch (escape)
                    {
                        case 'n': bytes.Add(10); break;
                        case 'r': bytes.Add(13); break;
                        case 't': bytes.Add(9); break;
                        case 'b': bytes.Add(8); break;
                        case 'f': bytes.Add(12); break;
                        case 'v': bytes.Add(11); break;
                        case '\\': bytes.Add((byte)'\\'); break;
                        case '\'': bytes.Add((byte)'\''); break;
                        default: bytes.Add((byte)escape); break;
                    }
                }
            }
            return bytes.ToArray();
        }

        private static Material ResolveCourtMaterial(string repoRoot, long fileId, long pathId,
                                                     Dictionary<long, Material> cache)
        {
            if (pathId == 0) return null;
            if (fileId != 0 || !CourtMaterials.TryGetValue(pathId, out CourtMaterialRecipe recipe))
                throw new InvalidDataException("Court material pointer is not in the tracked recipe: " + fileId + ":" + pathId);
            if (cache.TryGetValue(pathId, out Material known)) return known;
            string sourcePath = Path.GetFullPath(FindCourtDump(repoRoot, "Material", recipe.bundle, recipe.pathId));
            if (!File.Exists(sourcePath)) throw new FileNotFoundException("Court material source is missing", sourcePath);
            JObject source = JObject.Parse(File.ReadAllText(sourcePath));
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string assetName = SanitizeAssetName(recipe.name);
            string assetPath = CourtMaterialFolder + "/" + assetName + ".mat";
            EnsureAssetFolder(projectRoot, CourtMaterialFolder);
            // Fire is an HDRP-style graph with no built-in equivalent.  Its flame mask on a built-in additive
            // particle shader keeps the 12 play-on-awake flame quads from rendering as opaque white squares.
            bool fire = recipe.name == "Fire";
            Shader shader = (fire ? Shader.Find("Legacy Shaders/Particles/Additive") : null) ??
                            Shader.Find("Standard") ?? Shader.Find("Unlit/Texture");
            if (shader == null) throw new InvalidOperationException("Court material fallback shader is unavailable");
            Material material = AssetDatabase.LoadAssetAtPath<Material>(assetPath);
            if (material == null)
            {
                if (File.Exists(Path.Combine(projectRoot, assetPath.Replace('/', Path.DirectorySeparatorChar))))
                    throw new InvalidDataException("Court material asset exists but is not a Material: " + assetPath);
                material = new Material(shader);
                AssetDatabase.CreateAsset(material, assetPath);
            }
            material.shader = shader;
            // Keep the object name equal to the file name so a reimport does not rename the asset.
            material.name = assetName;
            JObject properties = source["m_SavedProperties"] as JObject;
            ApplyCourtTextures(repoRoot, material, properties?["m_TexEnvs"] as JArray);
            ApplyCourtColors(material, properties?["m_Colors"] as JArray);
            ApplyCourtFloats(material, properties?["m_Floats"] as JArray);
            if (fire && material.HasProperty("_TintColor")) material.SetColor("_TintColor", new Color(1f, 0.55f, 0.2f, 0.6f));
            JArray keywords = source["m_ValidKeywords"] as JArray;
            if (keywords != null)
                foreach (string keyword in keywords.Values<string>())
                    // URP spells the metallic/gloss-map keyword differently from the Standard shader.
                    if (!string.IsNullOrEmpty(keyword))
                        material.EnableKeyword(keyword == "_METALLICSPECGLOSSMAP" ? "_METALLICGLOSSMAP" : keyword);
            bool alphaClip = keywords != null && keywords.Values<string>().Contains("_ALPHATEST_ON");
            bool transparent = keywords != null && keywords.Values<string>().Contains("_SURFACE_TYPE_TRANSPARENT");
            if ((FindCourtFloat(properties?["m_Floats"] as JArray, "_Surface") ?? 0f) > 0.5f) transparent = true;
            if (alphaClip)
            {
                material.EnableKeyword("_ALPHATEST_ON");
                SetMaterialFloat(material, "_Mode", 1f);
                SetMaterialFloat(material, "_Cutoff", FindCourtFloat(properties?["m_Floats"] as JArray, "_Cutoff") ?? 0.5f);
            }
            if (transparent)
            {
                material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                SetMaterialFloat(material, "_Mode", 3f);
                SetMaterialFloat(material, "_SrcBlend", 1f);
                SetMaterialFloat(material, "_DstBlend", 10f);
                SetMaterialFloat(material, "_ZWrite", 0f);
            }
            int renderQueue = source.Value<int?>("m_CustomRenderQueue") ?? -1;
            material.renderQueue = renderQueue;
            EditorUtility.SetDirty(material);
            cache[pathId] = material;
            return material;
        }

        private static void ApplyCourtTextures(string repoRoot, Material material, JArray entries)
        {
            if (entries == null) return;
            foreach (JArray pair in entries.OfType<JArray>())
            {
                string property = (string)pair[0];
                JObject value = pair.Count > 1 ? pair[1] as JObject : null;
                JToken texturePointer = value?["m_Texture"];
                long pathId = texturePointer?.Value<long?>("m_PathID") ?? 0L;
                string target = CourtTextureProperty(material, property);
                if (pathId != 0)
                {
                    if (!CourtTextures.TryGetValue(pathId, out CourtTextureRecipe textureRecipe))
                        throw new InvalidDataException("Court material texture path ID is not in the tracked recipe: " + pathId);
                    string generated = FindCourtTextureTarget(repoRoot, textureRecipe);
                    Texture texture = generated == null ? null : AssetDatabase.LoadAssetAtPath<Texture>(generated);
                    if (texture == null) throw new InvalidDataException("Court material texture is missing for " +
                                                                         textureRecipe.bundle + ":" + pathId);
                    if (!string.IsNullOrEmpty(target)) material.SetTexture(target, texture);
                }
                if (value != null && !string.IsNullOrEmpty(target) && material.HasProperty(target))
                {
                    JObject scale = value["m_Scale"] as JObject;
                    JObject offset = value["m_Offset"] as JObject;
                    if (scale != null) material.SetTextureScale(target, new Vector2(scale.Value<float?>("x") ?? 1f, scale.Value<float?>("y") ?? 1f));
                    if (offset != null) material.SetTextureOffset(target, new Vector2(offset.Value<float?>("x") ?? 0f, offset.Value<float?>("y") ?? 0f));
                }
            }
        }

        private static string CourtTextureProperty(Material material, string property)
        {
            if (material.HasProperty(property)) return property;
            if (property == "_BaseMap" && material.HasProperty("_MainTex")) return "_MainTex";
            // The flame silhouette is the only Fire texture the built-in particle shader can use.
            if (property == "_Mask_Texture" && material.HasProperty("_MainTex")) return "_MainTex";
            return null;
        }

        private static void ApplyCourtColors(Material material, JArray entries)
        {
            if (entries == null) return;
            foreach (JArray pair in entries.OfType<JArray>())
            {
                string property = (string)pair[0];
                JObject color = pair.Count > 1 ? pair[1] as JObject : null;
                if (color == null) continue;
                Color value = new Color(color.Value<float?>("r") ?? 0f, color.Value<float?>("g") ?? 0f,
                                        color.Value<float?>("b") ?? 0f, color.Value<float?>("a") ?? 1f);
                if (material.HasProperty(property)) material.SetColor(property, value);
                if (property == "_BaseColor" && material.HasProperty("_Color")) material.SetColor("_Color", value);
            }
        }

        private static void ApplyCourtFloats(Material material, JArray entries)
        {
            if (entries == null) return;
            foreach (JArray pair in entries.OfType<JArray>())
            {
                string property = (string)pair[0];
                float value = pair.Count > 1 ? pair[1].Value<float>() : 0f;
                // URP stores smoothness as _Smoothness; Standard reads _Glossiness, or _GlossMapScale with a
                // metallic/gloss map.  The json also carries stale Standard values for both, so URP's wins.
                if (property == "_Glossiness" || property == "_GlossMapScale") continue;
                if (property == "_Smoothness")
                {
                    SetMaterialFloat(material, "_Glossiness", value);
                    SetMaterialFloat(material, "_GlossMapScale", value);
                    continue;
                }
                SetMaterialFloat(material, property, value);
            }
        }

        private static float? FindCourtFloat(JArray entries, string property)
        {
            if (entries == null) return null;
            foreach (JArray pair in entries.OfType<JArray>())
                if ((string)pair[0] == property) return pair.Count > 1 ? pair[1].Value<float>() : 0f;
            return null;
        }

        private static void SetMaterialFloat(Material material, string property, float value)
        {
            if (material.HasProperty(property)) material.SetFloat(property, value);
        }

        // The extraction may assign a different ~N filename in another readable copy.  The index's
        // (type, bundle basename, path ID) identity is stable, while output_file is only a local filename.
        private static readonly Dictionary<string, string> CourtDumpCache =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private static readonly Dictionary<string, string> CourtTextureTargetCache =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private static string FindCourtDump(string repoRoot, string type, string bundle, long pathId)
        {
            string key = type + "|" + bundle.ToLowerInvariant() + "|" + pathId;
            if (CourtDumpCache.TryGetValue(key, out string known)) return known;
            string indexPath = Path.Combine(repoRoot, "res", "_index.csv");
            if (!File.Exists(indexPath)) throw new FileNotFoundException("resource index is missing", indexPath);
            string wantedBundle = Path.GetFileName((bundle ?? "").Replace('\\', '/')).ToLowerInvariant();
            string found = null;
            foreach (Dictionary<string, string> row in ReadCourtCsv(indexPath))
            {
                if (!string.Equals(row.TryGetValue("type", out string actualType) ? actualType : "", type,
                                   StringComparison.Ordinal) ||
                    !string.Equals(Path.GetFileName((row.TryGetValue("source_bundle", out string actualBundle)
                                                     ? actualBundle : "").Replace('\\', '/')).ToLowerInvariant(),
                                   wantedBundle, StringComparison.Ordinal) ||
                    !long.TryParse(row.TryGetValue("path_id", out string actualId) ? actualId : "", out long parsedId) ||
                    parsedId != pathId) continue;
                string output = row.TryGetValue("output_file", out string value) ? value.Replace('\\', '/') : "";
                if (string.IsNullOrEmpty(output)) continue;
                string[] candidates = { Path.Combine(repoRoot, "res", output),
                                        Path.Combine(repoRoot, "res", "_serialized", output) };
                foreach (string candidate in candidates)
                {
                    string full = Path.GetFullPath(candidate);
                    string resRoot = Path.GetFullPath(Path.Combine(repoRoot, "res")) + Path.DirectorySeparatorChar;
                    if (full.StartsWith(resRoot, StringComparison.OrdinalIgnoreCase) && File.Exists(full))
                    {
                        if (found != null && !string.Equals(found, full, StringComparison.OrdinalIgnoreCase))
                            throw new InvalidDataException("resource identity is ambiguous: " + key);
                        found = full;
                    }
                }
            }
            if (found == null) throw new FileNotFoundException("resource identity is missing: " + key, indexPath);
            CourtDumpCache[key] = found;
            return found;
        }

        private static string FindCourtTextureTarget(string repoRoot, CourtTextureRecipe recipe)
        {
            string key = recipe.bundle.ToLowerInvariant() + "|" + recipe.pathId;
            if (CourtTextureTargetCache.TryGetValue(key, out string known)) return known;
            // Verify the source identity in the local extraction before trusting the imported target ledger.
            // The ledger records where this copy was written; _index.csv proves which shipped object it was.
            FindCourtDump(repoRoot, "Texture2D", recipe.bundle, recipe.pathId);
            string ledgerPath = Path.Combine(repoRoot, "res", ".manosaba", "reconstruction", "unity",
                                              "a13_import_ledger.csv");
            if (!File.Exists(ledgerPath)) throw new FileNotFoundException("content import ledger is missing", ledgerPath);
            string wantedBundle = Path.GetFileName(recipe.bundle.Replace('\\', '/')).ToLowerInvariant();
            string target = null;
            foreach (Dictionary<string, string> row in ReadCourtCsv(ledgerPath))
            {
                string type = row.TryGetValue("type", out string actualType) ? actualType : "";
                string status = row.TryGetValue("status", out string actualStatus) ? actualStatus : "";
                string sourceBundle = row.TryGetValue("bundle", out string actualBundle) ? actualBundle : "";
                string path = row.TryGetValue("path_id", out string actualId) ? actualId : "";
                if (type != "Texture2D" || status != "IMPORTED" ||
                    Path.GetFileName(sourceBundle.Replace('\\', '/')).ToLowerInvariant() != wantedBundle ||
                    path != recipe.pathId.ToString(CultureInfo.InvariantCulture)) continue;
                string candidate = NormalizeCourtAssetTarget(repoRoot,
                    row.TryGetValue("target_path", out string value) ? value : "");
                if (candidate == null) continue;
                if (target != null && !string.Equals(target, candidate, StringComparison.Ordinal))
                    throw new InvalidDataException("texture identity is ambiguous: " + key);
                target = candidate;
            }
            if (target == null) throw new FileNotFoundException("imported texture identity is missing: " + key, ledgerPath);
            CourtTextureTargetCache[key] = target;
            return target;
        }

        private static string NormalizeCourtAssetTarget(string repoRoot, string target)
        {
            string value = (target ?? "").Replace('\\', '/');
            int marker = value.IndexOf("/UnityProject/Assets/", StringComparison.OrdinalIgnoreCase);
            if (marker >= 0) value = "Assets/" + value.Substring(marker + "/UnityProject/Assets/".Length);
            else if (value.StartsWith("UnityProject/", StringComparison.OrdinalIgnoreCase)) value = value.Substring("UnityProject/".Length);
            if (!value.StartsWith("Assets/", StringComparison.Ordinal)) return null;
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string full = Path.GetFullPath(Path.Combine(projectRoot, value.Replace('/', Path.DirectorySeparatorChar)));
            string allowed = Path.GetFullPath(Path.Combine(projectRoot, "Assets/GameContent")) + Path.DirectorySeparatorChar;
            if (!full.StartsWith(allowed, StringComparison.OrdinalIgnoreCase) || !File.Exists(full)) return null;
            return value;
        }

        private static readonly Dictionary<string, List<Dictionary<string, string>>> CourtCsvCache =
            new Dictionary<string, List<Dictionary<string, string>>>(StringComparer.OrdinalIgnoreCase);

        private static List<Dictionary<string, string>> ReadCourtCsv(string path)
        {
            string key = Path.GetFullPath(path);
            if (CourtCsvCache.TryGetValue(key, out List<Dictionary<string, string>> cached)) return cached;
            List<Dictionary<string, string>> rows = ParseCourtCsv(path);
            CourtCsvCache[key] = rows;
            return rows;
        }

        private static List<Dictionary<string, string>> ParseCourtCsv(string path)
        {
            string text = File.ReadAllText(path, Encoding.UTF8);
            if (text.Length > 0 && text[0] == '\ufeff') text = text.Substring(1);
            var records = new List<List<string>>();
            var record = new List<string>();
            var field = new StringBuilder();
            bool quoted = false;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (quoted)
                {
                    if (c == '"')
                    {
                        if (i + 1 < text.Length && text[i + 1] == '"') { field.Append('"'); i++; }
                        else quoted = false;
                    }
                    else field.Append(c);
                }
                else if (c == '"') quoted = true;
                else if (c == ',') { record.Add(field.ToString()); field.Clear(); }
                else if (c == '\n' || c == '\r')
                {
                    if (c == '\r' && i + 1 < text.Length && text[i + 1] == '\n') i++;
                    record.Add(field.ToString()); field.Clear();
                    if (record.Count > 1 || record[0].Length > 0) records.Add(record);
                    record = new List<string>();
                }
                else field.Append(c);
            }
            if (field.Length > 0 || record.Count > 0) { record.Add(field.ToString()); records.Add(record); }
            if (records.Count == 0) throw new InvalidDataException("empty CSV: " + path);
            var rows = new List<Dictionary<string, string>>();
            foreach (List<string> values in records.Skip(1))
            {
                var row = new Dictionary<string, string>(StringComparer.Ordinal);
                for (int i = 0; i < records[0].Count; i++) row[records[0][i]] = i < values.Count ? values[i] : "";
                rows.Add(row);
            }
            return rows;
        }

        private static string SanitizeAssetName(string value)
        {
            var result = new StringBuilder();
            foreach (char c in value ?? "CourtMaterial")
                result.Append(char.IsLetterOrDigit(c) || c == '_' || c == '-' ? c : '_');
            return result.ToString();
        }

        private static void EnsureAssetFolder(string projectRoot, string assetFolder)
        {
            Directory.CreateDirectory(Path.Combine(projectRoot, assetFolder.Replace('/', Path.DirectorySeparatorChar)));
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

        private static void RebuildPackedAnimationClip(Node node, AnimationClip clip, List<Node> nodes, JobRecord job,
                                                       HashSet<string> permittedPaths = null)
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
            if (permittedPaths != null)
            {
                // A source clip can contain stale curves aimed at a sibling of its
                // actual Animator. Unity ignores these; a whole-prefab path lookup
                // must not turn them into effective curves in the rebuilt clip.
                pathByHash.Clear();
                foreach (string path in permittedPaths)
                {
                    uint hash = Crc32(path);
                    if (pathByHash.TryGetValue(hash, out string prior) && prior != path)
                        throw new InvalidOperationException("Ambiguous story animation path CRC");
                    pathByHash[hash] = path;
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
                    if (permittedPaths != null)
                        job.messages.Add(node.pid + ": source_ignored_binding pathHash=" + pathHash + " outside actual Animator hierarchy");
                    else
                    {
                        job.warnings.Add(node.pid + ".m_ClipBindingConstant.genericBindings: path hash " + pathHash + " not found in prefab hierarchy");
                        job.warning_count++;
                    }
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
            if (typeId == 1 && attribute == 2086281974u && component == 0)
            {
                // Unity's discrete binding is for int/enum properties, not this bool.
                // The source has isIntCurve=0; its existing stepped keys supply the
                // discontinuities. DiscreteCurve left all five W glass states unchanged
                // in the real SampleAnimation A/B, while FloatCurve applied them.
                binding = EditorCurveBinding.FloatCurve(path, typeof(GameObject), "m_IsActive");
                return true;
            }
            if (typeId == 4)
            {
                string property = attribute == 1 ? "m_LocalPosition" :
                                  attribute == 3 ? "m_LocalScale" :
                                  attribute == 4 ? "localEulerAnglesRaw" : null;
                if (property == null) return false;
                binding = EditorCurveBinding.FloatCurve(path, typeof(Transform), property + suffix);
                return true;
            }
            if (typeId == 212 && component == 0)
            {
                // CRC32 of the exact serialized SpriteRenderer channel names. ED3 animates RGB
                // as well as alpha; treating RGB hashes as material properties loses its light cycle.
                string channel = attribute == 2526845255u ? "r" : attribute == 4215373228u ? "g" :
                                 attribute == 2334886179u ? "b" : attribute == 304273561u ? "a" : null;
                if (channel != null)
                {
                    binding = EditorCurveBinding.FloatCurve(path, typeof(SpriteRenderer), "m_Color." + channel);
                    return true;
                }
            }
            // A float property of the renderer's material (binding custom type 22).  Its hash is the property
            // name's CRC32 with the top nibble replaced by 8: the cut-in Timelines drive the Shader Graph
            // properties _Float, _Tick, _Fader and _Fader2 this way.  Only the names listed below are known.
            if (typeId == 212 && component == 0 &&
                AnimatedMaterialProperties.Value.TryGetValue(attribute, out string materialProperty))
            {
                binding = EditorCurveBinding.FloatCurve(path, typeof(SpriteRenderer), "material." + materialProperty);
                return true;
            }
            return false;
        }

        private static readonly Lazy<Dictionary<uint, string>> AnimatedMaterialProperties = new Lazy<Dictionary<uint, string>>(() =>
        {
            var map = new Dictionary<uint, string>();
            foreach (string name in new[] { "_Float", "_Tick", "_Fader", "_Fader2", "_Speed", "_EdgeSize", "_FogAmount", "_transparent" })
                map[(Crc32(name) & 0x0FFFFFFFu) | 0x80000000u] = name;
            // Packed customType 22 encodes Color channels in nibbles 4/5/6/7,
            // retaining the lower 28 bits of the property's CRC (not CRC of ".r").
            // Verified on Switch ObjectionCutIn_w's four animated _Color channels.
            string[] channels = { ".r", ".g", ".b", ".a" };
            for (uint i = 0; i < 4; i++)
                map[(Crc32("_Color") & 0x0FFFFFFFu) | ((4u + i) << 28)] = "_Color" + channels[i];
            return map;
        });

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
                    // The cut-ins' materials are Shader Graphs the shipped build only carries as compiled programs.
                    Material cutInMaterial = ResolveCutInMaterial(repoRoot, job, pathId);
                    if (cutInMaterial != null)
                    {
                        job.pointers_sidecar++;
                        return cutInMaterial;
                    }
                    // The ObjectionCutIn Timelines' glass sounds are in no ledger row; content.py extracts them by
                    // identity (ensure_cutin_audio) and they are bound here by the same path ids.
                    if (CutInAudio.TryGetValue(pathId, out string cutInAudioPath) && IsCutInPrefab(job.asset_path))
                    {
                        AudioClip cutInClip = AssetDatabase.LoadAssetAtPath<AudioClip>(cutInAudioPath);
                        if (cutInClip != null) job.pointers_sidecar++;
                        else
                        {
                            job.unresolved_asset_missing++;
                            if (job.missing_asset_paths.Count < 20 && !job.missing_asset_paths.Contains(cutInAudioPath))
                                job.missing_asset_paths.Add(cutInAudioPath);
                        }
                        return cutInClip;
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
