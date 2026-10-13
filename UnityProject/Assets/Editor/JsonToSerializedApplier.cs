// Shared JSON -> SerializedObject applier.
//
// Both composite converters (ScriptableObject and prefab/scene object graph) copy the shipped field
// values onto Unity objects by *serialised name*: the typetree dumps use exactly the names
// SerializedProperty.FindProperty resolves, so the mapping is name-for-name.  Everything that is
// easy to get wrong lives here once:
//
//   * strings first: SerializedProperty.isArray is true for a string, so a naive isArray-first
//     branch misreads every string as an array and leaves it empty
//   * enum indices are written as indices (that is how Unity serialises them)
//   * null vs non-null PPtr: null is a value; a non-null pointer is resolved through a caller
//     supplied resolver, and reported as unresolved instead of being dropped
//   * [SerializeReference] values are stored as {"rid": N} placeholders plus a `references` side
//     table ({"RefIds": [{rid, type:{class,ns,asm}, data}]}); instances are created level by level
//     because assigning managedReferenceValue only materialises children after
//     ApplyModifiedProperties
//
// Callers get a per-object report so a partial conversion is visible, never silent.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

namespace Manosaba.EditorTools
{
    public static class JsonToSerializedApplier
    {
        public class Options
        {
            /// <summary>Resolves a non-null PPtr token to an asset; return null to record it as unbound.</summary>
            public Func<JToken, UnityEngine.Object> ResolvePointer;

            /// <summary>Optional name prefix used in warnings (e.g. the object path inside a prefab).</summary>
            public string PathPrefix = "";

            /// <summary>
            /// Every top-level `m_*` key is skipped when true (right for ScriptableObjects: the only ones are the
            /// object header).  The prefab builder sets it to false and lists just the header keys it owns in
            /// <see cref="IgnoreKeys"/>, so component data such as Image.m_Sprite, Graphic.m_Color, TMP m_fontAsset
            /// and Selectable.m_TargetGraphic is applied instead of silently dropped.
            /// </summary>
            public bool SkipEngineFields = true;

            /// <summary>Extra keys to ignore (e.g. the references side table).</summary>
            public HashSet<string> IgnoreKeys = new HashSet<string> { "references" };

            /// <summary>
            /// A non-null pointer that cannot be resolved normally overwrites the field with null.  That is right for
            /// a script field (its default is null) but wrong for an engine field whose *default* is an object, such
            /// as SpriteRenderer.m_Materials[0] = Sprites-Default: nulling it turns the sprite magenta.  When true,
            /// an unresolved pointer is counted and reported but the field keeps the value it already has.
            /// </summary>
            public bool PreserveUnresolvedPointers = false;

            /// <summary>Content edits preserve fields that cannot safely be mapped back to Unity.</summary>
            public bool SafeEditableOnly = false;
            public bool AllowManagedReferences = false;

            /// <summary>Maximum number of warnings kept per object (the counters are always complete).</summary>
            public int MaxWarnings = 200;
        }

        public class Report
        {
            public int applied;
            public int warnings;
            public int pointersBound;
            public int pointersUnbound;
            /// <summary>Pointers bound only after converting the resolved asset to the field's type (see AdaptToFieldType).</summary>
            public int pointersAdapted;
            /// <summary>String leaves that name a type in the renamed Assembly-CSharp (see RemapAssemblyQualifiedName).</summary>
            public int assemblyNamesRemapped;
            public int managedTotal;
            public int managedApplied;
            public int managedFailed;
            public int skipped;
            /// <summary>
            /// Leaf values actually written (string/bool/int/float/enum/vector/...): the number that must be
            /// compared against the dump, because "applied" also counts containers and managed-reference
            /// placeholders and stayed at its full value while the old applier wrote nothing (2026-09-17).
            /// </summary>
            public int leavesWritten;
            /// <summary>`hasValue` booleans written as true: the Naninovel parameter count of a script asset.</summary>
            public int hasValueTrueWritten;
            public List<string> messages = new List<string>();
            public int maxMessages = 200;

            public void Warn(string message)
            {
                warnings++;
                if (messages.Count < maxMessages) messages.Add(message);
            }
        }

        private class PendingRef
        {
            public string path;
            public long rid;
            public bool assigned;
            public JToken entry;
        }

        [Serializable]
        public class FieldEditability
        {
            public string field;
            public string editability;
            public string reason;
            public int occurrences = 1;
        }

        /// <summary>Describe actual serialized fields; unsupported source nodes are never labelled editable.</summary>
        public static List<FieldEditability> DescribeEditableFields(SerializedObject serialized, JObject json,
                                                                    bool allowManagedReferences = false,
                                                                    bool skipEngineFields = true,
                                                                    HashSet<string> ignoreKeys = null)
        {
            var fields = new List<FieldEditability>();
            var refs = new Dictionary<long, JToken>();
            foreach (JToken entry in json["references"]?["RefIds"] as JArray ?? new JArray())
                if (entry["rid"] != null) refs[entry.Value<long>("rid")] = entry["data"];
            foreach (JProperty field in json.Properties())
            {
                if (field.Name == "references" || (skipEngineFields && field.Name.StartsWith("m_", StringComparison.Ordinal)) ||
                    (ignoreKeys != null && ignoreKeys.Contains(field.Name)))
                {
                    fields.Add(new FieldEditability { field = field.Name, editability = "READ_ONLY",
                        reason = "source identity / managed-reference type registry" });
                    continue;
                }
                Describe(serialized.FindProperty(field.Name), field.Value, field.Name, refs,
                         allowManagedReferences, fields, 0);
            }
            return CompactEditableFields(fields);
        }

        private sealed class CompactField
        {
            public FieldEditability field;
            public readonly HashSet<string> reasons = new HashSet<string>(StringComparer.Ordinal);
            public readonly List<string> orderedReasons = new List<string>();
        }

        private static List<FieldEditability> CompactEditableFields(IEnumerable<FieldEditability> fields)
        {
            var byPattern = new Dictionary<string, CompactField>(StringComparer.Ordinal);
            var ordered = new List<CompactField>();
            foreach (FieldEditability source in fields)
            {
                string pattern = Regex.Replace(source.field, @"\.Array\.data\[\d+\]", ".Array.data[*]");
                if (!byPattern.TryGetValue(pattern, out CompactField group))
                {
                    group = new CompactField { field = new FieldEditability {
                        field = pattern, editability = source.editability, occurrences = 0 } };
                    byPattern.Add(pattern, group);
                    ordered.Add(group);
                }
                else if (!string.Equals(group.field.editability, source.editability, StringComparison.Ordinal))
                    group.field.editability = "PARTIAL";
                group.field.occurrences += Math.Max(1, source.occurrences);
                if (!string.IsNullOrEmpty(source.reason) && group.reasons.Add(source.reason))
                    group.orderedReasons.Add(source.reason);
            }
            var compact = new List<FieldEditability>(ordered.Count);
            foreach (CompactField group in ordered)
            {
                group.field.reason = string.Join("; ", group.orderedReasons);
                compact.Add(group.field);
            }
            return compact;
        }

        private static void Describe(SerializedProperty prop, JToken token, string path, Dictionary<long, JToken> refs,
                                     bool managed, List<FieldEditability> fields, int depth)
        {
            if (depth > 64) throw new InvalidOperationException("editable field graph exceeds 64 levels");
            if (prop == null)
            {
                fields.Add(new FieldEditability { field = path, editability = "READ_ONLY", reason = "no serialized property" });
                return;
            }
            if (prop.propertyType == SerializedPropertyType.ManagedReference)
            {
                fields.Add(new FieldEditability { field = path, editability = managed ? "PARTIAL" : "READ_ONLY",
                    reason = "existing resolved managed type; edit primitive values, keep rid/type registry consistent" });
                if (!managed) return;
                if (token is JObject obj && obj["rid"] != null) refs.TryGetValue(obj.Value<long>("rid"), out token);
                if (token is JObject data)
                    foreach (JProperty child in data.Properties())
                        Describe(prop.FindPropertyRelative(child.Name), child.Value, path + "." + child.Name,
                                 refs, managed, fields, depth + 1);
                return;
            }
            if (prop.propertyType == SerializedPropertyType.String)
            {
                fields.Add(new FieldEditability { field = path, editability = "FULL", reason = "string" });
                return;
            }
            if (prop.isArray && token is JArray arr)
            {
                var arrayField = new FieldEditability { field = path, editability = "FULL",
                    reason = "array/list; element support listed below" };
                fields.Add(arrayField);
                if (arr.Count != prop.arraySize)
                {
                    arrayField.editability = "READ_ONLY";
                    arrayField.reason = "source resize was rejected; existing array retained";
                    return;
                }
                int firstChild = fields.Count;
                for (int i = 0; i < arr.Count && i < prop.arraySize; i++)
                    Describe(prop.GetArrayElementAtIndex(i), arr[i], path + ".Array.data[" + i + "]",
                             refs, managed, fields, depth + 1);
                if (fields.Skip(firstChild).Any(f => f.editability != "FULL")) arrayField.editability = "PARTIAL";
                return;
            }
            if (prop.propertyType == SerializedPropertyType.Generic && token is JObject nested)
            {
                foreach (JProperty child in nested.Properties())
                    Describe(prop.FindPropertyRelative(child.Name), child.Value, path + "." + child.Name,
                             refs, managed, fields, depth + 1);
                return;
            }
            bool pointer = prop.propertyType == SerializedPropertyType.ObjectReference;
            bool stablePointer = pointer && (token?["m_PathID"]?.Value<long>() == 0 || ResolveStablePointer(token) != null);
            bool supported = ReadValue(prop) != null && (!pointer || stablePointer);
            fields.Add(new FieldEditability { field = path, editability = supported ? "FULL" : "READ_ONLY",
                reason = pointer && !stablePointer ? "non-null dump PPtr has no stable GUID mapping; retained" : prop.propertyType.ToString() });
        }

        /// <summary>Read primitives for content and built-bundle probes using the same property names as the applier.</summary>
        public static JToken ReadValue(SerializedProperty prop)
        {
            if (prop == null) return null;
            switch (prop.propertyType)
            {
                case SerializedPropertyType.String: return new JValue(prop.stringValue);
                case SerializedPropertyType.Boolean: return new JValue(prop.boolValue);
                case SerializedPropertyType.Integer: return new JValue(prop.longValue);
                case SerializedPropertyType.Enum: case SerializedPropertyType.LayerMask: return new JValue(prop.intValue);
                case SerializedPropertyType.Float: return new JValue(prop.doubleValue);
                case SerializedPropertyType.ObjectReference:
                    if (prop.objectReferenceValue == null) return JValue.CreateNull();
                    if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(prop.objectReferenceValue, out string guid, out long fileId))
                        return new JObject { ["guid"] = guid, ["file_id"] = fileId };
                    return null;
                case SerializedPropertyType.Vector2: return new JObject { ["x"] = prop.vector2Value.x, ["y"] = prop.vector2Value.y };
                case SerializedPropertyType.Vector3: return VectorValue(prop.vector3Value);
                case SerializedPropertyType.Vector4: return new JObject { ["x"] = prop.vector4Value.x, ["y"] = prop.vector4Value.y, ["z"] = prop.vector4Value.z, ["w"] = prop.vector4Value.w };
                case SerializedPropertyType.Quaternion: return new JObject { ["x"] = prop.quaternionValue.x, ["y"] = prop.quaternionValue.y, ["z"] = prop.quaternionValue.z, ["w"] = prop.quaternionValue.w };
                case SerializedPropertyType.Color: return new JObject { ["r"] = prop.colorValue.r, ["g"] = prop.colorValue.g, ["b"] = prop.colorValue.b, ["a"] = prop.colorValue.a };
                case SerializedPropertyType.Rect: return new JObject { ["x"] = prop.rectValue.x, ["y"] = prop.rectValue.y, ["width"] = prop.rectValue.width, ["height"] = prop.rectValue.height };
                case SerializedPropertyType.Vector2Int: return new JObject { ["x"] = prop.vector2IntValue.x, ["y"] = prop.vector2IntValue.y };
                case SerializedPropertyType.Vector3Int: return new JObject { ["x"] = prop.vector3IntValue.x, ["y"] = prop.vector3IntValue.y, ["z"] = prop.vector3IntValue.z };
            }
            return null;
        }

        private static JObject VectorValue(Vector3 value) => new JObject { ["x"] = value.x, ["y"] = value.y, ["z"] = value.z };

        /// <summary>Explicit GUID + local file id references are accepted; ambiguous asset subobjects are rejected.</summary>
        public static UnityEngine.Object ResolveStablePointer(JToken token)
        {
            string guid = (string)token?["guid"];
            if (string.IsNullOrEmpty(guid) || !Regex.IsMatch(guid, "^[0-9a-fA-F]{32}$")) return null;
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            if (string.IsNullOrEmpty(assetPath)) return null;
            long wanted = token["file_id"]?.Value<long>() ?? 0;
            if (wanted == 0) return AssetDatabase.LoadMainAssetAtPath(assetPath);
            foreach (UnityEngine.Object asset in AssetDatabase.LoadAllAssetsAtPath(assetPath))
                if (AssetDatabase.TryGetGUIDAndLocalFileIdentifier(asset, out string _, out long localId) && localId == wanted)
                    return asset;
            return null;
        }

        /// <summary>Copy every field of a dump JSON onto the serialised object.</summary>
        public static Report ApplyAll(SerializedObject serialized, JObject json, Options options = null)
        {
            options = options ?? new Options();
            var report = new Report { maxMessages = options.MaxWarnings };

            var refTable = new Dictionary<long, JToken>();
            if (json["references"] is JObject refs)
            {
                foreach (JToken entry in refs["RefIds"] as JArray ?? new JArray())
                {
                    if (entry["rid"] != null) refTable[entry.Value<long>("rid")] = entry;
                }
            }
            report.managedTotal = refTable.Count;

            var pending = new List<PendingRef>();
            foreach (JProperty field in json.Properties())
            {
                string name = field.Name;
                if (name == "m_Name") continue;
                if (options.IgnoreKeys != null && options.IgnoreKeys.Contains(name)) continue;
                if (options.SkipEngineFields && name.StartsWith("m_", StringComparison.Ordinal)) continue;

                SerializedProperty prop = serialized.FindProperty(name);
                if (prop == null)
                {
                    report.Warn(options.PathPrefix + name + ": no serialised field");
                    report.skipped++;
                    continue;
                }
                Apply(prop, field.Value, options.PathPrefix + name, serialized, refTable, pending, options, report);
            }

            // Materialise the managed-reference graph level by level: assigning managedReferenceValue
            // changes a property's shape, so children only exist after ApplyModifiedProperties.
            int processed = 0;
            var existingManagedRids = new Dictionary<long, long>();
            while (processed < pending.Count)
            {
                int levelEnd = pending.Count;
                for (int i = processed; i < levelEnd; i++) AssignInstance(pending[i], serialized, refTable, options, report, existingManagedRids);
                serialized.ApplyModifiedPropertiesWithoutUndo();
                for (int i = processed; i < levelEnd; i++)
                {
                    ApplyInstanceData(pending[i], serialized, refTable, pending, options, report);
                }
                serialized.ApplyModifiedPropertiesWithoutUndo();
                processed = levelEnd;
            }

            serialized.ApplyModifiedPropertiesWithoutUndo();
            return report;
        }

        private static void Apply(SerializedProperty prop, JToken token, string path, SerializedObject serialized,
                                  Dictionary<long, JToken> refTable, List<PendingRef> pending,
                                  Options options, Report report)
        {
            if (options.SafeEditableOnly && prop.propertyType == SerializedPropertyType.ManagedReference &&
                !options.AllowManagedReferences)
            {
                report.skipped++;
                report.Warn(path + ": READ_ONLY managed-reference graph");
                return;
            }
            if (token == null || token.Type == JTokenType.Null)
            {
                report.applied++;
                return;
            }

            if (prop.propertyType == SerializedPropertyType.ManagedReference)
            {
                // A { rid: N } token is the first pass: queue creation of the concrete instance.
                if (token["rid"] != null)
                {
                    pending.Add(new PendingRef { path = prop.propertyPath, rid = token.Value<long>("rid") });
                    report.applied++;
                    return;
                }

                // After the instance is materialised, ApplyInstanceData feeds the reference-table
                // "data" object back through this method. At that point the property is still
                // ManagedReference, but the token intentionally has no rid; recurse into the
                // concrete instance's serialised children instead of treating it as an empty value.
                if (token.Type != JTokenType.Object)
                {
                    report.Warn(path + ": expected managed-reference object, got " + token.Type);
                    return;
                }
                foreach (JProperty child in ((JObject)token).Properties())
                {
                    SerializedProperty childProp = prop.FindPropertyRelative(child.Name);
                    if (childProp == null)
                    {
                        report.Warn(path + "." + child.Name + ": no serialised field");
                        report.skipped++;
                        continue;
                    }
                    Apply(childProp, child.Value, path + "." + child.Name, serialized, refTable, pending, options, report);
                }
                report.applied++;
                return;
            }

            // Unity reports isArray == true for a string (a string is a char array in the
            // serialisation API), so the string case has to come first -- otherwise every string
            // field is mistaken for an array and silently left empty.
            if (prop.propertyType == SerializedPropertyType.String)
            {
                string text = token.Type == JTokenType.String ? token.Value<string>() : token.ToString();
                string remapped = RemapAssemblyQualifiedName(text);
                if (!ReferenceEquals(remapped, text)) report.assemblyNamesRemapped++;
                prop.stringValue = remapped;
                report.applied++;
                report.leavesWritten++;
                return;
            }

            if (prop.isArray)
            {
                if (token.Type != JTokenType.Array)
                {
                    report.Warn(path + ": expected array, got " + token.Type);
                    return;
                }
                var items = (JArray)token;
                if (options.SafeEditableOnly && items.Count != prop.arraySize)
                {
                    string resizeReason;
                    if (ContainsUnstablePointer(items))
                        resizeReason = "array resize includes unmapped references";
                    else if (CanResizeEditableArray(prop, options, out resizeReason))
                        resizeReason = null;
                    if (resizeReason != null)
                    {
                        report.skipped++;
                        report.Warn(path + ": READ_ONLY " + resizeReason + "; existing array retained");
                        return;
                    }
                }
                prop.arraySize = items.Count;
                for (int i = 0; i < items.Count; i++)
                {
                    Apply(prop.GetArrayElementAtIndex(i), items[i], path + "[" + i + "]", serialized, refTable,
                          pending, options, report);
                }
                report.applied++;
                return;
            }

            if (options.SafeEditableOnly && prop.propertyType != SerializedPropertyType.Generic &&
                prop.propertyType != SerializedPropertyType.ObjectReference && ReadValue(prop) == null)
            {
                report.skipped++;
                report.Warn(path + ": READ_ONLY " + prop.propertyType + " has no verified value adapter");
                return;
            }

            switch (prop.propertyType)
            {
                case SerializedPropertyType.Integer:
                    // Unity's SerializedProperty reports both signed and unsigned 32-bit
                    // typetree fields as Integer.  AnimationClip binding hashes and streamed
                    // curve data can exceed Int32.MaxValue even though they are only 32 bits;
                    // token.Value<int>() throws OverflowException before Unity can receive the
                    // raw bits.  longValue accepts the serialized integer width and preserves
                    // those values (the same path already used by SafeEditableOnly).
                    prop.longValue = token.Type == JTokenType.Boolean ? (token.Value<bool>() ? 1L : 0L) : token.Value<long>();
                    report.leavesWritten++;
                    break;
                case SerializedPropertyType.Boolean:
                    prop.boolValue = token.Type == JTokenType.Boolean ? token.Value<bool>() : token.Value<int>() != 0;
                    report.leavesWritten++;
                    if (prop.boolValue && prop.name == "hasValue") report.hasValueTrueWritten++;
                    break;
                case SerializedPropertyType.Float:
                    if (options.SafeEditableOnly) prop.doubleValue = token.Value<double>();
                    else prop.floatValue = token.Value<float>();
                    report.leavesWritten++;
                    break;
                case SerializedPropertyType.Enum:
                    if (token.Type == JTokenType.Integer)
                    {
                        // The dump holds the RAW serialized value, not an index into enumNames.  The two coincide only
                        // for an enum numbered 0..n-1 in declaration order; TMP's FontWeight (100..900),
                        // TextAlignmentOptions (0x101, 0x102, ...), FontFeature, Canvas.AdditionalShaderChannels and every
                        // [Flags] enum are not, and treating the value as an index rejected them ("enum index 400 out of
                        // range (9)", 128 hits per UI prefab set) or, worse, picked the wrong member when the value
                        // happened to be a valid index.
                        prop.intValue = unchecked((int)token.Value<long>());
                        report.leavesWritten++;
                    }
                    else
                    {
                        int index = Array.IndexOf(prop.enumNames, token.ToString());
                        if (index >= 0) { prop.enumValueIndex = index; report.leavesWritten++; }
                        else report.Warn(path + ": enum name '" + token + "' unknown");
                    }
                    break;
                case SerializedPropertyType.ObjectReference:
                    long pathId = token["m_PathID"] != null ? token.Value<long>("m_PathID") : 0;
                    if (pathId == 0 && token["guid"] == null)
                    {
                        prop.objectReferenceValue = null;
                        report.applied++;
                        report.leavesWritten++;
                        return;
                    }
                    UnityEngine.Object target = options.ResolvePointer?.Invoke(token);
                    if (target != null) target = AdaptToFieldType(prop, target, report);
                    if (target != null)
                    {
                        prop.objectReferenceValue = target;
                        report.pointersBound++;
                        report.leavesWritten++;
                    }
                    else
                    {
                        if (!options.PreserveUnresolvedPointers && !options.SafeEditableOnly) prop.objectReferenceValue = null;
                        report.pointersUnbound++;
                        report.Warn(path + ": unresolved PPtr (m_FileID=" + token["m_FileID"] + ", m_PathID=" + pathId + ")");
                    }
                    break;
                case SerializedPropertyType.Generic:
                    if (token.Type != JTokenType.Object)
                    {
                        report.Warn(path + ": expected object, got " + token.Type);
                        return;
                    }
                    foreach (JProperty child in ((JObject)token).Properties())
                    {
                        SerializedProperty childProp = prop.FindPropertyRelative(child.Name);
                        if (childProp == null)
                        {
                            report.Warn(path + "." + child.Name + ": no serialised field");
                            continue;
                        }
                        Apply(childProp, child.Value, path + "." + child.Name, serialized, refTable, pending, options, report);
                    }
                    break;
                case SerializedPropertyType.ManagedReference:
                    report.Warn(path + ": unexpected managed-reference token " + token.Type);
                    return;
                case SerializedPropertyType.Vector2:
                    prop.vector2Value = ReadVector2(token);
                    report.leavesWritten++;
                    break;
                case SerializedPropertyType.Vector3:
                    prop.vector3Value = ReadVector3(token);
                    report.leavesWritten++;
                    break;
                case SerializedPropertyType.Vector4:
                    JToken w4 = token["w"];
                    prop.vector4Value = new Vector4(ReadFloat(token, "x"), ReadFloat(token, "y"),
                                                    ReadFloat(token, "z"), w4 != null ? w4.Value<float>() : 0f);
                    report.leavesWritten++;
                    break;
                case SerializedPropertyType.Quaternion:
                    prop.quaternionValue = new Quaternion(ReadFloat(token, "x"), ReadFloat(token, "y"),
                                                          ReadFloat(token, "z"), ReadFloat(token, "w", 1f));
                    report.leavesWritten++;
                    break;
                case SerializedPropertyType.Color:
                    prop.colorValue = ReadColor(token);
                    report.leavesWritten++;
                    break;
                case SerializedPropertyType.Rect:
                    prop.rectValue = new Rect(ReadFloat(token, "x"), ReadFloat(token, "y"),
                                              ReadFloat(token, "width"), ReadFloat(token, "height"));
                    report.leavesWritten++;
                    break;
                case SerializedPropertyType.LayerMask:
                    prop.intValue = ReadLayerMask(token);
                    report.leavesWritten++;
                    break;
                case SerializedPropertyType.Vector2Int:
                    prop.vector2IntValue = new Vector2Int(ReadInt(token, "x"), ReadInt(token, "y"));
                    report.leavesWritten++;
                    break;
                case SerializedPropertyType.Vector3Int:
                    prop.vector3IntValue = new Vector3Int(ReadInt(token, "x"), ReadInt(token, "y"), ReadInt(token, "z"));
                    report.leavesWritten++;
                    break;
                case SerializedPropertyType.RectInt:
                    prop.rectIntValue = new RectInt(ReadInt(token, "x"), ReadInt(token, "y"),
                                                    ReadInt(token, "width"), ReadInt(token, "height"));
                    report.leavesWritten++;
                    break;
                case SerializedPropertyType.Bounds:
                    prop.boundsValue = new Bounds(ReadVector3(token["m_Center"]), ReadVector3(token["m_Extent"]) * 2f);
                    report.leavesWritten++;
                    break;
                case SerializedPropertyType.Gradient:
                    prop.gradientValue = ReadGradient(token);
                    report.leavesWritten++;
                    break;
                case SerializedPropertyType.AnimationCurve:
                    // ParticleSystem modules expose their min/max curves as this leaf type, not
                    // Generic children. Never replace a malformed curve with an empty default.
                    prop.animationCurveValue = ReadAnimationCurve(token);
                    report.leavesWritten++;
                    break;
                case SerializedPropertyType.ArraySize:
                    report.Warn(path + ": ArraySize property is set through its array");
                    return;
                default:
                    report.Warn(path + ": unsupported property type " + prop.propertyType + " left at default");
                    return;
            }

            report.applied++;
        }

        /// <summary>"Sprite" from "PPtr&lt;Sprite&gt;", "ChoiceHandlerButton" from "PPtr&lt;$ChoiceHandlerButton&gt;"; null when unknown.</summary>
        private static string ExpectedPointerType(SerializedProperty prop)
        {
            string t = prop.type;
            if (string.IsNullOrEmpty(t) || !t.StartsWith("PPtr<", StringComparison.Ordinal) || !t.EndsWith(">", StringComparison.Ordinal))
                return null;
            return t.Substring(5, t.Length - 6).TrimStart('$');
        }

        /// <summary>
        /// The pointer resolver knows an asset *path*; the field knows the object *type* it needs.  Two shipped
        /// shapes differ from what LoadAssetAtPath returns, and assigning the wrong type is refused by Unity:
        ///   * a sprite-mode PNG's main asset is its Texture2D, the field wants the Sprite sub-asset
        ///     (Image.m_Sprite, SpriteRenderer.m_Sprite, ...);
        ///   * a prefab's main asset is its root GameObject, a template field wants a component of that prefab
        ///     (ChoiceHandlerPanel.defaultButtonPrefab, RebuildableToggle._labelPrefab): the component of the
        ///     declared field type on the prefab root, the only place the shipped data ever points.
        /// Anything else is returned unchanged.  Adapted pointers are counted, so the rule is visible in reports.
        /// </summary>
        private static UnityEngine.Object AdaptToFieldType(SerializedProperty prop, UnityEngine.Object target, Report report)
        {
            string wanted = ExpectedPointerType(prop);
            if (wanted == null || wanted == "Object") return target;

            if (wanted == "Sprite" && target is Texture2D)
            {
                Sprite sprite = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(target)).OfType<Sprite>().FirstOrDefault();
                if (sprite != null) report.pointersAdapted++;
                return sprite;
            }

            if (target is GameObject go && wanted != "GameObject")
            {
                List<Type> candidates = ComponentTypesNamed(wanted);
                foreach (Type candidate in candidates)
                {
                    Component onRoot = go.GetComponent(candidate);
                    if (onRoot != null)              // Unity's overloaded !=: GetComponent returns a fake null
                    {
                        report.pointersAdapted++;
                        return onRoot;
                    }
                }
                // The shipped pointer names one specific component.  When it is not on the prefab root it lives further
                // down (RebuildableToggle._labelPrefab -> the label child of RebuildableToggle.prefab).  That is only
                // accepted when the declared type occurs exactly once in the whole prefab, so the pick is not a guess;
                // several candidates are reported as ambiguous and left unbound.
                foreach (Type candidate in candidates)
                {
                    Component[] all = go.GetComponentsInChildren(candidate, true);
                    if (all.Length == 1)
                    {
                        report.pointersAdapted++;
                        return all[0];
                    }
                    if (all.Length > 1)
                    {
                        report.Warn(prop.propertyPath + ": " + all.Length + " " + candidate.Name + " components in prefab '" +
                                    go.name + "', pointer target is ambiguous");
                        return null;
                    }
                }
                return null;
            }
            return target;
        }

        private static Dictionary<string, List<Type>> _componentTypesByName;

        private static List<Type> ComponentTypesNamed(string name)
        {
            if (_componentTypesByName == null)
            {
                _componentTypesByName = new Dictionary<string, List<Type>>(StringComparer.Ordinal);
                foreach (System.Reflection.Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
                {
                    Type[] types;
                    try { types = asm.GetTypes(); }
                    catch (System.Reflection.ReflectionTypeLoadException ex) { types = ex.Types.Where(t => t != null).ToArray(); }
                    foreach (Type t in types)
                    {
                        if (!typeof(Component).IsAssignableFrom(t)) continue;
                        if (!_componentTypesByName.TryGetValue(t.Name, out List<Type> list))
                            _componentTypesByName[t.Name] = list = new List<Type>();
                        list.Add(t);
                    }
                }
            }
            return _componentTypesByName.TryGetValue(name, out List<Type> found) ? found : new List<Type>();
        }

        // Resizing removes or creates whole elements, including fields the leaf applier deliberately retains.
        // Check the existing serialized shape before touching arraySize. This does not mutate the target.
        private static bool CanResizeEditableArray(SerializedProperty array, Options options, out string reason)
        {
            reason = null;
            if (array.arraySize == 0 && !IsSupportedEmptyArrayElement(array.arrayElementType))
            {
                reason = "empty array element type is not verified: " + array.arrayElementType;
                return false;
            }
            var visitedManagedIds = new HashSet<long>();
            using (SerializedProperty cursor = array.Copy())
            using (SerializedProperty end = array.GetEndProperty())
            {
                bool enterChildren = true;
                while (cursor.Next(enterChildren) && !SerializedProperty.EqualContents(cursor, end))
                {
                    enterChildren = false;
                    if (cursor.propertyType == SerializedPropertyType.ArraySize ||
                        cursor.propertyType == SerializedPropertyType.String) continue;
                    if (cursor.propertyType == SerializedPropertyType.ManagedReference)
                    {
                        if (!options.AllowManagedReferences)
                        {
                            reason = "array element includes a retained managed-reference graph";
                            return false;
                        }
                        // Unity can expose shared/cyclic managed children. Enter each serialized managed ID once.
                        enterChildren = cursor.managedReferenceValue != null &&
                                        visitedManagedIds.Add(cursor.managedReferenceId);
                        continue;
                    }
                    if (cursor.propertyType == SerializedPropertyType.Generic)
                    {
                        if (cursor.isArray && cursor.arraySize == 0 &&
                            !IsSupportedEmptyArrayElement(cursor.arrayElementType))
                        {
                            reason = "array element includes an unverified empty array: " + cursor.propertyPath;
                            return false;
                        }
                        if (!cursor.isArray && !cursor.hasChildren)
                        {
                            reason = "array element includes an opaque serialized value: " + cursor.propertyPath;
                            return false;
                        }
                        enterChildren = true;
                        continue;
                    }
                    if (ReadValue(cursor) == null)
                    {
                        reason = "array element includes retained " + cursor.propertyType + ": " + cursor.propertyPath;
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool IsSupportedEmptyArrayElement(string elementType)
        {
            // No temporary element insertion: a rejected edit must preserve the complete old serialized shape.
            // Unknown structs, PPtrs, enums and managed types require an existing element to inspect.
            switch (elementType)
            {
                case "bool": case "sbyte": case "byte": case "short": case "ushort":
                case "int": case "uint": case "long": case "ulong": case "float": case "double": case "string":
                case "SInt8": case "UInt8": case "SInt16": case "UInt16": case "SInt32": case "UInt32":
                case "SInt64": case "UInt64":
                case "Vector2": case "Vector2f": case "Vector3": case "Vector3f": case "Vector4": case "Vector4f":
                case "Quaternion": case "Quaternionf": case "Color": case "ColorRGBA": case "Color32":
                case "Rect": case "Rectf": case "Vector2Int": case "Vector2i": case "Vector3Int": case "Vector3i":
                    return true;
                default: return false;
            }
        }

        private static bool ContainsUnstablePointer(JToken token)
        {
            if (token is JObject obj)
            {
                if (obj["guid"] != null && ResolveStablePointer(obj) == null) return true;
                if (obj["m_PathID"] != null && obj["m_PathID"].Value<long>() != 0 && ResolveStablePointer(obj) == null) return true;
                return obj.Properties().Any(p => ContainsUnstablePointer(p.Value));
            }
            return token is JArray array && array.Any(ContainsUnstablePointer);
        }

        private static float ReadFloat(JToken token, string key, float fallback = 0f)
        {
            JToken value = token?[key];
            return value == null ? fallback : value.Value<float>();
        }

        private static int ReadInt(JToken token, string key)
        {
            JToken value = token?[key];
            return value == null ? 0 : value.Value<int>();
        }

        /// <summary>Reconstruct scalar particle curves, retaining explicit tangents and weights.</summary>
        private static AnimationCurve ReadAnimationCurve(JToken token)
        {
            if (!(token?["m_Curve"] is JArray source) || source.Count > 100000)
                throw new InvalidOperationException("AnimationCurve requires a bounded m_Curve array");
            var keys = new Keyframe[source.Count];
            float previous = float.NegativeInfinity;
            for (int i = 0; i < keys.Length; i++)
            {
                JToken key = source[i];
                float time = CurveNumber(key, "time"), value = CurveNumber(key, "value");
                if (time < previous) throw new InvalidOperationException("AnimationCurve keys are not time ordered");
                previous = time;
                int mode = key["weightedMode"]?.Value<int>() ?? 0;
                if (mode < 0 || mode > 3) throw new InvalidOperationException("AnimationCurve weightedMode outside 0..3");
                keys[i] = new Keyframe(time, value, CurveNumber(key, "inSlope", true),
                    CurveNumber(key, "outSlope", true), CurveNumber(key, "inWeight", false, 1f / 3f),
                    CurveNumber(key, "outWeight", false, 1f / 3f)) { weightedMode = (WeightedMode)mode };
            }
            return new AnimationCurve(keys)
            {
                preWrapMode = CurveWrap(token["m_PreInfinity"]),
                postWrapMode = CurveWrap(token["m_PostInfinity"]),
            };
        }

        private static float CurveNumber(JToken token, string key, bool allowInfinity = false, float? fallback = null)
        {
            if (token?[key] == null && !fallback.HasValue)
                throw new InvalidOperationException("AnimationCurve is missing " + key);
            float value = token?[key]?.Value<float>() ?? fallback.Value;
            if (float.IsNaN(value) || (!allowInfinity && float.IsInfinity(value)))
                throw new InvalidOperationException("AnimationCurve has invalid " + key);
            return value;
        }

        private static WrapMode CurveWrap(JToken token)
        {
            // Serialized InfinityType differs from the public WrapMode enum: 2 means
            // constant/clamp, NOT public WrapMode.Loop. Title particle dumps use 2.
            switch (token?.Value<int>() ?? 2)
            {
                case 2: return WrapMode.ClampForever;
                case 3: return WrapMode.Loop;
                case 4: return WrapMode.PingPong;
                default: throw new InvalidOperationException("Unsupported serialized AnimationCurve infinity mode " + token);
            }
        }

        /// <summary>Validate actual Unity values after applying a ParticleSystem dump.</summary>
        public static int VerifyAnimationCurves(SerializedObject serialized, JObject json)
        {
            serialized.Update();
            int checkedCurves = 0;
            void Check(SerializedProperty property, JToken value, JObject parentSource = null)
            {
                if (property == null || value == null) return;
                if (property.propertyType == SerializedPropertyType.AnimationCurve)
                {
                    AnimationCurve expected = ReadAnimationCurve(value), actual = property.animationCurveValue;
                    bool normalizedEmptyDelay = IsNormalizedEmptyStartDelay(serialized, property, parentSource, expected, actual);
                    if (actual == null || (actual.length != expected.length && !normalizedEmptyDelay) ||
                        actual.preWrapMode != expected.preWrapMode || actual.postWrapMode != expected.postWrapMode)
                        throw new InvalidOperationException("AnimationCurve readback mismatch: " + property.propertyPath +
                            " expected keys=" + expected.length + " wrap=" + expected.preWrapMode + "/" + expected.postWrapMode +
                            " actual keys=" + (actual == null ? -1 : actual.length) + " wrap=" +
                            (actual == null ? "null" : actual.preWrapMode + "/" + actual.postWrapMode));
                    Keyframe[] a = actual.keys, e = expected.keys;
                    for (int i = 0; i < e.Length; i++)
                        if (a[i].time != e[i].time || a[i].value != e[i].value ||
                            a[i].inTangent != e[i].inTangent || a[i].outTangent != e[i].outTangent ||
                            a[i].weightedMode != e[i].weightedMode ||
                            (e[i].weightedMode != WeightedMode.None &&
                             (a[i].inWeight != e[i].inWeight || a[i].outWeight != e[i].outWeight)))
                            throw new InvalidOperationException("AnimationCurve key readback mismatch: " + property.propertyPath + "[" + i +
                                "] expected " + CurveKeySummary(e[i]) + " actual " + CurveKeySummary(a[i]));
                    checkedCurves++;
                }
                else if (property.isArray && value is JArray array)
                    for (int i = 0; i < Math.Min(array.Count, property.arraySize); i++) Check(property.GetArrayElementAtIndex(i), array[i]);
                else if (property.propertyType == SerializedPropertyType.Generic && value is JObject obj)
                    foreach (JProperty child in obj.Properties()) Check(property.FindPropertyRelative(child.Name), child.Value, obj);
            }
            foreach (JProperty field in json.Properties()) Check(serialized.FindProperty(field.Name), field.Value);
            return checkedCurves;
        }

        private static bool IsNormalizedEmptyStartDelay(SerializedObject serialized, SerializedProperty property,
            JObject source, AnimationCurve expected, AnimationCurve actual)
        {
            // Measured Unity 6000.0.48f1 normalization after prefab save: constant zero startDelay
            // changes its unused empty min/max curves to two zero keys at t=0/1. No other curves
            // receive this exception; active and nonempty curves must still match every source key.
            if ((property.propertyPath != "startDelay.maxCurve" && property.propertyPath != "startDelay.minCurve") ||
                source?["minMaxState"]?.Value<int>() != 0 || expected.length != 0 || actual == null || actual.length != 2 ||
                ReadFloat(source, "scalar") != 0f || ReadFloat(source, "minScalar") != 0f)
                return false;
            SerializedProperty mode = serialized.FindProperty("startDelay.minMaxState");
            SerializedProperty scalar = serialized.FindProperty("startDelay.scalar");
            SerializedProperty minimum = serialized.FindProperty("startDelay.minScalar");
            if (mode == null || scalar == null || minimum == null || mode.intValue != 0 ||
                scalar.floatValue != 0f || minimum.floatValue != 0f) return false;
            Keyframe[] keys = actual.keys;
            return keys[0].time == 0f && keys[1].time == 1f && keys.All(key =>
                key.value == 0f && key.inTangent == 0f && key.outTangent == 0f && key.weightedMode == WeightedMode.None);
        }

        private static string CurveKeySummary(Keyframe key) =>
            "time=" + key.time + " value=" + key.value + " slopes=" + key.inTangent + "/" + key.outTangent +
            " weightedMode=" + key.weightedMode + " weights=" + key.inWeight + "/" + key.outWeight;

        /// <summary>
        /// A serialized Gradient is eight colour slots `key0..key7` (RGBA), eight `ctime0..7` and `atime0..7`
        /// (ushort, 0..65535) and the two counts. Colour key i is the RGB of key_i at ctime_i; alpha key j
        /// is the A of key_j at atime_j. The same slot array carries both.
        /// </summary>
        private static Gradient ReadGradient(JToken token)
        {
            int colorCount = ReadInt(token, "m_NumColorKeys");
            int alphaCount = ReadInt(token, "m_NumAlphaKeys");
            var colorKeys = new GradientColorKey[colorCount];
            for (int i = 0; i < colorCount; i++)
            {
                JToken key = token["key" + i];
                colorKeys[i] = new GradientColorKey(new Color(ReadFloat(key, "r"), ReadFloat(key, "g"), ReadFloat(key, "b"), 1f),
                                                    ReadInt(token, "ctime" + i) / 65535f);
            }
            var alphaKeys = new GradientAlphaKey[alphaCount];
            for (int i = 0; i < alphaCount; i++)
            {
                alphaKeys[i] = new GradientAlphaKey(ReadFloat(token["key" + i], "a", 1f), ReadInt(token, "atime" + i) / 65535f);
            }
            var gradient = new Gradient();
            gradient.SetKeys(colorKeys, alphaKeys);
            gradient.mode = (GradientMode)ReadInt(token, "m_Mode");
            return gradient;
        }

        private static Vector2 ReadVector2(JToken token)
        {
            return new Vector2(ReadFloat(token, "x"), ReadFloat(token, "y"));
        }

        private static Vector3 ReadVector3(JToken token)
        {
            return new Vector3(ReadFloat(token, "x"), ReadFloat(token, "y"), ReadFloat(token, "z"));
        }

        /// <summary>Typetrees store LayerMask as unsigned m_Bits; Unity's property exposes the same 32 bits as int.</summary>
        private static int ReadLayerMask(JToken token)
        {
            JToken bits = token is JObject mask ? mask["m_Bits"] : token;
            return unchecked((int)Read32Bits(bits, "LayerMask"));
        }

        private static uint Read32Bits(JToken token, string fieldType)
        {
            if (token == null || token.Type != JTokenType.Integer)
                throw new InvalidOperationException(fieldType + " requires a 32-bit integer; existing value retained");
            long value = token.Value<long>();
            if (value < int.MinValue || value > uint.MaxValue)
                throw new InvalidOperationException(fieldType + " is outside the 32-bit range; existing value retained");
            return unchecked((uint)value);
        }

        private static Color ReadColor(JToken token)
        {
            if (!(token is JObject color))
                throw new InvalidOperationException("Color requires channel values or packed rgba; existing value retained");
            if (color["rgba"] != null)
            {
                uint rgba = Read32Bits(color["rgba"], "Color32.rgba");
                return new Color((rgba & 255) / 255f, ((rgba >> 8) & 255) / 255f,
                                 ((rgba >> 16) & 255) / 255f, ((rgba >> 24) & 255) / 255f);
            }
            foreach (string channel in new[] { "r", "g", "b" })
                if (color[channel] == null || (color[channel].Type != JTokenType.Integer && color[channel].Type != JTokenType.Float))
                    throw new InvalidOperationException("Color requires numeric r/g/b channels; existing value retained");
            if (color["a"] != null && color["a"].Type != JTokenType.Integer && color["a"].Type != JTokenType.Float)
                throw new InvalidOperationException("Color alpha must be numeric; existing value retained");
            return new Color(ReadFloat(token, "r"), ReadFloat(token, "g"),
                             ReadFloat(token, "b"), ReadFloat(token, "a", 1f));
        }

        /// <summary>Create the concrete instance named by the reference table and assign it.</summary>
        private static void AssignInstance(PendingRef pendingRef, SerializedObject serialized,
                                           Dictionary<long, JToken> refTable, Options options, Report report,
                                           Dictionary<long, long> existingManagedRids)
        {
            if (!refTable.TryGetValue(pendingRef.rid, out JToken entry))
            {
                report.Warn(pendingRef.path + ": rid " + pendingRef.rid + " not in references table");
                report.managedFailed++;
                return;
            }

            JToken typeInfo = entry["type"];
            string className = (string)typeInfo?["class"];
            string ns = (string)typeInfo?["ns"];
            string asm = (string)typeInfo?["asm"];
            string fullName = string.IsNullOrEmpty(ns) ? className : ns + "." + className;

            Type concrete = ResolveType(fullName, Alias(asm));
            if (concrete == null)
            {
                report.Warn(pendingRef.path + ": managed reference type not found: " + fullName + " (" + asm + ")");
                report.managedFailed++;
                return;
            }

            SerializedProperty prop = serialized.FindProperty(pendingRef.path);
            if (prop == null)
            {
                report.Warn(pendingRef.path + ": property not found for rid " + pendingRef.rid);
                report.managedFailed++;
                return;
            }

            try
            {
                object current = prop.managedReferenceValue;
                if (options.SafeEditableOnly)
                {
                    if (current == null || current.GetType() != concrete)
                    {
                        report.Warn(pendingRef.path + ": READ_ONLY managed creation/type replacement; existing graph retained");
                        report.managedFailed++;
                        return;
                    }
                    // Recreating an existing command empties nested arrays and drops retained native values.
                    // Edit its current serialized graph so array guards inspect the actual existing elements.
                    long existingId = prop.managedReferenceId;
                    if (existingManagedRids.TryGetValue(existingId, out long sourceRid) && sourceRid != pendingRef.rid)
                    {
                        report.Warn(pendingRef.path + ": READ_ONLY conflicting source RIDs for a shared existing managed object");
                        report.managedFailed++;
                        return;
                    }
                    existingManagedRids[existingId] = pendingRef.rid;
                }
                else prop.managedReferenceValue = CreateSerializableInstance(concrete);
                pendingRef.assigned = true;
                pendingRef.entry = entry;
            }
            catch (Exception ex)
            {
                report.Warn(pendingRef.path + ": assigning " + fullName + " failed: " + ex.GetType().Name + ": " + ex.Message);
                report.managedFailed++;
            }
        }

        private static void ApplyInstanceData(PendingRef pendingRef, SerializedObject serialized,
                                             Dictionary<long, JToken> refTable, List<PendingRef> pending,
                                             Options options, Report report)
        {
            if (!pendingRef.assigned) return;

            SerializedProperty typed = serialized.FindProperty(pendingRef.path);
            if (typed == null)
            {
                report.Warn(pendingRef.path + ": typed property missing after ApplyModifiedProperties");
                report.managedFailed++;
                return;
            }

            report.managedApplied++;
            if (pendingRef.entry?["data"] != null)
            {
                Apply(typed, pendingRef.entry["data"], pendingRef.path, serialized, refTable, pending, options, report);
            }
        }

        /// <summary>
        /// Allocate an instance Unity can use as a managed reference value.
        ///
        /// Decompiled classes often declare only a parameterised constructor (ILSpy emits the
        /// constructors the IL has, and a class with any declared constructor gets no implicit
        /// parameterless one), so Activator.CreateInstance fails with MissingMethodException for
        /// types such as Naninovel.CommandScriptLine.  Unity's deserialiser does not need a
        /// constructor either: the fields are written by the serialiser afterwards, which makes an
        /// uninitialised allocation the correct fallback (192,280 of 304,280 references failed this
        /// way before the fallback existed).
        /// </summary>
        public static object CreateSerializableInstance(Type type)
        {
            try
            {
                return Activator.CreateInstance(type);
            }
            catch (MissingMethodException)
            {
                try
                {
                    return Activator.CreateInstance(type, nonPublic: true);
                }
                catch (MissingMethodException)
                {
                    return System.Runtime.Serialization.FormatterServices.GetUninitializedObject(type);
                }
            }
        }

        /// <summary>The reconstruction renames Assembly-CSharp (see tools/unity-rebuild/apply_game_sources.py).</summary>
        public static string Alias(string assembly)
        {
            return assembly == "Assembly-CSharp" ? "WitchTrials.Game" : assembly;
        }

        // "Namespace.Type, Assembly[, Version=..][, Culture=..][, PublicKeyToken=..]", generic arguments included.
        private static readonly Regex AssemblyQualifiedTypeName = new Regex(
            @"^[A-Za-z_][\w.+`]*(\[.*\])?, [A-Za-z_][\w.\-]*(, Version=[^,\]]+)?(, Culture=[^,\]]+)?(, PublicKeyToken=[^,\]]+)?$",
            RegexOptions.CultureInvariant);

        // The assembly token itself; the look-ahead keeps Assembly-CSharp-firstpass / -Editor untouched.
        private static readonly Regex RenamedAssemblyToken = new Regex(@"(?<=, )Assembly-CSharp(?=\s*(,|\]|$))",
            RegexOptions.CultureInvariant);

        /// <summary>
        /// A string that IS an assembly-qualified type name and names the shipped Assembly-CSharp gets the
        /// reconstruction's name for that assembly (<see cref="Alias"/>).  The shipped data stores such names verbatim:
        /// Naninovel's ActorMetadata.Implementation (`WitchTrials.Views.WitchTrialsBackground, Assembly-CSharp, ...`),
        /// which ActorManager resolves through Type.GetType -- with the old name the engine never finished initializing
        /// ("actor implementation type for 'IBackgroundActor' is not found", G4b round 2) -- and UnityEvent's
        /// PersistentCall.m_TargetAssemblyTypeName on the title/option/save buttons (21 in 8 prefabs).  Any other string
        /// is returned as the same instance, so callers can count rewrites with ReferenceEquals.
        /// </summary>
        public static string RemapAssemblyQualifiedName(string value)
        {
            if (string.IsNullOrEmpty(value) || value.IndexOf("Assembly-CSharp", StringComparison.Ordinal) < 0) return value;
            if (!AssemblyQualifiedTypeName.IsMatch(value)) return value;
            string remapped = RenamedAssemblyToken.Replace(value, Alias("Assembly-CSharp"));
            return remapped == value ? value : remapped;
        }

        public static Type ResolveType(string typeName, string assemblyName)
        {
            if (string.IsNullOrEmpty(typeName)) return null;
            if (!string.IsNullOrEmpty(assemblyName))
            {
                Type qualified = Type.GetType(typeName + ", " + assemblyName, throwOnError: false);
                if (qualified != null) return qualified;
            }
            foreach (System.Reflection.Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type hit = asm.GetType(typeName, throwOnError: false);
                if (hit != null) return hit;
            }
            return null;
        }
    }
}
