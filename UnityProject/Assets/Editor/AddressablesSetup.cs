// Creates the project's own Addressables configuration (G3 step 1).
//
//   Unity -batchmode -quit -projectPath UnityProject \
//         -executeMethod Manosaba.EditorTools.AddressablesSetup.Run -logFile -
//
// Reads tools/unity-rebuild/out/unity/addressables_plan_<group>.json (produced by
// tools/unity-rebuild/a13f_plan_addressables.py) and:
//
//   1. creates Assets/AddressableAssetsData/AddressableAssetSettings.asset if it does not exist
//   2. creates one group per family in the plan (the shipped build carries no Editor group layout,
//      so the layout is a documented reconstruction decision, never fabricated data)
//   3. adds one entry per asset with the *shipped catalog's primary key* as its address
//
// The address is the part that matters, and it is worth stating precisely because a stale version of this
// comment ("the shipped container path") sent a later investigation down the wrong path: the shipped catalog
// has 29,097 locations and **not one of their `internal_id`s appears as a key** - the asset-path form exists
// only as the location's internal_id.  What the engine and the plan share is the *key*, which for a scenario is
// `Naninovel/Scripts/<script path>` (the location's primary_key, built from the project root path and the
// loader's path prefix).  A rebuilt catalog can therefore be compared with the shipped one key by key, while
// internal_ids are a separate mapping.
//
// Labels are applied too, and *registered*: the shipped catalog carries labels such as
// `Scripts-System` and the umbrella label `Naninovel` (the game's MasterData._assetKeyNames) alongside
// the keys, and Addressables only puts a label into the catalog when the label is declared on the
// settings object.  AddressableAssetEntry.CreateKeyList (Editor/Settings/AddressableAssetEntry.cs,
// Addressables 2.4.6) checks `parentGroup.Settings.GetLabels()` and, for a label that is not in it,
// both omits the key *and removes the label from the entry* -- so `SetLabel(label, true, force: false)`
// silently produced a catalog with zero label keys, i.e. no `Naninovel` key and a dead boot chain.
// Passing force: true calls settings.AddLabel for us; the counters below record what was registered.
//
// Result: tools/unity-rebuild/out/unity/addressables_setup_result.json
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace Manosaba.EditorTools
{
    public static class AddressablesSetup
    {
        private const string PlanDirectoryRelative = "../tools/unity-rebuild/out/unity";
        private const string PlanPatternEnv = "MANOSABA_ADDRESSABLES_PLAN";
        private const string DefaultPlan = "addressables_plan_core.json";
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/addressables_setup_result.json";

        [MenuItem("Manosaba/Setup Addressables (from plan)")]
        public static void Run()
        {
            int code = Execute();
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        public static int Execute()
        {
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string repoRoot = Directory.GetParent(projectRoot).FullName;
            string planName = Environment.GetEnvironmentVariable(PlanPatternEnv);
            if (string.IsNullOrEmpty(planName)) planName = DefaultPlan;
            string planPath = Path.GetFullPath(Path.Combine(projectRoot, PlanDirectoryRelative, planName));
            string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));

            var result = new SetupResult { generated = DateTime.UtcNow.ToString("o"), plan = planName };
            if (!File.Exists(planPath))
            {
                result.status = "BLOCKED_NO_PLAN";
                result.error = "missing " + planPath;
                Write(outPath, result);
                Debug.LogError("[AddressablesSetup] " + result.error);
                return 2;
            }

            try
            {
                JObject plan = JObject.Parse(File.ReadAllText(planPath));
                var entries = plan["entries"] as JArray ?? new JArray();
                result.entries_planned = entries.Count;

                AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.GetSettings(true);
                if (settings == null)
                {
                    result.status = "BLOCKED_NO_SETTINGS";
                    result.error = "AddressableAssetSettingsDefaultObject.GetSettings(true) returned null";
                    Write(outPath, result);
                    Debug.LogError("[AddressablesSetup] " + result.error);
                    return 2;
                }
                result.settings_path = AssetDatabase.GetAssetPath(settings);

                // ---- groups ---------------------------------------------------------------
                var groups = new Dictionary<string, AddressableAssetGroup>(StringComparer.Ordinal);
                var plannedGuids = new HashSet<string>(StringComparer.Ordinal);
                // every label the plan asks for; checked against the settings at the end, because a
                // label that is only on the entry never reaches the built catalog
                var plannedLabels = new HashSet<string>(StringComparer.Ordinal);
                foreach (string name in entries.Where(e => (string)e["status"] != "excluded")
                             .Select(e => (string)e["group"]).Where(g => !string.IsNullOrEmpty(g))
                             .Distinct().OrderBy(g => g, StringComparer.Ordinal))
                {
                    AddressableAssetGroup existing = settings.groups
                        .FirstOrDefault(g => g != null && g.Name == name);
                    if (existing == null)
                    {
                        existing = settings.CreateGroup(name, false, false, false,
                            settings.DefaultGroup != null ? settings.DefaultGroup.Schemas : null);
                        result.groups_created++;
                    }
                    groups[name] = existing;
                }

                // ---- entries --------------------------------------------------------------
                foreach (JToken token in entries)
                {
                    string assetPath = (string)token["asset_path"];
                    string address = (string)token["address"];
                    string groupName = (string)token["group"];
                    // documented exclusions stay in the plan (with a reason) but never enter the settings
                    if ((string)token["status"] == "excluded")
                    {
                        result.entries_excluded++;
                        result.excluded_samples.Add((string)token["container_path"] ?? assetPath);
                        continue;
                    }
                    if (string.IsNullOrEmpty(assetPath) || !groups.TryGetValue(groupName ?? "", out AddressableAssetGroup group))
                    {
                        result.entries_skipped++;
                        continue;
                    }

                    string guid = AssetDatabase.AssetPathToGUID(assetPath);
                    if (string.IsNullOrEmpty(guid))
                    {
                        result.entries_missing_asset++;
                        if (result.missing_samples.Count < 20) result.missing_samples.Add(assetPath);
                        continue;
                    }

                    AddressableAssetEntry entry = settings.CreateOrMoveEntry(guid, group, false, false);
                    if (entry == null)
                    {
                        result.entries_skipped++;
                        continue;
                    }
                    plannedGuids.Add(guid);
                    entry.SetAddress(address);
                    if (token["labels"] is JArray labels)
                    {
                        foreach (JToken label in labels)
                        {
                            string labelName = (string)label;
                            if (string.IsNullOrEmpty(labelName)) continue;
                            plannedLabels.Add(labelName);
                            // force: true registers the label on the settings; without it Addressables
                            // drops the label from the catalog and strips it from the entry (see header)
                            if (entry.SetLabel(labelName, true, true, false)) result.labels_newly_added++;
                            result.labels_applied++;
                        }
                    }
                    result.entries_added++;
                }

                // ---- prune stale entries ----------------------------------------------------
                // The plan is authoritative for the groups it names: an entry that is no longer planned
                // (for example an asset that has since been excluded from the content build) must not
                // linger in the settings, because a lingering entry still reaches the build.  Only the
                // groups named by this plan are pruned, so plans can be applied one family at a time.
                if (Environment.GetEnvironmentVariable("MANOSABA_ADDRESSABLES_PRUNE") != "0")
                {
                    foreach (var kv in groups)
                    {
                        foreach (AddressableAssetEntry stale in kv.Value.entries.ToList())
                        {
                            if (stale == null || plannedGuids.Contains(stale.guid)) continue;
                            string stalePath = stale.AssetPath;
                            if (settings.RemoveAssetEntry(stale.guid, false))
                            {
                                result.entries_pruned++;
                                if (result.pruned_samples.Count < 20) result.pruned_samples.Add(stalePath);
                            }
                        }
                    }
                }

                settings.SetDirty(AddressableAssetSettings.ModificationEvent.BatchModification, null, true, true);
                AssetDatabase.SaveAssets();
                result.entry_count_total = settings.groups.Where(g => g != null).Sum(g => g.entries.Count);
                result.group_summary = settings.groups.Where(g => g != null)
                    .OrderBy(g => g.Name, StringComparer.Ordinal)
                    .Select(g => g.Name + "=" + g.entries.Count).ToList();

                // A label on an entry is not enough: Addressables only emits it into the catalog when
                // the settings object declares it. Report what the settings actually carry, so a
                // silently unregistered label (the bug that produced a catalog without `Naninovel`)
                // can never pass unnoticed again.
                var registeredLabels = new HashSet<string>(settings.GetLabels() ?? new List<string>(),
                    StringComparer.Ordinal);
                result.labels_registered = registeredLabels.Count;
                result.settings_labels = registeredLabels.OrderBy(l => l, StringComparer.Ordinal).ToList();
                result.labels_unregistered = plannedLabels.Count(l => !registeredLabels.Contains(l));
                if (result.labels_unregistered > 0)
                {
                    Debug.LogWarning($"[AddressablesSetup] {result.labels_unregistered} planned label(s) are not " +
                                     "registered on the Addressables settings and would be dropped from the catalog: " +
                                     string.Join(", ", plannedLabels.Where(l => !registeredLabels.Contains(l))
                                         .OrderBy(l => l, StringComparer.Ordinal).Take(20)));
                }
                if (result.entries_pruned > 0)
                {
                    Debug.LogWarning($"[AddressablesSetup] pruned {result.entries_pruned} stale entries no longer in the " +
                                     "plan (the plan is authoritative for the groups it names)");
                }
                result.status = result.entries_missing_asset == 0 ? "OK" : "OK_WITH_MISSING_ASSETS";
                if (result.labels_unregistered > 0) result.status = result.status + "_WITH_UNREGISTERED_LABELS";
                if (result.entries_excluded > 0)
                {
                    result.status = result.status + "_WITH_EXCLUSIONS";
                    Debug.LogWarning($"[AddressablesSetup] {result.entries_excluded} entries are excluded from the " +
                                     "content build (documented in tools/unity-rebuild/content_build_exclusions.json): " +
                                     string.Join(", ", result.excluded_samples));
                }
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.error = ex.ToString();
                Debug.LogError("[AddressablesSetup] " + ex);
            }

            Write(outPath, result);
            Debug.Log($"[AddressablesSetup] {result.status}: planned={result.entries_planned} added={result.entries_added} " +
                      $"missing={result.entries_missing_asset} excluded={result.entries_excluded} pruned={result.entries_pruned} " +
                      $"groups={result.groups_created} new; labels applied={result.labels_applied} " +
                      $"newly={result.labels_newly_added} registered={result.labels_registered} " +
                      $"unregistered={result.labels_unregistered}; wrote {outPath}");
            return result.status.StartsWith("OK") ? 0 : 1;
        }

        private static void Write(string path, SetupResult result)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, JsonUtility.ToJson(result, true), new UTF8Encoding(false));
        }

        [Serializable]
        public class SetupResult
        {
            public string status = "NOT_RUN";
            public string generated;
            public string plan;
            public string settings_path;
            public string error;
            public int entries_planned;
            public int entries_added;
            public int entries_skipped;
            public int entries_missing_asset;
            public int entries_excluded;
            public int entries_pruned;
            public int labels_applied;
            public int labels_newly_added;
            public int labels_registered;
            public int labels_unregistered;
            public int entry_count_total;
            public int groups_created;
            public List<string> group_summary = new List<string>();
            public List<string> settings_labels = new List<string>();
            public List<string> missing_samples = new List<string>();
            public List<string> excluded_samples = new List<string>();
            public List<string> pruned_samples = new List<string>();
        }
    }
}

