// Authoritative Addressables catalog dump, produced by the real Addressables runtime.
//
// UNITY_READINESS_CODEX_AUDIT.md section 7.1 requires the complete catalog to be loaded
// through the matching runtime/API so that real keys, GUIDs, labels, IResourceLocation,
// ResourceType, ProviderId, InternalId and dependencies are enumerated -- an ASCII string
// search of catalog.bin is explicitly not sufficient.
//
// This script reads the ORIGINAL shipped catalog (StreamingAssets/aa/catalog.bin) through
// the Addressables 2.4.6 runtime and writes a complete JSON dump for
// tools/audit/a07_addressables.py to consume.
//
// A key that reaches thousands of locations is not an address but an *umbrella label*: the game's
// MasterData._assetKeyNames is ["Naninovel"], and the umbrella key `Naninovel` reaches 28,150 of the
// shipped catalog's 29,097 locations. A per-key list capped at 64 locations cannot express that
// membership, and the earlier version of this dump hid exactly that fact, so:
//
//   * every key carries `locations_complete` (is its capped `locations` list whole?), and
//   * the report carries a top-level `label_membership` section: every key reaching more than one
//     location, with its *complete* list of location internal_ids, mirrored into the compact side
//     artifact <dump name>_label_membership.tsv (one "key<TAB>internal_id" line per edge).
//
// tools/unity-rebuild/a13f_plan_addressables.py derives the shipped addressing scheme (addresses and
// labels) from those two sections and fails loudly when a key's membership is still incomplete.
//
// Two runtime load routes are attempted, in this order:
//
//   1. ContentCatalogData.LoadFromFile(path, cacheSize) + ContentCatalogData.CreateCustomLocator()
//      -- the exact pair of calls ContentCatalogProvider / BinaryAssetProvider use to
//      materialise a binary (.bin) catalog and turn it into the locator the runtime
//      searches (see Runtime/ResourceLocators/ContentCatalogData.cs and
//      Runtime/Initialization/InitializationOperation.cs in com.unity.addressables).
//      Both members are `internal` in Addressables 2.4.6, so they are invoked by
//      reflection. No catalog byte is parsed by hand here: the runtime's own
//      BinaryStorageBuffer/Serializer does the decoding.
//   2. Addressables.LoadContentCatalogAsync(path) -- the supported public API, used only
//      when route 1 is unavailable. It needs the project's own Addressables runtime data
//      to initialise first, so it can legitimately fail in a project that has not built
//      Addressables content yet.
//
// Note on the Addressables 1.x surface this file originally targeted: it does not exist in
// 2.4.6. ContentCatalogData.LoadFromFile is no longer public; instanceProviderData and
// sceneProviderData are now the single-valued InstanceProviderData / SceneProviderData
// (plus ResourceProviderData); resourceLocations / Keys / Locate moved off
// ContentCatalogData onto the locator (IResourceLocator.Keys, IResourceLocator.Locate,
// IResourceLocator.AllLocations); and IResourceLocation.HasLocationType no longer exists.
//
// It never writes to the game directory: the catalog is copied to a scratch folder first.
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;
using UnityEngine.ResourceManagement.Util;

namespace Manosaba.EditorTools
{
    public static class CatalogDump
    {
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/catalog_dump.json";

        /// <summary>Scratch folder the catalog is copied into (deterministic, deleted afterwards).</summary>
        private const string ScratchFolderName = "manosaba-catalog-dump";

        /// <summary>Cache size handed to the runtime loader; the runtime default is 1024.</summary>
        private const int RuntimeCacheSize = 1024;

        /// <summary>The placeholder the content build writes for the runtime path (verified in the shipped catalog bytes).</summary>
        private const string RuntimePathPlaceholder = "{UnityEngine.AddressableAssets.Addressables.RuntimePath}";

        /// <summary>Per-key location list is capped so a label covering thousands of entries cannot explode the dump.</summary>
        private const int MaxLocationsPerKey = 64;

        /// <summary>
        /// Safety cap for the per-key membership lists in `label_membership` (and in the side artifact).
        /// It is deliberately far above anything this catalog contains (the largest key, the umbrella
        /// label `Naninovel`, reaches 28,150 locations) so that membership is complete in practice while
        /// a pathological catalog still cannot produce an unbounded dump. When it does bite, the affected
        /// key carries `locations_truncated: true` and the report says so -- never silently.
        /// </summary>
        private const int MaxMembershipLocationsPerKey = 250000;

        [MenuItem("Manosaba/Dump Shipped Addressables Catalog")]
        public static void Dump()
        {
            int exitCode = Run();
            if (Application.isBatchMode) EditorApplication.Exit(exitCode);
        }

        public static int Run()
        {
            var report = new DumpReport { generated = DateTime.UtcNow.ToString("o") };
            try
            {
                string projectRoot = Directory.GetParent(Application.dataPath).FullName;
                string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));
                // MANOSABA_CATALOG_OUT keeps a dump of a different catalog from overwriting the shipped
                // one (which tools/audit/a07_addressables.py consumes by name).
                string overrideOut = Environment.GetEnvironmentVariable("MANOSABA_CATALOG_OUT");
                if (!string.IsNullOrEmpty(overrideOut))
                {
                    outPath = Path.GetFullPath(Path.Combine(projectRoot, overrideOut));
                }
                Directory.CreateDirectory(Path.GetDirectoryName(outPath));

                string catalogSrc = null;
                string gameRoot = ResolveGameRoot();
                // MANOSABA_CATALOG_PATH lets the same runtime-parsing path dump a *different* catalog:
                // the project's own content build (Library/com.unity.addressables/aa/<platform>/catalog.bin)
                // is written in the same binary format, so it can be compared with the shipped one.
                string overridePath = Environment.GetEnvironmentVariable("MANOSABA_CATALOG_PATH");
                if (!string.IsNullOrEmpty(overridePath))
                {
                    catalogSrc = Path.GetFullPath(overridePath);
                }
                else if (string.IsNullOrEmpty(gameRoot))
                {
                    return Blocked(report, outPath, "Game root not configured. Set the MANOSABA_GAME_ROOT environment " +
                        "variable, the EditorPrefs key Manosaba.GameRoot, or game_root in tools/audit/audit.config.json " +
                        "to the folder containing manosaba_Data (or set MANOSABA_CATALOG_PATH to dump a specific catalog).");
                }
                else
                {
                    catalogSrc = Path.Combine(gameRoot, "manosaba_Data", "StreamingAssets", "aa", "catalog.bin");
                }

                report.catalog_source = catalogSrc;
                if (!File.Exists(catalogSrc))
                {
                    return Blocked(report, outPath, "catalog.bin not found at " + catalogSrc);
                }

                report.catalog_bytes = new FileInfo(catalogSrc).Length;
                report.catalog_sha256 = Sha256(catalogSrc);
                report.catalog_hash_file_content = ReadHashSidecar(catalogSrc);
                report.addressables_package_version = AddressablesPackageVersion();
                // In the Editor Addressables.RuntimePath is the project's Library build path; every
                // "{...RuntimePath}" in the catalog is expanded to it by Addressables.ResolveInternalId.
                report.runtime_path_used = Addressables.RuntimePath;

                // Work on a copy so the original data stays read-only (section 3.1.3).
                string scratch = Path.Combine(Path.GetTempPath(), ScratchFolderName);
                Directory.CreateDirectory(scratch);
                string catalogCopy = Path.Combine(scratch, "catalog.bin");
                File.Copy(catalogSrc, catalogCopy, true);

                IResourceLocator locator = null;
                ContentCatalogData data = null;

                // Route 1: the runtime's own binary catalog loader (reflection, internal API).
                try
                {
                    data = LoadFromFileWithRuntime(catalogCopy);
                    if (data == null) throw new InvalidOperationException("ContentCatalogData.LoadFromFile returned null");
                    locator = CreateLocatorWithRuntime(data, catalogCopy);
                    report.load_route = "ContentCatalogData.LoadFromFile + CreateCustomLocator (runtime internals)";
                }
                catch (Exception ex)
                {
                    report.load_attempts.Add("runtime-internal loader: " + Describe(ex));
                    Debug.LogWarning("[CatalogDump] runtime-internal catalog loader failed: " + Describe(ex));
                }

                // Route 2: the supported public API, only if route 1 did not produce a locator.
                if (locator == null)
                {
                    try
                    {
                        locator = LoadWithPublicApi(catalogCopy);
                        report.load_route = "Addressables.LoadContentCatalogAsync (public API)";
                    }
                    catch (Exception ex)
                    {
                        report.load_attempts.Add("Addressables.LoadContentCatalogAsync: " + Describe(ex));
                        Debug.LogWarning("[CatalogDump] Addressables.LoadContentCatalogAsync failed: " + Describe(ex));
                    }
                }

                // The runtime loader reads the whole file into memory, so the scratch copy can go now.
                TryDelete(scratch);

                if (locator == null)
                {
                    report.status = "PARSE_FAILED";
                    report.blocked_reason = "no runtime loader produced a resource locator; see load_attempts";
                    WriteReport(outPath, report);
                    Debug.LogError("[CatalogDump] " + report.blocked_reason);
                    return 3;
                }

                report.status = "PARSED";
                report.locator_id = locator.LocatorId;

                if (data != null)
                {
                    report.provider_id = data.ProviderId;
                    report.build_result_hash = data.BuildResultHash;
                    report.local_hash = data.LocalHash;
                    report.instance_provider_data = DescribeProvider(data.InstanceProviderData);
                    report.scene_provider_data = DescribeProvider(data.SceneProviderData);
                    if (data.InstanceProviderData.Id != null || data.InstanceProviderData.ObjectType.ClassName != null)
                    {
                        report.instance_provider_data_types.Add(ProviderTypeName(data.InstanceProviderData));
                    }
                    if (data.SceneProviderData.Id != null || data.SceneProviderData.ObjectType.ClassName != null)
                    {
                        report.scene_provider_data_types.Add(ProviderTypeName(data.SceneProviderData));
                    }
                    if (data.ResourceProviderData != null)
                    {
                        foreach (ObjectInitializationData provider in data.ResourceProviderData)
                        {
                            report.resource_provider_data.Add(DescribeProvider(provider));
                        }
                    }
                }

                CollectLabels(report);
                var labelSet = new HashSet<string>(report.labels, StringComparer.Ordinal);

                // Enumerate every key the shipped catalog really declares, and through it every
                // IResourceLocation the runtime would hand to a loader.
                var locations = new List<LocationRecord>();
                var seenLocations = new HashSet<string>(StringComparer.Ordinal);
                var keyRecords = new List<KeyRecord>();
                // Every key that reaches more than one location is a *label-like* key: the umbrella
                // label the game asks for by name (`Naninovel`, 28,150 locations) is one of these, and
                // its membership is what a per-key list capped at 64 locations cannot express.
                var membershipRecords = new List<MembershipRecord>();
                var providerIds = new SortedDictionary<string, int>(StringComparer.Ordinal);
                var resourceTypes = new SortedDictionary<string, int>(StringComparer.Ordinal);
                var internalIdPrefixes = new SortedDictionary<string, int>(StringComparer.Ordinal);
                int locationEdges = 0;
                int dependencyEdges = 0;
                int membershipEdges = 0;
                int membershipTruncatedKeys = 0;

                foreach (object key in locator.Keys)
                {
                    string keyText;
                    if (key is string s)
                    {
                        keyText = s;
                    }
                    else
                    {
                        report.non_string_keys++;
                        keyText = key == null ? "(null)" : key.ToString();
                    }

                    var record = new KeyRecord
                    {
                        key = keyText,
                        key_type = key == null ? "(null)" : key.GetType().FullName,
                        is_label = labelSet.Contains(keyText),
                        looks_like_guid = LooksLikeGuid(keyText),
                    };
                    // collected only for label-like keys (location_count > 1), never for the 57k
                    // single-location keys whose (capped) list is already complete
                    List<string> membership = null;
                    bool membershipTruncated = false;

                    try
                    {
                        IList<IResourceLocation> found;
                        if (locator.Locate(key, typeof(object), out found) && found != null)
                        {
                            record.location_count = found.Count;
                            if (found.Count > 1) membership = new List<string>(Math.Min(found.Count, MaxMembershipLocationsPerKey));
                            foreach (IResourceLocation loc in found)
                            {
                                if (loc == null)
                                {
                                    report.null_locations++;
                                    continue;
                                }

                                locationEdges++;
                                if (record.locations.Count < MaxLocationsPerKey)
                                {
                                    record.locations.Add(loc.InternalId);
                                }
                                else
                                {
                                    record.locations_truncated = true;
                                }

                                if (membership != null)
                                {
                                    if (membership.Count < MaxMembershipLocationsPerKey) membership.Add(loc.InternalId);
                                    else membershipTruncated = true;
                                }

                                string identity = LocationIdentity(loc);
                                if (seenLocations.Add(identity))
                                {
                                    locations.Add(ToLocationRecord(loc, report.runtime_path_used));
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        record.locate_error = Describe(ex);
                    }

                    // `locations_truncated` keeps its original cap-driven meaning; `locations_complete`
                    // is the honest answer to "is this entry's location list whole?" and is computed
                    // from the actual counts, so it is also correct for null locations and locate errors.
                    record.locations_complete = record.locate_error == null && record.location_count == record.locations.Count;

                    if (membership != null)
                    {
                        membershipEdges += membership.Count;
                        if (membershipTruncated) membershipTruncatedKeys++;
                        membershipRecords.Add(new MembershipRecord
                        {
                            key = keyText,
                            key_type = record.key_type,
                            looks_like_guid = record.looks_like_guid,
                            location_count = record.location_count,
                            locations_truncated = membershipTruncated,
                            locations = membership,
                        });
                    }

                    if (record.looks_like_guid) report.guid_like_keys++;
                    keyRecords.Add(record);
                }

                keyRecords.Sort((a, b) => string.CompareOrdinal(a.key, b.key));
                locations.Sort((a, b) => string.CompareOrdinal(a.internal_id, b.internal_id));
                membershipRecords.Sort((a, b) => string.CompareOrdinal(a.key, b.key));

                foreach (LocationRecord loc in locations)
                {
                    dependencyEdges += loc.dependency_count;
                    Increment(providerIds, loc.provider_id);
                    Increment(resourceTypes, loc.resource_type);
                    Increment(internalIdPrefixes, NormalisePrefix(loc.internal_id));
                }

                report.locations = locations;
                report.location_count = locations.Count;
                report.location_edges = locationEdges;
                report.dependency_edges = dependencyEdges;
                report.distinct_provider_ids = providerIds.Count;
                report.distinct_resource_types = resourceTypes.Count;
                report.provider_id_histogram = ToHistogram(providerIds);
                report.resource_type_histogram = ToHistogram(resourceTypes);
                report.internal_id_prefix_histogram = ToHistogram(internalIdPrefixes);

                report.keys = keyRecords;
                report.key_count = keyRecords.Count;
                report.keys_without_locations = keyRecords.Count(k => k.location_count == 0);
                report.keys_with_multiple_locations = keyRecords.Count(k => k.location_count > 1);
                report.keys_with_complete_locations = keyRecords.Count(k => k.locations_complete);

                report.label_membership = membershipRecords;
                report.label_membership_count = membershipRecords.Count;
                report.label_membership_edges = membershipEdges;
                report.label_membership_truncated_keys = membershipTruncatedKeys;

                // The compact side artifact carries the same membership as `label_membership` in one
                // "key<TAB>internal_id" line per edge: for the shipped catalog 30,048 lines / ~3 MB,
                // greppable and diffable without loading 60 MB of JSON.
                report.label_membership_artifact = MembershipArtifactPath(outPath);
                WriteMembershipArtifact(report.label_membership_artifact, membershipRecords);
                WriteReport(outPath, report);

                Debug.Log("[CatalogDump] wrote " + outPath + " route=" + report.load_route +
                          " locations=" + report.location_count + " keyEdges=" + report.location_edges +
                          " keys=" + report.key_count + " labelLikeKeys=" + report.label_membership_count +
                          " membershipEdges=" + report.label_membership_edges +
                          " truncatedMemberships=" + report.label_membership_truncated_keys);
                if (membershipTruncatedKeys > 0)
                {
                    Debug.LogWarning("[CatalogDump] " + membershipTruncatedKeys + " label-like key(s) exceed " +
                        MaxMembershipLocationsPerKey + " locations and are truncated in label_membership; " +
                        "consumers must treat their membership as incomplete");
                }
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

        // -- runtime load routes ------------------------------------------------------------

        /// <summary>
        /// The runtime's own binary catalog loader. ContentCatalogData.LoadFromFile is
        /// `internal static` in Addressables 2.4.6, so it is reached by exact signature.
        /// </summary>
        private static ContentCatalogData LoadFromFileWithRuntime(string path)
        {
            const BindingFlags flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            MethodInfo loader =
                FindMethod(typeof(ContentCatalogData), "LoadFromFile", flags, typeof(string), typeof(int)) ??
                FindMethod(typeof(ContentCatalogData), "LoadFromFile", flags, typeof(string));
            if (loader == null)
            {
                throw new MissingMethodException(
                    "ContentCatalogData.LoadFromFile is absent from the installed Addressables package (" +
                    AddressablesPackageVersion() + "); the binary catalog format is only readable through the matching runtime.");
            }

            ParameterInfo[] parameters = loader.GetParameters();
            object[] args = parameters.Length == 2 ? new object[] { path, RuntimeCacheSize } : new object[] { path };
            return (ContentCatalogData)loader.Invoke(null, args);
        }

        /// <summary>
        /// The locator factory the runtime itself calls after loading a content catalog
        /// (InitializationOperation.OnCatalogDataLoaded). Binary catalogs expose
        /// CreateCustomLocator(id, providerSuffix, cacheSize); the JSON catalog variant
        /// exposes CreateCustomLocator(id, providerSuffix) / CreateLocator(providerSuffix).
        /// </summary>
        private static IResourceLocator CreateLocatorWithRuntime(ContentCatalogData data, string locatorId)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            MethodInfo creator =
                FindMethod(typeof(ContentCatalogData), "CreateCustomLocator", flags, typeof(string), typeof(string), typeof(int)) ??
                FindMethod(typeof(ContentCatalogData), "CreateCustomLocator", flags, typeof(string), typeof(string));
            if (creator != null)
            {
                object[] args = creator.GetParameters().Length == 3
                    ? new object[] { locatorId, null, RuntimeCacheSize }
                    : new object[] { locatorId, null };
                var locator = creator.Invoke(data, args) as IResourceLocator;
                if (locator != null) return locator;
            }

            MethodInfo publicCreator = FindMethod(typeof(ContentCatalogData), "CreateLocator", flags, typeof(string));
            if (publicCreator != null)
            {
                var locator = publicCreator.Invoke(data, new object[] { null }) as IResourceLocator;
                if (locator != null) return locator;
            }

            throw new MissingMethodException(
                "no locator factory (CreateCustomLocator/CreateLocator) exists on ContentCatalogData in Addressables " +
                AddressablesPackageVersion() + ".");
        }

        /// <summary>
        /// Supported public route. It initialises the Addressables runtime first, which is what
        /// registers the ContentCatalogProvider the catalog location is resolved through; the
        /// returned handle is deliberately never released because the locator reads through it.
        /// </summary>
        private static IResourceLocator LoadWithPublicApi(string catalogCopy)
        {
            AsyncOperationHandle<IResourceLocator> init = Addressables.InitializeAsync();
            if (!init.IsDone) init.WaitForCompletion();

            AsyncOperationHandle<IResourceLocator> handle = Addressables.LoadContentCatalogAsync(catalogCopy, false);
            if (!handle.IsDone) handle.WaitForCompletion();

            if (handle.Status != AsyncOperationStatus.Succeeded || handle.Result == null)
            {
                string detail = handle.OperationException == null
                    ? "(no exception reported)"
                    : handle.OperationException.GetType().Name + ": " + handle.OperationException.Message;
                throw new InvalidOperationException("status=" + handle.Status + " " + detail);
            }

            return handle.Result;
        }

        // -- evidence helpers ---------------------------------------------------------------

        private static LocationRecord ToLocationRecord(IResourceLocation loc, string runtimePath)
        {
            var record = new LocationRecord
            {
                internal_id = loc.InternalId,
                shipped_internal_id = ToShippedInternalId(loc.InternalId, runtimePath),
                provider_id = loc.ProviderId,
                primary_key = loc.PrimaryKey,
                resource_type = loc.ResourceType == null ? null : loc.ResourceType.FullName,
                resource_type_assembly = loc.ResourceType == null ? null : loc.ResourceType.Assembly.GetName().Name,
                has_resource_type = loc.ResourceType != null,
                dependency_count = loc.Dependencies == null ? 0 : loc.Dependencies.Count,
                dependency_hash_code = loc.DependencyHashCode,
                has_dependencies = loc.HasDependencies,
                data_type = loc.Data == null ? null : loc.Data.GetType().FullName,
            };

            if (loc.Dependencies != null)
            {
                foreach (IResourceLocation dependency in loc.Dependencies)
                {
                    record.dependency_internal_ids.Add(dependency == null ? "(null)" : dependency.InternalId);
                }
            }

            return record;
        }

        /// <summary>
        /// The runtime expands "{UnityEngine.AddressableAssets.Addressables.RuntimePath}" through
        /// Addressables.ResolveInternalId before handing out IResourceLocation.InternalId, so the
        /// verbatim value is Editor-machine specific. This reverses exactly that one substitution
        /// (separators normalised to '/') so the value can be compared with the shipped
        /// StreamingAssets/aa tree. It is derived evidence, never the authoritative value.
        /// </summary>
        private static string ToShippedInternalId(string internalId, string runtimePath)
        {
            if (string.IsNullOrEmpty(internalId) || string.IsNullOrEmpty(runtimePath)) return null;
            string id = internalId.Replace('\\', '/');
            string prefix = runtimePath.Replace('\\', '/').TrimEnd('/');
            if (prefix.Length == 0 || !id.StartsWith(prefix + "/", StringComparison.OrdinalIgnoreCase)) return null;
            return RuntimePathPlaceholder + id.Substring(prefix.Length);
        }

        private static string LocationIdentity(IResourceLocation loc)
        {
            return (loc.InternalId ?? "(null)") + "|" + (loc.ProviderId ?? "(null)") + "|" +
                   (loc.ResourceType == null ? "(null)" : loc.ResourceType.FullName);
        }

        private static ProviderRecord DescribeProvider(ObjectInitializationData provider)
        {
            return new ProviderRecord
            {
                id = provider.Id,
                type_class = provider.ObjectType.ClassName,
                type_assembly = provider.ObjectType.AssemblyName,
                data = provider.Data,
            };
        }

        private static string ProviderTypeName(ObjectInitializationData provider)
        {
            string typeName = provider.ObjectType.Value == null
                ? provider.ObjectType.ClassName
                : provider.ObjectType.Value.FullName;
            return string.IsNullOrEmpty(provider.Id) ? typeName : provider.Id + " -> " + typeName;
        }

        private static void CollectLabels(DumpReport report)
        {
            try
            {
                var settings = UnityEditor.AddressableAssets.AddressableAssetSettingsDefaultObject.Settings;
                report.addressable_settings_present = settings != null;
                if (settings != null && settings.GetLabels() != null)
                {
                    report.labels.AddRange(settings.GetLabels().Where(l => !string.IsNullOrEmpty(l)));
                }
            }
            catch (Exception ex)
            {
                report.labels_error = Describe(ex);
            }
        }

        private static string AddressablesPackageVersion()
        {
            try
            {
                var info = UnityEditor.PackageManager.PackageInfo.FindForAssembly(typeof(ContentCatalogData).Assembly);
                if (info != null && !string.IsNullOrEmpty(info.version)) return info.version;
            }
            catch { /* fall through to the assembly version */ }
            Version v = typeof(ContentCatalogData).Assembly.GetName().Version;
            return v == null ? "(unknown)" : v.ToString();
        }

        private static string ReadHashSidecar(string catalogPath)
        {
            try
            {
                string hashPath = Path.ChangeExtension(catalogPath, ".hash");
                return File.Exists(hashPath) ? File.ReadAllText(hashPath).Trim() : null;
            }
            catch (Exception ex)
            {
                return "(unreadable: " + Describe(ex) + ")";
            }
        }

        private static string ResolveGameRoot()
        {
            // Same lookup order as tools/unity-rebuild/common.ps1: environment variable, then an
            // Editor override, then the repo's audit config. Nothing is hard-coded (section 3.1.4).
            string gameRoot = Environment.GetEnvironmentVariable("MANOSABA_GAME_ROOT");
            if (!string.IsNullOrEmpty(gameRoot)) return gameRoot;

            gameRoot = EditorPrefs.GetString("Manosaba.GameRoot", string.Empty);
            if (!string.IsNullOrEmpty(gameRoot)) return gameRoot;

            return ReadGameRootFromAuditConfig();
        }

        private static string ReadGameRootFromAuditConfig()
        {
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            foreach (string name in new[] { "audit.config.json", "audit.config.local.json" })
            {
                try
                {
                    string path = Path.GetFullPath(Path.Combine(projectRoot, "../tools/audit", name));
                    if (!File.Exists(path)) continue;

                    string json = File.ReadAllText(path).TrimStart('\uFEFF');
                    var config = JsonUtility.FromJson<AuditConfig>(json);
                    if (config != null && !string.IsNullOrEmpty(config.game_root)) return config.game_root;
                }
                catch (Exception ex)
                {
                    Debug.LogWarning("[CatalogDump] could not read " + name + ": " + Describe(ex));
                }
            }

            return string.Empty;
        }

        [Serializable]
        public class AuditConfig
        {
            public string game_root;
        }

        private static MethodInfo FindMethod(Type type, string name, BindingFlags flags, params Type[] signature)
        {
            return type.GetMethod(name, flags, null, signature, null);
        }

        private static string Describe(Exception ex)
        {
            if (ex is TargetInvocationException && ex.InnerException != null) ex = ex.InnerException;
            return ex.GetType().Name + ": " + ex.Message;
        }

        private static bool LooksLikeGuid(string key)
        {
            if (key == null || key.Length != 32) return false;
            foreach (char c in key)
            {
                bool hex = (c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F');
                if (!hex) return false;
            }
            return true;
        }

        private static int Blocked(DumpReport report, string outPath, string reason)
        {
            report.status = "BLOCKED";
            report.blocked_reason = reason;
            WriteReport(outPath, report);
            Debug.LogError("[CatalogDump] " + reason);
            return 2;
        }

        private static List<HistogramEntry> ToHistogram(SortedDictionary<string, int> map)
        {
            return map.Select(kv => new HistogramEntry { key = kv.Key, count = kv.Value }).ToList();
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
            // JsonUtility only serialises public fields of [Serializable] types -- no dictionaries.
            File.WriteAllText(path, JsonUtility.ToJson(report, true), new UTF8Encoding(false));
        }

        /// <summary>
        /// Side artifact path: the dump's own file name with a suffix, so it follows both the default
        /// location and the MANOSABA_CATALOG_OUT override without a second environment variable.
        /// </summary>
        private static string MembershipArtifactPath(string outPath)
        {
            string dir = Path.GetDirectoryName(outPath);
            string name = Path.GetFileNameWithoutExtension(outPath) + "_label_membership.tsv";
            return Path.Combine(dir ?? ".", name);
        }

        /// <summary>
        /// One "key&lt;TAB&gt;internal_id" line per key -&gt; location edge for every label-like key
        /// (a key reaching more than one location), preceded by a one-line comment header. The order
        /// matches `label_membership` in the JSON, so the two can be compared line by line.
        /// </summary>
        private static void WriteMembershipArtifact(string path, List<MembershipRecord> membership)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            var sb = new StringBuilder(4 * 1024 * 1024);
            sb.Append("# key\tinternal_id\t(lines: one per key->location edge; keys reaching >1 location only;\n");
            sb.Append("#  the same membership as the dump's label_membership section, in the same order)\n");
            foreach (MembershipRecord record in membership)
            {
                foreach (string internalId in record.locations)
                {
                    sb.Append(record.key).Append('\t').Append(internalId).Append('\n');
                }
            }
            File.WriteAllText(path, sb.ToString(), new UTF8Encoding(false));
        }

        [Serializable]
        public class DumpReport
        {
            public string status = "NOT_RUN";
            public string generated;
            public string blocked_reason;

            // ---- input identity -----------------------------------------------------------
            public string catalog_source;
            public long catalog_bytes;
            public string catalog_sha256;
            public string catalog_hash_file_content;
            public string addressables_package_version;
            public string runtime_path_used;

            // ---- which runtime route produced the data ------------------------------------
            public string load_route;
            public List<string> load_attempts = new List<string>();
            public string locator_id;
            public string provider_id;
            public string build_result_hash;
            public string local_hash;
            public ProviderRecord instance_provider_data = new ProviderRecord();
            public ProviderRecord scene_provider_data = new ProviderRecord();
            public List<ProviderRecord> resource_provider_data = new List<ProviderRecord>();
            public List<string> instance_provider_data_types = new List<string>();
            public List<string> scene_provider_data_types = new List<string>();

            // ---- totals -------------------------------------------------------------------
            public int location_count;
            public int location_edges;
            public int key_count;
            public int dependency_edges;
            public int null_locations;
            public int non_string_keys;
            public int guid_like_keys;
            public int keys_without_locations;
            public int keys_with_multiple_locations;
            public int keys_with_complete_locations;
            public int distinct_provider_ids;
            public int distinct_resource_types;

            // ---- labels -------------------------------------------------------------------
            public bool addressable_settings_present;
            public string labels_error;
            public List<string> labels = new List<string>();

            // ---- distributions ------------------------------------------------------------
            public List<HistogramEntry> provider_id_histogram = new List<HistogramEntry>();
            public List<HistogramEntry> resource_type_histogram = new List<HistogramEntry>();
            public List<HistogramEntry> internal_id_prefix_histogram = new List<HistogramEntry>();

            // ---- label-like keys (complete membership) ------------------------------------
            public int label_membership_count;
            public int label_membership_edges;
            public int label_membership_truncated_keys;
            public string label_membership_artifact;

            // ---- full data ----------------------------------------------------------------
            public List<LocationRecord> locations = new List<LocationRecord>();
            public List<KeyRecord> keys = new List<KeyRecord>();
            public List<MembershipRecord> label_membership = new List<MembershipRecord>();

            public List<string> limitations = new List<string>
            {
                "internal_id is the verbatim IResourceLocation.InternalId, i.e. after Addressables.ResolveInternalId " +
                "expanded the {" + RuntimePathPlaceholder + "} placeholder to this Editor's Addressables.RuntimePath " +
                "(see runtime_path_used); shipped_internal_id reverses that single substitution (separators normalised " +
                "to '/') and is derived evidence only. It is null when the InternalId does not start with " +
                "runtime_path_used, for example the AssetBundle locations that carry the placeholder with '\\' " +
                "separators.",
                "Keys, GUIDs and labels share one key table in the catalog, so lookups_like_guid / is_label are " +
                "classification hints, not proof of how a key is used.",
                "Each key lists at most " + MaxLocationsPerKey + " of its locations in `locations`; " +
                "`locations_complete` says whether that list is whole, and `label_membership` carries the complete " +
                "membership of every key that reaches more than one location (location_count is always complete).",
                "`label_membership` covers every key reaching more than one location -- label-like keys, not only " +
                "keys the Editor calls labels: a key such as the umbrella label `Naninovel` is how the game asks " +
                "Addressables for ~28k locations at once, and its membership is not recoverable from the " +
                "single-key lists alone. Each entry is capped at " + MaxMembershipLocationsPerKey + " locations and " +
                "sets locations_truncated if that cap is ever reached.",
                "Locations are de-duplicated by (InternalId, ProviderId, ResourceType); location_edges counts every " +
                "key -> location edge before de-duplication, and label_membership_edges counts the edges of the " +
                "label-like keys only (i.e. the lines written to the artifact; it falls short of the sum of their " +
                "location_count values only when the per-key cap was reached, which " +
                "label_membership_truncated_keys reports).",
                "Labels come from the project's own AddressableAssetSettings; a project without Addressables settings " +
                "reports addressable_settings_present=false and an empty label list. A key being listed in `labels` " +
                "is not proof that the shipped build declared it as a label -- the shipped build's own label " +
                "metadata is not in the catalog.",
            };
        }

        [Serializable]
        public class HistogramEntry
        {
            public string key;
            public int count;
        }

        [Serializable]
        public class ProviderRecord
        {
            public string id;
            public string type_class;
            public string type_assembly;
            public string data;
        }

        [Serializable]
        public class LocationRecord
        {
            public string internal_id;
            public string shipped_internal_id;
            public string provider_id;
            public string resource_type;
            public string resource_type_assembly;
            public bool has_resource_type;
            public string primary_key;
            public int dependency_count;
            public int dependency_hash_code;
            public bool has_dependencies;
            public string data_type;
            public List<string> dependency_internal_ids = new List<string>();
        }

        [Serializable]
        public class KeyRecord
        {
            public string key;
            public string key_type;
            public bool is_label;
            public bool looks_like_guid;
            public int location_count;
            public bool locations_truncated;
            public bool locations_complete;
            public string locate_error;
            public List<string> locations = new List<string>();
        }

        /// <summary>
        /// A label-like key (one reaching more than one location) and its complete location membership,
        /// so a consumer can derive the shipped addressing scheme without the 64-location cap of
        /// <see cref="KeyRecord.locations"/> hiding an umbrella key such as `Naninovel`.
        /// </summary>
        [Serializable]
        public class MembershipRecord
        {
            public string key;
            public string key_type;
            public bool looks_like_guid;
            public int location_count;
            public bool locations_truncated;
            public List<string> locations = new List<string>();
        }
    }
}

