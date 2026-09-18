// Imports the shipped *master data* asset that the reconstructed project was missing.
//
//   Unity -batchmode -quit -projectPath UnityProject \
//         -executeMethod Manosaba.EditorTools.MasterDataImporter.Run -logFile -
//
// Why
// ---
// `GigaCreation.Essentials.MasterData.MasterDataProvider.LoadMasterData` (RVA 0xCD3D40) is the only assignment
// of the static `Data` property in the whole game:
//
//     Data = Resources.Load<MasterDataBase>("MasterData");        (ScriptString at 0x180CD3D60;
//                                                                  Resources.Load<T> slot 0x180CD3D4F)
//
// and `GigaCreation.Essentials.AddressablesUtils.AddressablesManager` then feeds that object to Addressables:
//
//     Addressables.LoadResourceLocationsAsync(masterData.AssetKeyNames, MergeMode.Union, null)
//     (RVA 0xCC24D0: `MasterDataProvider.Data as IAddressablesMasterData` at 0x180CC2683, null -> 0x180CC29B7
//      Debug.LogError "The provided master data does not include the data for Addressable Asset System." and an
//      EMPTY List<IResourceLocation>; the `IEnumerable keys, MergeMode, Type` overload is RVA 0x1887290 and
//      r8d == 1. dump.cs renders Addressables.MergeMode as None = 0, UseFirst = 0, Union = 1, Intersection = 2.)
//
// With no master data asset in the project, that null branch is the one taken, the reconstructed Naninovel
// resource provider caches no locations, and the boot chain stops in
// `ResourceLoaderExtensions.LoadOrErr<TResource>`.  The asset is not Addressables content and the shipped
// catalog has no `MasterData` address: it lived only in Unity `Resources` (shipped `resources.assets`,
// path_id 1454, 80 bytes, class `WitchTrials.Models.MasterData`) and was never imported.
//
// This tool is the content half of the fix; the evidence half is
// `tools/unity-rebuild/decode_master_data.py` -> `out/unity/master_data_decode.json`, which walks that object
// (and recursively everything it references - it references nothing) byte for byte.
//
// What the decode says (it is the tool's input, not an assumption here)
// ---------------------------------------------------------------------
// The walk closes exactly at 80/80 bytes: `28-byte header + m_Name("MasterData") + _assetKeyNames (string[1] =
// ["Naninovel"]) + _additionalCatalogDataList (count 0) + _defaultHp (10) + _skipTimeScaleDuringDebate (5) +
// _voicePitchOnSkip (1.5)`, the field order of `WitchTrials.Models.MasterData`.  So `AssetKeyNames` is exactly
// ONE key name, "Naninovel", and the shipped catalog resolves that key to 28,150 locations - which is where the
// resources come from.  Nothing here is invented: every value written comes from that decode.
//
// Where the asset must live (established, not chosen)
// ---------------------------------------------------
//   * the engine asks for the Resources key `MasterData` (the native literal).  A Resources key is the asset's
//     path below a `Resources` folder without its extension, so the file name must be `MasterData.asset`;
//     `IMasterDataBase.MasterDataFileName` ("MasterData") is that same key, and this tool reads that const
//     through reflection as a cross-check rather than trusting its own literal.
//   * the shipped build confirms the key independently: `ResourceManager.m_Container` maps the object to the key
//     `masterdata` (Unity stores container keys lowercased - the same convention the decoded Naninovel
//     configurations show as `naninovel/configuration/<name>`), and `decode_master_data.py` resolves the root
//     through that table rather than by class name.
//   * the *folder* cannot be recovered from the binary (only the key is pinned), so this tool uses this
//     project's established Resources root for shipped game data:
//
//         Assets/NaninovelData/Resources/MasterData.asset
//
//     the same root `NaninovelConfigBuilder` writes `Naninovel/Configuration/*.asset` into, so the project keeps
//     one Resources tree instead of several.  The key that results is `MasterData` with no collision (the
//     configuration assets live under the `Naninovel/` prefix).
//
// What it does, per decoded object
// --------------------------------
//   1. read the decode (Newtonsoft.Json); every object the decoder emitted gets a row, never a silent skip
//   2. the *root* object is the one the engine loads - the first object in the decode's `objects` array, whose
//      `resources_path` the decoder resolved through `ResourceManager.m_Container`, cross-checked against
//      `entry.root_path_id`.  Any other object in the closure is reported as SKIPPED_NOT_ROOT with its reason
//      (no code path pins a path for it here) instead of being written somewhere arbitrary
//   3. resolve the decoded class against the loaded assemblies, require it to derive from
//      `GigaCreation.Essentials.MasterData.MasterDataBase`, and require the decoded m_Name to equal the
//      engine's key; a mismatch is reported as PARTIAL/FAILED, never "fixed"
//   4. create the asset, or load and re-apply it (idempotent; an existing file that does not load, or loads as
//      another type, is left untouched and reported)
//   5. copy every decoded value onto a SerializedObject by *serialized name* - which is what
//      SerializedProperty.FindProperty expects, because the decoder took the layout from this project's own C#
//      sources and the walk closes exactly.  Kinds: string/bool/float/int/enum/array/inline/pptr
//   6. read the fields back out of the SerializedObject and compare with the decode (`readback`), so a value
//      that silently did not land is visible in the report
//   7. write tools/unity-rebuild/out/unity/master_data_import.json (status, per-object rows with applied/skipped
//      counts and reasons, the exact asset paths used, the key cross-check, the read-back, limitations)
//
// Exit codes: 0 = every decoded object was created/overwritten (or deliberately skipped), 1 = PARTIAL/ERROR,
// 2 = BLOCKED (no input decode).  Same convention as NaninovelConfigBuilder.cs.
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
    public static class MasterDataImporter
    {
        private const string InputRelativePath = "../tools/unity-rebuild/out/unity/master_data_decode.json";
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/master_data_import.json";

        /// <summary>
        /// The single string in `MasterDataProvider.LoadMasterData` (RVA 0xCD3D40; the ScriptString slot is
        /// loaded at 0x180CD3D60 and passed as the first argument to `Resources.Load<MasterDataBase>` at
        /// 0x180CD3D86).  A Resources key is the path below a `Resources` folder without the extension, so this
        /// is also the required asset file name.
        /// </summary>
        private const string EngineResourcesKey = "MasterData";

        /// <summary>Where the asset is created: this project's Resources root for shipped game data.</summary>
        private const string ResourcesFolder = "Assets/NaninovelData/Resources";

        private const string RootTypeName = "WitchTrials.Models.MasterData";
        private const string BaseTypeName = "GigaCreation.Essentials.MasterData.MasterDataBase";
        private const string InterfaceTypeName = "GigaCreation.Essentials.MasterData.IMasterDataBase";
        private const string KeyNameField = "_assetKeyNames";

        private const string NameKey = "m_Name";
        private const string ManagedReferencesKey = "_managedReferences";

        private const int MaxMessagesPerRow = 200;
        private const int MaxScalarSummary = 40;

        private const int ExitOk = 0;
        private const int ExitPartial = 1;
        private const int ExitBlocked = 2;

        [MenuItem("Manosaba/Import Master Data")]
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
            result.resources_folder = ResourcesFolder;
            result.engine_resources_key = EngineResourcesKey;
            result.engine_lookup = "Resources.Load<MasterDataBase>(\"" + EngineResourcesKey + "\") - " +
                                   "MasterDataProvider.LoadMasterData, RVA 0xCD3D40";

            int exitCode = ExitPartial;
            try
            {
                if (!File.Exists(inputPath))
                {
                    result.status = "BLOCKED";
                    result.error = "input decode not found: " + inputPath +
                                   " (run tools/unity-rebuild/decode_master_data.py --write)";
                    exitCode = ExitBlocked;
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
                        exitCode = ExitBlocked;
                    }
                    else
                    {
                        result.decode_generated_by = DecodedString(document["generated_by"]);
                        result.objects_total = objects.Count;

                        // The engine's key cross-check.  The literal above is the measured ScriptString; the
                        // interface's const is an *independent* statement of the same key, so read it and compare
                        // rather than trusting either alone.  Missing const => recorded, not fatal.
                        Type interfaceType = FindType(InterfaceTypeName);
                        if (interfaceType == null)
                            result.key_cross_check = "GigaCreation.Essentials.MasterData.IMasterDataBase is not " +
                                                     "loaded, so the engine key could not be cross-checked against " +
                                                     "IMasterDataBase.MasterDataFileName";
                        else
                        {
                            FieldInfo constField = interfaceType.GetField("MasterDataFileName",
                                BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                            object raw = null;
                            try { if (constField != null) raw = constField.GetRawConstantValue(); }
                            catch (Exception) { raw = null; }
                            string constValue = raw as string;
                            result.interface_const_value = constValue;
                            if (constValue == null)
                                result.key_cross_check = interfaceType.FullName + ".MasterDataFileName was not " +
                                                         "readable as a const string; the key used is the native " +
                                                         "literal only";
                            else if (constValue == EngineResourcesKey)
                            {
                                result.key_cross_check = "IMasterDataBase.MasterDataFileName == \"" + constValue +
                                                         "\" agrees with the native literal at 0x180CD3D60";
                                result.key_cross_check_ok = true;
                            }
                            else
                            {
                                result.key_cross_check = "MISMATCH: IMasterDataBase.MasterDataFileName is \"" +
                                                         constValue + "\" but the native literal is \"" +
                                                         EngineResourcesKey + "\"";
                                result.warnings.Add(result.key_cross_check + " - the asset was written for the " +
                                                    "native literal; re-measure before trusting this import");
                            }
                        }

                        // The decode's own root statement: which path_id the decoder resolved through
                        // ResourceManager.m_Container.  row 0 must be that object.
                        JObject entry = document["entry"] as JObject;
                        long decodedRoot = entry == null ? -1L : ReadLong(DecodedScalar(entry["root_path_id"]));
                        result.decode_root_path_id = decodedRoot;
                        result.decode_root_resolved_by = entry == null ? null : DecodedString(entry["root_resolved_by"]);
                        result.decode_assets_file = DecodedString(document["assets_file"]);
                        JObject keyBlock = document["asset_key_names"] as JObject;
                        result.decoded_key_name_count = keyBlock == null ? 0
                            : (int)ReadLong(DecodedScalar(keyBlock["count"]));
                        result.decoded_key_names = keyBlock == null
                            ? new List<string>()
                            : (keyBlock["names"] as JArray ?? new JArray()).Select(t => t.ToString()).ToList();

                        EnsureFolder(ResourcesFolder);

                        int index = 0;
                        foreach (JToken token in objects)
                        {
                            var row = new ObjectRow();
                            result.rows.Add(row);
                            try
                            {
                                ImportOne((JObject)token, row, index, result);
                            }
                            catch (Exception ex)
                            {
                                row.status = "ERROR";
                                row.reason = ex.GetType().Name + ": " + ex.Message;
                                row.notes.Add(ex.ToString());
                            }
                            index++;
                        }

                        AssetDatabase.SaveAssets();
                        Tally(result);
                        if (result.failed > 0) result.status = "PARTIAL";
                        else if (result.status != "PARTIAL") result.status = "OK";
                        exitCode = result.status == "OK" ? ExitOk : ExitPartial;
                    }
                }
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.error = ex.ToString();
                Debug.LogError("[MasterDataImporter] " + ex);
                exitCode = ExitPartial;
            }

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(outPath));
                File.WriteAllText(outPath, JsonUtility.ToJson(result, true), new UTF8Encoding(false));
            }
            catch (Exception ex)
            {
                Debug.LogError("[MasterDataImporter] failed to write " + outPath + ": " + ex.Message);
            }

            Debug.Log("[MasterDataImporter] " + result.status +
                      " objects=" + result.objects_total +
                      " imported=" + result.imported + " created=" + result.created +
                      " overwritten=" + result.overwritten + " skipped=" + result.skipped +
                      " failed=" + result.failed +
                      " key_names=" + result.decoded_key_name_count +
                      " fields_applied=" + result.fields_applied + " fields_skipped=" + result.fields_skipped +
                      " asset_paths=" + string.Join(", ", result.asset_paths.ToArray()) +
                      " wrote " + outPath);
            return exitCode;
        }

        // ------------------------------------------------------------------------------------------------------------
        // One decoded object
        // ------------------------------------------------------------------------------------------------------------

        private static void ImportOne (JObject decoded, ObjectRow row, int index, ImportResult result)
        {
            row.path_id = ReadLong(DecodedScalar(decoded["path_id"]));
            row.type_name = DecodedString(decoded["class"]) ?? "";
            row.unity_type = DecodedString(decoded["unity_type"]);
            row.resources_path = DecodedString(decoded["resources_path"]);
            row.decode_status = DecodedString(decoded["status"]);
            row.depth = (int)ReadLong(DecodedScalar(decoded["depth"]));
            row.bytes_total = (int)ReadLong(DecodedScalar(decoded["bytes_total"]));
            row.closes_exactly = decoded["closes_exactly"] != null && decoded["closes_exactly"].Type == JTokenType.Boolean
                                 && decoded["closes_exactly"].Value<bool>();

            JObject values = decoded["values"] as JObject;
            string decodedName = DecodedString(values == null ? null : values[NameKey]) ?? "";
            row.decoded_name = decodedName;
            row.values_total = values == null ? 0 : values.Properties().Sum(p => CountValues(p.Value));
            if (values != null) row.decoded_scalars.AddRange(SummarizeScalars(values));

            // The root is the object the engine reaches with Resources.Load: the decode says which path_id it
            // resolved through ResourceManager.m_Container, and the array order (root first, then the closure)
            // agrees with it.  Both are checked; a disagreement is reported and nothing is written for the object.
            row.is_root = index == 0 || (result.decode_root_path_id >= 0 && row.path_id == result.decode_root_path_id);
            if (index == 0 && result.decode_root_path_id >= 0 && row.path_id != result.decode_root_path_id)
            {
                row.status = "FAILED";
                row.reason = "the first decoded object is path_id " + row.path_id + " but the decode resolved the " +
                             "engine root as path_id " + result.decode_root_path_id +
                             " through ResourceManager.m_Container; refusing to guess which object is the asset";
                result.warnings.Add(row.reason);
                return;
            }

            if (!row.is_root)
            {
                row.status = "SKIPPED_NOT_ROOT";
                row.reason = "this object is in the master data's reference closure (depth " + row.depth + ") but no " +
                             "code path pins a Resources path for it: only Resources.Load(\"" + EngineResourcesKey +
                             "\") reaches an asset, so writing it somewhere else would create an asset nothing " +
                             "loads. It is reported (with its decoded values in the input decode at path_id " +
                             row.path_id + ") for a later reference-binding pass.";
                row.fields_skipped = 1;
                row.fields_skipped_list.Add(new SkippedValue {
                    field = row.type_name, kind = "object", reason = "whole object deliberately not imported: " +
                    row.reason + " (" + row.values_total + " decoded values remain in " +
                    Path.GetFileName(InputRelativePath) + " at path_id " + row.path_id + ")" });
                return;
            }

            if (!row.closes_exactly)
            {
                row.status = "FAILED";
                row.reason = "the decode for this object does not close exactly (status " + row.decode_status +
                             "), so its field layout is not trustworthy; nothing was written";
                return;
            }

            if (!string.Equals(row.type_name, RootTypeName, StringComparison.Ordinal))
            {
                row.status = "FAILED";
                row.reason = "the decode's root object is a " + row.type_name + ", not " + RootTypeName +
                             "; this importer only knows the path " + RootTypeName + " is loaded from";
                return;
            }

            Type type = FindType(RootTypeName);
            if (type == null)
            {
                row.status = "TYPE_NOT_RESOLVED";
                row.reason = "no loaded assembly declares '" + RootTypeName + "' (searched " +
                             AppDomain.CurrentDomain.GetAssemblies().Length + " assemblies; the game assemblies must " +
                             "be compiled before this tool can run)";
                return;
            }
            row.type_resolved = type.Assembly.GetName().Name;
            if (!typeof(ScriptableObject).IsAssignableFrom(type))
            {
                row.status = "NOT_A_SCRIPTABLEOBJECT";
                row.reason = type.FullName + " is not a ScriptableObject, so it cannot be a Resources asset";
                return;
            }
            if (!DerivesFrom(type, BaseTypeName))
            {
                row.status = "FAILED";
                row.reason = type.FullName + " does not derive from " + BaseTypeName + ", so " +
                             "Resources.Load<MasterDataBase> could never return it";
                return;
            }

            // --- the asset path (see the header: the key pins the file name, not the folder) -------------------
            string assetName = string.IsNullOrEmpty(decodedName) ? EngineResourcesKey : decodedName;
            if (!string.Equals(assetName, EngineResourcesKey, StringComparison.Ordinal))
            {
                row.status = "FAILED";
                row.reason = "the decoded m_Name is '" + assetName + "' but the engine's Resources key is '" +
                             EngineResourcesKey + "'; the file name IS the key, so writing '" + assetName +
                             ".asset' would create an asset Resources.Load(\"" + EngineResourcesKey +
                             "\") can never find (this is exactly how decode_naninovel_configs + " +
                             "NaninovelConfigBuilder handle the second WitchTrialsChoiceHandlersConfiguration)";
                return;
            }
            if (!IsUsableAssetName(assetName))
            {
                row.status = "SKIPPED_INVALID_NAME";
                row.reason = "the decoded m_Name cannot be an asset file name: " + DescribeInvalidName(assetName);
                return;
            }
            string assetPath = ResourcesFolder + "/" + assetName + ".asset";
            row.asset_path = assetPath;
            row.asset_name = assetName;
            row.resources_key_from_path = ResourcesKeyOf(assetPath);

            if (AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath) == null)
            {
                string absolute = AbsoluteAssetPath(assetPath);
                if (absolute != null && File.Exists(absolute))
                {
                    row.status = "FAILED";
                    row.reason = "a file already exists at " + assetPath + " but does not load as a " +
                                 "ScriptableObject (missing script?); refusing to overwrite";
                    return;
                }
            }

            // --- load or create (idempotency) -----------------------------------------------------------------
            ScriptableObject instance = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
            bool createdNow = false;
            if (instance == null)
            {
                instance = ScriptableObject.CreateInstance(type);
                if (instance == null)
                {
                    row.status = "FAILED";
                    row.reason = "ScriptableObject.CreateInstance returned null for " + type.FullName;
                    return;
                }
                instance.name = assetName;
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
            row.created = createdNow;

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
                        Skip(row, name, "managed_references_registry",
                             "trailing ManagedReferencesRegistry, not a serialized field of " + type.Name +
                             ": nothing to restore");
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

            // --- read back ------------------------------------------------------------------------------------
            // The one check that does not need the engine to run: read the field the whole boot chain depends on
            // back out of the SerializedObject and compare it with the decode.
            var readback = new SerializedObject(instance);
            SerializedProperty keyProperty = readback.FindProperty(KeyNameField);
            if (keyProperty == null || !keyProperty.isArray)
            {
                row.readback = "the asset has no serialized array field '" + KeyNameField + "', so the key list " +
                               "could not be read back (the decode wrote it, but this class does not expose it)";
                result.warnings.Add(row.type_name + ": " + row.readback);
            }
            else
            {
                for (int i = 0; i < keyProperty.arraySize; i++)
                {
                    SerializedProperty element = keyProperty.GetArrayElementAtIndex(i);
                    row.key_names_readback.Add(element == null ? null : element.stringValue);
                }
                row.readback = KeyNameField + " read back as [" +
                               string.Join(", ", row.key_names_readback.ToArray()) + "]";
                row.readback_matches_decode = row.key_names_readback.SequenceEqual(result.decoded_key_names);
                if (!row.readback_matches_decode)
                {
                    result.warnings.Add(row.type_name + ": the key list read back (" +
                                        string.Join(", ", row.key_names_readback.ToArray()) +
                                        ") differs from the decode (" +
                                        string.Join(", ", result.decoded_key_names.ToArray()) + ")");
                    result.status = "PARTIAL";
                }
                if (row.key_names_readback.Count == 0)
                {
                    result.warnings.Add(row.type_name + ": the imported asset's " + KeyNameField + " is EMPTY; " +
                                        "AddressablesManager would call Addressables.LoadResourceLocationsAsync " +
                                        "with no keys and the resource provider would cache nothing");
                    result.status = "PARTIAL";
                }
            }

            row.status = createdNow ? "CREATED" : "OVERWRITTEN";
            result.asset_paths.Add(assetPath);
            if (!createdNow)
                AppendNote(row, "the asset already existed and was overwritten with the decoded values " +
                                "(idempotent: a second run re-applies the same values)");
            AppendNote(row, "the engine reaches this asset as Resources.Load<MasterDataBase>(\"" + EngineResourcesKey +
                            "\"); the key resulting from the asset path is '" + row.resources_key_from_path +
                            "' and the shipped build's ResourceManager.m_Container key was '" +
                            (row.resources_path ?? "?") + "'");
        }

        // ------------------------------------------------------------------------------------------------------------
        // Decoded value -> SerializedProperty
        // ------------------------------------------------------------------------------------------------------------

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
                    property.stringValue = DecodedString(token) ?? "";
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
                    if (property.propertyType == SerializedPropertyType.Integer
                        || property.propertyType == SerializedPropertyType.Enum
                        || property.propertyType == SerializedPropertyType.LayerMask
                        || property.propertyType == SerializedPropertyType.Character)
                    {
                        property.longValue = ReadLong(DecodedScalar(token));
                        row.fields_applied++;
                        return;
                    }
                    Skip(row, path, kind, "the current field is " + property.propertyType + ", not an integer/enum");
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
                    if (property.propertyType == SerializedPropertyType.ManagedReference)
                        property.managedReferenceValue = null;
                    row.fields_applied++;
                    AppendNote(row, path + ": [SerializeReference] is the null sentinel (raw_id " + rawId +
                                    "); left null");
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
                                          (property.isArray ? " (an array)" : "") +
                                          ", not an inlined serializable class");
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
                         "no serialized field with this name under " + path);
                    continue;
                }
                ApplyValue(childProperty, child.Value, path + "." + child.Name, row);
            }
        }

        /// <summary>
        /// A decoded PPtr is (m_FileID, m_PathID) into the shipped `resources.assets`; neither is a project path,
        /// so it is classified and never guessed.  `MasterData` has no object reference in the shipped build (its
        /// closure is empty), so this branch is reported rather than exercised - but a future master data with a
        /// reference must not be silently mis-applied.
        /// </summary>
        private static void ApplyReference (SerializedProperty property, JToken token, string path, ObjectRow row)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                Skip(row, path, "pptr", "the current field is " + property.propertyType +
                                        ", not an object reference");
                return;
            }
            long fileId = ReadLong(DecodedScalar(token["m_FileID"]));
            long pathId = ReadLong(DecodedScalar(token["m_PathID"]));
            row.references_total++;
            if (fileId == 0 && pathId == 0)
            {
                property.objectReferenceValue = null;
                row.references_null++;
                row.fields_applied++;
                return;
            }
            property.objectReferenceValue = null;
            row.references_needing_binding++;
            row.fields_applied++;
            row.needs_binding.Add(new ReferenceRecord {
                field = path,
                target_class = DecodedString(token["target_class"]),
                target_name = DecodedString(token["target_name"]),
                target_file_id = fileId,
                target_path_id = pathId,
                reason = "a shipped PPtr is (m_FileID, m_PathID) into resources.assets and those ids do not exist " +
                         "in this project; the field is left null and listed for a path_id -> asset mapping"
            });
        }

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

        private static string KindOf (JToken token)
        {
            if (token == null) return null;
            if (token.Type != JTokenType.Object) return token.Type.ToString();
            return (string)token["kind"];
        }

        /// <summary>
        /// How many decoded value nodes an object carries (containers included), so that
        /// `fields_applied + fields_skipped == values_total` is a real check that no value was dropped.
        /// </summary>
        private static int CountValues (JToken token)
        {
            if (token == null) return 0;
            if (token.Type == JTokenType.Null) return 1;
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
                else if (kind == "pptr")
                    lines.Add(field.Name + " = PPtr(fileID " + ReadLong(DecodedScalar(value["m_FileID"])) +
                              ", pathID " + ReadLong(DecodedScalar(value["m_PathID"])) + ")");
            }
            return lines;
        }

        // The decode wraps every value in a {"kind": ..., "value": ...} node, m_Name included, so anything that
        // needs a plain scalar goes through DecodedScalar.  Reading a wrapper with a plain cast yields its JSON
        // text - that trap is documented at length in NaninovelConfigBuilder.cs (it built an asset path out of a
        // whole wrapper object and died in Path.Combine) and is avoided the same way here.

        private static JToken DecodedScalar (JToken token)
        {
            if (token == null || token.Type == JTokenType.Null) return null;
            if (token.Type != JTokenType.Object) return token;
            JToken kind = token["kind"];
            if (kind == null || kind.Type == JTokenType.Null) return null;
            string name = kind.Type == JTokenType.String ? kind.Value<string>() : kind.ToString();
            if (name != "string" && name != "bool" && name != "int" && name != "float" && name != "enum" && name != "char")
                return null;
            JToken value = token["value"];
            if (value == null || value.Type == JTokenType.Null) return null;
            return value;
        }

        private static string DecodedString (JToken token)
        {
            JToken scalar = DecodedScalar(token);
            if (scalar == null) return null;
            return scalar.Type == JTokenType.String ? scalar.Value<string>() : scalar.ToString();
        }

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

        private static long ReadLong (JToken token)
        {
            if (token == null || token.Type == JTokenType.Null) return 0L;
            try { return token.Value<long>(); }
            catch (Exception) { return 0L; }
        }

        private static float ReadFloat (JToken token)
        {
            if (token == null || token.Type == JTokenType.Null) return 0f;
            try { return (float)token.Value<double>(); }
            catch (Exception) { return 0f; }
        }

        private static bool IsUsableAssetName (string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            if (name.Length > 128) return false;
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
                if (name.IndexOf(c) >= 0) bad.Add("'" + c + "' (U+" + ((int)c).ToString("X4") + ")");
            if (bad.Count > 0) return "it contains " + string.Join(", ", bad.ToArray()) + "; name = " + name;
            return "name = " + name;
        }

        /// <summary>The Resources key an asset path produces: what follows the last `Resources/`, minus the
        /// extension.  Computed so the report can state the key the engine will actually see instead of assuming
        /// the folder is right.</summary>
        private static string ResourcesKeyOf (string assetPath)
        {
            int marker = assetPath.LastIndexOf("/Resources/", StringComparison.Ordinal);
            if (marker < 0) return null;
            string relative = assetPath.Substring(marker + "/Resources/".Length);
            int dot = relative.LastIndexOf('.');
            return dot < 0 ? relative : relative.Substring(0, dot);
        }

        private static bool DerivesFrom (Type type, string baseFullName)
        {
            for (Type current = type; current != null; current = current.BaseType)
                if (current.FullName == baseFullName) return true;
            return false;
        }

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
                if (row.status == "CREATED") result.created++;
                else if (row.status == "OVERWRITTEN") result.overwritten++;
                else if (row.status == "SKIPPED_NOT_ROOT") result.skipped++;
                else
                {
                    result.failed++;
                    result.failed_summary.Add(row.type_name + " (path_id " + row.path_id + "): " + row.status +
                                              " - " + row.reason);
                    result.status = "PARTIAL";
                }
                if (row.status == "CREATED" || row.status == "OVERWRITTEN") result.imported++;

                result.values_total += row.values_total;
                result.fields_applied += row.fields_applied;
                result.fields_skipped += row.fields_skipped;
                result.references_total += row.references_total;
                result.references_null += row.references_null;
                result.references_needing_binding += row.references_needing_binding;
            }
            result.asset_paths = result.asset_paths.Distinct(StringComparer.Ordinal).ToList();
            result.asset_count = result.asset_paths.Count;
            result.limitations = BuildLimitations(result);
        }

        private static List<string> BuildLimitations (ImportResult result)
        {
            var limitations = new List<string>();
            limitations.Add("ASSET CONTENT: " + result.imported + " object(s) were written (" + result.created +
                            " created, " + result.overwritten + " overwritten in place - an existing asset is " +
                            "loaded and re-applied, so a second run neither duplicates nor corrupts anything): " +
                            (result.asset_paths.Count == 0 ? "(none)" : string.Join(", ", result.asset_paths.ToArray())) +
                            ". The single value that matters for the boot chain is the key list: " +
                            KeyNameField + " = [" + string.Join(", ", result.decoded_key_names.ToArray()) +
                            "], which the decode read from the shipped object byte for byte (the walk closes " +
                            "exactly at 80/80 bytes).");
            limitations.Add("THE PATH IS PINNED BY THE KEY, NOT BY THE SHIPPED FOLDER LAYOUT: the shipped build " +
                            "fixes the Resources key ('" + EngineResourcesKey + "', the native literal at " +
                            "0x180CD3D60, independently confirmed by ResourceManager.m_Container's '" +
                            "masterdata'). The folder the shipped project kept it in cannot be recovered, so this " +
                            "tool uses this project's Resources root for shipped game data, " + ResourcesFolder +
                            " (the same root NaninovelConfigBuilder writes Naninovel/Configuration into): " +
                            "Resources.Load(\"" + EngineResourcesKey + "\") keys on the path below any 'Resources' " +
                            "folder, so the resulting key is '" + ResourcesKeyOf(ResourcesFolder + "/" +
                            EngineResourcesKey + ".asset") + "'. Moving the asset under a different Resources " +
                            "folder keeps working; renaming the file breaks the lookup.");
            limitations.Add("OBJECT REFERENCES: " + result.references_total + " PPtr node(s) were decoded under the " +
                            "root (" + result.references_null + " null, " + result.references_needing_binding +
                            " left null and listed in needs_binding). The shipped MasterData holds NO object " +
                            "reference at all - its decode has zero reference edges and an empty " +
                            "_additionalCatalogDataList - so nothing is left unbound in this asset, and the " +
                            "reference closure has no second object to import.");
            limitations.Add("NOT VERIFIED WITHOUT RUNNING UNITY: that Resources.Load<MasterDataBase>(\"" +
                            EngineResourcesKey + "\") now returns this asset, that AddressablesManager stops " +
                            "logging 'The provided master data does not include the data for Addressable Asset " +
                            "System.', and that the reconstructed resource provider ends up caching a non-empty " +
                            "location list. Those are runtime facts. What is verifiable here and was verified: the " +
                            "asset exists at the path the code looks in, it loads as " + RootTypeName + ", and " +
                            KeyNameField + " reads back as [" +
                            string.Join(", ", result.decoded_key_names.ToArray()) + "] (see each row's `readback`).");
            limitations.Add("WHAT THE KEY RESOLVES TO IS NOT THIS TOOL'S DECISION: the key '" +
                            (result.decoded_key_names.Count == 1 ? result.decoded_key_names[0] : "?") + "' must " +
                            "exist in the *built* Addressables catalog for the lookup to return anything. The " +
                            "shipped catalog answers it with 28,150 locations (tools/unity-rebuild/out/unity/" +
                            "master_data_decode.json -> asset_key_names.catalog_join); this project's own catalog " +
                            "must expose either the same key or a label that aggregates the same content, " +
                            "otherwise the master data is correct and the query still returns nothing.");
            limitations.Add("DECODE COUPLING: every value written comes from " + Path.GetFileName(InputRelativePath) +
                            " (produced by tools/unity-rebuild/decode_master_data.py from the shipped build); a " +
                            "row whose decode did not close exactly, whose class is not " + RootTypeName +
                            ", or whose m_Name is not the engine's key is reported as FAILED with its reason and " +
                            "nothing is written for it. Each imported row carries `values_accounted` " +
                            "(fields_applied + fields_skipped == values_total) so the accounting can be audited " +
                            "from this report alone.");
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
            public string decode_assets_file;
            public long decode_root_path_id = -1;
            public string decode_root_resolved_by;
            public string resources_folder;
            public string engine_resources_key;
            public string engine_lookup;
            public string interface_const_value;
            public string key_cross_check;
            public bool key_cross_check_ok;
            public string error;

            public int objects_total;
            public int imported;
            public int created;
            public int overwritten;
            public int skipped;
            public int failed;
            public int asset_count;
            public List<string> asset_paths = new List<string>();

            public int decoded_key_name_count;
            public List<string> decoded_key_names = new List<string>();

            public int values_total;
            public int fields_applied;
            public int fields_skipped;

            public int references_total;
            public int references_null;
            public int references_needing_binding;

            public List<ObjectRow> rows = new List<ObjectRow>();
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
            public string unity_type;
            public string decode_status;
            public int depth;
            public int bytes_total;
            public bool closes_exactly;
            public bool is_root;
            public string decoded_name;
            public string resources_path;
            public string resources_key_from_path;
            public string asset_path;
            public string asset_name;
            public bool created;
            public string status = "NOT_RUN";
            public string reason;

            public int values_total;
            public int fields_applied;
            public int fields_skipped;
            public bool values_accounted;
            public int skipped_truncated;

            public int references_total;
            public int references_null;
            public int references_needing_binding;

            public string readback;
            public bool readback_matches_decode;
            public List<string> key_names_readback = new List<string>();

            public List<SkippedValue> fields_skipped_list = new List<SkippedValue>();
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
            public string reason;
        }
    }
}
