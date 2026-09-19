// ScriptableObject converter (G2 composite half).
//
//   Unity -batchmode -quit -projectPath UnityProject \
//         -executeMethod Manosaba.EditorTools.ScriptableObjectConverter.Run -logFile -
//
// Reads tools/unity-rebuild/out/unity/so_convert_plan.json (produced by
// tools/unity-rebuild/a13c_plan_scriptableobjects.py) and rebuilds each asset:
//
//   1. resolve the reconstructed C# type from the plan (type + assembly name)
//   2. load the existing asset of that type at the planned path (keeps its .meta GUID), else
//      ScriptableObject.CreateInstance(type)
//   3. copy the shipped field values from the typetree JSON onto a SerializedObject
//   4. AssetDatabase.CreateAsset when the asset is new (an existing one is only SetDirty)
//   5. reconcile: `hasValue: true` leaves written vs expected from the dump, and the GUID before/after
//
// Field values come from the dump's *serialised* names, which are exactly the names
// SerializedProperty.FindProperty uses, so the mapping is name-for-name; engine fields (m_*) are
// skipped, and every value the applier cannot place is reported instead of being dropped:
//
//   * PPtr nodes that are not null are counted as `needs_binding` (phase 2: resolve through
//     out/unity/script_binding.json style tables and assign object references)
//   * unknown fields, unsupported property types and out-of-range enum indices are warnings
//
// Exit codes: 0 = every job converted, 1 = some job failed, 2 = BLOCKED (no plan / no jobs).
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

namespace Manosaba.EditorTools
{
    public static class ScriptableObjectConverter
    {
        private const string PlanDirectoryRelative = "../tools/unity-rebuild/out/unity";
        private const string PlanPatternEnv = "MANOSABA_SO_PLAN";
        private const string DefaultPlanPattern = "so_convert_plan_*.json";
        private const string OutputEnv = "MANOSABA_SO_OUT";
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/so_convert_result.json";

        [MenuItem("Manosaba/Convert ScriptableObjects (from plan)")]
        public static void Run()
        {
            int code = Execute();
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        public static int Execute()
        {
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string planDir = Path.GetFullPath(Path.Combine(projectRoot, PlanDirectoryRelative));
            string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));
            // A probe run (one-job plan) must not overwrite the authoritative full-run result.
            string outOverride = Environment.GetEnvironmentVariable(OutputEnv);
            if (!string.IsNullOrEmpty(outOverride)) outPath = Path.GetFullPath(outOverride);
            string pattern = Environment.GetEnvironmentVariable(PlanPatternEnv);
            if (string.IsNullOrEmpty(pattern)) pattern = DefaultPlanPattern;

            var result = new ConvertResult { generated = DateTime.UtcNow.ToString("o"), plan_dir = planDir, plan_pattern = pattern };
            if (!Directory.Exists(planDir))
            {
                result.status = "BLOCKED_NO_PLAN_DIR";
                result.error = "missing " + planDir;
                Write(outPath, result);
                Debug.LogError("[SOConverter] " + result.error);
                return 2;
            }

            string[] planFiles = Directory.GetFiles(planDir, pattern);
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
            result.plan_files = planFiles.Select(Path.GetFileName).OrderBy(n => n, StringComparer.Ordinal).ToList();
            result.jobs_total = jobs.Count;
            if (jobs.Count == 0)
            {
                result.status = "BLOCKED_NO_JOBS";
                result.error = result.error ?? ("no jobs matched " + pattern + " in " + planDir);
                Write(outPath, result);
                Debug.LogError("[SOConverter] " + result.error);
                return 2;
            }

            foreach (JToken jobToken in jobs)
            {
                var job = new JobRecord
                {
                    asset_path = (string)jobToken["asset_path"],
                    target = (string)jobToken["target"],
                    type_name = (string)jobToken["type"],
                    assembly = (string)jobToken["assembly"],
                    dump = (string)jobToken["dump"],
                    plan_file = (string)jobToken["plan_file"],
                };
                try
                {
                    ConvertOne(projectRoot, job);
                }
                catch (Exception ex)
                {
                    job.status = "ERROR";
                    job.error = ex.GetType().Name + ": " + ex.Message;
                }
                if (job.status == "CONVERTED")
                {
                    // A [SerializeReference] field holds a polymorphic object graph; transferring it
                    // needs the concrete type of every element.  An asset whose managed references or
                    // assigned-parameter count do not reconcile with the dump exists but is not
                    // content-complete, and is reported as CONVERTED_PARTIAL rather than quietly counted
                    // as converted.
                    if (job.managed_references_failed > 0 || job.params_with_value_written != job.params_with_value_dump)
                    {
                        job.status = "CONVERTED_PARTIAL";
                        result.converted_partial++;
                    }
                    else
                    {
                        result.converted++;
                    }
                }
                else
                {
                    result.failed++;
                }
                result.jobs.Add(job);
                result.params_with_value_dump += job.params_with_value_dump;
                result.params_with_value_written += job.params_with_value_written;
            }

            AssetDatabase.SaveAssets();
            result.status = result.failed == 0 ? (result.converted_partial == 0 ? "OK" : "OK_WITH_PARTIAL_ASSETS") : "PARTIAL";
            Write(outPath, result);
            Debug.Log($"[SOConverter] {result.status}: converted={result.converted} partial={result.converted_partial} failed={result.failed} " +
                      $"of {result.jobs_total}; parameters written {result.params_with_value_written}/{result.params_with_value_dump}; wrote {outPath}");
            return result.failed == 0 ? 0 : 1;
        }

        private static void ConvertOne(string projectRoot, JobRecord job)
        {
            // Plan paths (dump) are repo-relative: the Unity project is a subdirectory of the repo.
            string repoRoot = Directory.GetParent(projectRoot).FullName;
            string dumpPath = Path.GetFullPath(Path.Combine(repoRoot, job.dump));
            if (!File.Exists(dumpPath))
            {
                job.status = "MISSING_DUMP";
                job.error = "not found: " + dumpPath;
                return;
            }

            string assetPath = job.target.Replace('\\', '/');
            int assetsIndex = assetPath.IndexOf("/Assets/", StringComparison.Ordinal);
            if (assetsIndex >= 0) assetPath = assetPath.Substring(assetsIndex + 1);
            if (!assetPath.StartsWith("Assets/", StringComparison.Ordinal))
            {
                job.status = "BAD_TARGET";
                job.error = "target is not under Assets/: " + job.target;
                return;
            }
            job.asset_path = assetPath;

            Type type = JsonToSerializedApplier.ResolveType(job.type_name, job.assembly);
            if (type == null)
            {
                job.status = "TYPE_NOT_FOUND";
                return;
            }
            if (!typeof(ScriptableObject).IsAssignableFrom(type))
            {
                job.status = "NOT_A_SCRIPTABLEOBJECT";
                return;
            }

            Directory.CreateDirectory(Path.GetDirectoryName(Path.GetFullPath(assetPath)));

            // Update an existing asset IN PLACE instead of DeleteAsset + CreateAsset.  The delete/create pair
            // mints a new .meta GUID (nothing measured in this repo shows Unity reusing it for a ScriptableObject),
            // which would orphan the 440 m_GUID entries of the Manosaba.Scenarios Addressables group and fire
            // Naninovel's ScriptAssetProcessor.OnWillDeleteAsset (EditorResources SetDirty + SaveAssets) once per
            // script.  The applier rewrites every field, resets every array size and re-materialises every managed
            // reference, so the old content is fully replaced; only the container object (and its GUID) survives.
            // A type mismatch at the path is the one case where the old asset must go.
            UnityEngine.Object existing = AssetDatabase.LoadMainAssetAtPath(assetPath);
            string guidBefore = existing != null ? AssetDatabase.AssetPathToGUID(assetPath) : "";
            ScriptableObject instance;
            bool created;
            if (existing != null && existing.GetType() == type)
            {
                instance = (ScriptableObject)existing;
                created = false;
            }
            else
            {
                if (existing != null)
                {
                    job.warnings.Add("existing asset at " + assetPath + " is " + existing.GetType().FullName +
                                     ", expected " + type.FullName + "; replaced (GUID not preserved)");
                    AssetDatabase.DeleteAsset(assetPath);
                    guidBefore = "";
                }
                instance = ScriptableObject.CreateInstance(type);
                created = true;
            }

            var json = JObject.Parse(File.ReadAllText(dumpPath));
            var serialized = new SerializedObject(instance);

            if (json["m_Name"] != null) instance.name = (string)json["m_Name"];
            // Field values are copied by the shared applier: it owns the string/isArray trap, the
            // enum index convention, PPtr handling and the [SerializeReference] side table
            // ({"rid": N} + references.RefIds) that makes a Naninovel script asset complete.
            var report = JsonToSerializedApplier.ApplyAll(serialized, json, new JsonToSerializedApplier.Options());
            job.fields_applied = report.applied;
            job.leaves_written = report.leavesWritten;
            job.warning_count = report.warnings;
            job.managed_references_total = report.managedTotal;
            job.managed_references_applied = report.managedApplied;
            job.managed_references_failed = report.managedFailed;
            job.needs_binding = report.pointersUnbound;
            foreach (string message in report.messages) job.warnings.Add(message);

            // Content reconciliation.  The 2026-09-17 run reported "managed references 304,280 applied / 0 failed"
            // for 440 script assets whose every parameter was empty: the applier had created the instances and
            // then dropped their data, and nothing counted the values.  So the number that decides CONVERTED is
            // the number of `hasValue: true` leaves written versus the number the dump carries; any gap is a
            // partial conversion, never a success.
            job.params_with_value_dump = CountExpectedHasValue(json);
            job.params_with_value_written = report.hasValueTrueWritten;
            if (job.params_with_value_written != job.params_with_value_dump)
            {
                job.warnings.Add("parameter reconciliation failed: dump has " + job.params_with_value_dump +
                                 " assigned parameters, " + job.params_with_value_written + " were written");
                job.warning_count++;
            }

            if (created) AssetDatabase.CreateAsset(instance, assetPath);
            else EditorUtility.SetDirty(instance);

            string guidAfter = AssetDatabase.AssetPathToGUID(assetPath);
            job.guid_before = guidBefore;
            job.guid_after = guidAfter;
            if (!string.IsNullOrEmpty(guidBefore) && guidBefore != guidAfter)
            {
                job.status = "GUID_CHANGED";
                job.error = "GUID changed " + guidBefore + " -> " + guidAfter + " (Addressables entries keyed by GUID are now stale)";
                return;
            }
            job.status = "CONVERTED";
        }

        /// <summary>
        /// Number of `hasValue: true` leaves the applier is EXPECTED to write for this dump: one per assigned
        /// Naninovel parameter/nullable, counted the way the applier consumes the file.  A `{rid: N}` token is
        /// expanded into references.RefIds[N].data at every place it is used (the applier creates a separate
        /// instance and applies the data once per use, e.g. a command listed both in `playlist` and in
        /// `lines[].command`), so counting the raw `"hasValue": 1` tokens of the file would undercount those.
        /// </summary>
        private static int CountExpectedHasValue(JObject json)
        {
            var refTable = new Dictionary<long, JToken>();
            if (json["references"] is JObject refs)
            {
                foreach (JToken entry in refs["RefIds"] as JArray ?? new JArray())
                {
                    if (entry["rid"] != null) refTable[entry.Value<long>("rid")] = entry;
                }
            }
            int count = 0;
            foreach (JProperty field in json.Properties())
            {
                if (field.Name == "references" || field.Name == "m_Name" || field.Name.StartsWith("m_", StringComparison.Ordinal)) continue;
                count += CountHasValue(field.Value, refTable, 0);
            }
            return count;
        }

        private static int CountHasValue(JToken token, Dictionary<long, JToken> refTable, int depth)
        {
            if (depth > 64) throw new InvalidOperationException("managed-reference nesting deeper than 64 levels while reconciling");
            int count = 0;
            switch (token)
            {
                case JObject obj:
                    JToken rid = obj["rid"];
                    if (rid != null && rid.Type == JTokenType.Integer)
                    {
                        if (refTable.TryGetValue(rid.Value<long>(), out JToken entry) && entry["data"] != null)
                            count += CountHasValue(entry["data"], refTable, depth + 1);
                        break;
                    }
                    foreach (JProperty p in obj.Properties())
                    {
                        if (p.Name == "hasValue")
                        {
                            if (p.Value.Type == JTokenType.Boolean ? p.Value.Value<bool>() : p.Value.Type == JTokenType.Integer && p.Value.Value<int>() != 0)
                                count++;
                        }
                        else count += CountHasValue(p.Value, refTable, depth);
                    }
                    break;
                case JArray arr:
                    foreach (JToken item in arr) count += CountHasValue(item, refTable, depth);
                    break;
            }
            return count;
        }

        private static void Write(string path, ConvertResult result)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, JsonUtility.ToJson(result, true), new UTF8Encoding(false));
        }

        [Serializable]
        public class ConvertResult
        {
            public string status = "NOT_RUN";
            public string generated;
            public string plan_dir;
            public string plan_pattern;
            public List<string> plan_files = new List<string>();
            public string error;
            public int jobs_total;
            public int converted;
            public int converted_partial;
            public int failed;
            public int params_with_value_dump;
            public int params_with_value_written;
            public List<JobRecord> jobs = new List<JobRecord>();
        }

        [Serializable]
        public class JobRecord
        {
            public string asset_path;
            public string target;
            public string type_name;
            public string assembly;
            public string dump;
            public string plan_file;
            public string status = "NOT_RUN";
            public string error;
            public string guid_before;
            public string guid_after;
            public int fields_applied;
            public int leaves_written;
            public int warning_count;
            public int needs_binding;
            public int managed_references_total;
            public int managed_references_applied;
            public int managed_references_failed;
            /// <summary>`hasValue: 1` count in the dump = assigned Naninovel parameters (and other nullables).</summary>
            public int params_with_value_dump;
            /// <summary>`hasValue` booleans the applier actually wrote as true; must equal the dump count.</summary>
            public int params_with_value_written;
            public List<string> warnings = new List<string>();
        }
    }
}



