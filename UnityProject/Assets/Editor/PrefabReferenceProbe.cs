using System;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Manosaba.EditorTools
{
    /// <summary>
    /// Diagnostic: prints, for the prefabs named in MANOSABA_PREFAB_PROBE (';'-separated project paths), every component with its script
    /// state and every serialized object reference (null or not, with the target asset) -- what a runtime "null reference in a prefab
    /// field" looks like at the asset level, before any bundle is involved.
    ///   Unity -batchmode -nographics -quit -projectPath UnityProject -executeMethod Manosaba.EditorTools.PrefabReferenceProbe.Run
    /// Output: tools/unity-rebuild/out/unity/prefab_reference_probe.txt
    /// </summary>
    public static class PrefabReferenceProbe
    {
        public static void Run()
        {
            var sb = new StringBuilder();
            string list = Environment.GetEnvironmentVariable("MANOSABA_PREFAB_PROBE") ?? "";
            foreach (string path in list.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                sb.AppendLine("=== " + path);
                var root = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                if (!root) { sb.AppendLine("  (not loadable as GameObject)"); continue; }
                sb.AppendLine("  missing scripts (whole hierarchy): " + root.GetComponentsInChildren<Transform>(true).Sum(t => GameObjectUtility.GetMonoBehavioursWithMissingScriptCount(t.gameObject)));
                foreach (var component in root.GetComponentsInChildren<Component>(true))
                {
                    if (!component) { sb.AppendLine("  <missing component>"); continue; }
                    var mono = component as MonoBehaviour;
                    if (mono == null) continue;
                    sb.AppendLine("  " + component.gameObject.name + " :: " + component.GetType().FullName);
                    var so = new SerializedObject(component);
                    var it = so.GetIterator();
                    for (bool enter = true; it.NextVisible(enter); enter = false)
                    {
                        if (it.propertyType != SerializedPropertyType.ObjectReference || it.name == "m_Script") continue;
                        var target = it.objectReferenceValue;
                        sb.AppendLine("      " + it.propertyPath + " = " + (target ? target.GetType().Name + " '" + target.name + "' @ " + AssetDatabase.GetAssetPath(target) : "NULL (fileID " + it.objectReferenceInstanceIDValue + ")"));
                    }
                }
            }
            string outPath = Path.GetFullPath(Path.Combine(Application.dataPath, "../../tools/unity-rebuild/out/unity/prefab_reference_probe.txt"));
            File.WriteAllText(outPath, sb.ToString(), new UTF8Encoding(false));
            Debug.Log("[PrefabReferenceProbe] wrote " + outPath);
            if (Application.isBatchMode) EditorApplication.Exit(0);
        }
    }
}
