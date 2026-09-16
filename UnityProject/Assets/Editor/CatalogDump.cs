// Authoritative Addressables catalog dump, produced by the real Addressables runtime.
//
// UNITY_READINESS_CODEX_AUDIT.md section 7.1 requires the complete catalog to be loaded
// through the matching runtime/API so that real keys, GUIDs, labels, IResourceLocation,
// ResourceType, ProviderId, InternalId and dependencies are enumerated -- an ASCII string
// search of catalog.bin is explicitly not sufficient.
//
// This script reads the ORIGINAL shipped catalog (StreamingAssets/aa/catalog.bin) through
// ContentCatalogData + the Addressables runtime locator machinery and writes a complete
// JSON dump for tools/audit/a07_addressables.py to consume.
//
// It never writes to the game directory: the catalog is copied to a scratch folder first.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.Util;

namespace Manosaba.EditorTools
{
    public static class CatalogDump
    {
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/catalog_dump.json";

        [MenuItem("Manosaba/Dump Shipped Addressables Catalog")]
        public static void Dump()
        {
            int exitCode = Run();
            if (Application.isBatchMode) EditorApplication.Exit(exitCode);
        }

        public static int Run()
        {
            var report = new DumpReport();
            try
            {
                string projectRoot = Directory.GetParent(Application.dataPath).FullName;
                string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));
                Directory.CreateDirectory(Path.GetDirectoryName(outPath));

                string gameRoot = EditorPrefs.GetString("Manosaba.GameRoot", string.Empty);
                if (string.IsNullOrEmpty(gameRoot))
                {
                    gameRoot = Environment.GetEnvironmentVariable("MANOSABA_GAME_ROOT");
                }
                if (string.IsNullOrEmpty(gameRoot))
                {
                    report.status = "BLOCKED";
                    report.blocked_reason =
                        "Game root not configured. Set the MANOSABA_GAME_ROOT environment variable " +
                        "or EditorPrefs key Manosaba.GameRoot to the folder containing manosaba_Data.";
                    WriteReport(outPath, report);
                    Debug.LogError("[CatalogDump] " + report.blocked_reason);
                    return 2;
                }

                string catalogSrc = Path.Combine(gameRoot, "manosaba_Data", "StreamingAssets", "aa", "catalog.bin");
                report.catalog_source = catalogSrc;
                if (!File.Exists(catalogSrc))
                {
                    report.status = "BLOCKED";
                    report.blocked_reason = "catalog.bin not found at " + catalogSrc;
                    WriteReport(outPath, report);
                    Debug.LogError("[CatalogDump] " + report.blocked_reason);
                    return 2;
                }
                report.catalog_bytes = new FileInfo(catalogSrc).Length;
                report.catalog_sha256 = Sha256(catalogSrc);

                // Work on a copy so the original data stays read-only (section 3.1.3).
                string scratch = Path.Combine(Path.GetTempPath(), "manosaba-catalog-" + Guid.NewGuid().ToString("N"));
                Directory.CreateDirectory(scratch);
                string catalogCopy = Path.Combine(scratch, "catalog.bin");
                File.Copy(catalogSrc, catalogCopy, true);

                report.status = "PARSED";
                ContentCatalogData data = null;
                try
                {
                    data = ContentCatalogData.LoadFromFile(catalogCopy);
                }
                catch (Exception ex)
                {
                    report.status = "PARSE_FAILED";
                    report.blocked_reason = ex.GetType().Name + ": " + ex.Message;
                    Debug.LogError("[CatalogDump] ContentCatalogData.LoadFromFile failed: " + ex);
                }

                if (data == null)
                {
                    WriteReport(outPath, report);
                    return 3;
                }

                report.provider_id = data.ProviderId;
                report.build_result_hash = data.BuildResultHash;
                report.instance_provider_data_types = data.instanceProviderData == null
                    ? new List<string>()
                    : data.instanceProviderData.Select(d => d.GetType().FullName).ToList();
                report.scene_provider_data_types = data.sceneProviderData == null
                    ? new List<string>()
                    : data.sceneProviderData.Select(d => d.GetType().FullName).ToList();

                // Enumerate every resource location the shipped catalog really declares.
                var locations = new List<LocationRecord>();
                var providerIds = new SortedDictionary<string, int>(StringComparer.Ordinal);
                var resourceTypes = new SortedDictionary<string, int>(StringComparer.Ordinal);
                var internalIdPrefixes = new SortedDictionary<string, int>(StringComparer.Ordinal);
                int dependencyEdges = 0;

                if (data.resourceLocations != null)
                {
                    foreach (IResourceLocation loc in data.resourceLocations)
                    {
                        if (loc == null) { report.null_locations++; continue; }
                        var rec = new LocationRecord
                        {
                            internal_id = loc.InternalId,
                            provider_id = loc.ProviderId,
                            resource_type = loc.ResourceType == null ? null : loc.ResourceType.FullName,
                            dependency_count = loc.Dependencies == null ? 0 : loc.Dependencies.Count,
                            primary_key = loc.PrimaryKey,
                            has_location_type = loc.HasLocationType,
                        };
                        if (loc.Dependencies != null)
                        {
                            dependencyEdges += loc.Dependencies.Count;
                            rec.dependency_internal_ids = loc.Dependencies
                                .Where(d => d != null).Select(d => d.InternalId).ToList();
                        }
                        locations.Add(rec);
                        Increment(providerIds, loc.ProviderId);
                        Increment(resourceTypes, rec.resource_type ?? "(null)");
                        Increment(internalIdPrefixes, NormalisePrefix(loc.InternalId));
                    }
                }
                report.locations = locations;
                report.location_count = locations.Count;
                report.dependency_edges = dependencyEdges;
                report.provider_id_histogram = providerIds;
                report.resource_type_histogram = resourceTypes;
                report.internal_id_prefix_histogram = internalIdPrefixes;

                // Keys: the catalog's real addressable keys map to one or more locations.
                var keyRecords = new List<KeyRecord>();
                if (data.Keys != null)
                {
                    foreach (object key in data.Keys)
                    {
                        string ks = key as string;
                        if (ks == null) { report.non_string_keys++; ks = key == null ? "(null)" : key.ToString(); }
                        var kr = new KeyRecord { key = ks };
                        try
                        {
                            if (data.Locate(ks, typeof(object), out IList<IResourceLocation> found) && found != null)
                            {
                                kr.location_count = found.Count;
                                kr.locations = found.Where(l => l != null).Select(l => l.InternalId).ToList();
                            }
                        }
                        catch (Exception ex)
                        {
                            kr.locate_error = ex.GetType().Name + ": " + ex.Message;
                        }
                        keyRecords.Add(kr);
                    }
                }
                report.keys = keyRecords;
                report.key_count = keyRecords.Count;
                report.keys_without_locations = keyRecords.Count(k => k.location_count == 0);
                report.keys_with_multiple_locations = keyRecords.Count(k => k.location_count > 1);

                // Labels live in the Addressables settings groups, taken from the project's
                // own AddressableAssetSettings when present.
                report.labels = CollectLabels();

                TryDelete(scratch);
                WriteReport(outPath, report);
                Debug.Log("[CatalogDump] wrote " + outPath + " locations=" + report.location_count +
                          " keys=" + report.key_count);
                return 0;
            }
            catch (Exception ex)
            {
                report.status = "ERROR";
                report.blocked_reason = ex.ToString();
                Debug.LogError("[CatalogDump] " + ex);
                try
                {
                    string projectRoot = Directory.GetParent(Application.dataPath).FullName;
                    WriteReport(Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath)), report);
                }
                catch { /* nothing further we can do */ }
                return 4;
            }
        }

        private static List<string> CollectLabels()
        {
            var labels = new List<string>();
            try
            {
                var settings = UnityEditor.AddressableAssets.AddressableAssetSettingsDefaultObject.Settings;
                if (settings != null && settings.GetLabels() != null)
                {
                    labels.AddRange(settings.GetLabels().Where(l => !string.IsNullOrEmpty(l)));
                }
            }
            catch (Exception ex)
            {
                labels.Add("(label collection failed: " + ex.GetType().Name + ")");
            }
            return labels;
        }

        private static void Increment(SortedDictionary<string, int> map, string key)
        {
            key = key ?? "(null)";
            map.TryGetValue(key, out int n);
            map[key] = n + 1;
        }

        private static string NormalisePrefix(string internalId)
        {
            if (string.IsNullOrEmpty(internalId)) return "(empty)";
            int brace = internalId.IndexOf('{');
            if (brace >= 0) return internalId.Substring(0, Math.Min(brace + 1, internalId.Length)) + "...}";
            int slash = internalId.LastIndexOf('/');
            return slash > 0 ? internalId.Substring(0, slash + 1) + "..." : internalId;
        }

        private static string Sha256(string path)
        {
            using (var sha = System.Security.Cryptography.SHA256.Create())
            using (var fs = File.OpenRead(path))
            {
                byte[] hash = sha.ComputeHash(fs);
                var sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash) sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }

        private static void TryDelete(string dir)
        {
            try { if (Directory.Exists(dir)) Directory.Delete(dir, true); } catch { }
        }

        private static void WriteReport(string path, DumpReport report)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            File.WriteAllText(path, JsonUtility.ToJson(report, true), new UTF8Encoding(false));
        }

        [Serializable]
        public class DumpReport
        {
            public string status = "NOT_RUN";
            public string blocked_reason;
            public string catalog_source;
            public long catalog_bytes;
            public string catalog_sha256;
            public string provider_id;
            public string build_result_hash;
            public List<string> instance_provider_data_types = new List<string>();
            public List<string> scene_provider_data_types = new List<string>();
            public int location_count;
            public int key_count;
            public int dependency_edges;
            public int null_locations;
            public int non_string_keys;
            public int keys_without_locations;
            public int keys_with_multiple_locations;
            public List<string> labels = new List<string>();
            public SortedDictionary<string, int> provider_id_histogram = new SortedDictionary<string, int>(StringComparer.Ordinal);
            public SortedDictionary<string, int> resource_type_histogram = new SortedDictionary<string, int>(StringComparer.Ordinal);
            public SortedDictionary<string, int> internal_id_prefix_histogram = new SortedDictionary<string, int>(StringComparer.Ordinal);
            public List<LocationRecord> locations = new List<LocationRecord>();
            public List<KeyRecord> keys = new List<KeyRecord>();
        }

        [Serializable]
        public class LocationRecord
        {
            public string internal_id;
            public string provider_id;
            public string resource_type;
            public int dependency_count;
            public string primary_key;
            public bool has_location_type;
            public List<string> dependency_internal_ids = new List<string>();
        }

        [Serializable]
        public class KeyRecord
        {
            public string key;
            public int location_count;
            public string locate_error;
            public List<string> locations = new List<string>();
        }
    }
}
