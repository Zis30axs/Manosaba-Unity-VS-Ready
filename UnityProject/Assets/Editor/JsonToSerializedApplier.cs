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

            /// <summary>Engine fields (m_*) are skipped by default; the prefab builder applies some of them itself.</summary>
            public bool SkipEngineFields = true;

            /// <summary>Extra keys to ignore (e.g. the references side table).</summary>
            public HashSet<string> IgnoreKeys = new HashSet<string> { "references" };

            /// <summary>Maximum number of warnings kept per object (the counters are always complete).</summary>
            public int MaxWarnings = 200;
        }

        public class Report
        {
            public int applied;
            public int warnings;
            public int pointersBound;
            public int pointersUnbound;
            public int managedTotal;
            public int managedApplied;
            public int managedFailed;
            public int skipped;
            public List<string> messages = new List<string>();

            public void Warn(string message)
            {
                warnings++;
                if (messages.Count < 200) messages.Add(message);
            }
        }

        private class PendingRef
        {
            public string path;
            public long rid;
            public bool assigned;
            public JToken entry;
        }

        /// <summary>Copy every field of a dump JSON onto the serialised object.</summary>
        public static Report ApplyAll(SerializedObject serialized, JObject json, Options options = null)
        {
            options = options ?? new Options();
            var report = new Report();

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
            while (processed < pending.Count)
            {
                int levelEnd = pending.Count;
                for (int i = processed; i < levelEnd; i++) AssignInstance(pending[i], serialized, refTable, options, report);
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
                prop.stringValue = token.Type == JTokenType.String ? token.Value<string>() : token.ToString();
                report.applied++;
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
                prop.arraySize = items.Count;
                for (int i = 0; i < items.Count; i++)
                {
                    Apply(prop.GetArrayElementAtIndex(i), items[i], path + "[" + i + "]", serialized, refTable,
                          pending, options, report);
                }
                report.applied++;
                return;
            }

            switch (prop.propertyType)
            {
                case SerializedPropertyType.Integer:
                    prop.intValue = token.Type == JTokenType.Boolean ? (token.Value<bool>() ? 1 : 0) : token.Value<int>();
                    break;
                case SerializedPropertyType.Boolean:
                    prop.boolValue = token.Type == JTokenType.Boolean ? token.Value<bool>() : token.Value<int>() != 0;
                    break;
                case SerializedPropertyType.Float:
                    prop.floatValue = token.Value<float>();
                    break;
                case SerializedPropertyType.Enum:
                    if (token.Type == JTokenType.Integer)
                    {
                        int index = token.Value<int>();
                        if (index >= 0 && index < prop.enumNames.Length) prop.enumValueIndex = index;
                        else report.Warn(path + ": enum index " + index + " out of range (" + prop.enumNames.Length + ")");
                    }
                    else
                    {
                        int index = Array.IndexOf(prop.enumNames, token.ToString());
                        if (index >= 0) prop.enumValueIndex = index;
                        else report.Warn(path + ": enum name '" + token + "' unknown");
                    }
                    break;
                case SerializedPropertyType.ObjectReference:
                    long pathId = token["m_PathID"] != null ? token.Value<long>("m_PathID") : 0;
                    if (pathId == 0)
                    {
                        prop.objectReferenceValue = null;
                        report.applied++;
                        return;
                    }
                    UnityEngine.Object target = options.ResolvePointer?.Invoke(token);
                    if (target != null)
                    {
                        prop.objectReferenceValue = target;
                        report.pointersBound++;
                    }
                    else
                    {
                        prop.objectReferenceValue = null;
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
                    break;
                case SerializedPropertyType.Vector3:
                    prop.vector3Value = ReadVector3(token);
                    break;
                case SerializedPropertyType.Vector4:
                    JToken w4 = token["w"];
                    prop.vector4Value = new Vector4(ReadFloat(token, "x"), ReadFloat(token, "y"),
                                                    ReadFloat(token, "z"), w4 != null ? w4.Value<float>() : 0f);
                    break;
                case SerializedPropertyType.Quaternion:
                    prop.quaternionValue = new Quaternion(ReadFloat(token, "x"), ReadFloat(token, "y"),
                                                          ReadFloat(token, "z"), ReadFloat(token, "w", 1f));
                    break;
                case SerializedPropertyType.Color:
                    prop.colorValue = new Color(ReadFloat(token, "r"), ReadFloat(token, "g"),
                                                ReadFloat(token, "b"), ReadFloat(token, "a", 1f));
                    break;
                case SerializedPropertyType.Rect:
                    prop.rectValue = new Rect(ReadFloat(token, "x"), ReadFloat(token, "y"),
                                              ReadFloat(token, "width"), ReadFloat(token, "height"));
                    break;
                case SerializedPropertyType.LayerMask:
                    prop.intValue = token.Type == JTokenType.Integer ? token.Value<int>() : 0;
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

        private static float ReadFloat(JToken token, string key, float fallback = 0f)
        {
            JToken value = token?[key];
            return value == null ? fallback : value.Value<float>();
        }

        private static Vector2 ReadVector2(JToken token)
        {
            return new Vector2(ReadFloat(token, "x"), ReadFloat(token, "y"));
        }

        private static Vector3 ReadVector3(JToken token)
        {
            return new Vector3(ReadFloat(token, "x"), ReadFloat(token, "y"), ReadFloat(token, "z"));
        }

        /// <summary>Create the concrete instance named by the reference table and assign it.</summary>
        private static void AssignInstance(PendingRef pendingRef, SerializedObject serialized,
                                           Dictionary<long, JToken> refTable, Options options, Report report)
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
                prop.managedReferenceValue = CreateSerializableInstance(concrete);
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
