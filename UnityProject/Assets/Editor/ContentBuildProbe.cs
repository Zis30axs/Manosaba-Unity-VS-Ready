// Diagnoses which assets the Addressables/SBP content build cannot serialize, *without* running a
// full content build.
//
//   Unity -batchmode -quit -projectPath UnityProject \
//         -executeMethod Manosaba.EditorTools.ContentBuildProbe.Run -logFile -
//
// Why this exists: a content build aborts on the first asset whose type tree Unity cannot build
// (observed: "Invalid type UniRx.ReactiveProperty`1<int> for instance field
// WitchTrials.Views.SaveLoadUi:_loadingSlotCount" from
// AddressableAssetEntry.GatherMainAndReferencedSerializedTypes ->
// ContentBuildInterface.GetTypeForObjects).  A full build takes ~4 minutes per attempt and stops at
// the first offender, so finding the complete bad set by rebuild is quadratic.  This probe calls the
// same API per asset (ContentBuildInterface.GetPlayerObjectIdentifiersInAsset + GetTypeForObjects)
// and reports every asset that throws, plus -- for diagnostics -- the serializable fields whose type
// comes from a stub assembly, which is what the serialized-file writer then chokes on.
//
// Input : tools/unity-rebuild/out/unity/<MANOSABA_CONTENT_PROBE_IN or addressables_plan_prefabs.json>
// Output: tools/unity-rebuild/out/unity/content_build_probe.json
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;

namespace Manosaba.EditorTools
{
    public static class ContentBuildProbe
    {
        private const string PlanDirectoryRelative = "../tools/unity-rebuild/out/unity";
        private const string DefaultPlan = "addressables_plan_prefabs.json";
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/content_build_probe.json";

        /// <summary>Assemblies that are Il2CppDumper stubs: their types cannot be serialized or written.</summary>
        private static readonly string[] StubAssemblyPrefixes =
        {
            "UniRx", "UniTask", "LeTai", "MPUIKit", "UIPolygon", "LitMotion", "FastEnum", "ppt-noise",
        };

        [MenuItem("Manosaba/Probe Content Build Serializability")]
        public static void Run()
        {
            int code = Execute();
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        public static int Execute()
        {
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string planName = Environment.GetEnvironmentVariable("MANOSABA_CONTENT_PROBE_IN");
            if (string.IsNullOrEmpty(planName)) planName = DefaultPlan;
            string planPath = Path.GetFullPath(Path.Combine(projectRoot, PlanDirectoryRelative, planName));
            string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));

            var result = new ProbeResult
            {
                generated = DateTime.UtcNow.ToString("o"),
                plan = planName,
                build_target = EditorUserBuildSettings.activeBuildTarget.ToString(),
            };
            try
            {
                JObject plan = JObject.Parse(File.ReadAllText(planPath));
                var entries = plan["entries"] as JArray ?? new JArray();
                result.entries_in_plan = entries.Count;
                foreach (JToken token in entries)
                {
                    string assetPath = (string)token["asset_path"];
                    if (string.IsNullOrEmpty(assetPath) || assetPath.StartsWith("Packages/")) continue;
                    if (!assetPath.EndsWith(".prefab", StringComparison.OrdinalIgnoreCase)) continue;
                    if (result.probed.Contains(assetPath)) continue;
                    result.probed.Add(assetPath);
                }
                result.prefabs_probed = result.probed.Count;
            }
            catch (Exception ex)
            {
                result.status = "BLOCKED_NO_PLAN";
                result.error = ex.GetType().Name + ": " + ex.Message + " (plan " + planPath + ")";
                Write(outPath, result);
                Debug.LogError("[ContentBuildProbe] " + result.error);
                return 2;
            }

            var target = EditorUserBuildSettings.activeBuildTarget;
            foreach (string assetPath in result.probed)
            {
                var record = new AssetRecord { asset_path = assetPath };
                try
                {
                    GUID guid = new GUID(AssetDatabase.AssetPathToGUID(assetPath));
                    if (guid.Empty())
                    {
                        record.status = "NO_GUID";
                    }
                    else
                    {
                        ObjectIdentifier[] ids =
                            ContentBuildInterface.GetPlayerObjectIdentifiersInAsset(guid, target);
                        record.object_count = ids.Length;
                        Type[] types = ContentBuildInterface.GetTypeForObjects(ids);
                        record.type_count = types.Length;
                        record.status = "OK";
                        record.stub_types = StubTypesOf(types).ToList();
                    }
                }
                catch (Exception ex)
                {
                    record.status = "THROWS";
                    record.error = ex.GetType().Name + ": " + ex.Message;
                }

                if (record.status != "OK" || record.stub_types.Count > 0)
                {
                    record.component_stub_fields = ComponentStubFields(assetPath, record).ToList();
                }
                if (record.status != "OK") result.failing++;
                if (record.stub_types.Count > 0) result.with_stub_types++;
                result.assets.Add(record);
            }

            result.status = result.failing == 0 ? "OK" : "OK_WITH_FAILURES";
            Write(outPath, result);
            Debug.Log($"[ContentBuildProbe] {result.status}: probed {result.prefabs_probed} prefabs, " +
                      $"{result.failing} throw, {result.with_stub_types} reference stub types; wrote {outPath}");
            foreach (AssetRecord record in result.assets.Where(a => a.status != "OK").Take(20))
            {
                Debug.LogWarning("[ContentBuildProbe] " + record.asset_path + " -> " + record.error);
            }
            return 0;   // the probe's job is to report, not to gate
        }

        /// <summary>Serialized component types that come from a stub assembly (Engine/package types are skipped).</summary>
        private static IEnumerable<string> StubTypesOf(IEnumerable<Type> types)
        {
            var seen = new HashSet<string>(StringComparer.Ordinal);
            foreach (Type type in types)
            {
                if (type == null) continue;
                string assembly = type.Assembly.GetName().Name ?? "";
                if (!IsStub(assembly)) continue;
                if (seen.Add(type.FullName)) yield return type.FullName;
            }
        }

        /// <summary>
        /// Every serializable instance field (public or [SerializeField]) whose type comes from a stub
        /// assembly: this is the set Unity's serialized-file writer tries to write and fails on.
        /// </summary>
        private static IEnumerable<string> ComponentStubFields(string assetPath, AssetRecord record)
        {
            GameObject root = null;
            try { root = AssetDatabase.LoadAssetAtPath<GameObject>(assetPath); }
            catch (Exception ex) { record.load_error = ex.GetType().Name + ": " + ex.Message; }
            if (root == null) return Enumerable.Empty<string>();

            var found = new List<string>();
            foreach (Component component in root.GetComponentsInChildren<Component>(true))
            {
                if (component == null) continue;
                Type type = component.GetType();
                foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.Public |
                                                           BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
                {
                    bool serialized = field.IsPublic ||
                                      field.IsDefined(typeof(SerializeField), true);
                    if (!serialized) continue;
                    if (field.IsDefined(typeof(NonSerializedAttribute), true)) continue;
                    if (!IsStub(field.FieldType.Assembly.GetName().Name ?? "")) continue;
                    found.Add(type.FullName + ":" + field.Name + " (" + field.FieldType.FullName + ")");
                }
            }
            return found;
        }

        private static bool IsStub(string assembly)
        {
            foreach (string prefix in StubAssemblyPrefixes)
            {
                if (assembly.StartsWith(prefix, StringComparison.Ordinal)) return true;
            }
            return false;
        }

        private static void Write(string path, ProbeResult result)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, JsonUtility.ToJson(result, true), new UTF8Encoding(false));
        }

        [Serializable]
        public class AssetRecord
        {
            public string asset_path;
            public string status = "NOT_RUN";
            public string error;
            public string load_error;
            public int object_count;
            public int type_count;
            public List<string> stub_types = new List<string>();
            public List<string> component_stub_fields = new List<string>();
        }

        [Serializable]
        public class ProbeResult
        {
            public string status = "NOT_RUN";
            public string generated;
            public string plan;
            public string build_target;
            public string error;
            public int entries_in_plan;
            public int prefabs_probed;
            public int failing;
            public int with_stub_types;
            public List<string> probed = new List<string>();
            public List<AssetRecord> assets = new List<AssetRecord>();
        }
    }
}
