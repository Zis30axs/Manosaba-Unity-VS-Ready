// Imports the shipped Naninovel *configuration* assets that the reconstructed project was missing.
//
//   Unity -batchmode -quit -projectPath UnityProject \
//         -executeMethod Manosaba.EditorTools.NaninovelConfigBuilder.Run -logFile -
//
// Why
// ---
// The engine boots through `Naninovel.RuntimeInitializer.OnApplicationLoaded -> Initialize -> Engine.Initialize`,
// and every engine service is handed a configuration object by `ProjectConfigurationProvider`, which resolves
// each `Naninovel.Configuration` subclass with
//
//     Resources.Load($"{resourcesPath}/{type.Name}", type) ?? ScriptableObject.CreateInstance(type)
//     (ProjectConfigurationProvider.cs:57-64, resourcesPath = "Naninovel/Configuration", :15)
//
// Because not one configuration asset had been imported, every service was built on a *default* configuration
// and the engine never finished initialising (measured: `Naninovel.InputManager.InitializeService()` threw a
// NullReferenceException from `Engine/<Initialize>d__54.MoveNext` at Engine.cs:150 - see
// UNITY_RUNTIME_INTEGRATION.md §33.2).  The engine also used the wrong `EngineConfiguration.InitializeOnApplicationLoad`
// (default `true`; the shipped asset says `false`), so the engine was self-initialising in `OnApplicationLoaded`
// instead of being initialised explicitly by the reconstructed `InitializeNaninovelAsyncCore`.
//
// This tool is the content half of that fix: it turns the byte-level decode of the shipped `resources.assets`
// (tools/unity-rebuild/decode_naninovel_configs.py -> out/unity/naninovel_configs.json) back into real
// configuration assets, at exactly the path the engine asks for:
//
//   Assets/NaninovelData/Resources/Naninovel/Configuration/<TypeName>.asset
//
// which is the same location Naninovel's own editor code writes to (`ConfigurationSettings.SaveConfigurationObject`,
// ConfigurationSettings.cs:184-193: `PackagePath.GeneratedDataPath + "/Resources/" + ProjectConfigurationProvider.DefaultResourcesPath`
// + `{type.Name}.asset`, GeneratedDataPath defaulting to "Assets/NaninovelData" - PackagePath.cs:59-69).
//
// What it does, per decoded object
// -------------------------------
//   1. resolve the type by name against the loaded assemblies (the real package at Packages/com.elringus.naninovel
//      and the game's own assemblies are both present at compile time); a type that cannot be resolved is
//      *reported* as a row with status TYPE_NOT_RESOLVED, never skipped silently
//   2. classify: an object is a configuration asset iff the shipped `ResourceManager.m_Container` key it was
//      reachable by starts with `naninovel/configuration/` (25 of the 31).  The other six are reported as
//      deliberately not imported, each with its own reason (see `NotImportedReason`)
//   3. create (or load and overwrite, so a second run is idempotent) the asset at the engine's lookup path
//   4. copy every decoded field onto a SerializedObject by *serialized name* - which is exactly what
//      SerializedProperty.FindProperty/FindPropertyRelative expect, because the decoder took the layout from
//      this project's own C# sources and every one of the 31 byte walks closes exactly.  Value kinds:
//      string/bool/float/int/enum/Vector2/Vector3/Vector2Int/Rect/Color/inline/array/PPtr/[SerializeReference]
//   5. handle object references by *classification*, never by guessing (see BindVerifiedReference)
//   6. write tools/unity-rebuild/out/unity/naninovel_config_import.json (status, a row per object, per-field
//      skips with reasons, references null/bound/needing binding, totals, limitations)
//
// The decode wraps *every* value in a {"kind": ..., "value": ...} node, m_Name included, so anything that needs a
// plain scalar is read through DecodedScalar/DecodedString/DecodedBool.  Reading a wrapper with a plain cast yields
// its JSON text: that is how the first run built an asset path out of the whole wrapper object and died in
// Path.Combine ("Illegal characters in path") on path_id 1465 - the one object whose *decoded* name is used as a
// file name.  Names are now unwrapped and then validated (IsUsableAssetName) before any path is built, so a name
// that cannot be a file name costs its own row (SKIPPED_INVALID_NAME, reported) and never the rest of the batch.
//
// Nothing is dropped silently: every decoded value node either lands on a SerializedProperty or appears in that
// object's `fields_skipped` list with a reason, and each imported row carries `values_accounted`
// (fields_applied + fields_skipped == values_total) so the accounting can be audited from the report alone.
//
// Exit codes: 0 = every configuration object was created/overwritten, 1 = PARTIAL/ERROR, 2 = BLOCKED
// (no input decode).  This mirrors the repo's other batchmode tools (ScriptableObjectConverter.cs:22).
//
// Relationship to the other editor tools: field application is deliberately local instead of reusing
// JsonToSerializedApplier.cs, for two reasons - that applier has no applier for Vector2Int (9 decoded values in
// `CameraConfigurationExtended.ResolutionsForPc` would be reported as unsupported and left at default), and its
// PPtr branch counts a pointer as "applied" without classifying it.  The string-before-isArray trap it documents
// is preserved here.
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
    public static class NaninovelConfigBuilder
    {
        private const string InputRelativePath = "../tools/unity-rebuild/out/unity/naninovel_configs.json";
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/naninovel_config_import.json";

        /// <summary>Where the engine looks: any `Resources` folder + "Naninovel/Configuration" (see the header).</summary>
        private const string ConfigurationFolder = "Assets/NaninovelData/Resources/Naninovel/Configuration";

        /// <summary>The shipped Resources-key prefix that makes an object a configuration asset.</summary>
        private const string ConfigurationResourcePrefix = "naninovel/configuration/";

        private const string NameKey = "m_Name";
        private const string ManagedReferencesKey = "_managedReferences";

        // --- the one verified reference identity ------------------------------------------------------------------
        // A decoded PPtr is a (m_FileID, m_PathID) pair into the shipped `resources.assets`; neither can be turned
        // into a project asset.  The single reference this pass *can* identify unambiguously is the compiler
        // localization: the package ships the very same object and Naninovel's own editor code hard-codes the path
        // when ScriptsConfiguration.CompilerLocalization is empty:
        //
        //     var path = PathUtils.Combine(PackagePath.PrefabsPath, "DefaultCompiler.asset");
        //     var asset = AssetDatabase.LoadAssetAtPath<CompilerLocalization>(path);
        //     property.objectReferenceValue = asset;                       (ScriptsSettings.cs:57-59)
        //
        // Verified statically as well: m_Script guid ea4376a87ff645beb74ce0726bf667f9 == CompilerLocalization.cs.meta,
        // and the decoded PPtr's m_PathID (1485) is the decoded `Naninovel.CompilerLocalization` object itself.
        private const string DefaultCompilerAssetPath = "Packages/com.elringus.naninovel/Prefabs/DefaultCompiler.asset";
        private const string DefaultCompilerTypeName = "Naninovel.CompilerLocalization";
        private const string DefaultCompilerObjectName = "DefaultCompiler";

        private const int MaxMessagesPerRow = 200;
        private const int MaxScalarSummary = 40;
        private const int MaxCandidatePaths = 5;
        private const int MaxCandidateScan = 200;

        /// <summary>A decoded name longer than this is not used as a file name (see IsUsableAssetName). The longest
        /// configuration type name in this project is 60 characters, so this cannot reject a legitimate asset.</summary>
        private const int MaxAssetNameLength = 128;

        [MenuItem("Manosaba/Import Naninovel Configurations")]
        public static void Run ()
        {
            int code = Execute();
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        public static int Execute ()
        {
            var result = new ImportResult { generated = DateTime.UtcNow.ToString("o") };
            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string inputPath = Path.GetFullPath(Path.Combine(projectRoot, InputRelativePath));
            string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));
            result.input = inputPath;
            result.asset_folder = ConfigurationFolder;
            result.engine_lookup = "Resources.Load(\"Naninovel/Configuration/\" + type.Name) - ProjectConfigurationProvider.cs:57";

            int exitCode = 1;
            try
            {
                if (!File.Exists(inputPath))
                {
                    result.status = "BLOCKED";
                    result.error = "input decode not found: " + inputPath +
                                   " (run tools/unity-rebuild/decode_naninovel_configs.py --write)";
                    exitCode = 2;
                }
                else
                {
                    result.input_bytes = new FileInfo(inputPath).Length;
                    JObject document = JObject.Parse(File.ReadAllText(inputPath));
                    JArray objects = document["objects"] as JArray;
                    if (objects == null || objects.Count == 0)
                    {
                        result.status = "BLOCKED";
                        result.error = "the decode contains no `objects` array: " + inputPath;
                        exitCode = 2;
                    }
                    else
                    {
                        result.decode_generated_by = DecodedString(document["generated_by"]);
                        result.objects_total = objects.Count;

                        // The decode names every object it could walk; keeping the path_id -> (class, name) table
                        // lets a PPtr be cross-checked against the object it pointed at ("path_id 1485 == the
                        // decoded `Naninovel.CompilerLocalization` named DefaultCompiler"), which is evidence
                        // rather than an assumption.
                        decodedByPathId = new Dictionary<long, JObject>();
                        foreach (JToken token in objects)
                        {
                            var decodedRow = token as JObject;
                            if (decodedRow == null || decodedRow["path_id"] == null) continue;
                            decodedByPathId[ReadLong(DecodedScalar(decodedRow["path_id"]))] = decodedRow;
                        }

                        EnsureFolder(ConfigurationFolder);
                        var claimedPaths = new Dictionary<string, long>(StringComparer.Ordinal);

                        foreach (JToken token in objects)
                        {
                            var row = new ObjectRow();
                            result.rows.Add(row);
                            try
                            {
                                ImportOne((JObject)token, row, claimedPaths, result);
                            }
                            catch (Exception ex)
                            {
                                row.status = "ERROR";
                                row.reason = ex.GetType().Name + ": " + ex.Message;
                                row.notes.Add(ex.ToString());
                            }
                        }

                        AssetDatabase.SaveAssets();
                        Tally(result);
                        result.status = result.configuration_failed > 0 || result.status == "PARTIAL" ? "PARTIAL" : "OK";
                        exitCode = result.status == "OK" ? 0 : 1;
                    }
                }
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.error = ex.ToString();
                Debug.LogError("[NaninovelConfigBuilder] " + ex);
                exitCode = 1;
            }

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outPath));
                File.WriteAllText(outPath, JsonUtility.ToJson(result, true), new UTF8Encoding(false));
            }
            catch (Exception ex)
            {
                Debug.LogError("[NaninovelConfigBuilder] failed to write " + outPath + ": " + ex.Message);
            }

            Debug.Log("[NaninovelConfigBuilder] " + result.status +
                      " objects=" + result.objects_total +
                      " configuration=" + result.configuration_objects + " created=" + result.configuration_created +
                      " overwritten=" + result.configuration_overwritten + " failed=" + result.configuration_failed +
                      " not_imported=" + result.not_imported +
                      " fields_applied=" + result.fields_applied + " fields_skipped=" + result.fields_skipped +
                      " refs_null=" + result.references_null + " refs_bound=" + result.references_bound +
                      " refs_needing_binding=" + result.references_needing_binding +
                      " wrote " + outPath);
            return exitCode;
        }

        // ------------------------------------------------------------------------------------------------------------
        // One decoded object
        // ------------------------------------------------------------------------------------------------------------

        private static void ImportOne (JObject decoded, ObjectRow row, Dictionary<string, long> claimedPaths,
                                       ImportResult result)
        {
            row.path_id = ReadLong(DecodedScalar(decoded["path_id"]));
            row.type_name = DecodedString(decoded["class"]) ?? "";
            string resourcesPath = DecodedString(decoded["resources_path"]);
            row.resources_path = resourcesPath;
            // m_Name is a *typed wrapper* in the decode ({"kind":"string","value":"..."}), like every other value, so
            // it must be unwrapped before it can be used as a name.  Reading the wrapper directly is what produced
            // "Illegal characters in path" on path_id 1465: the asset path came out as the whole JSON object.
            string decodedName = DecodedString(decoded["values"] == null ? null : decoded["values"][NameKey]) ?? "";
            row.decoded_name = decodedName;
            row.is_configuration = !string.IsNullOrEmpty(resourcesPath) &&
                                   resourcesPath.StartsWith(ConfigurationResourcePrefix, StringComparison.OrdinalIgnoreCase);

            JObject values = decoded["values"] as JObject;
            row.values_total = values == null ? 0 : values.Properties().Sum(p => CountValues(p.Value));
            if (values != null) row.decoded_scalars.AddRange(SummarizeScalars(values));

            if (!row.is_configuration)
            {
                row.status = "SKIPPED_NOT_CONFIGURATION";
                row.reason = NotImportedReason(row);
                // Explicit, so "every decoded value is accounted for" holds for the whole file and not just for
                // the imported objects: the values themselves stay in the input decode.
                row.fields_skipped_list.Add(new SkippedValue {
                    field = row.type_name,
                    kind = "object",
                    reason = "whole object deliberately not imported: " + row.reason +
                             " (" + row.values_total + " decoded values remain in " + Path.GetFileName(InputRelativePath) +
                             " at path_id " + row.path_id + ")"
                });
                row.fields_skipped = 1;
                AppendNote(row, "not a configuration asset: nothing was created for it");
                return;
            }

            Type type = FindType(row.type_name);
            if (type == null)
            {
                row.status = "TYPE_NOT_RESOLVED";
                row.reason = "no loaded assembly declares '" + row.type_name + "' (searched " +
                             AppDomain.CurrentDomain.GetAssemblies().Length + " assemblies; the real package and the " +
                             "game assemblies must both be compiled before this tool can run)";
                return;
            }
            row.type_resolved = type.Assembly.GetName().Name;
            if (!typeof(ScriptableObject).IsAssignableFrom(type))
            {
                row.status = "NOT_A_SCRIPTABLEOBJECT";
                row.reason = type.FullName + " (" + type.Assembly.GetName().Name + ") is not a ScriptableObject";
                return;
            }
            if (!DerivesFromConfiguration(type))
                result.warnings.Add(row.type_name + " does not derive from Naninovel.Configuration; created anyway");

            // --- asset path -----------------------------------------------------------------------------------
            // The engine asks for `<TypeName>`; the decode also carries the Resources key the object was reachable
            // by, so the two can be compared instead of assumed.  They agree for 24 of the 25 configuration
            // objects.  The 25th is the second `WitchTrialsChoiceHandlersConfiguration` instance: same type, a
            // different m_Name and a different Resources key ("choicehandlersconfigurationwitchtrials"), i.e. an
            // asset no type-name lookup can ever reach.  It is restored under its own decoded name so that its
            // content is not lost, and it is recorded as such - see `secondary_of_type`.
            string assetName = string.IsNullOrEmpty(decodedName) ? type.Name : decodedName;
            bool canonical = resourcesPath.Equals(ConfigurationResourcePrefix + type.Name.ToLowerInvariant(),
                                                  StringComparison.OrdinalIgnoreCase);
            // The name goes into a file path, so it is validated *before* any path is built: Path.Combine throws
            // ArgumentException("Illegal characters in path") on the wrapper text that broke path_id 1465, and a tool
            // that throws out of the middle of a batch must not be able to leave the remaining objects unimported.
            string fileName = canonical ? type.Name : assetName;
            if (!IsUsableAssetName(fileName))
            {
                row.status = "SKIPPED_INVALID_NAME";
                row.reason = "no path was built and no asset was created, because the name cannot be an asset file " +
                             "name: " + DescribeInvalidName(fileName) +
                             (canonical ? " (the name is the configuration type name)" : " (the name is the decoded m_Name)");
                return;
            }
            row.secondary_of_type = !canonical;
            string assetPath = ConfigurationFolder + "/" + fileName + ".asset";
            row.asset_path = assetPath;
            row.asset_name = fileName;
            if (!canonical)
                AppendNote(row, "the shipped Resources key is '" + resourcesPath + "', which is not " +
                                "'naninovel/configuration/" + type.Name.ToLowerInvariant() + "': this is a second " +
                                "instance of a configuration type whose engine path ('" + type.Name + ".asset') is " +
                                "written by the object the key does match, so it is restored under its own decoded " +
                                "m_Name '" + assetName + "'");

            if (claimedPaths.TryGetValue(assetPath, out long otherPathId))
            {
                row.status = "DUPLICATE_ASSET_PATH";
                row.reason = "path_id " + otherPathId + " already claimed " + assetPath + " in this run";
                return;
            }
            claimedPaths[assetPath] = row.path_id;

            // --- load or create (idempotency) -----------------------------------------------------------------
            ScriptableObject instance = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
            bool createdNow = false;
            if (instance == null)
            {
                string absolute = AbsoluteAssetPath(assetPath);
                if (absolute != null && File.Exists(absolute))
                {
                    row.status = "FAILED";
                    row.reason = "a file already exists at " + assetPath +
                                 " but does not load as a ScriptableObject (missing script?); refusing to overwrite";
                    return;
                }
                instance = ScriptableObject.CreateInstance(type);
                if (instance == null)
                {
                    row.status = "FAILED";
                    row.reason = "ScriptableObject.CreateInstance returned null for " + type.FullName;
                    return;
                }
                instance.name = row.asset_name;
                AssetDatabase.CreateAsset(instance, assetPath);
                createdNow = true;
            }
            else if (instance.GetType() != type)
            {
                row.status = "FAILED";
                row.reason = "the existing asset " + assetPath + " holds a " + instance.GetType().FullName +
                             ", not " + type.FullName + "; refusing to overwrite";
                return;
            }

            // --- values ---------------------------------------------------------------------------------------
            var serialized = new SerializedObject(instance);
            if (values != null)
            {
                foreach (JProperty field in values.Properties())
                {
                    string name = field.Name;
                    if (name == NameKey)
                    {
                        row.fields_applied++;
                        row.notes.Add("m_Name = '" + decodedName + "' applied as the asset name");
                        continue;
                    }
                    if (name == ManagedReferencesKey)
                    {
                        // Not a class field: Unity appends it to the *object* because the class holds
                        // [SerializeReference] members.  Applying it through FindProperty would be wrong.
                        var registry = field.Value;
                        Skip(row, name, "managed_references_registry",
                             "trailing ManagedReferencesRegistry, not a serialized field of " + type.Name +
                             " (m_Version " + ReadLong(DecodedScalar(registry["m_Version"])) + ", m_RefCount " +
                             ReadLong(DecodedScalar(registry["m_RefCount"])) + "): nothing to restore, every " +
                             "[SerializeReference] in this object is the null sentinel");
                        continue;
                    }
                    SerializedProperty property = serialized.FindProperty(name);
                    if (property == null)
                    {
                        Skip(row, name, KindOf(field.Value),
                             "the current " + type.Name + " has no serialized field with this name (the decode and " +
                             "this project's sources disagree; the value is left at the C# default)");
                        continue;
                    }
                    ApplyValue(property, field.Value, name, row);
                }
            }
            serialized.ApplyModifiedPropertiesWithoutUndo();
            EditorUtility.SetDirty(instance);

            if (row.fields_applied + row.fields_skipped != row.values_total)
            {
                result.warnings.Add(row.type_name + " (path_id " + row.path_id + "): applied " + row.fields_applied +
                                    " + skipped " + row.fields_skipped + " != decoded " + row.values_total);
            }
            else row.values_accounted = true;

            row.status = createdNow ? (row.secondary_of_type ? "CREATED_SECONDARY" : "CREATED") : "OVERWRITTEN";
            if (!createdNow)
                AppendNote(row, "the asset already existed (Naninovel's editor writes defaults into this folder - " +
                                "ConfigurationSettings.SaveConfigurationObject) and was overwritten with the decoded values");
        }

        /// <summary>
        /// Why an object that is not reachable by a `naninovel/configuration/...` key is deliberately not
        /// converted into an asset.  Each reason names the code path (or the package asset) that owns the object
        /// instead, so "not imported" is a decision with evidence rather than a gap.
        /// </summary>
        private static string NotImportedReason (ObjectRow row)
        {
            switch (row.type_name)
            {
                case "Naninovel.EngineVersion":
                    return "not a Naninovel.Configuration: the package already ships this exact object as " +
                           "Resources/Naninovel/EngineVersion.asset (EngineVersion.LoadFromResources -> " +
                           "Engine.LoadInternalResource, EngineVersion.cs:27-30). Writing a second asset under a " +
                           "Resources folder would duplicate the key 'Naninovel/EngineVersion' and make which copy " +
                           "wins arbitrary. Decoded values are recorded in decoded_scalars (the package's copy " +
                           "differs only in buildDate); if the version check ever needs the shipped build date, " +
                           "that is a one-field edit to the package asset, not a reason to duplicate it.";
                case "Naninovel.ProjectStats":
                    return "not a Naninovel.Configuration: resolved with Resources.Load<ProjectStats>(\"Naninovel/ProjectStats\") " +
                           "and (re)generated at build time into the transient resources folder by " +
                           "ProjectStatsResolver.GetOrCreateAsset() (Editor/Tools/ProjectStatsResolver.cs:63-69). A " +
                           "static copy would shadow that generator. Its three counters are recorded in " +
                           "decoded_scalars and are the input for a later pass that decides whether to freeze them.";
                case "Naninovel.ProjectResources":
                    return "not a Naninovel.Configuration: ProjectResources.Get() resolves it through " +
                           "Resources.Load(\"UnityCommon/ProjectResources\") (ProjectResources.cs:17-25). The shipped " +
                           "asset lists 276 resource paths of the *shipped* Addressables layout; importing it here " +
                           "would advertise paths this project does not have, which is worse than the empty " +
                           "fallback instance the runtime already creates.";
                case "Naninovel.CompilerLocalization":
                    return "not a Naninovel.Configuration: the package ships the same object at " +
                           DefaultCompilerAssetPath + " (m_Name 'DefaultCompiler'), and this pass binds " +
                           "ScriptsConfiguration.CompilerLocalization to that asset - the same assignment " +
                           "Naninovel's own script settings editor performs (ScriptsSettings.cs:57-59). Importing " +
                           "a second copy would be a duplicate of a package asset.";
                case "Naninovel.UI.LoadingIcon":
                    // The two decoded values are animationSpeed 100 and animationDelay 2; the package's prefabs were
                    // read to confirm they already carry exactly those values (not assumed).
                    return "a component instance, not an asset: the decode carries a MonoBehaviour (m_GameObject " +
                           "PPtr) that lived on a prefab inside the shipped resources.assets and has no Resources key " +
                           "(" + (row.resources_path == null ? "resources_path is null" : "resources_path '" + row.resources_path + "'") +
                           "). Nothing is lost: the package's own prefabs carry a LoadingIcon component " +
                           "(guid ea2dea69bf62c2c40b68cc3b3b3a9fad in Resources/Naninovel/EngineInitializationUI.prefab " +
                           "and Prefabs/DefaultUI/LoadingUI.prefab) whose serialized values (animationSpeed 100, " +
                           "animationDelay 2) are identical to the decoded shipped values.";
                case "Naninovel.UI.EngineInitializationUI":
                    return "a component instance, not an asset: the decode carries a MonoBehaviour (m_GameObject " +
                           "PPtr) that lived on a prefab inside the shipped resources.assets and has no Resources key " +
                           "(" + (row.resources_path == null ? "resources_path is null" : "resources_path '" + row.resources_path + "'") +
                           "). The package's Resources/Naninovel/EngineInitializationUI.prefab carries an " +
                           "EngineInitializationUI component (guid e489904e88864efb8c67cd3b69db2be5) whose serialized " +
                           "values (disableInteraction 0, visibleOnAwake 0, controlOpacity 1, fadeTime 0.3, " +
                           "ignoreTimeScale 1, focusObject null, focusMode 0) are identical to the decoded shipped " +
                           "values, and EngineConfiguration.CustomInitializationUI is deliberately left unbound so the " +
                           "engine falls back to that prefab (RuntimeInitializer.cs:47-49). The shipped object is a " +
                           "*game* component (WitchTrials.Views.EngineInitializationUiModified) whose UnityEvent wiring " +
                           "(onInitializationProgress -> Slider.set_value, path_id 1793) belongs to a prefab this " +
                           "project does not have yet; restoring it is a separate, prefab-shaped job.";
                default:
                    return "not reachable by a '" + ConfigurationResourcePrefix + "...' Resources key" +
                           (row.resources_path == null ? " (resources_path is null)" : " (resources_path '" + row.resources_path + "')") +
                           ", so no engine lookup would ever read an asset created for it";
            }
        }

        // ------------------------------------------------------------------------------------------------------------
        // Decoded value -> SerializedProperty
        // ------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Copy one decoded value node onto its property.  <paramref name="path"/> is the dotted
        /// SerializedProperty-ish path used in the report ("Metadata.metas[3].CustomSpriteMaterial"), which is how
        /// a reader can find the same node in the input decode.
        /// </summary>
        private static void ApplyValue (SerializedProperty property, JToken token, string path, ObjectRow row)
        {
            if (token == null || token.Type == JTokenType.Null)
            {
                row.fields_applied++;
                return;
            }
            if (token.Type != JTokenType.Object)
            {
                Skip(row, path, token.Type.ToString(), "the decoded value is a bare JSON " + token.Type +
                                                       ", not a {kind, value} node this applier understands");
                return;
            }

            string kind = KindOf(token);
            switch (kind)
            {
                case "string":
                    // A string is reported by Unity as isArray == true, so this case must precede the array branch.
                    if (property.propertyType != SerializedPropertyType.String)
                    {
                        Skip(row, path, kind, "the current field is " + property.propertyType + ", not a string");
                        return;
                    }
                    // Type names in the renamed Assembly-CSharp (ActorMetadata.Implementation for the WitchTrials
                    // background and the debate printer) are rewritten to the reconstruction's assembly name, the same
                    // rule the prefab/scenario applier uses (JsonToSerializedApplier.RemapAssemblyQualifiedName).
                    string decoded = DecodedString(token) ?? "";
                    string remapped = JsonToSerializedApplier.RemapAssemblyQualifiedName(decoded);
                    if (!ReferenceEquals(remapped, decoded))
                        row.notes.Add(path + ": assembly-qualified type name remapped: " + decoded + " -> " + remapped);
                    property.stringValue = remapped;
                    row.fields_applied++;
                    return;

                case "bool":
                    if (property.propertyType != SerializedPropertyType.Boolean)
                    {
                        Skip(row, path, kind, "the current field is " + property.propertyType + ", not a bool");
                        return;
                    }
                    property.boolValue = DecodedBool(token, false);
                    row.fields_applied++;
                    return;

                case "float":
                    // SerializedPropertyType has no Double member (verified against Unity 6000.0.48f1's
                    // UnityEditor.CoreModule), so a `double` field reports as Float and is written through
                    // floatValue.  No decoded field in this dataset is a double (the decode contains 0 f64 slots),
                    // so the lossy case cannot arise here; if a future field is a double, this branch still writes it
                    // and the row's values_accounted check keeps the accounting honest.
                    if (property.propertyType != SerializedPropertyType.Float)
                    {
                        Skip(row, path, kind, "the current field is " + property.propertyType + ", not a float");
                        return;
                    }
                    property.floatValue = ReadFloat(DecodedScalar(token));
                    row.fields_applied++;
                    return;

                case "int":
                case "enum":
                    ApplyInteger(property, token, path, kind, row);
                    return;

                case "vec2":
                    ApplyVector(property, token, path, kind, row, 2);
                    return;
                case "vec3":
                    ApplyVector(property, token, path, kind, row, 3);
                    return;
                case "vec4":
                    ApplyVector(property, token, path, kind, row, 4);
                    return;

                case "quat":
                    if (property.propertyType != SerializedPropertyType.Quaternion)
                    {
                        Skip(row, path, kind, "the current field is " + property.propertyType + ", not a Quaternion");
                        return;
                    }
                    {
                        JArray q = token["value"] as JArray;
                        property.quaternionValue = new Quaternion(At(q, 0), At(q, 1), At(q, 2), At(q, 3, 1f));
                    }
                    row.fields_applied++;
                    return;

                case "rect":
                    if (property.propertyType != SerializedPropertyType.Rect)
                    {
                        Skip(row, path, kind, "the current field is " + property.propertyType + ", not a Rect");
                        return;
                    }
                    {
                        JArray r = token["value"] as JArray;
                        property.rectValue = new Rect(At(r, 0), At(r, 1), At(r, 2), At(r, 3));
                    }
                    row.fields_applied++;
                    return;

                case "vec2int":
                    if (property.propertyType == SerializedPropertyType.Vector2Int)
                    {
                        JArray vi = token["value"] as JArray;
                        property.vector2IntValue = new Vector2Int(AtInt(vi, 0), AtInt(vi, 1));
                        row.fields_applied++;
                        return;
                    }
                    Skip(row, path, kind, "the current field is " + property.propertyType + ", not a Vector2Int");
                    return;

                case "vec3int":
                    if (property.propertyType == SerializedPropertyType.Vector3Int)
                    {
                        JArray v3i = token["value"] as JArray;
                        property.vector3IntValue = new Vector3Int(AtInt(v3i, 0), AtInt(v3i, 1), AtInt(v3i, 2));
                        row.fields_applied++;
                        return;
                    }
                    Skip(row, path, kind, "the current field is " + property.propertyType + ", not a Vector3Int");
                    return;

                case "rectint":
                    if (property.propertyType == SerializedPropertyType.RectInt)
                    {
                        JArray ri = token["value"] as JArray;
                        property.rectIntValue = new RectInt(AtInt(ri, 0), AtInt(ri, 1), AtInt(ri, 2), AtInt(ri, 3));
                        row.fields_applied++;
                        return;
                    }
                    Skip(row, path, kind, "the current field is " + property.propertyType + ", not a RectInt");
                    return;

                case "color":
                    if (property.propertyType != SerializedPropertyType.Color)
                    {
                        Skip(row, path, kind, "the current field is " + property.propertyType + ", not a Color");
                        return;
                    }
                    {
                        JArray c = token["value"] as JArray;
                        property.colorValue = new Color(At(c, 0), At(c, 1), At(c, 2), At(c, 3, 1f));
                    }
                    row.fields_applied++;
                    return;

                case "color32":
                    if (property.propertyType != SerializedPropertyType.Color)
                    {
                        Skip(row, path, kind, "the current field is " + property.propertyType + ", not a Color");
                        return;
                    }
                    {
                        JArray c32 = token["value"] as JArray;
                        property.colorValue = new Color32((byte)AtInt(c32, 0), (byte)AtInt(c32, 1),
                                                          (byte)AtInt(c32, 2), (byte)AtInt(c32, 3, 255));
                    }
                    row.fields_applied++;
                    return;

                case "array":
                    ApplyArray(property, token, path, row);
                    return;

                case "inline":
                    ApplyInline(property, token, path, row);
                    return;

                case "pptr":
                    ApplyReference(property, token, path, row);
                    return;

                case "managed_ref":
                {
                    long rawId = ReadLong(DecodedScalar(token["raw_id"]));
                    if (rawId >= 0)
                    {
                        Skip(row, path, kind, "[SerializeReference] holds a live reference (raw id " + rawId +
                                              ") whose polymorphic payload the decode does not carry");
                        return;
                    }
                    // Unity's "no reference" sentinel (0xFFFFFFFFFFFFFFFE = -2) - the shipped object is null here.
                    if (property.propertyType == SerializedPropertyType.ManagedReference)
                        property.managedReferenceValue = null;
                    row.fields_applied++;
                    AppendNote(row, path + ": [SerializeReference] is the null sentinel (raw_id " + rawId +
                                    "); left null" + (property.propertyType == SerializedPropertyType.ManagedReference
                                        ? "" : " (the current field is " + property.propertyType + ", not a managed reference)"));
                    return;
                }

                case "unity_event":
                    Skip(row, path, kind, "UnityEvent listener lists are not restored by this pass");
                    return;

                default:
                    Skip(row, path, kind, "no applier for decoded value kind '" + kind + "'");
                    return;
            }
        }

        private static void ApplyInteger (SerializedProperty property, JToken token, string path, string kind, ObjectRow row)
        {
            long value = ReadLong(DecodedScalar(token));
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    property.longValue = value;
                    row.fields_applied++;
                    return;
                case SerializedPropertyType.Enum:
                    property.intValue = unchecked((int)value);
                    CheckEnumValue(property, DecodedString(token["enum_type"]), value, path, row);
                    row.fields_applied++;
                    return;
                case SerializedPropertyType.LayerMask:
                case SerializedPropertyType.Character:
                    property.intValue = unchecked((int)value);
                    row.fields_applied++;
                    return;
                default:
                    Skip(row, path, kind, "the current field is " + property.propertyType + ", not an integer/enum");
                    return;
            }
        }

        /// <summary>
        /// A decoded enum is the raw integer Unity serialized.  It is written as the raw integer (that is what
        /// intValue stores), but a value with no name in the *current* enum is worth saying out loud: it means the
        /// shipped build and this project disagree about that enum.
        /// </summary>
        private static void CheckEnumValue (SerializedProperty property, string enumTypeName, long value, string path,
                                           ObjectRow row)
        {
            if (value >= 0 && value < property.enumNames.Length) return;
            Type enumType = FindType(enumTypeName);
            if (enumType != null && enumType.IsEnum && enumType.IsDefined(typeof(FlagsAttribute), false))
            {
                AppendNote(row, path + ": " + value + " is a [Flags] combination of " + enumTypeName +
                                " with no single name; written as the raw integer");
                return;
            }
            AppendNote(row, path + ": enum value " + value + " has no name in the current " + (enumTypeName ?? "enum") +
                            " (" + property.enumNames.Length + " names); written as the raw integer, which is how Unity " +
                            "serializes it either way");
        }

        private static void ApplyVector (SerializedProperty property, JToken token, string path, string kind,
                                        ObjectRow row, int size)
        {
            SerializedPropertyType expected = size == 2 ? SerializedPropertyType.Vector2
                : size == 3 ? SerializedPropertyType.Vector3 : SerializedPropertyType.Vector4;
            if (property.propertyType != expected)
            {
                Skip(row, path, kind, "the current field is " + property.propertyType + ", not a " + expected);
                return;
            }
            JArray v = token["value"] as JArray;
            if (size == 2) property.vector2Value = new Vector2(At(v, 0), At(v, 1));
            else if (size == 3) property.vector3Value = new Vector3(At(v, 0), At(v, 1), At(v, 2));
            else property.vector4Value = new Vector4(At(v, 0), At(v, 1), At(v, 2), At(v, 3));
            row.fields_applied++;
        }

        private static void ApplyArray (SerializedProperty property, JToken token, string path, ObjectRow row)
        {
            if (!property.isArray)
            {
                Skip(row, path, "array", "the current field is " + property.propertyType + " and not an array/list");
                return;
            }
            JArray items = token["items"] as JArray;
            int count = items == null ? 0 : items.Count;
            int declared = (int)ReadLong(DecodedScalar(token["count"]));
            if (declared != count)
                AppendNote(row, path + ": the decode declares " + declared + " items but carries " + count);
            property.arraySize = count;
            row.fields_applied++;
            for (int i = 0; i < count; i++)
            {
                SerializedProperty element = property.GetArrayElementAtIndex(i);
                if (element == null)
                {
                    Skip(row, path + "[" + i + "]", KindOf(items[i]), "no serialized element at this index");
                    continue;
                }
                ApplyValue(element, items[i], path + "[" + i + "]", row);
            }
        }

        private static void ApplyInline (SerializedProperty property, JToken token, string path, ObjectRow row)
        {
            if (property.propertyType != SerializedPropertyType.Generic || property.isArray)
            {
                Skip(row, path, "inline", "the current field is " + property.propertyType +
                                          (property.isArray ? " (an array)" : "") + ", not an inlined serializable class");
                return;
            }
            JObject fields = token["fields"] as JObject;
            if (fields == null)
            {
                Skip(row, path, "inline", "the decoded inline value carries no fields");
                return;
            }
            row.fields_applied++;
            foreach (JProperty child in fields.Properties())
            {
                SerializedProperty childProperty = property.FindPropertyRelative(child.Name);
                if (childProperty == null)
                {
                    Skip(row, path + "." + child.Name, KindOf(child.Value),
                         "no serialized field with this name under " + path + " (the decode and this project's " +
                         "sources disagree; the value is left at the C# default)");
                    continue;
                }
                ApplyValue(childProperty, child.Value, path + "." + child.Name, row);
            }
        }

        // ------------------------------------------------------------------------------------------------------------
        // Object references
        // ------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// A decoded PPtr is (m_FileID, m_PathID) into the shipped `resources.assets`; neither is a project path, so
        /// it is *classified* and never guessed:
        ///
        ///   * (0, 0)              -> the shipped field is null: the property is set to null and counted
        ///   * a verified identity -> bound, with the evidence that makes it unambiguous (BindVerifiedReference)
        ///   * anything else       -> left null and recorded in `needs_binding` with the target class, name and
        ///                            path_id, plus (for information only, never auto-bound) whether this project
        ///                            holds assets that *look* like the target - see FindCandidates
        /// </summary>
        private static void ApplyReference (SerializedProperty property, JToken token, string path, ObjectRow row)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                Skip(row, path, "pptr", "the current field is " + property.propertyType + ", not an object reference");
                return;
            }

            long fileId = ReadLong(DecodedScalar(token["m_FileID"]));
            long pathId = ReadLong(DecodedScalar(token["m_PathID"]));
            string targetClass = DecodedString(token["target_class"]);
            string targetName = DecodedString(token["target_name"]);
            row.references_total++;

            var record = new ReferenceRecord {
                field = path,
                target_class = targetClass,
                target_name = targetName,
                target_file_id = fileId,
                target_path_id = pathId,
                target_name_reliable = DecodedBool(token["target_name_reliable"], true)
            };
            if (fileId == 0)
            {
                JObject decodedTarget;
                if (decodedByPathId != null && decodedByPathId.TryGetValue(pathId, out decodedTarget))
                {
                    string targetNameKey = DecodedString(decodedTarget["values"] == null
                        ? null : decodedTarget["values"][NameKey]) ?? "";
                    record.decoded_object_match = "path_id " + pathId + " is the decoded object '" +
                                                  (DecodedString(decodedTarget["class"]) ?? "") + "' (m_Name '" +
                                                  targetNameKey + "', resources_path " +
                                                  (DecodedString(decodedTarget["resources_path"]) ?? "null") + ")";
                }
            }

            if (fileId == 0 && pathId == 0)
            {
                property.objectReferenceValue = null;
                row.references_null++;
                row.fields_applied++;
                return;
            }

            UnityEngine.Object bound = BindVerifiedReference(record);
            if (bound != null)
            {
                property.objectReferenceValue = bound;
                row.references_bound++;
                row.references_bound_list.Add(record);
                row.fields_applied++;
                return;
            }

            property.objectReferenceValue = null;
            row.references_needing_binding++;
            row.needs_binding.Add(record);
            row.fields_applied++;   // the field itself was written (as null); the *reference* is what is deferred
        }

        /// <summary>
        /// The one reference identity this project can establish without guessing: `Naninovel.CompilerLocalization`
        /// named "DefaultCompiler" is `Packages/com.elringus.naninovel/Prefabs/DefaultCompiler.asset`, because
        /// Naninovel's own editor code assigns exactly that path (ScriptsSettings.cs:57-59) and the shipped PPtr's
        /// target is the decoded `Naninovel.CompilerLocalization` object itself.  Anything else returns null and is
        /// reported; a null here is a stated gap, not a silent one.
        /// </summary>
        private static UnityEngine.Object BindVerifiedReference (ReferenceRecord record)
        {
            if (record.target_file_id != 0)
            {
                record.reason = "the shipped reference lives in another serialized file (m_FileID " +
                                record.target_file_id + "), so no path_id mapping inside resources.assets can ever " +
                                "resolve it";
                FindCandidates(record);
                return null;
            }

            if (record.target_class == DefaultCompilerTypeName && record.target_name == DefaultCompilerObjectName)
            {
                var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(DefaultCompilerAssetPath);
                if (asset == null)
                {
                    record.reason = "the documented counterpart " + DefaultCompilerAssetPath +
                                    " could not be loaded (is the Naninovel package embedded at Packages/com.elringus.naninovel?)";
                    return null;
                }
                if (asset.GetType().FullName != DefaultCompilerTypeName)
                {
                    record.reason = DefaultCompilerAssetPath + " loads as " + asset.GetType().FullName +
                                    ", not " + DefaultCompilerTypeName + "; not bound";
                    return null;
                }
                record.binding = DefaultCompilerAssetPath;
                record.reason = "bound: the package's own DefaultCompiler asset";
                record.evidence = "Naninovel's editor assigns exactly this path when " +
                                  "ScriptsConfiguration.CompilerLocalization is empty (ScriptsSettings.cs:57-59: " +
                                  "PathUtils.Combine(PackagePath.PrefabsPath, \"DefaultCompiler.asset\")); the shipped " +
                                  "PPtr's target path_id is the decoded Naninovel.CompilerLocalization object itself; " +
                                  "m_Script guid ea4376a87ff645beb74ce0726bf667f9 == CompilerLocalization.cs.meta";
                return asset;
            }

            record.reason = DescribeUnbound(record);
            FindCandidates(record);
            return null;
        }

        private static string DescribeUnbound (ReferenceRecord record)
        {
            if (record.target_class == null)
                return "the decode could not read the target's class or name, so nothing here identifies it";
            if (string.IsNullOrEmpty(record.target_name))
                return "the target is a " + record.target_class + " with an empty m_Name, so it cannot be identified " +
                       "by name; it is a component instance inside the shipped resources.assets";
            return "the target is a " + record.target_class + " named '" + record.target_name + "' (path_id " +
                   record.target_path_id + ") in the shipped resources.assets; no project asset is identified as that " +
                   "object by this pass, so the field is left null and listed for a later path_id -> asset mapping";
        }

        /// <summary>
        /// Look for project assets that *look* like the shipped target (exact name, matching type) and record them
        /// as candidates.  Deliberately never binds: name + type is a resemblance, not an identity - the shipped
        /// reference is a specific object and a wrong link would be silent.  The result is for the next pass.
        /// </summary>
        private static void FindCandidates (ReferenceRecord record)
        {
            if (record.target_class == null || string.IsNullOrEmpty(record.target_name))
            {
                record.candidates = "not searched (no reliable target name)";
                return;
            }
            if (!record.target_name_reliable)
            {
                record.candidates = "not searched (the decode marked the target name unreliable)";
                return;
            }
            if (record.target_name.IndexOf(' ') >= 0 || record.target_name.IndexOf('"') >= 0)
            {
                record.candidates = "not searched (the name needs quoting in an AssetDatabase filter)";
                return;
            }

            string key = record.target_class + "|" + record.target_name;
            Candidates cached;
            if (candidateCache.TryGetValue(key, out cached))
            {
                record.candidates = cached.summary;
                record.candidate_paths = new List<string>(cached.paths);
                return;
            }

            string simple = record.target_class;
            int dot = simple.LastIndexOf('.');
            if (dot >= 0) simple = simple.Substring(dot + 1);

            var found = new List<string>();
            string summary;
            try
            {
                string[] guids = AssetDatabase.FindAssets(record.target_name + " t:" + simple);
                int scanned = 0;
                foreach (string guid in guids)
                {
                    if (++scanned > MaxCandidateScan) break;
                    string path = AssetDatabase.GUIDToAssetPath(guid);
                    if (string.IsNullOrEmpty(path)) continue;
                    if (!string.Equals(Path.GetFileNameWithoutExtension(path), record.target_name, StringComparison.Ordinal))
                        continue;
                    var asset = AssetDatabase.LoadMainAssetAtPath(path);
                    if (asset == null || asset.GetType().FullName != record.target_class) continue;
                    found.Add(path);
                    if (found.Count >= MaxCandidatePaths) break;
                }
                summary = found.Count == 0
                    ? "no asset of type " + record.target_class + " named '" + record.target_name + "' exists in this project (" +
                      guids.Length + " name hits, " + Math.Min(guids.Length, MaxCandidateScan) + " scanned)"
                    : found.Count + " candidate(s) found but NOT bound (name + type is a resemblance, not an identity): " +
                      string.Join(", ", found.ToArray());
            }
            catch (Exception ex)
            {
                summary = "candidate search failed: " + ex.GetType().Name + ": " + ex.Message;
            }
            candidateCache[key] = new Candidates { summary = summary, paths = found };
            record.candidates = summary;
            record.candidate_paths = new List<string>(found);
        }

        private class Candidates
        {
            public string summary;
            public List<string> paths = new List<string>();
        }

        private static readonly Dictionary<string, Candidates> candidateCache = new Dictionary<string, Candidates>(StringComparer.Ordinal);

        /// <summary>The decode's own path_id -> object table, used to cross-check what a PPtr pointed at.</summary>
        private static Dictionary<long, JObject> decodedByPathId;

        // ------------------------------------------------------------------------------------------------------------
        // Helpers
        // ------------------------------------------------------------------------------------------------------------

        private static void Skip (ObjectRow row, string field, string kind, string reason)
        {
            row.fields_skipped++;
            if (row.fields_skipped_list.Count < MaxMessagesPerRow)
                row.fields_skipped_list.Add(new SkippedValue { field = field, kind = kind, reason = reason });
            else
                row.skipped_truncated++;
        }

        private static void AppendNote (ObjectRow row, string note)
        {
            if (row.notes.Count < MaxMessagesPerRow) row.notes.Add(note);
        }

        /// <summary>The decoded value kind of a node ("string", "pptr", ...), safe on any token shape.</summary>
        private static string KindOf (JToken token)
        {
            if (token == null) return null;
            if (token.Type != JTokenType.Object) return token.Type.ToString();
            return (string)token["kind"];
        }

        /// <summary>
        /// How many decoded value nodes the object carries: one per node, containers included, so that
        /// `fields_applied + fields_skipped == values_total` is a real check on "no value was dropped" rather than a
        /// statement of intent.  An array or an inlined class counts once for itself plus once per element/field.
        /// </summary>
        private static int CountValues (JToken token)
        {
            if (token == null) return 0;
            if (token.Type == JTokenType.Null) return 1;   // counted as an applied no-op, same as ApplyValue does
            int count = 1;
            string kind = KindOf(token);
            if (kind == "array")
            {
                JArray items = token["items"] as JArray;
                if (items != null) foreach (JToken item in items) count += CountValues(item);
            }
            else if (kind == "inline")
            {
                JObject fields = token["fields"] as JObject;
                if (fields != null) foreach (JProperty child in fields.Properties()) count += CountValues(child.Value);
            }
            return count;
        }

        /// <summary>
        /// A short "field = value" summary of the object's top-level scalars, so a value that is deliberately not
        /// applied (a skipped object's counters, a version tag) is still visible in the report.
        /// </summary>
        private static List<string> SummarizeScalars (JObject values)
        {
            var lines = new List<string>();
            foreach (JProperty field in values.Properties())
            {
                if (lines.Count >= MaxScalarSummary) { lines.Add("... (" + values.Count + " top-level values total)"); break; }
                JToken value = field.Value;
                string kind = KindOf(value);
                if (kind == "string" || kind == "bool" || kind == "int" || kind == "float")
                {
                    string text = DecodedString(value) ?? "";
                    if (text.Length > 80) text = text.Substring(0, 80) + "...";
                    lines.Add(field.Name + " = " + text);
                }
                else if (kind == "enum")
                    lines.Add(field.Name + " = " + ReadLong(DecodedScalar(value)) + " (" +
                              (DecodedString(value["enum_type"]) ?? "?") + ")");
                else if (kind == "array")
                    lines.Add(field.Name + " = array[" + ReadLong(DecodedScalar(value["count"])) + "] of " +
                              (DecodedString(value["type"]) ?? "?"));
                else if (kind == "managed_ref")
                    lines.Add(field.Name + " = [SerializeReference] raw_id " + ReadLong(DecodedScalar(value["raw_id"])));
            }
            return lines;
        }

        // ------------------------------------------------------------------------------------------------------------
        // The decode's typed-wrapper unwrapping
        // ------------------------------------------------------------------------------------------------------------
        // tools/unity-rebuild/decode_naninovel_configs.py writes *every* decoded value as a typed wrapper
        //
        //     {"kind": "string", "value": "ChoiceHandlersConfigurationWitchTrials"}
        //
        // including `m_Name` and the auxiliary scalars (`enum_type`, `m_FileID`, `count`, ...).  A plain
        // `(string)token` on such a node yields the wrapper's JSON text instead of the value, which is exactly how
        // the first run of this tool produced the asset path
        //
        //     .../Configuration/{ "kind": "string", "value": "ChoiceHandlersConfigurationWitchTrials" }.asset
        //
        // and then died in Path.Combine with ArgumentException("Illegal characters in path") for path_id 1465.
        // Everything that needs a *plain scalar* therefore goes through DecodedScalar, which accepts both shapes:
        // a bare JSON scalar is returned as-is, a wrapper yields its `value`.  Only the scalar kinds are unwrapped:
        // the vector/rect/color kinds carry an *array* value and their readers take `token["value"] as JArray`
        // directly (that handling is deliberate and untouched).

        /// <summary>
        /// The scalar a decoded node carries: the node itself when it is already a bare string/number/bool, or the
        /// `value` of a {"kind": ..., "value": ...} wrapper.  Null when the node has no kind, has no value, or is not
        /// a scalar kind (array/inline/pptr/managed_ref/unity_event/registry), so a caller that needs a plain scalar
        /// can never mistake a structure for one.
        /// </summary>
        private static JToken DecodedScalar (JToken token)
        {
            if (token == null || token.Type == JTokenType.Null) return null;
            if (token.Type != JTokenType.Object) return token;
            JToken kind = token["kind"];
            if (kind == null || kind.Type == JTokenType.Null) return null;
            string name = kind.Type == JTokenType.String ? kind.Value<string>() : kind.ToString();
            if (name != "string" && name != "bool" && name != "int" && name != "float" && name != "enum" &&
                name != "char")
                return null;
            JToken value = token["value"];
            if (value == null || value.Type == JTokenType.Null) return null;
            return value;
        }

        /// <summary>The decoded scalar as text, or null when the node does not carry a scalar. Never the wrapper's
        /// JSON text.</summary>
        private static string DecodedString (JToken token)
        {
            JToken scalar = DecodedScalar(token);
            if (scalar == null) return null;
            return scalar.Type == JTokenType.String ? scalar.Value<string>() : scalar.ToString();
        }

        /// <summary>The decoded scalar as a bool (numbers are treated as 0/non-zero), or the fallback.</summary>
        private static bool DecodedBool (JToken token, bool fallback)
        {
            JToken scalar = DecodedScalar(token);
            if (scalar == null) return fallback;
            if (scalar.Type == JTokenType.Boolean) return scalar.Value<bool>();
            if (scalar.Type == JTokenType.Integer || scalar.Type == JTokenType.Float)
                return Math.Abs(scalar.Value<double>()) > double.Epsilon;
            bool parsed;
            return bool.TryParse(scalar.ToString(), out parsed) ? parsed : fallback;
        }

        // ------------------------------------------------------------------------------------------------------------
        // Asset names
        // ------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Whether a decoded name can be used as an asset file name.  Checked before any path is built, so a bad name
        /// can only ever cost its own row (status SKIPPED_INVALID_NAME) and never the rest of the batch.
        /// </summary>
        private static bool IsUsableAssetName (string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            if (name.Length > MaxAssetNameLength) return false;
            if (name != name.Trim()) return false;
            if (name == "." || name == "..") return false;
            if (name.EndsWith(".", StringComparison.Ordinal)) return false;
            foreach (char c in Path.GetInvalidFileNameChars())
                if (name.IndexOf(c) >= 0) return false;
            foreach (char c in name)
                if (char.IsControl(c)) return false;
            return true;
        }

        private static string DescribeInvalidName (string name)
        {
            if (string.IsNullOrEmpty(name)) return "it is empty";
            var bad = new List<string>();
            foreach (char c in Path.GetInvalidFileNameChars())
                if (name.IndexOf(c) >= 0) bad.Add("'" + Preview(c.ToString()) + "' (U+" + ((int)c).ToString("X4") + ")");
            if (bad.Count > 0) return "it contains " + string.Join(", ", bad.ToArray()) + "; name = " + Preview(name);
            if (name.Length > MaxAssetNameLength)
                return "it is " + name.Length + " characters long (limit " + MaxAssetNameLength + "); name = " +
                       Preview(name);
            if (name != name.Trim()) return "it has leading or trailing whitespace; name = " + Preview(name);
            if (name == "." || name == ".." || name.EndsWith(".", StringComparison.Ordinal))
                return "it is a reserved or trailing-dot file name; name = " + Preview(name);
            return "name = " + Preview(name);
        }

        /// <summary>One readable line for a name that may contain control characters (the wrapper text that broke
        /// path_id 1465 contains CR and LF), truncated so a report row stays a row.</summary>
        private static string Preview (string text)
        {
            if (text == null) return "null";
            var builder = new StringBuilder();
            foreach (char c in text)
            {
                if (c == '\r') builder.Append("\\r");
                else if (c == '\n') builder.Append("\\n");
                else if (c == '\t') builder.Append("\\t");
                else if (char.IsControl(c)) builder.Append("\\u").Append(((int)c).ToString("X4"));
                else builder.Append(c);
                if (builder.Length >= 160) { builder.Append("..."); break; }
            }
            return builder.ToString();
        }

        private static long ReadLong (JToken token)
        {
            if (token == null || token.Type == JTokenType.Null) return 0L;
            try { return token.Value<long>(); }
            catch (Exception) { return 0L; }
        }

        private static double ReadDouble (JToken token)
        {
            if (token == null || token.Type == JTokenType.Null) return 0d;
            try { return token.Value<double>(); }
            catch (Exception) { return 0d; }
        }

        private static float ReadFloat (JToken token) { return (float)ReadDouble(token); }

        private static float At (JArray array, int index, float fallback = 0f)
        {
            if (array == null || index >= array.Count || array[index] == null) return fallback;
            return ReadFloat(array[index]);
        }

        private static int AtInt (JArray array, int index, int fallback = 0)
        {
            if (array == null || index >= array.Count || array[index] == null) return fallback;
            return (int)ReadLong(array[index]);
        }

        /// <summary>The editor assembly has no asmdef, so every loaded assembly is searchable - including the real
        /// package and the reconstructed game assemblies.</summary>
        private static Type FindType (string fullName)
        {
            if (string.IsNullOrEmpty(fullName)) return null;
            Type cached;
            if (typeCache.TryGetValue(fullName, out cached)) return cached;
            Type found = null;
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                try { found = assembly.GetType(fullName, false); }
                catch (Exception) { /* dynamic assemblies can refuse lookups */ }
                if (found != null) break;
            }
            typeCache[fullName] = found;
            return found;
        }

        private static readonly Dictionary<string, Type> typeCache = new Dictionary<string, Type>(StringComparer.Ordinal);

        private static bool DerivesFromConfiguration (Type type)
        {
            for (Type current = type; current != null; current = current.BaseType)
                if (current.FullName == "Naninovel.Configuration") return true;
            return false;
        }

        private static void EnsureFolder (string assetFolder)
        {
            if (string.IsNullOrEmpty(assetFolder) || AssetDatabase.IsValidFolder(assetFolder)) return;
            int slash = assetFolder.LastIndexOf('/');
            if (slash <= 0) return;
            string parent = assetFolder.Substring(0, slash);
            string leaf = assetFolder.Substring(slash + 1);
            EnsureFolder(parent);
            if (!AssetDatabase.IsValidFolder(assetFolder))
                AssetDatabase.CreateFolder(parent, leaf);
        }

        /// <summary>The absolute path of an asset path, or null when the path cannot be built at all (a caller must
        /// treat null as "unknown", never as "does not exist").</summary>
        private static string AbsoluteAssetPath (string assetPath)
        {
            try
            {
                string projectRoot = Directory.GetParent(Application.dataPath).FullName;
                return Path.GetFullPath(Path.Combine(projectRoot, assetPath.Replace('/', Path.DirectorySeparatorChar)));
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static void Tally (ImportResult result)
        {
            foreach (ObjectRow row in result.rows)
            {
                if (row.status == "CREATED") result.configuration_created++;
                else if (row.status == "CREATED_SECONDARY") { result.configuration_created++; result.configuration_secondary++; }
                else if (row.status == "OVERWRITTEN") result.configuration_overwritten++;
                if (row.is_configuration)
                {
                    result.configuration_objects++;
                    if (row.status == "CREATED" || row.status == "CREATED_SECONDARY" || row.status == "OVERWRITTEN")
                        result.configuration_imported++;
                    else
                    {
                        result.configuration_failed++;
                        result.failed_summary.Add(row.type_name + " (path_id " + row.path_id + "): " +
                                                  row.status + " - " + row.reason);
                        result.status = "PARTIAL";
                    }
                    if (!row.values_accounted)
                    {
                        result.warnings.Add(row.type_name + " (path_id " + row.path_id + "): decoded values were not " +
                                            "fully accounted for (applied " + row.fields_applied + ", skipped " +
                                            row.fields_skipped + ", decoded " + row.values_total + ")");
                        result.status = "PARTIAL";
                    }
                }
                else
                {
                    result.not_imported++;
                    result.not_imported_summary.Add(row.type_name + " (path_id " + row.path_id + "): " +
                                                    row.status + " - " + row.reason);
                }

                result.values_total += row.values_total;
                result.fields_applied += row.fields_applied;
                result.fields_skipped += row.fields_skipped;
                result.references_total += row.references_total;
                result.references_null += row.references_null;
                result.references_bound += row.references_bound;
                result.references_needing_binding += row.references_needing_binding;
            }
            result.configuration_types = result.rows.Where(r => r.is_configuration)
                .Select(r => r.type_name).Distinct(StringComparer.Ordinal).Count();
            result.configuration_assets = result.rows
                .Where(r => r.status == "CREATED" || r.status == "CREATED_SECONDARY" || r.status == "OVERWRITTEN")
                .Select(r => r.asset_path).Distinct(StringComparer.Ordinal).Count();
            result.limitations = BuildLimitations(result);
        }

        /// <summary>
        /// Everything this pass knowingly did not do, in the report's own words: what a later pass has to decide, and
        /// what the engine will do in the meantime with each field that was left null.
        /// </summary>
        private static List<string> BuildLimitations (ImportResult result)
        {
            var limitations = new List<string>();
            limitations.Add("ASSET CONTENT: " + result.configuration_imported + " configuration object(s) were written to " +
                            ConfigurationFolder + ", covering " + result.configuration_types + " distinct configuration " +
                            "types in " + result.configuration_assets + " distinct asset file(s). " +
                            result.configuration_created + " created, " + result.configuration_overwritten +
                            " overwritten in place (an existing asset is loaded and re-applied, so a second run neither " +
                            "duplicates nor corrupts anything).");
            limitations.Add("NOT IMPORTED ON PURPOSE: " + result.not_imported + " of " + result.objects_total +
                            " decoded objects are not configuration assets (not reachable by a '" +
                            ConfigurationResourcePrefix + "...' Resources key) and were left alone, each with a reason " +
                            "in its row/not_imported_summary: EngineVersion, ProjectStats, ProjectResources, " +
                            "CompilerLocalization (package owns / regenerates each of these keys) and the two UI " +
                            "component instances (LoadingIcon, EngineInitializationUI), which are prefab components " +
                            "rather than assets.");
            limitations.Add("OBJECT REFERENCES: " + result.references_total + " PPtr nodes were decoded in the " +
                            "configuration objects - " + result.references_null + " null in the shipped object (left " +
                            "null), " + result.references_bound + " bound through a verified identity, " +
                            result.references_needing_binding + " left null and listed in `needs_binding`. A shipped " +
                            "PPtr is (m_FileID, m_PathID) into resources.assets and those ids do not exist in this " +
                            "project, so binding them needs a path_id -> asset table (a later pass); nothing here was " +
                            "invented, and each needs_binding record carries the target class, name, path_id and (for " +
                            "information only, never auto-bound) whether this project holds same-named same-typed assets.");
            limitations.Add("CONSEQUENCE OF THE UNBOUND REFERENCES: EngineConfiguration.CustomInitializationUI and " +
                            "AudioConfiguration.CustomAudioMixer are left null, which makes the engine fall back to " +
                            "the package's own resources - Engine.LoadInternalResource<ScriptableUIBehaviour>" +
                            "(\"EngineInitializationUI\") (RuntimeInitializer.cs:47-49) and " +
                            "Engine.LoadInternalResource<AudioMixer>(\"DefaultMixer\") (AudioManager.cs:91). The engine " +
                            "initialises with a working default in both cases, but audio routing (the shipped game " +
                            "used the 'WitchTrials' mixer with Master/Bgm/Sfx/Voice group paths) and the game's own " +
                            "initialization UI (WitchTrials.Views.EngineInitializationUiModified) are not reproduced. " +
                            "InputConfiguration.InputActions is null as well and its target lives in another " +
                            "serialized file (m_FileID 1), so no mapping inside resources.assets can ever recover it; " +
                            "combined with the shipped ProcessLegacyBindings = false this leaves input inert until the " +
                            "game's .inputactions asset is supplied. Actor metadata " +
                            "(Metadata.metas[*].CustomSpriteMaterial, 34 references in all) points at the materials " +
                            "'MaskableTransparent' / 'MaskableTransparentToned_1', which do not exist anywhere in " +
                            "this project (the tool re-checks at run time and records the candidates it finds), so " +
                            "sprites fall back to Naninovel's default material."); 
            limitations.Add("[SerializeReference]: all 54 managed references in the decode (actor metadata " +
                            "`customData`) are Unity's null sentinel (raw_id -2), so they are written as null - which " +
                            "is exactly what the shipped object holds. The four objects that carry a trailing " +
                            "ManagedReferencesRegistry (BackgroundsConfiguration, CharactersConfiguration, " +
                            "ChoiceHandlersConfiguration, TextPrintersConfiguration) therefore need no registry " +
                            "restored; the registry node is reported per row and skipped, not applied.");
            limitations.Add("DUPLICATE TYPE: `WitchTrials.Models.WitchTrialsChoiceHandlersConfiguration` occurs twice in " +
                            "the shipped build. The engine's lookup key is the type name, so the object whose Resources " +
                            "key matches it (path_id 1481, m_Name 'WitchTrialsChoiceHandlersConfiguration') owns " +
                            "WitchTrialsChoiceHandlersConfiguration.asset. The other one (path_id 1465, m_Name " +
                            "'ChoiceHandlersConfigurationWitchTrials') carries the same single field " +
                            "DefaultCriticalChoiceHintEnabled = true and differs only in the m_Name bytes (both objects " +
                            "are 76 bytes), and it is restored under its own decoded name so nothing is lost; the " +
                            "engine will never load it by type name. If you would rather not carry an asset the engine " +
                            "cannot reach, delete ChoiceHandlersConfigurationWitchTrials.asset after the run - nothing " +
                            "else depends on it.");
            limitations.Add("VERSION DRIFT, BOTH DIRECTIONS: the decoder took each layout from *this* project's C# " +
                            "sources and all 31 walks close exactly, so every decoded name should resolve. If one does " +
                            "not, the row lists it in fields_skipped with the reason and the C# default is kept - no " +
                            "value is dropped silently, and each imported row carries " +
                            "`values_accounted` (fields_applied + fields_skipped == values_total) so the accounting can " +
                            "be checked from the report. The " +
                            "other direction is invisible by construction: a field that exists in this package but was " +
                            "absent in the shipped build keeps its C# default and this pass cannot tell.");
            limitations.Add("NOT VERIFIED WITHOUT RUNNING UNITY: that the engine's initialization now completes. Note " +
                            "that the mechanism recorded in UNITY_RUNTIME_INTEGRATION.md §33.2 (\"InputManager." +
                            "InitializeService() dereferences a null InputConfiguration.InputActions\") does not " +
                            "reproduce from the sources: the only dereference of InputActions is inside " +
                            "InputSampler.InitializeInputAction(), which is guarded by `if (!config.InputActions) " +
                            "return;` (InputSampler.cs:195), and InitializeService's own synchronous prologue " +
                            "(InputManager.cs:44-50) dereferences only Configuration.Bindings, which the field " +
                            "initializer fills even for a default instance. Re-measure with an unfiltered stack trace. " +
                            "What the import changes for certain, and is verifiable in the assets themselves: " +
                            "EngineConfiguration.InitializeOnApplicationLoad flips from the default `true` to the " +
                            "shipped `false` (so the engine stops self-initialising in OnApplicationLoaded and waits " +
                            "for the reconstructed InitializeNaninovelAsyncCore), and " + result.fields_applied +
                            " of the " + result.values_total + " decoded value nodes in the decode were written onto " +
                            "the assets (the remainder belong to the six objects deliberately not imported).");
            return limitations;
        }

        // ------------------------------------------------------------------------------------------------------------
        // Report
        // ------------------------------------------------------------------------------------------------------------

        [Serializable]
        public class ImportResult
        {
            public string status = "NOT_RUN";
            public string generated;
            public string input;
            public long input_bytes;
            public string decode_generated_by;
            public string asset_folder;
            public string engine_lookup;
            public string error;

            public int objects_total;
            public int configuration_objects;
            public int configuration_types;
            public int configuration_assets;
            public int configuration_created;
            public int configuration_overwritten;
            public int configuration_secondary;
            public int configuration_imported;
            public int configuration_failed;
            public int not_imported;

            public int values_total;
            public int fields_applied;
            public int fields_skipped;

            public int references_total;
            public int references_null;
            public int references_bound;
            public int references_needing_binding;

            public List<ObjectRow> rows = new List<ObjectRow>();
            public List<string> not_imported_summary = new List<string>();
            public List<string> failed_summary = new List<string>();
            public List<string> limitations = new List<string>();
            public List<string> warnings = new List<string>();
        }

        [Serializable]
        public class ObjectRow
        {
            public long path_id;
            public string type_name;
            public string type_resolved;
            public bool is_configuration;
            public string resources_path;
            public string decoded_name;
            public string asset_path;
            public string asset_name;
            public bool secondary_of_type;
            public string status = "NOT_RUN";
            public string reason;

            public int values_total;
            public int fields_applied;
            public int fields_skipped;
            public bool values_accounted;
            public int skipped_truncated;

            public int references_total;
            public int references_null;
            public int references_bound;
            public int references_needing_binding;

            public List<SkippedValue> fields_skipped_list = new List<SkippedValue>();
            public List<ReferenceRecord> references_bound_list = new List<ReferenceRecord>();
            public List<ReferenceRecord> needs_binding = new List<ReferenceRecord>();
            public List<string> decoded_scalars = new List<string>();
            public List<string> notes = new List<string>();
        }

        [Serializable]
        public class SkippedValue
        {
            public string field;
            public string kind;
            public string reason;
        }

        [Serializable]
        public class ReferenceRecord
        {
            public string field;
            public string target_class;
            public string target_name;
            public long target_file_id;
            public long target_path_id;
            public bool target_name_reliable = true;
            public string decoded_object_match;
            public string reason;
            public string evidence;
            public string binding;
            public string candidates;
            public List<string> candidate_paths = new List<string>();
        }
    }
}
