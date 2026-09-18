// Reports why a rebuilt prefab component carries no serialized fields.
//
// Origin: the standalone Player faults `Engine.Initialize` at
//     Naninovel.UI.CustomUI.OnEnable -> CustomUI.FocusObject -> SaveLoadUi.FindFocusObject
//     -> Dictionary.get_Item  (KeyNotFoundException)
// because `SaveLoadUi.Awake` builds its `_panels` dictionary from the serialized array
//     [SerializeField] private SaveLoadPanelReference[] _panelReferences;      (SaveLoadUi.cs:345-351)
// and the rebuilt `Assets/GameContent/#WitchTrials/Prefabs/Naninovel/UI/CustomUI/SaveLoadUI.prefab`
// contains a `SaveLoadUi` MonoBehaviour with NO serialized fields at all, while its siblings on the same
// GameObject (`NaninovelScreen`) do carry 28.  A grep of every .prefab/.asset in the project for
// `_panelReferences` finds nothing, and the decoded source data
// (`res/Assets/#WitchTrials/Prefabs/Naninovel/UI/CustomUI/SaveLoadUI~30.asset.json`) DOES contain it:
//     [{"_mode": 2, "_panel": {"m_FileID": 0, "m_PathID": 8357002001689893366}},
//      {"_mode": 1, "_panel": {"m_FileID": 0, "m_PathID": 4888006390507002358}}]
// The prefab job reports `BUILT_PARTIAL`, `component_add_failed = 6`, `fields_applied = 265` and -- this is
// the part that needs a measurement rather than a theory -- NOT A SINGLE warning naming `_panelReferences`,
// although `JsonToSerializedApplier.ApplyAll` warns for every JSON key it cannot find as a property.
//
// So this probe answers, from the two ends at once:
//   * TYPE side   - does `SaveLoadUi` (as this project compiles it) expose a serializable `_panelReferences`
//                   at all?  Unity drops a field silently when its element type is not serializable or when
//                   the attribute is missing, and then no property exists for the applier to find.
//   * ASSET side  - what does the built prefab's `SaveLoadUi` component actually expose now?
// The same two readings are taken for a control type (`SaveLoadPanel`, 18 fields in the asset) so the
// difference between "this type is broken" and "this component was never applied" is visible.
//
//   Unity -batchmode -quit -projectPath UnityProject \
//         -executeMethod Manosaba.EditorTools.PrefabFieldProbe.Run -logFile -
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Manosaba.EditorTools
{
    public static class PrefabFieldProbe
    {
        private const string OutputRelativePath = "../tools/unity-rebuild/out/unity/prefab_field_probe.json";
        private const string TargetPrefab =
            "Assets/GameContent/#WitchTrials/Prefabs/Naninovel/UI/CustomUI/SaveLoadUI.prefab";
        private static readonly string[] ProbeTypes = { "SaveLoadUi", "SaveLoadPanelReference", "SaveLoadPanel" };

        public static void Run()
        {
            int code = Execute();
            if (Application.isBatchMode) EditorApplication.Exit(code);
        }

        public static int Execute()
        {
            var result = new ProbeResult { generated = DateTime.UtcNow.ToString("o"), prefab = TargetPrefab };
            try
            {
                // ---- 1. TYPE side ------------------------------------------------------------------
                foreach (string name in ProbeTypes)
                {
                    Type type = FindType(name);
                    var entry = new TypeEntry { name = name, resolved = type != null };
                    if (type != null)
                    {
                        entry.full_name = type.FullName;
                        entry.assembly = type.Assembly.GetName().Name;
                        entry.is_serializable = type.IsSerializable;
                        entry.base_type = type.BaseType?.FullName;
                        foreach (FieldInfo field in type.GetFields(BindingFlags.Instance | BindingFlags.Public |
                                                                   BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
                        {
                            bool serialized = field.IsPublic ||
                                              field.GetCustomAttributes(typeof(SerializeField), true).Length > 0;
                            entry.declared_fields.Add(field.Name + " : " + PrettyType(field.FieldType) +
                                                      (serialized ? "  [serialized]" : "  (not serialized)"));
                            if (field.Name == "_panelReferences")
                            {
                                entry.panel_references_type = PrettyType(field.FieldType);
                                entry.panel_references_serialized = serialized;
                                Type element = field.FieldType.IsArray
                                    ? field.FieldType.GetElementType() : field.FieldType;
                                entry.panel_references_element = PrettyType(element);
                                entry.element_is_serializable = element?.IsSerializable ?? false;
                                if (element != null)
                                {
                                    foreach (FieldInfo ef in element.GetFields(BindingFlags.Instance |
                                             BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly))
                                    {
                                        bool es = ef.IsPublic ||
                                                  ef.GetCustomAttributes(typeof(SerializeField), true).Length > 0;
                                        entry.element_fields.Add(ef.Name + " : " + PrettyType(ef.FieldType) +
                                                                 (es ? "  [serialized]" : "  (not serialized)"));
                                    }
                                }
                            }
                        }
                    }
                    result.types.Add(entry);
                }

                // ---- 2. ASSET side ------------------------------------------------------------------
                GameObject root = PrefabUtility.LoadPrefabContents(TargetPrefab);
                if (root == null)
                {
                    result.status = "FAILED";
                    result.error = "could not load prefab contents: " + TargetPrefab;
                }
                else
                {
                    try
                    {
                        result.root_name = root.name;
                        result.root_components = root.GetComponents<Component>()
                            .Where(c => c != null).Select(c => c.GetType().Name).ToList();
                        result.runtime_component_count = root.GetComponentsInChildren<Component>(true).Length;
                        foreach (Component component in root.GetComponentsInChildren<Component>(true))
                        {
                            if (component == null) continue;
                            string typeName = component.GetType().Name;
                            if (!ProbeTypes.Contains(typeName)) continue;
                            var entry = new ComponentEntry
                            {
                                type = typeName,
                                game_object = component.gameObject.name,
                                path = PathOf(component.transform, root.transform)
                            };
                            var serialized = new SerializedObject(component);
                            SerializedProperty iterator = serialized.GetIterator();
                            bool enterChildren = true;
                            int guard = 0;
                            while (iterator.NextVisible(enterChildren) && guard++ < 400)
                            {
                                enterChildren = false;
                                if (iterator.propertyPath == "m_Script") continue;
                                if (entry.properties.Count < 80) entry.properties.Add(iterator.propertyPath);
                            }
                            entry.property_count = entry.properties.Count;
                            entry.has_panel_references = serialized.FindProperty("_panelReferences") != null;
                            result.components.Add(entry);
                        }
                        result.status = "OK";
                    }
                    finally
                    {
                        PrefabUtility.UnloadPrefabContents(root);
                    }
                }
            }
            catch (Exception ex)
            {
                result.status = "ERROR";
                result.error = ex.ToString();
                Debug.LogError("[PrefabFieldProbe] " + ex);
            }

            string projectRoot = Directory.GetParent(Application.dataPath).FullName;
            string outPath = Path.GetFullPath(Path.Combine(projectRoot, OutputRelativePath));
            Directory.CreateDirectory(Path.GetDirectoryName(outPath));
            File.WriteAllText(outPath, JsonUtility.ToJson(result, true), new UTF8Encoding(false));

            var summary = new StringBuilder("[PrefabFieldProbe] " + result.status);
            foreach (TypeEntry t in result.types)
                summary.Append(" | ").Append(t.name).Append(": resolved=").Append(t.resolved)
                       .Append(" declFields=").Append(t.declared_fields.Count)
                       .Append(" _panelReferences=").Append(t.panel_references_type ?? "(none)")
                       .Append(t.panel_references_serialized ? " [serialized]" : " [NOT serialized]");
            foreach (ComponentEntry c in result.components)
                summary.Append(" | ").Append(c.type).Append('@').Append(c.game_object)
                       .Append(": props=").Append(c.property_count)
                       .Append(" hasPanelRefs=").Append(c.has_panel_references);
            Debug.Log(summary.ToString());
            return result.status == "OK" ? 0 : 1;
        }

        private static string PrettyType(Type type)
        {
            if (type == null) return "(null)";
            if (type.IsArray) return PrettyType(type.GetElementType()) + "[]";
            if (!type.IsGenericType) return type.Name;
            return type.Name.Split('`')[0] + "<" +
                   string.Join(",", type.GetGenericArguments().Select(PrettyType)) + ">";
        }

        private static string PathOf(Transform transform, Transform root)
        {
            var parts = new List<string>();
            for (Transform t = transform; t != null; t = t.parent) parts.Insert(0, t.name);
            return string.Join("/", parts);
        }

        private static Type FindType(string shortName)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                Type[] types;
                try { types = assembly.GetTypes(); }
                catch (ReflectionTypeLoadException ex) { types = ex.Types.Where(t => t != null).ToArray(); }
                catch (Exception) { continue; }
                Type hit = types.FirstOrDefault(t => t.Name == shortName);
                if (hit != null) return hit;
            }
            return null;
        }

        [Serializable]
        public class TypeEntry
        {
            public string name;
            public bool resolved;
            public string full_name;
            public string assembly;
            public bool is_serializable;
            public string base_type;
            public List<string> declared_fields = new List<string>();
            public string panel_references_type;
            public bool panel_references_serialized;
            public string panel_references_element;
            public bool element_is_serializable;
            public List<string> element_fields = new List<string>();
        }

        [Serializable]
        public class ComponentEntry
        {
            public string type;
            public string game_object;
            public string path;
            public int property_count;
            public bool has_panel_references;
            public List<string> properties = new List<string>();
        }

        [Serializable]
        public class ProbeResult
        {
            public string status = "NOT_RUN";
            public string generated;
            public string error;
            public string prefab;
            public string root_name;
            public int runtime_component_count;
            public List<string> root_components = new List<string>();
            public List<TypeEntry> types = new List<TypeEntry>();
            public List<ComponentEntry> components = new List<ComponentEntry>();
        }
    }
}
