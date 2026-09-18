// Diagnostic for the prefab hierarchy defect: does each primitive the builder relies on actually work?
//
//   Unity -batchmode -quit -projectPath UnityProject \
//         -executeMethod Manosaba.EditorTools.PrefabBuilderDiag.Run -logFile -
//
// Checks, in order:
//   1. AddComponent<RectTransform>() on a GameObject that already has a Transform
//   2. RectTransform.SetParent(other, false) on two objects of this batch session
//   3. the same with a Selectable (Button) on the parent, whose OnTransformParentChanged throws when
//      the object is half-built -- round 9's probe log showed those exceptions during SetParent
//   4. whether an exception thrown inside SetParent's callback leaves the child unparented
using System;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Manosaba.EditorTools
{
    public static class PrefabBuilderDiag
    {
        public static void Run()
        {
            var log = new StringBuilder();
            var temp = new List<GameObject>();

            GameObject parent = new GameObject("diag_parent");
            temp.Add(parent);
            Transform plain = parent.transform;
            log.AppendLine("1. GameObject starts with: " + plain.GetType().Name);

            RectTransform rect = null;
            try
            {
                rect = parent.AddComponent<RectTransform>();
                log.AppendLine("   AddComponent<RectTransform>() -> " + (rect == null ? "NULL" : rect.GetType().Name));
            }
            catch (Exception ex)
            {
                log.AppendLine("   AddComponent<RectTransform>() threw " + ex.GetType().Name + ": " + ex.Message);
            }
            log.AppendLine("   parent.transform is now: " + parent.transform.GetType().Name +
                           ", GetComponent<RectTransform>() -> " +
                           (parent.GetComponent<RectTransform>() == null ? "NULL" : "found"));

            GameObject child = new GameObject("diag_child");
            temp.Add(child);
            child.AddComponent<RectTransform>();
            try
            {
                child.transform.SetParent(parent.transform, false);
                log.AppendLine("2. SetParent -> parent.childCount=" + parent.transform.childCount +
                               ", child.parent=" + (child.transform.parent == null ? "NULL" : child.transform.parent.name));
            }
            catch (Exception ex)
            {
                log.AppendLine("2. SetParent threw " + ex.GetType().Name + ": " + ex.Message +
                               "; child.parent=" + (child.transform.parent == null ? "NULL" : child.transform.parent.name));
            }

            // 3/4: the builder adds components *before* parenting (which is what the real prefabs look
            // like), so a Selectable on the parent is half-initialised when SetParent runs its callback.
            GameObject selectableParent = new GameObject("diag_selectable_parent");
            temp.Add(selectableParent);
            selectableParent.AddComponent<RectTransform>();
            Button button = selectableParent.AddComponent<Button>();
            log.AppendLine("3. Button added: " + (button == null ? "NULL" : button.GetType().Name) +
                           " (m_TargetGraphic null => OnTransformParentChanged throws)");
            GameObject selectableChild = new GameObject("diag_selectable_child");
            temp.Add(selectableChild);
            selectableChild.AddComponent<RectTransform>();
            try
            {
                selectableChild.transform.SetParent(selectableParent.transform, false);
                log.AppendLine("   SetParent with Selectable parent -> childCount=" +
                               selectableParent.transform.childCount + ", child.parent=" +
                               (selectableChild.transform.parent == null ? "NULL" : selectableChild.transform.parent.name));
            }
            catch (Exception ex)
            {
                log.AppendLine("   SetParent threw " + ex.GetType().Name + ": " + ex.Message +
                               "; child.parent=" + (selectableChild.transform.parent == null ? "NULL" : selectableChild.transform.parent.name) +
                               "; parent.childCount=" + selectableParent.transform.childCount);
            }

            // 5. does a plain Transform child survive when the parent gets a RectTransform afterwards?
            GameObject lateParent = new GameObject("diag_late_parent");
            temp.Add(lateParent);
            GameObject lateChild = new GameObject("diag_late_child");
            temp.Add(lateChild);
            lateChild.transform.SetParent(lateParent.transform, false);
            int before = lateParent.transform.childCount;
            lateParent.AddComponent<RectTransform>();
            log.AppendLine("5. child parented to a plain Transform, then parent gains a RectTransform: " +
                           "childCount " + before + " -> " + lateParent.transform.childCount +
                           ", child.parent=" + (lateChild.transform.parent == null ? "NULL" : lateChild.transform.parent.name));

            // 6. Replicate the builder's exact transform dance on the real Pagination plan: create the
            //    GameObjects, then for each RectTransform node report what GetComponent and AddComponent
            //    actually return.  This is where the builder sees "reused" (impossible on a fresh
            //    GameObject) and silent ADD_FAILED nulls.
            string planPath = System.IO.Path.GetFullPath(System.IO.Path.Combine(
                Application.dataPath, "..", "..", "tools", "unity-rebuild", "out", "unity",
                "prefab_plan_probe.json"));
            if (System.IO.File.Exists(planPath))
            {
                var plan = Newtonsoft.Json.Linq.JObject.Parse(System.IO.File.ReadAllText(planPath));
                var byPid = new Dictionary<string, GameObject>();
                var jobs = (Newtonsoft.Json.Linq.JArray)plan["jobs"];
                var objects = (Newtonsoft.Json.Linq.JArray)jobs[0]["objects"];
                foreach (Newtonsoft.Json.Linq.JToken obj in objects)
                {
                    if ((string)obj["type"] != "GameObject") continue;
                    string dump = System.IO.Path.GetFullPath(System.IO.Path.Combine(
                        Application.dataPath, "..", "..", (string)obj["dump"]));
                    if (!System.IO.File.Exists(dump)) continue;
                    var doc = Newtonsoft.Json.Linq.JObject.Parse(System.IO.File.ReadAllText(dump));
                    var go = new GameObject((string)doc["m_Name"]);
                    temp.Add(go);
                    byPid[(string)obj["pid"]] = go;
                }
                log.AppendLine("6. replicated " + byPid.Count + " GameObjects from the real plan");
                int reused = 0, added = 0, failed = 0;
                foreach (Newtonsoft.Json.Linq.JToken obj in objects)
                {
                    if ((string)obj["type"] != "RectTransform") continue;
                    string dump = System.IO.Path.GetFullPath(System.IO.Path.Combine(
                        Application.dataPath, "..", "..", (string)obj["dump"]));
                    var doc = Newtonsoft.Json.Linq.JObject.Parse(System.IO.File.ReadAllText(dump));
                    string ownerPid = (string)doc["m_GameObject"]["m_PathID"];
                    if (!byPid.TryGetValue(ownerPid, out GameObject owner)) continue;
                    RectTransform existing = owner.GetComponent<RectTransform>();
                    RectTransform rt;
                    if (existing != null)                       // Unity's overloaded ==: false for fake-null
                    {
                        rt = existing;
                        reused++;
                    }
                    else
                    {
                        rt = owner.AddComponent<RectTransform>();
                        if (rt != null) added++; else failed++;
                    }
                    log.AppendLine("   " + owner.name.PadRight(24) +
                                   " GetComponent: ==null? " + (existing == null) +
                                   " reference-null? " + ReferenceEquals(existing, null) +
                                   " -> " + (rt == null ? "NO TRANSFORM" : rt.GetType().Name));
                }
                log.AppendLine("   totals: reused=" + reused + " added=" + added + " failed=" + failed);
            }
            else
            {
                log.AppendLine("6. no probe plan at " + planPath);
            }

            foreach (GameObject go in temp) UnityEngine.Object.DestroyImmediate(go);
            Debug.Log("[PrefabBuilderDiag]\n" + log);
            if (Application.isBatchMode) EditorApplication.Exit(0);
        }
    }
}
